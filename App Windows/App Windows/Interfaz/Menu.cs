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
                Text = recursos.GetString("MenuPatchNotes"),
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                RequestedTheme = ElementTheme.Dark,
                Margin = new Thickness(-30, 0, 0, 0)
            };

            item6.Click += BotonAbrirNotasParche;

            ObjetosVentana.menuItemMenu.Items.Add(item6);

            MenuFlyoutItem item7 = new MenuFlyoutItem
            {
                Text = recursos.GetString("MenuWeb"),
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                RequestedTheme = ElementTheme.Dark,
                Margin = new Thickness(-30, 0, 0, 0)
            };

            item7.Click += BotonAbrirWeb;

            ObjetosVentana.menuItemMenu.Items.Add(item7);

            MenuFlyoutItem item8 = new MenuFlyoutItem
            {
                Text = recursos.GetString("MenuExit"),
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                RequestedTheme = ElementTheme.Dark,
                Margin = new Thickness(-30, 0, 0, 0)
            };

            item8.Click += BotonCerrarApp;

            ObjetosVentana.menuItemMenu.Items.Add(item8);
        }

        public static void BotonAbrirSorteos(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();
            BarraTitulo.CambiarTitulo(recursos.GetString("MenuGiveaways"));

            AbrirEnlace("https://pepeizqdeals.com/giveaways/");
        }

        public static void BotonAbrirSocial(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();
            BarraTitulo.CambiarTitulo(recursos.GetString("MenuSocial"));

            AbrirEnlace("https://pepeizqdeals.com/follow-the-deals/");
        }

        public static void BotonAbrirFAQ(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();
            BarraTitulo.CambiarTitulo(recursos.GetString("MenuFAQ"));

            AbrirEnlace("https://pepeizqdeals.com/faq/");
        }

        public async static void BotonAbrirContactar(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();
            BarraTitulo.CambiarTitulo(recursos.GetString("MenuContact"));

            AbrirEnlace("https://pepeizqdeals.com/contact/");
        }

        public static void BotonAbrirNotasParche(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();
            BarraTitulo.CambiarTitulo(recursos.GetString("MenuPatchNotes"));

            AbrirEnlace("https://pepeizqapps.com/patch-notes/");
        }

        public static async void BotonAbrirWeb(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://pepeizqdeals.com/en/"));
        }

        public static void BotonCerrarApp(object sender, RoutedEventArgs e)
        {
            Application app = Application.Current;
            app.Exit();
        }

        public static async void AbrirEnlace(string enlace)
        {
            Pestañas.Visibilidad(ObjetosVentana.gridWeb, true);

            bool abierto = false;

            try
            {
                if (ObjetosVentana.wvWeb.Source != new Uri(enlace))
                {
                    ObjetosVentana.wvWeb.Source = new Uri(enlace);
                    abierto = true;
                }
            }
            catch { }

            if (abierto == true)
            {
                await Launcher.LaunchUriAsync(new Uri(enlace));

                BarraTitulo.CambiarTitulo(null);
                Pestañas.Visibilidad(ObjetosVentana.gridEntradas, true);
            }
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
