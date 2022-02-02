using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Windows.ApplicationModel.Resources;
using Windows.System;
using Windows.UI;
using static Wordpress;

namespace Entradas
{
    public class EntradaOfertas
    {
        [JsonPropertyName("message")]
        public string mensaje { get; set; }
        [JsonPropertyName("games")]
        public List<EntradaOfertasJuego> juegos { get; set; }
    }

    public class EntradaOfertasJuego
    {
        [JsonPropertyName("title")]
        public string titulo { get; set; }
        [JsonPropertyName("image")]
        public string imagen { get; set; }
        [JsonPropertyName("link")]
        public string enlace { get; set; }
        [JsonPropertyName("dscnt")]
        public string descuento { get; set; }
        [JsonPropertyName("price")]
        public string precio { get; set; }
        //public string price2;
        //public string drm;
        //public string revw1;
        //public string revw2;
        //public string revw3;
    }

    public static class Ofertas
    {
        public static Grid GenerarEntrada(Entrada entrada)
        {
            EntradaOfertas json = JsonSerializer.Deserialize<EntradaOfertas>(entrada.json);

            ResourceLoader recursos = new ResourceLoader();
          
            SolidColorBrush fondoMaestro = new SolidColorBrush
            {
                Color = (Color)Application.Current.Resources["ColorPrimario"],
                Opacity = 0.6
            };

            Grid gridMaestro = new Grid
            {
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

            ImageEx imagenTienda = new ImageEx
            {
                MaxWidth = 180,
                MaxHeight = 50,
                Source = new BitmapImage(new Uri(entrada.store_logo)),
                EnableLazyLoading = true,
                IsCacheEnabled = true
            };

            spIzquierda.Children.Add(imagenTienda);

            if (entrada.json_expanded == null)
            {               
                if (json != null)
                {
                    if (json.mensaje != null)
                    {
                        json.mensaje = json.mensaje.Replace("* ", null);
                        json.mensaje = json.mensaje.Replace("<b>", null);
                        json.mensaje = json.mensaje.Replace("</b>", null);
                        json.mensaje = json.mensaje.Trim();

                        TextBlock tb = new TextBlock
                        {
                            Text = json.mensaje,
                            Foreground = new SolidColorBrush(Colors.White),
                            TextWrapping = TextWrapping.Wrap,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Center,
                            Margin = new Thickness(0, 30, 0, 0),
                            FontSize = 16,
                            MaxWidth = 300,
                            LineHeight = 26
                        };

                        spIzquierda.Children.Add(tb);
                    }
                }
            }

            spIzquierda.SetValue(Grid.ColumnProperty, 0);
            gridMaestro.Children.Add(spIzquierda);

            //------------------------------------

            StackPanel spDerecha = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Stretch   
            };

            if (json != null)
            {
                if (json.juegos != null)
                {
                    AdaptiveGridView gv = new AdaptiveGridView
                    {
                        HorizontalAlignment = HorizontalAlignment.Center
                    };

                    int maximoAncho = 250;
                    int maximoAlto = 200;

                    if (json.juegos.Count == 1)
                    {
                        maximoAncho = 400;
                        maximoAlto = 300;
                    };
                   
                    foreach (EntradaOfertasJuego juego in json.juegos)
                    {
                        SolidColorBrush fondoJuego = new SolidColorBrush
                        {
                            Color = (Color)Application.Current.Resources["ColorPrimario"],
                            Opacity = 0.4
                        };

                        StackPanel spJuego = new StackPanel
                        {
                            Orientation = Orientation.Vertical,
                            Background = fondoJuego,
                            CornerRadius = new CornerRadius(2)
                        };

                        ImageEx imagenJuego = new ImageEx
                        {
                            Source = new BitmapImage(new Uri(juego.imagen)),
                            MaxHeight = maximoAlto,
                            MaxWidth = maximoAncho,
                            EnableLazyLoading = true,
                            IsCacheEnabled = true,
                            CornerRadius = new CornerRadius(2,2,0,0)
                        };

                        spJuego.Children.Add(imagenJuego);

                        StackPanel spDatos = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            HorizontalAlignment = HorizontalAlignment.Right
                        };

                        TextBlock tbDescuento = new TextBlock
                        {
                            Text = juego.descuento,
                            Foreground = new SolidColorBrush(Colors.White),
                            FontSize = 20
                        };

                        StackPanel spDescuento = new StackPanel
                        {
                            Background = new SolidColorBrush(Colors.ForestGreen),
                            Padding = new Thickness(10, 8, 10, 8)
                        };

                        spDescuento.Children.Add(tbDescuento);

                        spDatos.Children.Add(spDescuento);

                        TextBlock tbPrecio = new TextBlock
                        {
                            Text = juego.precio,
                            Foreground = new SolidColorBrush(Colors.White),
                            FontSize = 20
                        };

                        StackPanel spPrecio = new StackPanel
                        {
                            Background = new SolidColorBrush(Colors.Black),
                            Padding = new Thickness(10, 8, 10, 8)
                        };

                        spPrecio.Children.Add(tbPrecio);

                        spDatos.Children.Add(spPrecio);

                        spJuego.Children.Add(spDatos);

                        Button boton = new Button
                        {
                            Content = spJuego,
                            Padding = new Thickness(10, 10, 10, 10),
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            HorizontalContentAlignment = HorizontalAlignment.Center,
                            Tag = juego.enlace,
                            Background = new SolidColorBrush(Colors.Transparent),
                            BorderBrush = new SolidColorBrush(Colors.Transparent),
                            BorderThickness = new Thickness(0, 0, 0, 0)
                        };

                        boton.Click += BotonAbrirJuego;

                        gv.Items.Add(boton);
                    }

                    spDerecha.Children.Add(gv);

                    if (entrada.json_expanded != null)
                    {
                        EntradaOfertas jsonExpandido = JsonSerializer.Deserialize<EntradaOfertas>(entrada.json_expanded);

                        if (jsonExpandido.juegos.Count > 6)
                        {
                            SolidColorBrush fondoAmpliar = new SolidColorBrush
                            {
                                Color = (Color)Application.Current.Resources["ColorPrimario"],
                                Opacity = 0.9
                            };

                            TextBlock textoAmpliar = new TextBlock
                            {
                                Text = recursos.GetString("ShowDeals") + " (" + jsonExpandido.juegos.Count.ToString() + ")",
                                Foreground = new SolidColorBrush(Colors.White),
                                FontSize = 19
                            };

                            Button botonAmpliar = new Button
                            {
                                Content = textoAmpliar,
                                Margin = new Thickness(10, 10, 15, 15),
                                Padding = new Thickness(40, 15, 40, 15),
                                Tag = jsonExpandido,
                                Background = fondoAmpliar,
                                HorizontalAlignment = HorizontalAlignment.Center
                            };

                            botonAmpliar.Click += BotonAbrirExpandida;

                            spDerecha.Children.Add(botonAmpliar);
                        }
                    }
                }
            }

            spDerecha.SetValue(Grid.ColumnProperty, 1);
            gridMaestro.Children.Add(spDerecha);

            return gridMaestro;
        }

        public static async void BotonAbrirJuego(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            string enlace = boton.Tag as string;

            await Launcher.LaunchUriAsync(new Uri(enlace));
        }

        public static async void BotonAbrirExpandida(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            string json = boton.Tag as string;

            
        }
    }
}
