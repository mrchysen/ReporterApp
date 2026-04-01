using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReporterApp.Core.Cars;
using ReporterApp.Core.Reports;
using ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;
using ReporterApp.WindowApp.Utils;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage;

public partial class ReportPageViewModel : ObservableObject
{
    private readonly IViewModelMediator _mediator;
    private List<Car> _cars = null!;
    private IReportBuilder? _reportBuilder;
    private CarEnumerator _carEnumerator;
    private DateTime _date;

    private NextCarCommand _nextCarCommand;
    private PrevCarCommand _prevCarCommand;
    private WorkStatusChangeCommand _workStatusChangeCommand;
    private ChangeFuelCommand _changeFuelCommand;
    private PhotoAddedCommand _photoAddedCommand;
    private Change24ETStatusCommand _change24ETStatusCommand;
    private ChangeParkingCommand _changeParkingCommand;
    private ChangeAddedInfoCommand _changeAddedInfoCommand;

    public ReportPageViewModel(
        IViewModelMediator mediator,
        IReportBuilder? reportBuilder = null,
        List<Car>? cars = null)
    {
        _reportBuilder = reportBuilder;

        _cars = cars ?? [];
        _carEnumerator = new(_cars ?? []);
        _mediator = mediator;

        _nextCarCommand = new(_carEnumerator);
        _prevCarCommand = new(_carEnumerator);
        _changeFuelCommand = new(_carEnumerator);
        _photoAddedCommand = new(_carEnumerator);
        _changeParkingCommand = new(_carEnumerator);
        _changeAddedInfoCommand = new(_carEnumerator);
        _change24ETStatusCommand = new(_carEnumerator);
        _workStatusChangeCommand = new(_carEnumerator);

        _date = _mediator.FileManagementPageViewModel.ReportDate;

        CreateReportText();

        _mediator.FileManagementPageViewModel.PropertyChanged += (o, e) =>
        {
            _date = _mediator.FileManagementPageViewModel.ReportDate;
            CreateReportText();
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
        get => _reportBuilder;
        set => SetProperty(ref _reportBuilder, value);
    }

    public List<Car> Cars
    {
        get => _cars;
        set
        {
            SetProperty(ref _cars, value);
            _carEnumerator.Cars = _cars;
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
        ReportText = _reportBuilder?
            .AddBodyReportText(_cars, _date, true)
            .GetReport()!;
    }
}
