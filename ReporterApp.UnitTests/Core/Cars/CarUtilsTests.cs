using ReporterApp.Core.Cars;

namespace ReporterApp.UnitTests.Core.Cars;

public class CarUtilsTests
{
    [Theory]
    [InlineData(10,20)]
    [InlineData(10,11)]
    [InlineData(10, 0)]
    public void IsCarWasFueled_CarFueled_True(int fuelBegin, int fuelEnd)
    {
        // Arrange
        var func = CarUtils.IsCarWasFueled;

        var car = new Car()
        {
            FuelBegin = fuelBegin,
            FuelEnd = fuelEnd
        };

        // Act, Assert
        Assert.True(func(car));
    }

    [Theory]
    [InlineData(10, 10)]
    [InlineData(10, 5)]
    [InlineData(-10, 0)]
    public void IsCarWasFueled_CarWasNotFueledOrWrongData_False(int fuelBegin, int fuelEnd)
    {
        // Arrange
        var func = CarUtils.IsCarWasFueled;

        var car = new Car()
        {
            FuelBegin = fuelBegin,
            FuelEnd = fuelEnd
        };

        // Act, Assert
        Assert.False(func(car));
    }
}
