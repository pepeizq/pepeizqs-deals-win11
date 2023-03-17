using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using Otros;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.Globalization;
using Windows.Storage;
using Windows.System.UserProfile;
using Windows.UI;
using WinRT.Interop;
using static Principal.MainWindow;
using AppInstance = Microsoft.Windows.AppLifecycle.AppInstance;
using WindowId = Microsoft.UI.WindowId;

namespace Interfaz
{
    public static class Opciones
    {
        private static string arranqueID = "Arranquepepeizqdeals";

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

        public static async void CargarDatos()
        {
            int i = 0;
            foreach (Button2 boton in ObjetosVentana.spOpcionesBotones.Children)
            {
                boton.Tag = i;
                boton.Click += CambiarPestaña;
                boton.PointerEntered += Animaciones.EntraRatonBoton2;
                boton.PointerExited += Animaciones.SaleRatonBoton2;

                i += 1;
            }

            CambiarPestaña(i - 1);

            //---------------------------------

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;

            IReadOnlyList<string> idiomasApp = ApplicationLanguages.ManifestLanguages;

            foreach (var idioma in idiomasApp)
            {
                ObjetosVentana.cbOpcionesIdioma.Items.Add(idioma);
            }

            if (datos.Values["OpcionesIdioma"] == null)
            {
                IReadOnlyList<string> idiomasUsuario = GlobalizationPreferences.Languages;
                bool seleccionado = false;

                foreach (var idioma in idiomasUsuario)
                {
                    foreach (var idioma2 in idiomasApp)
                    {
                        if (idioma2 == idioma)
                        {
                            ObjetosVentana.cbOpcionesIdioma.SelectedItem = idioma2;
                            seleccionado = true;
                        }
                    }
                }

                if (seleccionado == false)
                {
                    ObjetosVentana.cbOpcionesIdioma.SelectedIndex = 0;
                }

                datos.Values["OpcionesIdioma"] = ObjetosVentana.cbOpcionesIdioma.SelectedItem;
            }
            else
            {
                ObjetosVentana.cbOpcionesIdioma.SelectedItem = datos.Values["OpcionesIdioma"];
            }

            ApplicationLanguages.PrimaryLanguageOverride = ObjetosVentana.cbOpcionesIdioma.SelectedItem.ToString();

            ObjetosVentana.cbOpcionesIdioma.SelectionChanged += CbOpcionIdioma;
            ObjetosVentana.cbOpcionesIdioma.PointerEntered += Animaciones.EntraRatonComboCaja2;
            ObjetosVentana.cbOpcionesIdioma.PointerExited += Animaciones.SaleRatonComboCaja2;

            //---------------------------------

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

            if (datos.Values["OpcionesPantalla"] == null)
            {
                datos.Values["OpcionesPantalla"] = 0;  
            }
      
            ObjetosVentana.cbOpcionesPantalla.SelectionChanged += CbOpcionPantalla;
            ObjetosVentana.cbOpcionesPantalla.PointerEntered += Animaciones.EntraRatonComboCaja2;
            ObjetosVentana.cbOpcionesPantalla.PointerExited += Animaciones.SaleRatonComboCaja2;
            ObjetosVentana.cbOpcionesPantalla.SelectedIndex = (int)datos.Values["OpcionesPantalla"];
            
            //---------------------------------

            ObjetosVentana.botonOpcionesActualizar.Click += BotonOpcionActualizar;
            ObjetosVentana.botonOpcionesActualizar.PointerEntered += Animaciones.EntraRatonBoton2;
            ObjetosVentana.botonOpcionesActualizar.PointerExited += Animaciones.SaleRatonBoton2;

            //---------------------------------

            ObjetosVentana.botonOpcionesLimpiar.Click += BotonOpcionLimpiar;
            ObjetosVentana.botonOpcionesLimpiar.PointerEntered += Animaciones.EntraRatonBoton2;
            ObjetosVentana.botonOpcionesLimpiar.PointerExited += Animaciones.SaleRatonBoton2;

            //---------------------------------

            StartupTask arranque = await StartupTask.GetAsync(arranqueID);

            if (arranque.State == StartupTaskState.DisabledByUser)
            {
                ObjetosVentana.toggleOpcionesArranque.IsEnabled = false;
            }

            ObjetosVentana.toggleOpcionesArranque.Toggled += ToggleOpcionArranque;
        }

        private static void CambiarPestaña(object sender, RoutedEventArgs e)
        {
            Button2 botonPulsado = sender as Button2;
            int pestañaMostrar = (int)botonPulsado.Tag;
            CambiarPestaña(pestañaMostrar);
        }

        private static void CambiarPestaña(int botonPulsado)
        {
            SolidColorBrush colorPulsado = new SolidColorBrush((Color)Application.Current.Resources["ColorPrimario"]);
            colorPulsado.Opacity = 0.6;

            int i = 0;
            foreach (Button2 boton in ObjetosVentana.spOpcionesBotones.Children)
            {
                if (i == botonPulsado)
                {
                    boton.Background = colorPulsado;
                }
                else
                {
                    boton.Background = new SolidColorBrush(Colors.Transparent);
                }

                i += 1;
            }

            foreach (StackPanel sp in ObjetosVentana.spOpcionesPestañas.Children)
            {
                sp.Visibility = Visibility.Collapsed;
            }

            StackPanel spMostrar = ObjetosVentana.spOpcionesPestañas.Children[botonPulsado] as StackPanel;
            spMostrar.Visibility = Visibility.Visible;
        }

        public static void CbOpcionIdioma(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;
            datos.Values["OpcionesIdioma"] = cb.SelectedItem;

            ApplicationLanguages.PrimaryLanguageOverride = datos.Values["OpcionesIdioma"].ToString();
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

        public static void BotonOpcionActualizar(object sender, RoutedEventArgs e)
        {
            BarraTitulo.CambiarTitulo(null);
            Wordpress.Cargar();
        }

        public static void CbOpcionPantalla(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;
            datos.Values["OpcionesPantalla"] = cb.SelectedIndex;

            IntPtr ventanaInt = WindowNative.GetWindowHandle(ObjetosVentana.ventana);
            WindowId ventanaID = Win32Interop.GetWindowIdFromWindow(ventanaInt);
            AppWindow ventana2 = AppWindow.GetFromWindowId(ventanaID);

            if (cb.SelectedIndex == 0)
            {
                ventana2.SetPresenter(AppWindowPresenterKind.Default);
            }
            else if (cb.SelectedIndex == 1)
            {
                ventana2.SetPresenter(AppWindowPresenterKind.FullScreen);
            }
            else if (cb.SelectedIndex == 2)
            {
                ventana2.SetPresenter(AppWindowPresenterKind.Overlapped);
            }
        }

        public static async void BotonOpcionLimpiar(object sender, RoutedEventArgs e)
        {
            await ApplicationData.Current.ClearAsync();
            AppInstance.Restart(null);
        }

        public static async void ToggleOpcionArranque(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggle = sender as ToggleSwitch;

            StartupTask arranque = await StartupTask.GetAsync(arranqueID);

            if (toggle.IsOn == false)
            {
                if (arranque.State == StartupTaskState.Enabled)
                {
                    arranque.Disable();
                }
            }
            else
            {
                if (arranque.State == StartupTaskState.Disabled)
                {
                    await arranque.RequestEnableAsync();
                }
            }
        }
    }
}
