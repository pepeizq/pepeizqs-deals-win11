﻿using CommunityToolkit.WinUI.UI.Controls;
using Interfaz;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Windows.System;
using Windows.UI;
using static Wordpress;

namespace Entradas
{
    public class EntradaBundles
    {
        [JsonPropertyName("moregames")]
        public string masJuegos { get; set; }
        [JsonPropertyName("price")]
        public string precio { get; set; }
        [JsonPropertyName("games")]
        public List<EntradaBundlesJuego> juegos { get; set; }
    }

    public class EntradaBundlesJuego
    {
        [JsonPropertyName("image")]
        public string imagen { get; set; }
    }

    public static class Bundles
    {
        public static Grid GenerarEntrada(Entrada entrada)
        {
            EntradaBundles json = JsonSerializer.Deserialize<EntradaBundles>(entrada.json);

            ResourceLoader recursos = new ResourceLoader();

            SolidColorBrush fondoMaestro = new SolidColorBrush
            {
                Color = (Color)Application.Current.Resources["ColorPrimario"],
                Opacity = 0.6
            };

            Grid gridMaestro = new Grid
            {
                Tag = entrada,
                Margin = new Thickness(0, 0, 0, 40),
                Background = fondoMaestro,
                Padding = new Thickness(20, 20, 20, 20),
                CornerRadius = new CornerRadius(5)
            };

            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();

            col1.Width = new GridLength(1, GridUnitType.Auto);
            col2.Width = new GridLength(1, GridUnitType.Star);

            gridMaestro.ColumnDefinitions.Add(col1);
            gridMaestro.ColumnDefinitions.Add(col2);

            StackPanel spIzquierda = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(30, 30, 40, 30),
                VerticalAlignment = VerticalAlignment.Center
            };

            Image imagenTienda = new Image
            {
                MaxWidth = 180,
                MaxHeight = 50,
                Source = new BitmapImage(new Uri(entrada.store_logo))
            };

            spIzquierda.Children.Add(imagenTienda);

            string temp = entrada.title.rendered;
            int temp2 = temp.IndexOf("•");
            temp = temp.Remove(0, temp2 + 1);

            int temp3 = temp.IndexOf("•");
            string temp4 = temp.Remove(temp3, temp.Length - temp3);

            string precioBundle = temp4.Trim();

            StackPanel spBundles = new StackPanel
            {
                Background = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(0, 20, 0, 0),
                Padding = new Thickness(12, 8, 12, 8),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock tbBundles = new TextBlock
            {
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20,
                Text = precioBundle
            };

            spBundles.Children.Add(tbBundles);
            spIzquierda.Children.Add(spBundles);

            spIzquierda.SetValue(Grid.ColumnProperty, 0);
            gridMaestro.Children.Add(spIzquierda);

            //------------------------------------

            StackPanel spDerecha = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            StackPanel spJuegos = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            AdaptiveGridView gvJuegos = new AdaptiveGridView
            {
                DesiredWidth = 150,
                Padding = new Thickness(5, 5, 0, 0),
                IsHitTestVisible = false,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            foreach (EntradaBundlesJuego juego in json.juegos)
            {
                Image imagenJuego = new Image
                {
                    Source = new BitmapImage(new Uri(juego.imagen)),
                    MaxHeight = 200,
                    MaxWidth = 400,
                    Margin = new Thickness(10, 10, 10, 10)
                };

                gvJuegos.Items.Add(imagenJuego);
            }

            spJuegos.Children.Add(gvJuegos);

            string subTitulo = entrada.title2;

            if (subTitulo != null)
            {
                if (subTitulo.ToLower().Contains("and more games"))
                {
                    TextBlock tbMasJuegos = new TextBlock()
                    {
                        Text = recursos.GetString("AndMoreGames"),
                        Foreground = new SolidColorBrush(Colors.White),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        FontSize = 20,
                        Margin = new Thickness(0, 10, 0, 25)
                    };

                    spJuegos.Children.Add(tbMasJuegos);
                }
            }

            SolidColorBrush fondoBundle = new SolidColorBrush
            {
                Color = (Color)Application.Current.Resources["ColorPrimario"],
                Opacity = 0.9
            };
          
            Button2 boton = new Button2
            {
                Content = spJuegos,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = fondoBundle,
                Margin = new Thickness(10, 10, 10, 10),
                Tag = entrada.redirect,
                RequestedTheme = ElementTheme.Dark,
                BorderThickness = new Thickness(0),
                CornerRadius = new CornerRadius(5)
            };

            boton.Click += BotonAbrirBundle;
            boton.PointerEntered += Animaciones.EntraRatonBoton2;
            boton.PointerExited += Animaciones.SaleRatonBoton2;

            spDerecha.Children.Add(boton);

            spDerecha.SetValue(Grid.ColumnProperty, 1);
            gridMaestro.Children.Add(spDerecha);

            return gridMaestro;
        }

        public static async void BotonAbrirBundle(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            string enlace = boton.Tag as string;

            await Launcher.LaunchUriAsync(new Uri(enlace));
        }
    }
}
