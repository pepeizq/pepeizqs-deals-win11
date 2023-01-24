using FontAwesome5;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI;
using static Principal.MainWindow;

namespace Interfaz
{
    public static class MasCosas
    {
        public static void TerminadaCarga()
        {
            if (ObjetosVentana.spMensajes.Visibility == Visibility.Visible)
            {
                foreach (StackPanel sp in ObjetosVentana.spMensajes.Children)
                {
                    if (sp.Children.Count > 0 && sp.Visibility == Visibility.Visible)
                    {
                        Button botonAbrir = sp.Children[2] as Button;
                        botonAbrir.Visibility = Visibility.Visible;

                        Button botonCerrar = sp.Children[3] as Button;
                        botonCerrar.Visibility = Visibility.Visible;                     
                    }
                }
            }
        }

        public static async void BotonAbrirMensaje(object sender, RoutedEventArgs e)
        {
            string enlace = null;
            
            if (sender.GetType() == typeof(Button))
            {
                Button boton = sender as Button;
                enlace = boton.Tag as string;
            }
            else if (sender.GetType() == typeof(MenuFlyoutItem))
            {
                MenuFlyoutItem item = sender as MenuFlyoutItem;
                enlace = item.Tag as string;
            }

            if (enlace != null)
            {
                await Launcher.LaunchUriAsync(new Uri(enlace));
            }

            ObjetosVentana.gridCarga.Visibility = Visibility.Collapsed;
            ObjetosVentana.nvPrincipal.SelectedItem = ObjetosVentana.nvPrincipal.MenuItems[1];
            Pestañas.Visibilidad(ObjetosVentana.gridEntradas, true);
        }

        public static void BotonCerrarMensaje(object sender, RoutedEventArgs e)
        {
            ObjetosVentana.gridCarga.Visibility = Visibility.Collapsed;
            ObjetosVentana.nvPrincipal.SelectedItem = ObjetosVentana.nvPrincipal.MenuItems[1];
            Pestañas.Visibilidad(ObjetosVentana.gridEntradas, true);
        }
    }
}
