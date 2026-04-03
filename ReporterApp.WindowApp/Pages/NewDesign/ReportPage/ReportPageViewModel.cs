using CommunityToolkit.Mvvm.ComponentModel;
using ReporterApp.Core.Cars;
using ReporterApp.Core.Reports;
using ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;
using ReporterApp.WindowApp.Services;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage;

public partial class ReportPageViewModel : ObservableObject
{
    private readonly ICarService _carService;
    private readonly IReportService _reportService;
    private readonly IFileManagementService _fileManagementService;
    private CarEnumerator _carEnumerator;

    private NextCarCommand _nextCarCommand;
    private PrevCarCommand _prevCarCommand;
    private WorkStatusChangeCommand _workStatusChangeCommand;
    private ChangeFuelCommand _changeFuelCommand;
    private PhotoAddedCommand _photoAddedCommand;
    private Change24ETStatusCommand _change24ETStatusCommand;
    private ChangeParkingCommand _changeParkingCommand;
    private ChangeAddedInfoCommand _changeAddedInfoCommand;

    public ReportPageViewModel(
        ICarService carService,
        IReportService reportService,
        IFileManagementService fileManagementService)
    {
        _carService = carService;
        _reportService = reportService;
        _fileManagementService = fileManagementService;

        _carEnumerator = new(_carService.Cars);
        
        _nextCarCommand = new(_carEnumerator);
        _prevCarCommand = new(_carEnumerator);
        _changeFuelCommand = new(_carEnumerator);
        _photoAddedCommand = new(_carEnumerator);
        _changeParkingCommand = new(_carEnumerator);
        _changeAddedInfoCommand = new(_carEnumerator);
        _change24ETStatusCommand = new(_carEnumerator);
        _workStatusChangeCommand = new(_carEnumerator);

        CreateReportText();

        _fileManagementService.PropertyChanged += (o, e) =>
        {
            if (e.PropertyName == nameof(IFileManagementService.ReportDate))
            {
                CreateReportText();
            }
        };

        _carEnumerator.PropertyChanged += (o, e) =>
        {
            if (e.PropertyName == "Current")
            {
                OnPropertyChanged(nameof(CurrentCarNumber));
                OnPropertyChanged(nameof(IsCurrentCarWorked));
                OnPropertyChanged(nameof(IsPhotoAdded));
                OnPropertyChanged(nameof(Was24ETArrive));
                OnPropertyChanged(nameof(WasParking));
                OnPropertyChanged(nameof(AddedInfoStatus));
                OnPropertyChanged(nameof(IsFueled));

                CreateReportText();
            }
        };
    }

    public string CurrentCarNumber => _carEnumerator.Current.Number;
    public bool IsCurrentCarWorked => _carEnumerator.Current.IsWorked;
    public bool IsPhotoAdded => _carEnumerator.Current.WasScreen;
    public bool Was24ETArrive => _carEnumerator.Current.Was24kmET;
    public bool WasParking => _carEnumerator.Current.Parking.Count > 0;
    public bool AddedInfoStatus => _carEnumerator.Current.AddInformation.Count > 0;
    public bool IsFueled => CarUtils.IsCarWasFueled(_carEnumerator.Current);

    public IReportBuilder? Builder
    {
        get => _reportService.Builder;
        set => _reportService.SetBuilder(value);
    }

    public List<Car> Cars
    {
        get => _carService.Cars;
        set
        {
            _carService.Cars = value;
            _carEnumerator.Cars = value;
            CreateReportText();
        }
    }

    [ObservableProperty]
    private string _reportText = null!;

    public ICommand NextCarCommand => _nextCarCommand;
    public ICommand PrevCarCommand => _prevCarCommand;
    public ICommand WorkStatusChangeCommand => _workStatusChangeCommand;
    public ICommand ChangeFuelCommand => _changeFuelCommand;
    public ICommand PhotoAddedCommand => _photoAddedCommand;
    public ICommand Change24ETStatusCommand => _change24ETStatusCommand;
    public ICommand ChangeParkingCommand => _changeParkingCommand;
    public ICommand ChangeAddedInfoCommand => _changeAddedInfoCommand;

    public void ResetCarInEnumerator() => _carEnumerator.Reset();

    private void CreateReportText()
    {
        ReportText = _reportService.Builder?
            .AddBodyReportText(_carService.Cars, _fileManagementService.ReportDate, true)
            .GetReport()!;
    }
}
