using System.Windows;

namespace Reporter.ModalWindows;

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
