using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
using static Principal.MainWindow;

namespace Interfaz
{
    public static class Pestañas
    {
        public static void Visibilidad(Grid grid)
        {
            ObjetosVentana.gridCarga.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridEntradasTodo.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridEntradasOfertas.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridEntradasBundles.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridEntradasGratis.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridEntradasSuscripciones.Visibility = Visibility.Collapsed;

            grid.Visibility = Visibility.Visible;
        }

        public static void CreadorItems(string nombre)
        {
            TextBlock tb = new TextBlock
            {
                Text = nombre,
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"])
            };

            tb.PointerEntered += Animaciones.EntraRaton;
            tb.PointerExited += Animaciones.SaleRaton;

            ObjetosVentana.nvPrincipal.MenuItems.Add(tb);
        }


    }
}
