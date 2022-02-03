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
            ObjetosVentana.svEntradasTodo.ViewChanging += svScroll;
            ObjetosVentana.svEntradasOfertas.ViewChanging += svScroll;
            ObjetosVentana.svEntradasBundles.ViewChanging += svScroll;
            ObjetosVentana.svEntradasGratis.ViewChanging += svScroll;
            ObjetosVentana.svEntradasSuscripciones.ViewChanging += svScroll;
            ObjetosVentana.svOpciones.ViewChanging += svScroll;
            ObjetosVentana.svOfertasExpandida.ViewChanging += svScroll;
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

            if (ObjetosVentana.gridEntradasTodo.Visibility == Visibility.Visible)
            {
                ObjetosVentana.svEntradasTodo.ChangeView(null, 0, null);
            }
            else if (ObjetosVentana.gridEntradasOfertas.Visibility == Visibility.Visible)
            {
                ObjetosVentana.svEntradasOfertas.ChangeView(null, 0, null);
            }
            else if (ObjetosVentana.gridEntradasBundles.Visibility == Visibility.Visible)
            {
                ObjetosVentana.svEntradasBundles.ChangeView(null, 0, null);
            }
            else if (ObjetosVentana.gridEntradasGratis.Visibility == Visibility.Visible)
            {
                ObjetosVentana.svEntradasGratis.ChangeView(null, 0, null);
            }
            else if (ObjetosVentana.gridEntradasSuscripciones.Visibility == Visibility.Visible)
            {
                ObjetosVentana.svEntradasSuscripciones.ChangeView(null, 0, null);
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
