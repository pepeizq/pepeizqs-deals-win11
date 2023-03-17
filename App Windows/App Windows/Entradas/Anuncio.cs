using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
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

                    Image imagenAnuncio = new Image
                    {
                        Source = new BitmapImage(new Uri(entrada.fifu_image_url)),
                        MaxHeight = 775,
                        MaxWidth = 2400
                    };

                    Button2 boton = new Button2()
                    {
                        Content = imagenAnuncio,
                        Padding = new Thickness(10, 10, 10, 10),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        Tag = entrada.redirect,
                        Background = new SolidColorBrush((Color)Application.Current.Resources["ColorPrimario"]),
                        RequestedTheme = ElementTheme.Dark,
                        BorderThickness = new Thickness(0)
                    };

                    boton.Click += BotonAbrirAnuncio;
                    boton.PointerEntered += Animaciones.EntraRatonBoton2;
                    boton.PointerExited += Animaciones.SaleRatonBoton2;

                    ObjetosVentana.spAnuncio.Children.Add(boton);

                    SymbolIcon iconoCerrar = new SymbolIcon
                    {
                        Symbol = Symbol.Cancel,
                        Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"])                     
                    };

                    Button2 botonCerrar = new Button2()
                    {
                        Content = iconoCerrar,
                        Margin = new Thickness(0, 25, 0, 0),
                        Padding = new Thickness(10, 10, 10, 10),
                        HorizontalAlignment = HorizontalAlignment.Right,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        Background = new SolidColorBrush((Color)Application.Current.Resources["ColorPrimario"]),
                        RequestedTheme = ElementTheme.Dark,
                        BorderThickness = new Thickness(0)
                    };

                    botonCerrar.Click += BotonCerrarAnuncio;
                    botonCerrar.PointerEntered += Animaciones.EntraRatonBoton2;
                    botonCerrar.PointerExited += Animaciones.SaleRatonBoton2;

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
