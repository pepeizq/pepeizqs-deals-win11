using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.Resources;
using Windows.UI;
using static Principal.MainWindow;

namespace Interfaz
{
    public static class Pestañas
    {
        public static void Cargar()
        {
            ResourceLoader recursos = new ResourceLoader();

            TextBlock tbActualizarTt = new TextBlock
            {
                Text = recursos.GetString("Refresh")
            };

            ToolTipService.SetToolTip(ObjetosVentana.nvItemActualizar, tbActualizarTt);
            ToolTipService.SetPlacement(ObjetosVentana.nvItemActualizar, PlacementMode.Bottom);

            ObjetosVentana.nvItemActualizar.PointerEntered += Animaciones.EntraRatonNvItem;
            ObjetosVentana.nvItemActualizar.PointerExited += Animaciones.SaleRatonNvItem;

            TextBlock tbOpcionesTt = new TextBlock
            {
                Text = recursos.GetString("Options")
            };

            ToolTipService.SetToolTip(ObjetosVentana.nvItemOpciones, tbOpcionesTt);
            ToolTipService.SetPlacement(ObjetosVentana.nvItemOpciones, PlacementMode.Bottom);

            ObjetosVentana.nvItemOpciones.PointerEntered += Animaciones.EntraRatonNvItem;
            ObjetosVentana.nvItemOpciones.PointerExited += Animaciones.SaleRatonNvItem;
        }

        public static void Visibilidad(Grid grid, bool nv)
        {
            ObjetosVentana.gridCarga.Visibility = Visibility.Collapsed;
            ObjetosVentana.nvPrincipal.Visibility = Visibility.Collapsed;           
            ObjetosVentana.gridEntradasTodo.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridEntradasOfertas.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridEntradasBundles.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridEntradasGratis.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridEntradasSuscripciones.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridOpciones.Visibility = Visibility.Collapsed;

            grid.Visibility = Visibility.Visible;

            if (nv == true)
            {
                ObjetosVentana.nvPrincipal.Visibility = Visibility.Visible;
            }
            else
            {
                ObjetosVentana.nvPrincipal.Visibility = Visibility.Collapsed;
            }
        }

        public static void CreadorItems(string nombre, string tooltip)
        {
            TextBlock tb = new TextBlock
            {
                Text = nombre,
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"])
            };

            Grid grid = new Grid
            {
                CornerRadius = new CornerRadius(3),
                Padding = new Thickness(5)
            };

            grid.Children.Add(tb);

            grid.PointerEntered += Animaciones.EntraRatonGrid;
            grid.PointerExited += Animaciones.SaleRatonGrid;

            if (tooltip != null)
            {
                TextBlock tbTt = new TextBlock
                {
                    Text = nombre
                };

                ToolTipService.SetToolTip(grid, tbTt);
                ToolTipService.SetPlacement(grid, PlacementMode.Bottom);
            }
            
            ObjetosVentana.nvPrincipal.MenuItems.Add(grid);
        }


    }
}
