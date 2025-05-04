using ReporterApp.Core.Utils;
using System.Collections;

namespace ReporterApp.Core.Cars;

public class CarEnumerator : ViewModelBase, IEnumerator<Car>
{
    private List<Car> _cars;
    private int _currentIndex;

    public CarEnumerator(List<Car> cars)
    {
        _cars = cars ?? throw new NullReferenceException(nameof(cars));
    }



    public Car Current => _cars[_currentIndex];
    object IEnumerator.Current => Current;

    public List<Car> Cars { get => _cars; set => _cars = value; }

    public void Dispose() { }

    public bool MoveNext()
    {
        bool isIndexNeedToBeZero = _currentIndex + 1 < _cars.Count;

        _currentIndex = isIndexNeedToBeZero ? _currentIndex + 1 : 0;
        
        NotifyPropertyChanged("Current");

        return isIndexNeedToBeZero;
    }

    public bool MoveBack()
    {
        bool isIndexNeedToMaximized = _currentIndex - 1 > -1;

        _currentIndex = isIndexNeedToMaximized ? _currentIndex - 1 : _cars.Count - 1;

        NotifyPropertyChanged("Current");

        return isIndexNeedToMaximized;
    }

    public void NotifyCurrentChanged() => NotifyPropertyChanged("Current");

    public void Reset()
    {
        _currentIndex = 0;

        NotifyPropertyChanged("Current");
    }
}
