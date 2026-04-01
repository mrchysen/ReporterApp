using ReporterApp.Core.Cars;
using ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;

namespace ReporterApp.UnitTests.WindowApp.Commands;

public class WorkStatusChangeCommandTests
{
    [Fact]
    public void Execute_IsWorkedFalse_TogglesToTrue()
    {
        // Arrange
        var cars = new List<Car> { new Car { Number = "A123BC", IsWorked = false } };
        var carEnumerator = new CarEnumerator(cars);
        var command = new WorkStatusChangeCommand(carEnumerator);

        // Act
        command.Execute(null);

        // Assert
        Assert.True(carEnumerator.Current.IsWorked);
    }

    [Fact]
    public void Execute_IsWorkedTrue_TogglesToFalse()
    {
        // Arrange
        var cars = new List<Car> { new Car { Number = "A123BC", IsWorked = true } };
        var carEnumerator = new CarEnumerator(cars);
        var command = new WorkStatusChangeCommand(carEnumerator);

        // Act
        command.Execute(null);

        // Assert
        Assert.False(carEnumerator.Current.IsWorked);
    }

    [Fact]
    public void Execute_MultipleCalls_TogglesEachTime()
    {
        // Arrange
        var cars = new List<Car> { new Car { Number = "A123BC", IsWorked = false } };
        var carEnumerator = new CarEnumerator(cars);
        var command = new WorkStatusChangeCommand(carEnumerator);

        // Act
        command.Execute(null); // true
        command.Execute(null); // false
        command.Execute(null); // true

        // Assert
        Assert.True(carEnumerator.Current.IsWorked);
    }

    [Fact]
    public void Execute_CallsNotifyCurrentChanged_PropertyChangedEventFired()
    {
        // Arrange
        var cars = new List<Car> { new Car { Number = "A123BC" } };
        var carEnumerator = new CarEnumerator(cars);
        var command = new WorkStatusChangeCommand(carEnumerator);
        var propertyChangedFired = false;
        carEnumerator.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == "Current")
                propertyChangedFired = true;
        };

        // Act
        command.Execute(null);

        // Assert
        Assert.True(propertyChangedFired);
    }

    [Fact]
    public void CanExecute_AlwaysReturnsTrue()
    {
        // Arrange
        var cars = new List<Car> { new Car { Number = "A123BC" } };
        var carEnumerator = new CarEnumerator(cars);
        var command = new WorkStatusChangeCommand(carEnumerator);

        // Assert
        Assert.True(command.CanExecute(null));
    }
}
