using ReporterApp.Core.Cars;
using ReporterApp.Core.Reports;
using ReporterApp.Core.Utils;
using ReporterApp.WindowApp.Pages.NewDesign.ReportPage.Commands;
using ReporterApp.WindowApp.Utils;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Pages.NewDesign.ReportPage;

public class ReportPageViewModel : ViewModelBase
{
    private readonly ViewModelMediator _mediator;
    private List<Car> _cars = null!;
    private IReportBuilder? _reportBuilder;
    private CarEnumerator _carEnumerator;
    private string _reportText = null!;
    private DateTime _date;

    private ICommand _nextCarCommand;
    private ICommand _prevCarCommand;
    private ICommand _workStatusChangeCommand;
    private ICommand _changeFuelCommand;
    private ICommand _photoAddedCommand;
    private ICommand _change24ETStatusCommand;
    private ICommand _changeParkingCommand;
    private ICommand _changeAddedInfoCommand;

    public ReportPageViewModel(
        ViewModelMediator mediator,
        IReportBuilder? reportBuilder = null,
        List<Car>? cars = null)
    {
        _reportBuilder = reportBuilder;

        _cars = cars ?? [];
        _carEnumerator = new(_cars ?? []);
        _mediator = mediator;

        _nextCarCommand = new NextCarCommand(_carEnumerator);
        _prevCarCommand = new PrevCarCommand(_carEnumerator);
        _changeFuelCommand = new ChangeFuelCommand(_carEnumerator);
        _photoAddedCommand = new PhotoAddedCommand(_carEnumerator);
        _changeParkingCommand = new ChangeParkingCommand(_carEnumerator);
        _changeAddedInfoCommand = new ChangeAddedInfoCommand(_carEnumerator);
        _change24ETStatusCommand = new Change24ETStatusCommand(_carEnumerator);
        _workStatusChangeCommand = new WorkStatusChangeCommand(_carEnumerator);

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
                NotifyPropertyChanged("CurrentCarNumber");
                NotifyPropertyChanged("IsCurrentCarWorked");
                NotifyPropertyChanged("IsPhotoAdded");
                NotifyPropertyChanged("Was24ETArrive");
                NotifyPropertyChanged("WasParking");
                NotifyPropertyChanged("AddedInfoStatus");
                NotifyPropertyChanged("IsFueled");

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
        set
        {
            _reportBuilder = value;
            CreateReportText();
        }
    }

    public List<Car> Cars
    {
        get => _cars;
        set
        {
            _cars = value;
            _carEnumerator.Cars = _cars;

            CreateReportText();
        }
    }

    public string ReportText
    {
        get => _reportText;
        set
        {
            _reportText = value;
            NotifyPropertyChanged();
        }
    }

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
