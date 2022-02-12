using FontAwesome5;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI;
using static Principal.MainWindow;

namespace Interfaz
{
    public static class MasCosas
    {
        public class Cosa
        {
            public Cosa(EFontAwesomeIcon icono, string enlace, string textoMensaje, string textoMenu)
            {
                this.icono = icono;
                this.enlace = enlace;
                this.textoMensaje = textoMensaje;
                this.textoMenu = textoMenu;
            }

            public EFontAwesomeIcon icono { get; set; }
            public string enlace { get; set; }
            public string textoMensaje { get; set; }
            public string textoMenu { get; set; }
        }

        public static List<Cosa> GenerarLista()
        {
            List<Cosa> cosas = new List<Cosa>
            {
                new Cosa(EFontAwesomeIcon.Solid_ThumbsUp, "ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName, "MessageRate", "MenuRate"),
                new Cosa(EFontAwesomeIcon.Brands_Github, "https://github.com/pepeizq/pepeizqs-deals-win11", "MessageGithub", "MenuGithub"),
                new Cosa(EFontAwesomeIcon.Solid_Cube, "https://pepeizqapps.com/", "MessageApps", "MenuApps")
            };

            return cosas;
        }

        public static void CargarMenu()
        {
            List<Cosa> cosas = GenerarLista();

            if (cosas.Count > 0)
            {
                if (ObjetosVentana.menuItemMenu.Items.Count > 0)
                {
                    MenuFlyoutSeparator separador = new MenuFlyoutSeparator
                    {
                        Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                        RequestedTheme = ElementTheme.Dark,
                        Height = 30
                    };

                    ObjetosVentana.menuItemMenu.Items.Add(separador);
                }

                ResourceLoader recursos = new ResourceLoader();

                foreach (var cosa in cosas)
                {
                    FontAwesome icono = new FontAwesome
                    {
                        Icon = cosa.icono,
                        Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"])
                    };

                    MenuFlyoutItem item = new MenuFlyoutItem
                    {
                        Icon = icono,
                        Text = recursos.GetString(cosa.textoMenu),
                        Tag = cosa.enlace,
                        Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                        RequestedTheme = ElementTheme.Dark
                    };

                    item.Click += BotonAbrirMensaje;

                    ObjetosVentana.menuItemMenu.Items.Add(item);
                }
            }
        }

        public static void MostrarMensaje()
        {
            if (ObjetosVentana.toggleOpcionesMensajes.IsOn == true &&
                ObjetosVentana.gridAnuncio.Visibility == Visibility.Collapsed)
            {
                List<Cosa> cosas = GenerarLista();

                if (cosas.Count > 0)
                {
                    ResourceLoader recursos = new ResourceLoader();
                    ObjetosVentana.spMensajes.Visibility = Visibility.Visible;

                    Random azarR = new Random();
                    int azar = azarR.Next(0, cosas.Count);

                    StackPanel sp = new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Background = new SolidColorBrush((Color)Application.Current.Resources["ColorPrimario"]),
                        Padding = new Thickness(20),
                        CornerRadius = new CornerRadius(5)
                    };

                    FontAwesome icono = new FontAwesome
                    {
                        Icon = cosas[azar].icono,
                        Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                        FontSize = 25,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(5, 0, 0, 0)
                    };

                    sp.Children.Add(icono);

                    TextBlock tb = new TextBlock
                    {
                        Text = recursos.GetString(cosas[azar].textoMensaje),
                        Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                        TextWrapping = TextWrapping.Wrap,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(30, 0, 30, 0),
                        MaxWidth = 500
                    };

                    sp.Children.Add(tb);

                    TextBlock tbAbrir = new TextBlock
                    {
                        Text = recursos.GetString("Open"),
                        Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"])
                    };

                    Button botonAbrir = new Button
                    {
                        Content = tbAbrir,
                        Background = new SolidColorBrush(Colors.Transparent),
                        Visibility = Visibility.Collapsed,
                        Margin = new Thickness(0, 0, 10, 0),
                        VerticalAlignment = VerticalAlignment.Center,
                        Tag = cosas[azar].enlace
                    };

                    sp.Children.Add(botonAbrir);

                    botonAbrir.Click += BotonAbrirMensaje;

                    FontAwesome iconoCerrar = new FontAwesome
                    {
                        Icon = EFontAwesomeIcon.Solid_Times,
                        Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"])
                    };

                    Button botonCerrar = new Button
                    {
                        Content = iconoCerrar,
                        Background = new SolidColorBrush(Colors.Transparent),
                        Visibility = Visibility.Collapsed,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    sp.Children.Add(botonCerrar);

                    botonCerrar.Click += BotonCerrarMensaje;

                    ObjetosVentana.spMensajes.Children.Add(sp);
                }
            }
        }

        public static void TerminadaCarga()
        {
            if (ObjetosVentana.spMensajes.Visibility == Visibility.Visible)
            {
                foreach (StackPanel sp in ObjetosVentana.spMensajes.Children)
                {
                    if (sp.Children.Count > 0 && sp.Visibility == Visibility.Visible)
                    {
                        Button botonAbrir = sp.Children[2] as Button;
                        botonAbrir.Visibility = Visibility.Visible;

                        Button botonCerrar = sp.Children[3] as Button;
                        botonCerrar.Visibility = Visibility.Visible;                     
                    }
                }

                if (ObjetosVentana.toggleOpcionesMensajes.IsOn == true && ObjetosVentana.toggleOpcionesMensajes.IsEnabled == true)
                {
                    ObjetosVentana.gridCarga.Visibility = Visibility.Collapsed;
                    ObjetosVentana.nvPrincipal.SelectedItem = ObjetosVentana.nvPrincipal.MenuItems[1];
                    Pestañas.Visibilidad(ObjetosVentana.gridEntradas, true);
                }
            }
        }

        public static async void BotonAbrirMensaje(object sender, RoutedEventArgs e)
        {
            string enlace = null;
            
            if (sender.GetType() == typeof(Button))
            {
                Button boton = sender as Button;
                enlace = boton.Tag as string;
            }
            else if (sender.GetType() == typeof(MenuFlyoutItem))
            {
                MenuFlyoutItem item = sender as MenuFlyoutItem;
                enlace = item.Tag as string;
            }

            if (enlace != null)
            {
                await Launcher.LaunchUriAsync(new Uri(enlace));
            }

            ObjetosVentana.gridCarga.Visibility = Visibility.Collapsed;
            ObjetosVentana.nvPrincipal.SelectedItem = ObjetosVentana.nvPrincipal.MenuItems[1];
            Pestañas.Visibilidad(ObjetosVentana.gridEntradas, true);
        }

        public static void BotonCerrarMensaje(object sender, RoutedEventArgs e)
        {
            ObjetosVentana.gridCarga.Visibility = Visibility.Collapsed;
            ObjetosVentana.nvPrincipal.SelectedItem = ObjetosVentana.nvPrincipal.MenuItems[1];
            Pestañas.Visibilidad(ObjetosVentana.gridEntradas, true);
        }
    }
}
