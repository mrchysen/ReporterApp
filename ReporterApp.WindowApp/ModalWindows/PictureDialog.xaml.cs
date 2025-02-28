using Core.Cars;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Reporter.ModalWindows;

public partial class PictureDialog : Window
{
    private Car _car;
    private BitmapSource? _bitmapSource;

    public PictureDialog(Car car)
    {
        InitializeComponent();

        _car = car;

        if (_car.WasScreen)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = _car.PictureMemoryStream;
            bitmapImage.EndInit();

            Image.Source = bitmapImage;
        }

        Image.MouseDown += (o, e) =>
        {
            var image = Clipboard.GetImage();

            if (image == null)
            {
                InfoTextBlock.Text = "Добавьте в буфер обмена изображение";
                return;
            }

            OkImage.Visibility = Visibility.Hidden;
            Image.Source = image;
            _bitmapSource = image;
        };

        SaveButton.Click += (o, e) =>
        {
            if (_bitmapSource == null)
            {
                return;
            }

            OkImage.Visibility = Visibility.Visible;
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(_bitmapSource));

            car.ImgPath = car.Number + ".png";
            encoder.Save(car.PictureMemoryStream);
            InfoTextBlock.Text = "Нажмите на изображения для передачи другого";
        };
    }
}
