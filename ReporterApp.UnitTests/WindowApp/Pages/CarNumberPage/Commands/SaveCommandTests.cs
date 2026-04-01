using Moq;
using ReporterApp.DAL.FileAccess;
using ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage;
using ReporterApp.WindowApp.Pages.NewDesign.FileManagementPage;
using ReporterApp.WindowApp.Pages.NewDesign.ReportPage;
using ReporterApp.WindowApp.Utils;

namespace ReporterApp.UnitTests.WindowApp.Pages.CarNumberPage;

public class SaveCommandTests
{
    private readonly Mock<ICarsFileWriter> _carsFileWriterMock = new();
    private readonly Mock<IViewModelMediator> _mediatorMock = new();

    private CarNumberViewModel _viewModel;

    public SaveCommandTests()
    {
        _mediatorMock.Setup(c => c.FileManagementPageViewModel)
            .Returns(new FileManagementPageViewModel());

        _viewModel = new CarNumberViewModel(
            _mediatorMock.Object,
            _carsFileWriterMock.Object);
    }

    [Fact]
    public void Execute_NoNumberAdded_OldCarsMerged()
    {
        // Arrange
        var cars = Enumerable.Range(0, 2)
            .Select(i => new Car()
            {
                Number = i.ToString(),
                IsWorked = true,
                Was24kmET = true
            })
            .ToList();

        var newCar = new Car
        {
            Number = "2"
        };

        var reportVM = new ReportPageViewModel(_mediatorMock.Object, null, cars);

        _viewModel.NumberText = $"{cars[0].Number}\r\n{newCar.Number}\r\n{cars[1].Number}";

        _mediatorMock.Setup(c => c.ReportPageViewModel)
            .Returns(reportVM);

        List<Car> mergedCars = new();

        _mediatorMock.Setup(c => c.SetCars(It.IsAny<List<Car>>(), It.IsAny<bool>()))
            .Callback((List<Car> c, bool _) => mergedCars.AddRange(c));

        // Act
        _viewModel.SaveCommand.Execute(null);

        // Assert
        foreach (var car in mergedCars.Where(cars.Contains))
        {
            Assert.True(car.IsWorked);
            Assert.True(car.Was24kmET);
        }

        Assert.Contains(newCar.Number, mergedCars.Select(s => s.Number));

        _carsFileWriterMock.Verify(c => c.WriteCarNumbers(It.IsAny<List<Car>>(), It.IsAny<string>()), Times.Once);
    }
}
