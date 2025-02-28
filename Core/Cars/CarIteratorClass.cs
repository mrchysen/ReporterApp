using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Core.Cars;

// ToDo подумать можно ли реализовать IEnumerable
public class CarIteratorClass : INotifyPropertyChanged
{
    // Fields \\
    protected List<Car> Cars;
    protected Car CurrentCar;
    protected int index = 0;

    // Properties \\
    public Car GetCar {
        get
        {
            return CurrentCar;
        }
        protected set
        {
            CurrentCar = value;

            NotifyPropertyChanged();
        }
    }
    public IEnumerable<Car> GetCars => Cars;

    // Constructors \\
    public CarIteratorClass(List<Car> cars)
    {
        if(cars == null)
            throw new NullReferenceException(nameof(cars));

        Cars = cars;

        if(Cars.Count > 0) 
        {
            GetCar = Cars.ElementAt(index);
        }
    }
    // Methods \\
    public void Next()
    {
        index = (index + 1 >= Cars.Count) ? 0 : index + 1;
        
        GetCar = Cars[index];
    }

    public void Back()
    {
        index = (index - 1 < 0) ? Cars.Count - 1 : index - 1;

        GetCar = Cars[index];
    }

    // DataBinding Methods \\
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GetCars"));

    }
    public void NotifyOutSide(string propertyName = "GetCar")
    {
        NotifyPropertyChanged(propertyName);
    }
}
