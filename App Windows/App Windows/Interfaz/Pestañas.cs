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
            ObjetosVentana.nvItemMenu.PointerEntered += Animaciones.EntraRatonNvItem2;
            ObjetosVentana.nvItemMenu.PointerExited += Animaciones.SaleRatonNvItem2;

            ObjetosVentana.nvItemVolver.PointerPressed += Ofertas.BotonCerrarExpandida;
            ObjetosVentana.nvItemVolver.PointerEntered += Animaciones.EntraRatonNvItem2;
            ObjetosVentana.nvItemVolver.PointerExited += Animaciones.SaleRatonNvItem2;

            ObjetosVentana.nvItemSubirArriba.PointerPressed += ScrollViewers.SubirArriba;
            ObjetosVentana.nvItemSubirArriba.PointerEntered += Animaciones.EntraRatonNvItem2;
            ObjetosVentana.nvItemSubirArriba.PointerExited += Animaciones.SaleRatonNvItem2;

            TextBlock tbSteamDeseadosTt = new TextBlock
            {
                Text = recursos.GetString("SteamWishlist")
            };

            ToolTipService.SetToolTip(ObjetosVentana.nvItemSteamDeseados, tbSteamDeseadosTt);
            ToolTipService.SetPlacement(ObjetosVentana.nvItemSteamDeseados, PlacementMode.Bottom);

            ObjetosVentana.nvItemSteamDeseados.PointerEntered += Animaciones.EntraRatonNvItem2;
            ObjetosVentana.nvItemSteamDeseados.PointerExited += Animaciones.SaleRatonNvItem2;

            TextBlock tbOpcionesTt = new TextBlock
            {
                Text = recursos.GetString("Options")
            };

            ToolTipService.SetToolTip(ObjetosVentana.nvItemOpciones, tbOpcionesTt);
            ToolTipService.SetPlacement(ObjetosVentana.nvItemOpciones, PlacementMode.Bottom);

            ObjetosVentana.nvItemOpciones.PointerEntered += Animaciones.EntraRatonNvItem2;
            ObjetosVentana.nvItemOpciones.PointerExited += Animaciones.SaleRatonNvItem2;
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
            StackPanel2 sp = new StackPanel2
            {
                CornerRadius = new CornerRadius(3),
                Padding = new Thickness(5),
                Orientation = Orientation.Horizontal,
                Height = 30
            };

            sp.PointerEntered += Animaciones.EntraRatonStackPanel2;
            sp.PointerExited += Animaciones.SaleRatonStackPanel2;

            TextBlock tb = new TextBlock
            {
                Text = nombre,
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                VerticalAlignment = VerticalAlignment.Center
            };

            sp.Children.Add(tb);

            if (nombre != null)
            {
                TextBlock tbTt = new TextBlock
                {
                    Text = nombre
                };

                ToolTipService.SetToolTip(sp, tbTt);
                ToolTipService.SetPlacement(sp, PlacementMode.Bottom);
            }

            ObjetosVentana.nvPrincipal.MenuItems.Insert(1, sp);
        }

        public static void EntraRatonNvItemMenu(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as NavigationViewItem);
        }
    }
}
