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
                        ObjetosVentana.botonTrialComprar.PointerEntered += Animaciones.EntraRatonBoton2;
                        ObjetosVentana.botonTrialComprar.PointerExited += Animaciones.SaleRatonBoton2;

                        ObjetosVentana.gridTrialMensajeSteamDeseados.Visibility = Visibility.Visible;
                        ObjetosVentana.botonTrialComprarSteamDeseados.Click += BotonAbrirCompra;
                        ObjetosVentana.botonTrialComprarSteamDeseados.PointerEntered += Animaciones.EntraRatonBoton2;
                        ObjetosVentana.botonTrialComprarSteamDeseados.PointerExited += Animaciones.SaleRatonBoton2;
                        ObjetosVentana.tbSteamDeseadosEnlaceCuenta.IsEnabled = false;

                        ObjetosVentana.gridTrialMensajeOpciones.Visibility = Visibility.Visible;
                        ObjetosVentana.botonTrialComprarOpciones.Click += BotonAbrirCompra;
                        ObjetosVentana.botonTrialComprarOpciones.PointerEntered += Animaciones.EntraRatonBoton2;
                        ObjetosVentana.botonTrialComprarOpciones.PointerExited += Animaciones.SaleRatonBoton2;
                        ObjetosVentana.toggleOpcionesNotificaciones.IsEnabled = false;
                    }           
                }
            }
        }

        public static async void BotonAbrirCompra(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?ProductId=9PFVL780F0MS"));
        }
    }
}
