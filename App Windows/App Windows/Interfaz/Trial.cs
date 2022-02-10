using System.Collections.Generic;
using Windows.Services.Store;
using Windows.System;
using System;
using Microsoft.UI.Xaml;
using static Principal.MainWindow;

namespace Interfaz
{
    public static class Trial
    {
        public static async void Detectar()
        {
            IReadOnlyList<User> usuarios = await User.FindAllAsync();

            if (usuarios != null)
            {
                if (usuarios.Count > 0)
                {
                    User usuario = usuarios[0];
                    StoreContext contexto = StoreContext.GetForUser(usuario);
                    StoreAppLicense licencia = await contexto.GetAppLicenseAsync();

                    if (licencia.IsActive == true && licencia.IsTrial == false)
                    {
                        ObjetosVentana.gridTrialMensaje.Visibility = Visibility.Collapsed;
                        ObjetosVentana.gridTrialMensajeSteamDeseados.Visibility = Visibility.Collapsed;
                        ObjetosVentana.gridTrialMensajeOpciones.Visibility = Visibility.Collapsed;             
                    }
                    else
                    {
                        ObjetosVentana.gridTrialMensaje.Visibility = Visibility.Visible;
                        ObjetosVentana.botonTrialComprar.Click += BotonAbrirCompra;
                        ObjetosVentana.botonTrialComprar.PointerEntered += Animaciones.EntraRatonBoton;
                        ObjetosVentana.botonTrialComprar.PointerExited += Animaciones.SaleRatonBoton;

                        ObjetosVentana.gridTrialMensajeSteamDeseados.Visibility = Visibility.Visible;
                        ObjetosVentana.botonTrialComprarSteamDeseados.Click += BotonAbrirCompra;
                        ObjetosVentana.botonTrialComprarSteamDeseados.PointerEntered += Animaciones.EntraRatonBoton;
                        ObjetosVentana.botonTrialComprarSteamDeseados.PointerExited += Animaciones.SaleRatonBoton;
                        ObjetosVentana.tbSteamDeseadosEnlaceCuenta.IsEnabled = false;

                        ObjetosVentana.gridTrialMensajeOpciones.Visibility = Visibility.Visible;
                        ObjetosVentana.botonTrialComprarOpciones.Click += BotonAbrirCompra;
                        ObjetosVentana.botonTrialComprarOpciones.PointerEntered += Animaciones.EntraRatonBoton;
                        ObjetosVentana.botonTrialComprarOpciones.PointerExited += Animaciones.SaleRatonBoton;
                        ObjetosVentana.toggleOpcionesNotificaciones.IsEnabled = false;
                        ObjetosVentana.toggleOpcionesMensajes.IsEnabled = false;
                    }

                    
                }
            }
        }

        public static async void BotonAbrirCompra(object sender, RoutedEventArgs e)
        {
            //IReadOnlyList<User> usuarios = await User.FindAllAsync();

            //if (usuarios != null)
            //{
            //    if (usuarios.Count > 0)
            //    {
            //        User usuario = usuarios[0];
            //        StoreContext contexto = StoreContext.GetForUser(usuario);
            //        await contexto.RequestPurchaseAsync("9P7836M1TW15");
            //    }
            //}

            await Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?ProductId=9P7836M1TW15"));
        }
    }
}
