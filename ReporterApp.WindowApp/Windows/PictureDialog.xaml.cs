using System.Windows;

namespace ReporterApp.WindowApp.Windows;

public partial class PictureDialog : Window
{
    public PictureDialog()
    {
        InitializeComponent();

        Image.MouseDown += (o, e) =>
        {
            var image = Clipboard.GetImage();

            if (image == null)
            {
                return;
            }

            Image.Source = image;
        };
    }
}
