using Microsoft.UI.Xaml;
using static Principal.MainWindow;

namespace Interfaz
{
    public static class Mensajes
    {
        public static async void Cargar()
        {
            if (ObjetosVentana.toggleOpcionesMensajes.IsEnabled == true &&
                ObjetosVentana.toggleOpcionesMensajes.IsOn == true &&
                ObjetosVentana.gridAnuncio.Visibility == Visibility.Collapsed)
            {

            }
        }
    }
}
