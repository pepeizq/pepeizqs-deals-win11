using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using Windows.UI;
using static Principal.MainWindow;

namespace Interfaz
{
    public static class Menu
    {
        public static void Cargar()
        {
            ResourceLoader recursos = new ResourceLoader();

            MenuFlyoutItem item1 = new MenuFlyoutItem
            {
                Text = recursos.GetString("MenuGiveaways"),
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                RequestedTheme = ElementTheme.Dark
            };

            item1.Click += BotonAbrirSorteos;

            ObjetosVentana.menuItemMenu.Items.Add(item1);

            MenuFlyoutItem item2 = new MenuFlyoutItem
            {
                Text = recursos.GetString("MenuSocial"),
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                RequestedTheme = ElementTheme.Dark
            };

            item2.Click += BotonAbrirSocial;

            ObjetosVentana.menuItemMenu.Items.Add(item2);

            MenuFlyoutItem item3 = new MenuFlyoutItem
            {
                Text = recursos.GetString("MenuWeb"),
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                RequestedTheme = ElementTheme.Dark
            };

            item3.Click += BotonAbrirWeb;

            ObjetosVentana.menuItemMenu.Items.Add(item3);

            ObjetosVentana.wvWeb.Source = new Uri("https://pepeizqdeals.com/en/");
        }

        public static void BotonAbrirSorteos(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();

            BarraTitulo.CambiarTitulo(recursos.GetString("MenuGiveaways"));
            Pestañas.Visibilidad(ObjetosVentana.gridWeb, true);

            if (ObjetosVentana.wvWeb.Source != new Uri("https://pepeizqdeals.com/giveaways/"))
            {
                ObjetosVentana.wvWeb.Source = new Uri("https://pepeizqdeals.com/giveaways/");
            }
        }

        public static void BotonAbrirSocial(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();

            BarraTitulo.CambiarTitulo(recursos.GetString("MenuSocial"));
            Pestañas.Visibilidad(ObjetosVentana.gridWeb, true);

            if (ObjetosVentana.wvWeb.Source != new Uri("https://pepeizqdeals.com/follow-the-deals/"))
            {
                ObjetosVentana.wvWeb.Source = new Uri("https://pepeizqdeals.com/follow-the-deals/");
            }
        }

        public static void BotonAbrirWeb(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();

            BarraTitulo.CambiarTitulo(recursos.GetString("MenuWeb"));
            Pestañas.Visibilidad(ObjetosVentana.gridWeb, true);

            if (ObjetosVentana.wvWeb.Source != new Uri("https://pepeizqdeals.com/en/"))
            {
                ObjetosVentana.wvWeb.Source = new Uri("https://pepeizqdeals.com/en/");
            }
        }
    }
}
