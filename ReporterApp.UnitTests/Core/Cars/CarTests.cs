using ReporterApp.Core.Cars;

namespace ReporterApp.UnitTests.Core.Cars;

/// <summary>
/// Тесты для модели Car.
/// </summary>
public class CarTests
{
    [Fact]
    public void Constructor_DefaultProperties_EmptyAndDefaultValues()
    {
        // Arrange & Act
        var car = new Car();

        // Assert
        Assert.Equal(string.Empty, car.Number);
        Assert.False(car.IsWorked);
        Assert.Equal(0, car.FuelBegin);
        Assert.Equal(0, car.FuelEnd);
        Assert.False(car.WasScreen);
        Assert.False(car.Was24kmET);
        Assert.Empty(car.Parking);
        Assert.Empty(car.AddInformation);
    }

    [Fact]
    public void Number_SetValue_PropertyChanged()
    {
        // Arrange
        var car = new Car();
        var propertyChanged = false;
        car.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Car.Number))
                propertyChanged = true;
        };

        // Act
        car.Number = "A123BC";

        // Assert
        Assert.Equal("A123BC", car.Number);
        Assert.True(propertyChanged);
    }

    [Fact]
    public void IsWorked_SetValue_PropertyChanged()
    {
        // Arrange
        var car = new Car();
        var propertyChanged = false;
        car.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Car.IsWorked))
                propertyChanged = true;
        };

        // Act
        car.IsWorked = true;

        // Assert
        Assert.True(car.IsWorked);
        Assert.True(propertyChanged);
    }

    [Fact]
    public void FuelBegin_SetValue_PropertyChanged()
    {
        // Arrange
        var car = new Car();
        var propertyChanged = false;
        car.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Car.FuelBegin))
                propertyChanged = true;
        };

        // Act
        car.FuelBegin = 50;

        // Assert
        Assert.Equal(50, car.FuelBegin);
        Assert.True(propertyChanged);
    }

    [Fact]
    public void FuelEnd_SetValue_PropertyChanged()
    {
        // Arrange
        var car = new Car();
        var propertyChanged = false;
        car.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Car.FuelEnd))
                propertyChanged = true;
        };

        // Act
        car.FuelEnd = 80;

        // Assert
        Assert.Equal(80, car.FuelEnd);
        Assert.True(propertyChanged);
    }

    [Fact]
    public void WasScreen_SetValue_PropertyChanged()
    {
        // Arrange
        var car = new Car();
        var propertyChanged = false;
        car.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Car.WasScreen))
                propertyChanged = true;
        };

        // Act
        car.WasScreen = true;

        // Assert
        Assert.True(car.WasScreen);
        Assert.True(propertyChanged);
    }

    [Fact]
    public void Was24kmET_SetValue_PropertyChanged()
    {
        // Arrange
        var car = new Car();
        var propertyChanged = false;
        car.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Car.Was24kmET))
                propertyChanged = true;
        };

        // Act
        car.Was24kmET = true;

        // Assert
        Assert.True(car.Was24kmET);
        Assert.True(propertyChanged);
    }

    [Fact]
    public void Parking_SetValue_PropertyChanged()
    {
        // Arrange
        var car = new Car();
        var propertyChanged = false;
        car.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Car.Parking))
                propertyChanged = true;
        };

        // Act
        car.Parking = new List<int> { 10, 20, 30 };

        // Assert
        Assert.Equal(new List<int> { 10, 20, 30 }, car.Parking);
        Assert.True(propertyChanged);
    }

    [Fact]
    public void AddInformation_SetValue_PropertyChanged()
    {
        // Arrange
        var car = new Car();
        var propertyChanged = false;
        car.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Car.AddInformation))
                propertyChanged = true;
        };

        // Act
        car.AddInformation = new List<string> { "Инфо 1", "Инфо 2" };

        // Assert
        Assert.Equal(new List<string> { "Инфо 1", "Инфо 2" }, car.AddInformation);
        Assert.True(propertyChanged);
    }

    [Fact]
    public void CloneTo_AllPropertiesCopied_TargetCarHasSameValues()
    {
        // Arrange
        var source = new Car
        {
            Number = "X001XX",
            IsWorked = true,
            FuelBegin = 30,
            FuelEnd = 70,
            WasScreen = true,
            Was24kmET = true,
            Parking = new List<int> { 15, 30 },
            AddInformation = new List<string> { "Замена колеса" }
        };

        var target = new Car();

        // Act
        var result = source.CloneTo(target);

        // Assert
        Assert.Same(target, result);
        Assert.Equal(source.Number, target.Number);
        Assert.Equal(source.IsWorked, target.IsWorked);
        Assert.Equal(source.FuelBegin, target.FuelBegin);
        Assert.Equal(source.FuelEnd, target.FuelEnd);
        Assert.Equal(source.WasScreen, target.WasScreen);
        Assert.Equal(source.Was24kmET, target.Was24kmET);
        Assert.Equal(source.Parking, target.Parking);
        Assert.Equal(source.AddInformation, target.AddInformation);
    }

    [Fact]
    public void CloneTo_CollectionsAreDeepCopied_ModifyingTargetDoesNotAffectSource()
    {
        // Arrange
        var source = new Car
        {
            Parking = new List<int> { 10, 20 },
            AddInformation = new List<string> { "Инфо" }
        };

        var target = new Car();

        // Act
        source.CloneTo(target);

        // Modify target collections
        target.Parking.Add(30);
        target.AddInformation.Add("Доп инфо");

        // Assert
        Assert.Equal(new List<int> { 10, 20 }, source.Parking);
        Assert.Equal(new List<string> { "Инфо" }, source.AddInformation);
    }

    [Fact]
    public void CloneTo_NullTarget_ThrowsArgumentNullException()
    {
        // Arrange
        var source = new Car();

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => source.CloneTo(null!));
    }
}
