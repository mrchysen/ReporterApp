﻿using Core.Cars;
using Reporter.ModalWindows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ReporterApp.WindowApp.Logic.ValueConverters;
using ReporterApp.WindowApp.ModalWindows;

namespace ReporterApp.WindowApp.Elements;

public partial class CarIteratorComponent : UserControl
{
    protected CarIteratorClass _iterator = new([ new Car() { Number = "XXX" }]);

    public CarIteratorComponent()
    {
        InitializeComponent();

        LeftButton.Content = "<--";
        RightButton.Content = "-->";
    }

    // Properties \\
    public CarIteratorClass Iterator 
    { 
        set 
        { 
            _iterator = value;

            SetBindings(); 
        } 
    }

    // Methods \\
    protected void SetBindings()
    {
        // TODO: перейти к xaml биндингу

        // NumericLabel \\
        Binding NumericLabelBinding = new Binding("GetCar");
        NumericLabelBinding.Source = _iterator;
        NumericLabel.SetBinding(Label.ContentProperty, NumericLabelBinding);
        
        // FuelButton - Text \\
        Binding FuelButtonBindingText = new Binding("GetCar");
        FuelButtonBindingText.Source = _iterator;
        FuelButtonBindingText.Converter = new PredicatConverter(
            car => (car.FuelBegin >= 0 && car.FuelEnd > 0) || car.FuelBegin > 0,
            "Введена", 
            "Ввести");
        FuelButton.SetBinding(Button.ContentProperty, FuelButtonBindingText);
        
        // FuelButton - Color \\
        Binding FuelButtonBindingColor = new Binding("GetCar");
        FuelButtonBindingColor.Source = _iterator;
        FuelButtonBindingColor.Converter = new ButtonColorConverter(
            car => (car.FuelBegin >= 0 && car.FuelEnd > 0) || car.FuelBegin > 0
        );
        FuelButton.SetBinding(Button.BackgroundProperty, FuelButtonBindingColor);
        
        // ScreenButton - Text \\
        Binding ScreenButtonBindingText = new Binding("GetCar");
        ScreenButtonBindingText.Source = _iterator;
        ScreenButtonBindingText.Converter = new PredicatConverter(car => car.WasScreen, "Да", "Нет");
        ScreenButton.SetBinding(Button.ContentProperty, ScreenButtonBindingText);

        // ScreenButton - Color \\
        Binding ScreenButtonBindingColor = new Binding("GetCar");
        ScreenButtonBindingColor.Source = _iterator;
        ScreenButtonBindingColor.Converter = new ButtonColorConverter(car => car.WasScreen);
        ScreenButton.SetBinding(Button.BackgroundProperty, ScreenButtonBindingColor);

        // ET24kmButton - Text \\
        Binding ET24kmButtonBindingText = new Binding("GetCar");
        ET24kmButtonBindingText.Source = _iterator;
        ET24kmButtonBindingText.Converter = new PredicatConverter(car => car.Was24kmET, "Да", "Нет");
        ET24kmButton.SetBinding(Button.ContentProperty, ET24kmButtonBindingText);

        // ET24kmButton - Color \\
        Binding ET24kmButtonBindingColor = new Binding("GetCar");
        ET24kmButtonBindingColor.Source = _iterator;
        ET24kmButtonBindingColor.Converter = new ButtonColorConverter(car => car.Was24kmET);
        ET24kmButton.SetBinding(Button.BackgroundProperty, ET24kmButtonBindingColor);

        // ParkingButton - Text \\
        Binding ParkingButtonBindingText = new Binding("GetCar");
        ParkingButtonBindingText.Source = _iterator;
        ParkingButtonBindingText.Converter = new PredicatConverter(
            (car) => car.Parking.Count > 0, "Введена", "Ввести");
        ParkingButton.SetBinding(Button.ContentProperty, ParkingButtonBindingText);

        // ParkingButton - Color \\
        Binding ParkingButtonBindingColor = new Binding("GetCar");
        ParkingButtonBindingColor.Source = _iterator;
        ParkingButtonBindingColor.Converter = new ButtonColorConverter(car => car.Parking.Count > 0);
        ParkingButton.SetBinding(Button.BackgroundProperty, ParkingButtonBindingColor);

        // WorkedButton - Text \\
        Binding WorkedButtonBindingText = new Binding("GetCar");
        WorkedButtonBindingText.Source = _iterator;
        WorkedButtonBindingText.Converter = 
            new PredicatConverter((car) => car.IsWorked, "Да", "Нет");
        WorkedButton.SetBinding(Button.ContentProperty, WorkedButtonBindingText);

        // WorkedButton - Color \\
        Binding WorkedButtonBindingColor = new Binding("GetCar");
        WorkedButtonBindingColor.Source = _iterator;
        WorkedButtonBindingColor.Converter = 
            new ButtonColorConverter((car) => car.IsWorked);
        WorkedButton.SetBinding(Button.BackgroundProperty, WorkedButtonBindingColor);

        // AddInfoButton - Text \\
        Binding AddInfoButtonBindingText = new Binding("GetCar");
        AddInfoButtonBindingText.Source = _iterator;
        AddInfoButtonBindingText.Converter = new PredicatConverter(
            (car) => car.AddInformation.Count > 0, "Введена", "Не введена");
        AddInfoButton.SetBinding(Button.ContentProperty, AddInfoButtonBindingText);

        // AddInfoButton - Color \\
        Binding AddInfoButtonBindingColor = new Binding("GetCar");
        AddInfoButtonBindingColor.Source = _iterator;
        AddInfoButtonBindingColor.Converter = 
            new ButtonColorConverter((car) => car.AddInformation.Count > 0);
        AddInfoButton.SetBinding(Button.BackgroundProperty, AddInfoButtonBindingColor);
    }

    private void RightButton_Click(object sender, RoutedEventArgs e)
    {
        _iterator?.Next();
    }

    private void LeftButton_Click(object sender, RoutedEventArgs e)
    {
        _iterator?.Back();
    }

    private void FuelButton_Click(object sender, RoutedEventArgs e)
    {
        if (_iterator == null)
            return;

        FuelDialog fuelDialog = new FuelDialog(_iterator.GetCar);

        fuelDialog.ShowDialog();

        _iterator.NotifyOutSide();
    }

    private void ScreenButton_Click(object sender, RoutedEventArgs e)
    {
        Car car = _iterator.GetCar;

        var pictureDialog = new PictureDialog(car);

        pictureDialog.ShowDialog();

        _iterator.NotifyOutSide();
    }

    private void ET24kmButton_Click(object sender, RoutedEventArgs e)
    {
        if (_iterator == null)
            return;

        Car car = _iterator.GetCar;

        if (car.Was24kmET)
            car.Was24kmET = false;
        else
            car.Was24kmET = true;

        _iterator.NotifyOutSide();
    }

    private void ParkingButton_Click(object sender, RoutedEventArgs e)
    {
        if (_iterator == null)
            return;

        ParkingDialog parkingDialog = new ParkingDialog(_iterator.GetCar);

        parkingDialog.ShowDialog();

        _iterator.NotifyOutSide();
    }

    private void WorkedButton_Click(object sender, RoutedEventArgs e)
    {
        if (_iterator == null)
            return;

        Car car = _iterator.GetCar;

        if (car.IsWorked)
            car.IsWorked = false;
        else
            car.IsWorked = true;

        _iterator.NotifyOutSide();
    }

    private void AddInfoButton_Click(object sender, RoutedEventArgs e)
    {
        if (_iterator == null)
            return;

        AddInfoDialog addInfoDialog = new AddInfoDialog(_iterator.GetCar);

        addInfoDialog.ShowDialog();

        _iterator.NotifyOutSide();
    }
}
