using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;
using static Principal.MainWindow;

namespace Interfaz
{
    public static class Opciones
    {
        public static void Cargar()
        {
            ResourceLoader recursos = new ResourceLoader();
            NavigationViewItem menu = ObjetosVentana.nvPrincipal.MenuItems[0] as NavigationViewItem;

            if (menu.Visibility == Visibility.Collapsed)
            {
                menu.Visibility = Visibility.Visible;
                ObjetosVentana.nvItemVolver.Visibility = Visibility.Collapsed;

                Pestañas.CreadorItems(recursos.GetString("Subscriptions"), null);
                Pestañas.CreadorItems(recursos.GetString("Free"), null);
                Pestañas.CreadorItems(recursos.GetString("Bundles"), null);
                Pestañas.CreadorItems(recursos.GetString("Deals"), null);
                Pestañas.CreadorItems(recursos.GetString("All"), null);
            }
            
        }

        public static async void ToggleOpcionNotificaciones(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
