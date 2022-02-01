using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.Resources;
using Windows.UI;
using static Wordpress;

namespace Entradas
{
    public static class Ofertas
    {
        public static Grid GenerarEntrada(Entrada entrada)
        {
            ResourceLoader recursos = new ResourceLoader();

            SolidColorBrush fondoMaestro = new SolidColorBrush
            {
                Color = (Color)Application.Current.Resources["ColorPrimario"],
                Opacity = 0.6
            };

            Grid gridMaestro = new Grid
            {
                Margin = new Thickness(0, 0, 0, 60),
                Background = fondoMaestro,
                Padding = new Thickness(20, 20, 20, 20)
            };

            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();

            col1.Width = new GridLength(1, GridUnitType.Auto);
            col2.Width = new GridLength(1, GridUnitType.Star);

            gridMaestro.ColumnDefinitions.Add(col1);
            gridMaestro.ColumnDefinitions.Add(col2);




            return gridMaestro;
        }
    }
}
