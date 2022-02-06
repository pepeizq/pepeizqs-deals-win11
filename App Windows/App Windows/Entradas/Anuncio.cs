using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using static Principal.MainWindow;
using static Wordpress;

namespace Entradas
{
    public static class Anuncio
    {
        public static void CargarEntrada(Entrada entrada)
        {
            if (ObjetosVentana.spAnuncio.Children.Count == 0)
            {
                ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;

                if (datos.Values[entrada.id.ToString()] == null)
                {
                    datos.Values[entrada.id.ToString()] = true;

                    ImageEx imagenAnuncio = new ImageEx
                    {
                        IsCacheEnabled = true,
                        EnableLazyLoading = true,
                        Source = entrada.fifu_image_url,
                        MaxHeight = 775,
                        MaxWidth = 2400,
                        CornerRadius = new CornerRadius(3)
                    };

                    Button boton = new Button()
                    {
                        Content = imagenAnuncio,
                        Padding = new Thickness(10, 10, 10, 10),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        Tag = entrada.redirect,
                        Background = new SolidColorBrush((Color)Application.Current.Resources["ColorPrimario"])
                    };

                    boton.Click += BotonAbrirAnuncio;

                    ObjetosVentana.spAnuncio.Children.Add(boton);

                    SymbolIcon iconoCerrar = new SymbolIcon
                    {
                        Symbol = Symbol.Cancel,
                        Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"])                     
                    };

                    Button botonCerrar = new Button()
                    {
                        Content = iconoCerrar,
                        Margin = new Thickness(0, 25, 0, 0),
                        Padding = new Thickness(10, 10, 10, 10),
                        HorizontalAlignment = HorizontalAlignment.Right,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        Background = new SolidColorBrush((Color)Application.Current.Resources["ColorPrimario"])
                    };

                    botonCerrar.Click += BotonCerrarAnuncio;

                    ObjetosVentana.spAnuncio.Children.Add(botonCerrar);
                }
            }           
        }

        public static async void BotonAbrirAnuncio(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            string enlace = boton.Tag as string;

            await Launcher.LaunchUriAsync(new Uri(enlace));

            ObjetosVentana.gridAnuncio.Visibility = Visibility.Collapsed;
        }

        public static void BotonCerrarAnuncio(object sender, RoutedEventArgs e)
        {
            ObjetosVentana.gridAnuncio.Visibility = Visibility.Collapsed;
        }
    }
}
