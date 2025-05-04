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
    private List<Car> _cars;
    private IReportBuilder _reportBuilder;
    private CarEnumerator _carEnumerator;
    private string _reportText = null!;
    private DateTime date;

    private ICommand _nextCarCommand;
    private ICommand _prevCarCommand;
    private ICommand _workStatusChangeCommand;
    private ICommand _changeFuelCommand;
    private ICommand _photoAddedCommand;
    private ICommand _change24ETStatusCommand;
    private ICommand _changeParkingCommand;
    private ICommand _changeAddedInfoCommand;

    public ReportPageViewModel(
        IReportBuilder reportBuilder,
        List<Car> cars,
        ViewModelMediator mediator)
    {
        _reportBuilder = reportBuilder;

        _carEnumerator = new(cars);
        _cars = cars;
        _mediator = mediator;

        _nextCarCommand = new NextCarCommand(_carEnumerator);
        _prevCarCommand = new PrevCarCommand(_carEnumerator);
        _workStatusChangeCommand = new WorkStatusChangeCommand(_carEnumerator);
        _changeFuelCommand = new ChangeFuelCommand(_carEnumerator);
        _photoAddedCommand = new PhotoAddedCommand(_carEnumerator);
        _change24ETStatusCommand = new Change24ETStatusCommand(_carEnumerator);
        _changeParkingCommand = new ChangeParkingCommand(_carEnumerator);
        _changeAddedInfoCommand = new ChangeAddedInfoCommand(_carEnumerator);

        date = _mediator.GetFileManagementPageViewModel()!.ReportDate;

        ReportText = _reportBuilder
            .AddBodyReportText(cars, date, true)
            .GetReport()!;

        _mediator.GetFileManagementPageViewModel()!.PropertyChanged += (o, e) =>
        {
            date = _mediator.GetFileManagementPageViewModel()!.ReportDate;

            ReportText = _reportBuilder
                                .AddBodyReportText(cars, date, true)
                                .GetReport()!;
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

                ReportText = _reportBuilder
                                .AddBodyReportText(cars, date, true)
                                .GetReport()!;
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

    public IReportBuilder Builder => _reportBuilder;
    public List<Car> Cars => _cars;

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
}
