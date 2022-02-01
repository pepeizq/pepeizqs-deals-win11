using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Foundation.Metadata;
using static Principal.MainWindow;

namespace Interfaz
{
    public static class Notificacion
    {
        public static async void Enseñar(string titulo, string contenido = null)
        {
            ContentDialog notificacion = new ContentDialog
            {
                Title = titulo,
                Content = contenido,
                CloseButtonText = "Ok"
            };

            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
            {
                notificacion.XamlRoot = ObjetosVentana.ventana.Content.XamlRoot;
            }

            ContentDialogResult result = await notificacion.ShowAsync();
        }
    }
}
