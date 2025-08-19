using AutoFixture;
using Moq;
using ReporterApp.DAL.FileAccess;
using ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage;
using ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage.Commands;
using ReporterApp.WindowApp.Pages.NewDesign.ReportPage;
using ReporterApp.WindowApp.Utils;

namespace ReporterApp.UnitTests.WindowApp.Pages.CarNumberPage.Commands;

public class SaveCommandTests
{
    private readonly Mock<ICarNumberViewModel> _carNumberViewModelMock = new();
    private readonly Mock<IViewModelMediator> _mediatorMock = new();
    private readonly Mock<ICarsFileWriter> _carsFileWriter = new();

    private SaveCommand _command;

    public SaveCommandTests()
    {
        _command = new(
            _carNumberViewModelMock.Object,
            _mediatorMock.Object,
            _carsFileWriter.Object);
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

        _carNumberViewModelMock.Setup(c => c.NumberText)
            .Returns($"{cars[0].Number}\r\n{cars[1].Number}");

        _mediatorMock.Setup(c => c.ReportPageViewModel)
            .Returns(new Mock<ReportPageViewModel>().Object);

        List<Car> mergedCars = new();

        _mediatorMock.Setup(c => c.SetCars(It.IsAny<List<Car>>(), It.IsAny<bool>()))
            .Callback((List<Car> c, bool _) => mergedCars.AddRange(c));

        // Act
        _command.Execute(null);

        // Assert
        foreach (var car in mergedCars) 
        { 
            Assert.True(car.IsWorked);
            Assert.True(car.Was24kmET);
        }
    }
}
