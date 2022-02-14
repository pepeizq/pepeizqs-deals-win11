using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Web.WebView2.Core;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using Windows.System;
using Windows.UI;
using static Principal.MainWindow;

namespace Interfaz
{
    public static class Menu
    {
        public static void Cargar()
        {
            //ObjetosVentana.wvWeb.Source = new Uri("https://pepeizqdeals.com/en/");

            ObjetosVentana.wvWeb.NavigationStarting += WvMenuNavegacionEmpieza;
            ObjetosVentana.wvWeb.NavigationCompleted += WvMenuNavegacionCompleta;

            ResourceLoader recursos = new ResourceLoader();

            MenuFlyoutItem item1 = new MenuFlyoutItem
            {
                Text = recursos.GetString("MenuPCGamePass"),
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                RequestedTheme = ElementTheme.Dark,
                Margin = new Thickness(-30, 0, 0, 0)
            };

            item1.Click += BotonAbrirPCGamePass;

            ObjetosVentana.menuItemMenu.Items.Add(item1);

            MenuFlyoutItem item2 = new MenuFlyoutItem
            {
                Text = recursos.GetString("MenuGiveaways"),
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                RequestedTheme = ElementTheme.Dark,
                Margin = new Thickness(-30, 0, 0, 0)
            };

            item2.Click += BotonAbrirSorteos;

            ObjetosVentana.menuItemMenu.Items.Add(item2);

            MenuFlyoutItem item3 = new MenuFlyoutItem
            {
                Text = recursos.GetString("MenuSocial"),
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                RequestedTheme = ElementTheme.Dark,
                Margin = new Thickness(-30, 0, 0, 0)
            };

            item3.Click += BotonAbrirSocial;

            ObjetosVentana.menuItemMenu.Items.Add(item3);

            MenuFlyoutItem item4 = new MenuFlyoutItem
            {
                Text = recursos.GetString("MenuFAQ"),
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                RequestedTheme = ElementTheme.Dark,
                Margin = new Thickness(-30, 0, 0, 0)
            };

            item4.Click += BotonAbrirFAQ;

            ObjetosVentana.menuItemMenu.Items.Add(item4);

            MenuFlyoutItem item5 = new MenuFlyoutItem
            {
                Text = recursos.GetString("MenuContact"),
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                RequestedTheme = ElementTheme.Dark,
                Margin = new Thickness(-30, 0, 0, 0)
            };

            item5.Click += BotonAbrirContactar;

            ObjetosVentana.menuItemMenu.Items.Add(item5);

            MenuFlyoutItem item6 = new MenuFlyoutItem
            {
                Text = recursos.GetString("MenuWeb"),
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                RequestedTheme = ElementTheme.Dark,
                Margin = new Thickness(-30, 0, 0, 0)
            };

            item6.Click += BotonAbrirWeb;

            ObjetosVentana.menuItemMenu.Items.Add(item6);
        }

        public static void BotonAbrirPCGamePass(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();

            BarraTitulo.CambiarTitulo(recursos.GetString("MenuPCGamePass"));
            Pestañas.Visibilidad(ObjetosVentana.gridWeb, true);

            if (ObjetosVentana.wvWeb.Source != new Uri("https://pepeizqdeals.com/pc-game-pass/"))
            {
                ObjetosVentana.wvWeb.Source = new Uri("https://pepeizqdeals.com/pc-game-pass/");
            }
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

        public static void BotonAbrirFAQ(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();

            BarraTitulo.CambiarTitulo(recursos.GetString("MenuFAQ"));
            Pestañas.Visibilidad(ObjetosVentana.gridWeb, true);

            if (ObjetosVentana.wvWeb.Source != new Uri("https://pepeizqdeals.com/faq/"))
            {
                ObjetosVentana.wvWeb.Source = new Uri("https://pepeizqdeals.com/faq/");
            }
        }

        public static void BotonAbrirContactar(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();

            BarraTitulo.CambiarTitulo(recursos.GetString("MenuContact"));
            Pestañas.Visibilidad(ObjetosVentana.gridWeb, true);

            if (ObjetosVentana.wvWeb.Source != new Uri("https://pepeizqdeals.com/contact/"))
            {
                ObjetosVentana.wvWeb.Source = new Uri("https://pepeizqdeals.com/contact/");
            }
        }

        public static async void BotonAbrirWeb(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://pepeizqdeals.com/en/"));
        }

        public static async void WvMenuNavegacionEmpieza(WebView2 sender, CoreWebView2NavigationStartingEventArgs e)
        {
            ObjetosVentana.gridWebCarga.Visibility = Visibility.Visible;

            string javascript1 = "var div = document.getElementById('page-header'); " + Environment.NewLine +
                                "div.remove();";
            await sender.ExecuteScriptAsync(javascript1);

            string javascript2 = "var div = document.getElementById('page-footer'); " + Environment.NewLine +
                    "div.remove();";
            await sender.ExecuteScriptAsync(javascript2);
        }

        public static void WvMenuNavegacionCompleta(WebView2 sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            ObjetosVentana.gridWebCarga.Visibility = Visibility.Collapsed;
        }
    }
}
