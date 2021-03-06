using Entradas;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using Windows.UI;
using static Principal.MainWindow;

namespace Interfaz
{
    public static class Pestañas
    {
        public static void Cargar()
        {
            ResourceLoader recursos = new ResourceLoader();

            ObjetosVentana.nvPrincipal.MenuItems.RemoveAt(0);
            ObjetosVentana.nvPrincipal.MenuItems.Insert(0, ObjetosVentana.nvItemMenu);

            ObjetosVentana.nvItemMenu.PointerEntered += EntraRatonNvItemMenu;
            ObjetosVentana.nvItemMenu.PointerEntered += Animaciones.EntraRatonNvItem;
            ObjetosVentana.nvItemMenu.PointerExited += Animaciones.SaleRatonNvItem;

            ObjetosVentana.nvItemVolver.PointerPressed += Ofertas.BotonCerrarExpandida;
            ObjetosVentana.nvItemVolver.PointerEntered += Animaciones.EntraRatonNvItem;
            ObjetosVentana.nvItemVolver.PointerExited += Animaciones.SaleRatonNvItem;

            ObjetosVentana.nvItemSubirArriba.PointerPressed += ScrollViewers.SubirArriba;
            ObjetosVentana.nvItemSubirArriba.PointerEntered += Animaciones.EntraRatonNvItem;
            ObjetosVentana.nvItemSubirArriba.PointerExited += Animaciones.SaleRatonNvItem;

            TextBlock tbSteamDeseadosTt = new TextBlock
            {
                Text = recursos.GetString("SteamWishlist")
            };

            ToolTipService.SetToolTip(ObjetosVentana.nvItemSteamDeseados, tbSteamDeseadosTt);
            ToolTipService.SetPlacement(ObjetosVentana.nvItemSteamDeseados, PlacementMode.Bottom);

            ObjetosVentana.nvItemSteamDeseados.PointerEntered += Animaciones.EntraRatonNvItem;
            ObjetosVentana.nvItemSteamDeseados.PointerExited += Animaciones.SaleRatonNvItem;

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
            ObjetosVentana.nvPrincipal.Visibility = Visibility.Collapsed;           
            ObjetosVentana.gridEntradas.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridBuscador.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridSteamDeseados.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridOpciones.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridOfertasExpandida.Visibility = Visibility.Collapsed;
            ObjetosVentana.gridWeb.Visibility = Visibility.Collapsed;

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
            
            ObjetosVentana.nvPrincipal.MenuItems.Insert(1, grid);
        }

        public static void EntraRatonNvItemMenu(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as NavigationViewItem);
        }
    }
}
