using ReporterApp.Core.Cars;
using ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;

namespace ReporterApp.UnitTests.WindowApp.Commands;

/// <summary>
/// Тесты для команд навигации.
/// </summary>
public class NavigationCommandTests
{
    #region NextCarCommand Tests

    [Fact]
    public void Execute_MovesToNextCar()
    {
        // Arrange
        var cars = new List<Car>
        {
            new Car { Number = "111" },
            new Car { Number = "222" }
        };
        var carEnumerator = new CarEnumerator(cars);
        var command = new NextCarCommand(carEnumerator);

        // Act
        command.Execute(null);

        // Assert
        Assert.Equal("222", carEnumerator.Current.Number);
    }

    [Fact]
    public void Execute_AtLastCar_WrapsToFirst()
    {
        // Arrange
        var cars = new List<Car>
        {
            new Car { Number = "111" },
            new Car { Number = "222" }
        };
        var carEnumerator = new CarEnumerator(cars);
        var command = new NextCarCommand(carEnumerator);

        // Act
        command.Execute(null); // 222
        command.Execute(null); // 111 (wrap)

        // Assert
        Assert.Equal("111", carEnumerator.Current.Number);
    }

    [Fact]
    public void Execute_SingleCar_WrapsToSame()
    {
        // Arrange
        var cars = new List<Car> { new Car { Number = "111" } };
        var carEnumerator = new CarEnumerator(cars);
        var command = new NextCarCommand(carEnumerator);

        // Act
        command.Execute(null);

        // Assert
        Assert.Equal("111", carEnumerator.Current.Number);
    }

    [Fact]
    public void CanExecute_AlwaysReturnsTrue()
    {
        // Arrange
        var cars = new List<Car> { new Car { Number = "111" } };
        var carEnumerator = new CarEnumerator(cars);
        var command = new NextCarCommand(carEnumerator);

        // Assert
        Assert.True(command.CanExecute(null));
    }

    #endregion

    #region PrevCarCommand Tests

    [Fact]
    public void Execute_PrevCar_MovesToPreviousCar()
    {
        // Arrange
        var cars = new List<Car>
        {
            new Car { Number = "111" },
            new Car { Number = "222" },
            new Car { Number = "333" }
        };
        var carEnumerator = new CarEnumerator(cars);
        var command = new PrevCarCommand(carEnumerator);

        // Act
        command.Execute(null);

        // Assert
        Assert.Equal("333", carEnumerator.Current.Number);
    }

    [Fact]
    public void Execute_PrevCar_AtFirstCar_WrapsToLast()
    {
        // Arrange
        var cars = new List<Car>
        {
            new Car { Number = "111" },
            new Car { Number = "222" },
            new Car { Number = "333" }
        };
        var carEnumerator = new CarEnumerator(cars);
        var command = new PrevCarCommand(carEnumerator);

        // Act
        command.Execute(null); // 333
        command.Execute(null); // 222

        // Assert
        Assert.Equal("222", carEnumerator.Current.Number);
    }

    [Fact]
    public void Execute_PrevCar_SingleCar_WrapsToSame()
    {
        // Arrange
        var cars = new List<Car> { new Car { Number = "111" } };
        var carEnumerator = new CarEnumerator(cars);
        var command = new PrevCarCommand(carEnumerator);

        // Act
        command.Execute(null);

        // Assert
        Assert.Equal("111", carEnumerator.Current.Number);
    }

    [Fact]
    public void CanExecute_PrevCar_AlwaysReturnsTrue()
    {
        // Arrange
        var cars = new List<Car> { new Car { Number = "111" } };
        var carEnumerator = new CarEnumerator(cars);
        var command = new PrevCarCommand(carEnumerator);

        // Assert
        Assert.True(command.CanExecute(null));
    }

    #endregion
}
