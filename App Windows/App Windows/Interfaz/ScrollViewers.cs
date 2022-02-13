using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using static Principal.MainWindow;

namespace Interfaz
{
    public static class ScrollViewers
    {
        public static void Cargar()
        {
            ObjetosVentana.svEntradas.ViewChanging += svScroll;
            ObjetosVentana.svBuscador.ViewChanging += svScroll;
            ObjetosVentana.svSteamDeseados.ViewChanging += svScroll;
            ObjetosVentana.svOpciones.ViewChanging += svScroll;
            ObjetosVentana.svOfertasExpandida.ViewChanging += svScroll;
            ObjetosVentana.svRedesSociales.ViewChanging += svScroll;
        }

        private static void svScroll(object sender, ScrollViewerViewChangingEventArgs args)
        {
            ScrollViewer sv = sender as ScrollViewer;

            if (sv.VerticalOffset > 50)
            {
                ObjetosVentana.nvItemSubirArriba.Visibility = Visibility.Visible;
            }
            else
            {
                ObjetosVentana.nvItemSubirArriba.Visibility = Visibility.Collapsed;
            }
        }

        public static void SubirArriba(object sender, RoutedEventArgs e)
        {
            NavigationViewItem nvItem = sender as NavigationViewItem;
            nvItem.Visibility = Visibility.Collapsed;

            Grid grid = nvItem.Content as Grid;
            grid.Background = new SolidColorBrush(Colors.Transparent);

            if (ObjetosVentana.gridEntradas.Visibility == Visibility.Visible)
            {
                ObjetosVentana.svEntradas.ChangeView(null, 0, null);
            }
            else if (ObjetosVentana.gridBuscador.Visibility == Visibility.Visible)
            {
                ObjetosVentana.svBuscador.ChangeView(null, 0, null);
            }
            else if (ObjetosVentana.gridSteamDeseados.Visibility == Visibility.Visible)
            {
                ObjetosVentana.svSteamDeseados.ChangeView(null, 0, null);
            }
            else if (ObjetosVentana.gridOpciones.Visibility == Visibility.Visible)
            {
                ObjetosVentana.svOpciones.ChangeView(null, 0, null);
            }
            else if (ObjetosVentana.gridOfertasExpandida.Visibility == Visibility.Visible)
            {
                ObjetosVentana.svOfertasExpandida.ChangeView(null, 0, null);
            }
        }

        public static void EnseñarSubir(ScrollViewer sv)
        {
            if (sv.VerticalOffset > 50)
            {
                ObjetosVentana.nvItemSubirArriba.Visibility = Visibility.Visible;
            }
            else
            {
                ObjetosVentana.nvItemSubirArriba.Visibility = Visibility.Collapsed;
            }
        }
    }
}
