using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Components;

public partial class CarIteratorComponent : StackPanel
{
    public CarIteratorComponent()
    {
        InitializeComponent();

        LeftButton.Content = "<--";
        RightButton.Content = "-->";
    }

    //public CarEnumerator Iterator 
    //{ 
    //    set 
    //    { 
    //        _Iterator = value;

    //        SetBindings(); 
    //    } 
    //}

    //protected void SetBindings()
    //{
    //    // NumericLabel \\
    //    Binding NumericLabelBinding = new Binding("GetCar");
    //    NumericLabelBinding.Source = _Iterator;
    //    NumericLabel.SetBinding(Label.ContentProperty, NumericLabelBinding);
        
    //    // FuelButton - Text \\
    //    Binding FuelButtonBindingText = new Binding("GetCar");
    //    FuelButtonBindingText.Source = _Iterator;
    //    FuelButtonBindingText.Converter = new PredicatConverter(new Predicate<Car>((car) =>
    //    {
    //        return (car.FuelBegin >= 0 && car.FuelEnd > 0) || car.FuelBegin > 0;
    //    }),"Введена", "Ввести");
    //    FuelButton.SetBinding(Button.ContentProperty, FuelButtonBindingText);
        
    //    // FuelButton - Color \\
    //    Binding FuelButtonBindingColor = new Binding("GetCar");
    //    FuelButtonBindingColor.Source = _Iterator;
    //    FuelButtonBindingColor.Converter = new ButtonColorConverter(new Predicate<Car>((car) =>
    //    {
    //        return (car.FuelBegin >= 0 && car.FuelEnd > 0) || car.FuelBegin > 0;
    //    }));
    //    FuelButton.SetBinding(Button.BackgroundProperty, FuelButtonBindingColor);
        
    //    // ScreenButton - Text \\
    //    Binding ScreenButtonBindingText = new Binding("GetCar");
    //    ScreenButtonBindingText.Source = _Iterator;
    //    ScreenButtonBindingText.Converter = new PredicatConverter(new Predicate<Car>((car) => car.WasScreen), "Да", "Нет");
    //    ScreenButton.SetBinding(Button.ContentProperty, ScreenButtonBindingText);

    //    // ScreenButton - Color \\
    //    Binding ScreenButtonBindingColor = new Binding("GetCar");
    //    ScreenButtonBindingColor.Source = _Iterator;
    //    ScreenButtonBindingColor.Converter = new ButtonColorConverter(new Predicate<Car>((car) =>
    //    {
    //        return car.WasScreen;
    //    }));
    //    ScreenButton.SetBinding(Button.BackgroundProperty, ScreenButtonBindingColor);

    //    // ET24kmButton - Text \\
    //    Binding ET24kmButtonBindingText = new Binding("GetCar");
    //    ET24kmButtonBindingText.Source = _Iterator;
    //    ET24kmButtonBindingText.Converter = new PredicatConverter(new Predicate<Car>((car) => car.Was24kmET), "Да", "Нет");
    //    ET24kmButton.SetBinding(Button.ContentProperty, ET24kmButtonBindingText);

    //    // ET24kmButton - Color \\
    //    Binding ET24kmButtonBindingColor = new Binding("GetCar");
    //    ET24kmButtonBindingColor.Source = _Iterator;
    //    ET24kmButtonBindingColor.Converter = new ButtonColorConverter(new Predicate<Car>((car) =>
    //    {
    //        return car.Was24kmET;
    //    }));
    //    ET24kmButton.SetBinding(Button.BackgroundProperty, ET24kmButtonBindingColor);

    //    // ParkingButton - Text \\
    //    Binding ParkingButtonBindingText = new Binding("GetCar");
    //    ParkingButtonBindingText.Source = _Iterator;
    //    ParkingButtonBindingText.Converter = new PredicatConverter(new Predicate<Car>((car) => car.Parking.Count > 0), "Введена", "Ввести");
    //    ParkingButton.SetBinding(Button.ContentProperty, ParkingButtonBindingText);

    //    // ParkingButton - Color \\
    //    Binding ParkingButtonBindingColor = new Binding("GetCar");
    //    ParkingButtonBindingColor.Source = _Iterator;
    //    ParkingButtonBindingColor.Converter = new ButtonColorConverter(new Predicate<Car>((car) =>
    //    {
    //        return car.Parking.Count > 0;
    //    }));
    //    ParkingButton.SetBinding(Button.BackgroundProperty, ParkingButtonBindingColor);

    //    // WorkedButton - Text \\
    //    Binding WorkedButtonBindingText = new Binding("GetCar");
    //    WorkedButtonBindingText.Source = _Iterator;
    //    WorkedButtonBindingText.Converter = new PredicatConverter(new Predicate<Car>((car) => car.IsWorked), "Да", "Нет");
    //    WorkedButton.SetBinding(Button.ContentProperty, WorkedButtonBindingText);

    //    // WorkedButton - Color \\
    //    Binding WorkedButtonBindingColor = new Binding("GetCar");
    //    WorkedButtonBindingColor.Source = _Iterator;
    //    WorkedButtonBindingColor.Converter = new ButtonColorConverter(new Predicate<Car>((car) =>
    //    {
    //        return car.IsWorked;
    //    }));
    //    WorkedButton.SetBinding(Button.BackgroundProperty, WorkedButtonBindingColor);

    //    // AddInfoButton - Text \\
    //    Binding AddInfoButtonBindingText = new Binding("GetCar");
    //    AddInfoButtonBindingText.Source = _Iterator;
    //    AddInfoButtonBindingText.Converter = new PredicatConverter(new Predicate<Car>((car) => car.AddInformation.Count > 0), "Введена", "Не введена");
    //    AddInfoButton.SetBinding(Button.ContentProperty, AddInfoButtonBindingText);

    //    // AddInfoButton - Color \\
    //    Binding AddInfoButtonBindingColor = new Binding("GetCar");
    //    AddInfoButtonBindingColor.Source = _Iterator;
    //    AddInfoButtonBindingColor.Converter = new ButtonColorConverter(new Predicate<Car>((car) =>
    //    {
    //        return car.AddInformation.Count > 0;
    //    }));
    //    AddInfoButton.SetBinding(Button.BackgroundProperty, AddInfoButtonBindingColor);
    //}

    //private void RightButton_Click(object sender, RoutedEventArgs e)
    //{
    //    _Iterator.MoveNext();
    //}

    //private void LeftButton_Click(object sender, RoutedEventArgs e)
    //{
    //    _Iterator.MoveBack();
    //}

    //private void FuelButton_Click(object sender, RoutedEventArgs e)
    //{
    //    if (_Iterator == null)
    //        return;

    //    FuelDialog fuelDialog = new FuelDialog(_Iterator.Current);

    //    fuelDialog.ShowDialog();

    //    //_Iterator.NotifyOutSide();
    //}

    //private void ScreenButton_Click(object sender, RoutedEventArgs e)
    //{
    //    Car car = _Iterator.Current;

    //    if(car.WasScreen)
    //        car.WasScreen = false;
    //    else
    //        car.WasScreen = true;

    //    // add image dialog here

    //    //_Iterator.NotifyOutSide();
    //}

    //private void ET24kmButton_Click(object sender, RoutedEventArgs e)
    //{
    //    if (_Iterator == null)
    //        return;

    //    Car car = _Iterator.Current;

    //    if (car.Was24kmET)
    //        car.Was24kmET = false;
    //    else
    //        car.Was24kmET = true;

    //    //_Iterator.NotifyOutSide();
    //}

    //private void ParkingButton_Click(object sender, RoutedEventArgs e)
    //{
    //    if (_Iterator == null)
    //        return;

    //    ParkingDialog parkingDialog = new ParkingDialog(_Iterator.Current);

    //    parkingDialog.ShowDialog();

    //    //_Iterator.NotifyOutSide();
    //}

    //private void WorkedButton_Click(object sender, RoutedEventArgs e)
    //{
    //    if (_Iterator == null)
    //        return;

    //    Car car = _Iterator.Current;

    //    if (car.IsWorked)
    //        car.IsWorked = false;
    //    else
    //        car.IsWorked = true;

    //    //_Iterator.NotifyOutSide();
    //}

    //private void AddInfoButton_Click(object sender, RoutedEventArgs e)
    //{
    //    if (_Iterator == null)
    //        return;

    //    AddInfoDialog addInfoDialog = new AddInfoDialog(_Iterator.Current);

    //    addInfoDialog.ShowDialog();

    //    //_Iterator.NotifyOutSide();
    //}
}
