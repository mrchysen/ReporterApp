using Moq;
using ReporterApp.Core.Cars;
using ReporterApp.DAL.FileAccess;
using ReporterApp.WindowApp.Pages.NewDesign.CarNumberPage;
using ReporterApp.WindowApp.Services;

namespace ReporterApp.UnitTests.WindowApp.Pages.CarNumberPage.Commands;

public class SaveCommandTests
{
    private readonly Mock<ICarsFileWriter> _carsFileWriterMock = new();
    private readonly Mock<ICarService> _carServiceMock = new();

    private CarNumberViewModel _viewModel;

    public SaveCommandTests()
    {
        _carServiceMock.Setup(c => c.Cars).Returns(new List<Car>());

        _viewModel = new CarNumberViewModel(
            _carServiceMock.Object,
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

        _carServiceMock.Setup(c => c.Cars).Returns(cars);
        _viewModel.NumberText = $"{cars[0].Number}\r\n{newCar.Number}\r\n{cars[1].Number}";

        List<Car> mergedCars = new();

        _carServiceMock.Setup(c => c.SetCarsWithReset(It.IsAny<List<Car>>()))
            .Callback((List<Car> c) => mergedCars.AddRange(c));

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
