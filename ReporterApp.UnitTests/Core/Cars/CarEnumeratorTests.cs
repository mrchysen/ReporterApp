using ReporterApp.Core.Cars;

namespace ReporterApp.UnitTests.Core.Cars;

public class CarEnumeratorTests
{
    [Fact]
    public void Constructor_CurrentCarSet_Seccess()
    {
        // Arrange
        List<Car> cars = [
            new() { Number = "111" } 
            ];

        var expectedCar = cars[0];

        var carEnumerator = new CarEnumerator(cars);

        // Act
        var car = carEnumerator.Current;

        // Arrange
        Assert.Equal(car.Number, expectedCar.Number);
    }

    [Fact]
    public void MoveNext_NextCarSet_Seccess()
    {
        // Arrange
        List<Car> cars = [
            new() { Number = "111" },
            new() { Number = "222" }
            ];

        var expectedCar = cars[1];

        var carEnumerator = new CarEnumerator(cars);

        // Act
        carEnumerator.MoveNext();

        // Arrange
        Assert.Equal(carEnumerator.Current.Number, expectedCar.Number);
    }

    [Fact]
    public void MoveNext_IndexOverflow_ZeroIndexSet()
    {
        // Arrange
        List<Car> cars = [
            new() { Number = "111" },
            new() { Number = "222" }
            ];

        var expectedCar = cars[0];

        var carEnumerator = new CarEnumerator(cars);

        // Act
        carEnumerator.MoveNext();
        carEnumerator.MoveNext();

        // Arrange
        Assert.Equal(carEnumerator.Current.Number, expectedCar.Number);
    }

    [Fact]
    public void MoveBack_IndexOverflow_MaxIndexSet()
    {
        // Arrange
        List<Car> cars = [
            new() { Number = "111" },
            new() { Number = "222" }
            ];

        var expectedCar = cars[1];

        var carEnumerator = new CarEnumerator(cars);

        // Act
        carEnumerator.MoveBack();

        // Arrange
        Assert.Equal(carEnumerator.Current.Number, expectedCar.Number);
    }

    [Fact]
    public void Reset_ResetExecuted_CurrentIsFirstCar()
    {
        // Arrange
        List<Car> cars = [
            new() { Number = "111" },
            new() { Number = "222" },
            new() { Number = "333" }
            ];

        var expectedCar = cars[0];

        var carEnumerator = new CarEnumerator(cars);

        // Act
        carEnumerator.MoveNext();
        carEnumerator.MoveNext();
        carEnumerator.Reset();

        // Arrange
        Assert.Equal(carEnumerator.Current.Number, expectedCar.Number);
    }
}
