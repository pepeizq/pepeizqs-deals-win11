using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Windows.UI.Core;

namespace Interfaz
{
    public static class Animaciones
    {
        public static void EntraRaton(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Hand, 1);
        }

        public static void SaleRaton(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 1);
        }
    }
}
