using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;
using Windows.Storage;
using static Principal.MainWindow;
using Otros;

namespace Interfaz
{
    public static class Opciones
    {
        public static void CargarPestaña()
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

        public static void CargarDatos()
        {
            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;

            if (ObjetosVentana.toggleOpcionesNotificaciones.IsEnabled == true)
            {
                if (datos.Values["OpcionesNotificaciones"] == null)
                {
                    datos.Values["OpcionesNotificaciones"] = true;
                }

                ObjetosVentana.toggleOpcionesNotificaciones.Toggled += ToggleOpcionNotificaciones;

                if (datos.Values["OpcionesNotificaciones"] is true)
                {                    
                    ObjetosVentana.toggleOpcionesNotificaciones.IsOn = true;
                }
                else
                {
                    ObjetosVentana.toggleOpcionesNotificaciones.IsOn = false;
                }
            }

            //---------------------------------

            if (datos.Values["OpcionesAnuncios"] == null)
            {
                datos.Values["OpcionesAnuncios"] = true;
            }

            ObjetosVentana.toggleOpcionesAnuncios.Toggled += ToggleOpcionAnuncios;

            if (datos.Values["OpcionesAnuncios"] is true)
            {
                ObjetosVentana.toggleOpcionesAnuncios.IsOn = true;
            }
            else
            {
                ObjetosVentana.toggleOpcionesAnuncios.IsOn = false;
            }

            //---------------------------------

            if (datos.Values["OpcionesMensajes"] == null)
            {
                datos.Values["OpcionesMensajes"] = true;
            }

            ObjetosVentana.toggleOpcionesMensajes.Toggled += ToggleOpcionMensajes;

            if (datos.Values["OpcionesMensajes"] is true)
            {
                ObjetosVentana.toggleOpcionesMensajes.IsOn = true;
            }
            else
            {
                ObjetosVentana.toggleOpcionesMensajes.IsOn = false;
            }

            //---------------------------------

            ObjetosVentana.botonOpcionesActualizar.Click += BotonOpcionActualizar;
            
        }

        public static void ToggleOpcionNotificaciones(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggle = sender as ToggleSwitch;

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;
            datos.Values["OpcionesNotificaciones"] = toggle.IsOn;

            if (toggle.IsOn == true)
            {
                Push.Escuchar();
            }
            else
            {
                Push.Parar();
            }
        }

        public static void ToggleOpcionAnuncios(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggle = sender as ToggleSwitch;

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;
            datos.Values["OpcionesAnuncios"] = toggle.IsOn;
        }

        public static void ToggleOpcionMensajes(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggle = sender as ToggleSwitch;

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;
            datos.Values["OpcionesMensajes"] = toggle.IsOn;
        }

        public static void BotonOpcionActualizar(object sender, RoutedEventArgs e)
        {
            BarraTitulo.CambiarTitulo(null);
            Wordpress.Cargar();
        }
    }
}
