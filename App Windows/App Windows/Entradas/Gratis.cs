﻿using CommunityToolkit.WinUI.UI.Controls;
using Interfaz;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Windows.System;
using Windows.UI;
using static Wordpress;

namespace Entradas
{
    public class EntradaGratis
    {
        [JsonPropertyName("image")]
        public string imagen { get; set; }
    }

    public static class Gratis
    {
        public static Grid GenerarEntrada(Entrada entrada)
        {
            EntradaGratis json = JsonSerializer.Deserialize<EntradaGratis>(entrada.json);

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

            string imagen = entrada.store_logo;

            if (imagen.Contains("https://i.imgur.com/") == true)
            {
                imagen = imagen.Replace("https://pepeizqdeals.com/wp-content/uploads/", null);
            }
     
            Image imagenTienda = new Image
            {
                MaxWidth = 180,
                MaxHeight = 50,
                Source = new BitmapImage(new Uri(imagen))
            };
            
            spIzquierda.Children.Add(imagenTienda);

            StackPanel spMensaje = new StackPanel
            {
                Background = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(0, 20, 0, 0),
                Padding = new Thickness(12, 8, 12, 8),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock tbMensaje = new TextBlock
            {
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20,
                Text = recursos.GetString("Free")
            };

            spMensaje.Children.Add(tbMensaje);
            spIzquierda.Children.Add(spMensaje);

            spIzquierda.SetValue(Grid.ColumnProperty, 0);
            gridMaestro.Children.Add(spIzquierda);

            //------------------------------------

            StackPanel spDerecha = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            AdaptiveGridView gv = new AdaptiveGridView
            {
                DesiredWidth = 400,
                Padding = new Thickness(2, 2, 2, 2),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            Image imagenJuego = new Image
            {
                Source = new BitmapImage(new Uri(json.imagen)),
                MaxHeight = 200,
                MaxWidth = 400
            };

            Button2 boton = new Button2
            {
                Content = imagenJuego,
                Padding = new Thickness(10, 10, 10, 10),
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Tag = entrada.redirect,
                Background = new SolidColorBrush(Colors.Transparent),
                RequestedTheme = ElementTheme.Dark,
                BorderThickness = new Thickness(0),
                CornerRadius = new CornerRadius(5)
            };

            boton.Click += BotonAbrirGratis;
            boton.PointerEntered += Animaciones.EntraRatonBoton2;
            boton.PointerExited += Animaciones.SaleRatonBoton2;

            gv.Items.Add(boton);

            spDerecha.Children.Add(gv);

            spDerecha.SetValue(Grid.ColumnProperty, 1);
            gridMaestro.Children.Add(spDerecha);

            return gridMaestro;
        }

        public static async void BotonAbrirGratis(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            string enlace = boton.Tag as string;

            await Launcher.LaunchUriAsync(new Uri(enlace));
        }
    }
}
