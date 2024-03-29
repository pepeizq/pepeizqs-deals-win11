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
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Windows.System;
using Windows.UI;
using static Principal.MainWindow;
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
        [JsonPropertyName("price2")]
        public string precio2 { get; set; }
        [JsonPropertyName("drm")]
        public string drm { get; set; }
        [JsonPropertyName("revw1")]
        public string analisisPorcentaje { get; set; }
        [JsonPropertyName("revw2")]
        public string analisisCantidad { get; set; }
        [JsonPropertyName("revw3")]
        public string analisisEnlace { get; set; }
    }

    public static class Ofertas
    {
        public static void Cargar()
        {
            ObjetosVentana.cbOrdenarOfertasExpandida.SelectionChanged += CambiarOrdenado;
            ObjetosVentana.cbOrdenarOfertasExpandida.PointerEntered += Animaciones.EntraRatonComboCaja2;
            ObjetosVentana.cbOrdenarOfertasExpandida.PointerExited += Animaciones.SaleRatonComboCaja2;
        }

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
                    int maximoAncho = 250;
                    int maximoAlto = 200;

                    if (json.juegos.Count == 1)
                    {
                        maximoAncho = 400;
                        maximoAlto = 300;
                    };

                    AdaptiveGridView gv = new AdaptiveGridView
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        DesiredWidth = maximoAncho
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

                        Image imagenJuego = new Image
                        {
                            Source = new BitmapImage(new Uri(juego.imagen)),
                            MaxHeight = maximoAlto,
                            MaxWidth = maximoAncho
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

                        Button2 boton = new Button2
                        {
                            Content = spJuego,
                            Padding = new Thickness(10, 10, 10, 10),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            HorizontalContentAlignment = HorizontalAlignment.Stretch,
                            VerticalContentAlignment = VerticalAlignment.Stretch,
                            Tag = juego.enlace,
                            Background = new SolidColorBrush(Colors.Transparent),
                            BorderBrush = new SolidColorBrush(Colors.Transparent),
                            RequestedTheme = ElementTheme.Dark,
                            BorderThickness = new Thickness(0),
                            CornerRadius = new CornerRadius(5)
                        };

                        boton.Click += BotonAbrirJuego;
                        boton.PointerEntered += Animaciones.EntraRatonBoton2;
                        boton.PointerExited += Animaciones.SaleRatonBoton2;
                        
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

                            Button2 botonAmpliar = new Button2
                            {
                                Content = textoAmpliar,
                                Margin = new Thickness(10, 10, 15, 15),
                                Padding = new Thickness(40, 15, 40, 15),
                                Tag = jsonExpandido,
                                Background = fondoAmpliar,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                RequestedTheme = ElementTheme.Dark,
                                BorderThickness = new Thickness(0),
                                CornerRadius = new CornerRadius(5)
                            };

                            botonAmpliar.Click += BotonAbrirExpandida;
                            botonAmpliar.PointerEntered += Animaciones.EntraRatonBoton2;
                            botonAmpliar.PointerExited += Animaciones.SaleRatonBoton2;

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

        public static void BotonAbrirExpandida(object sender, RoutedEventArgs e)
        {
            ScrollViewers.EnseñarSubir(ObjetosVentana.svOfertasExpandida);
            Pestañas.Visibilidad(ObjetosVentana.gridOfertasExpandida, true);
            BarraTitulo.CambiarTitulo(null);

            ObjetosVentana.nvItemVolver.Visibility = Visibility.Visible;

            for (int i = 0; i < ObjetosVentana.nvPrincipal.MenuItems.Count; i++)
            {
                if (i > 0 && i < ObjetosVentana.nvPrincipal.MenuItems.Count - 1)
                {
                    ObjetosVentana.nvPrincipal.MenuItems.RemoveAt(i);
                    i -= 1;
                }             
            }

            NavigationViewItem menu = ObjetosVentana.nvPrincipal.MenuItems[0] as NavigationViewItem;
            menu.Visibility = Visibility.Collapsed;

            Button boton = sender as Button;
            EntradaOfertas json = boton.Tag as EntradaOfertas;

            ObjetosVentana.tbMensajeOfertasExpandida.Visibility = Visibility.Collapsed;

            if (json != null)
            {
                if (json.mensaje != null)
                {
                    ObjetosVentana.tbMensajeOfertasExpandida.Text = json.mensaje;
                    ObjetosVentana.tbMensajeOfertasExpandida.Text = ObjetosVentana.tbMensajeOfertasExpandida.Text.Replace("<b>", null);
                    ObjetosVentana.tbMensajeOfertasExpandida.Text = ObjetosVentana.tbMensajeOfertasExpandida.Text.Replace("</b>", null);
                    ObjetosVentana.tbMensajeOfertasExpandida.Visibility = Visibility.Visible;
                }

                ObjetosVentana.cbOrdenarOfertasExpandida.Tag = json.juegos;
                GenerarListadoOfertas(json.juegos, ObjetosVentana.cbOrdenarOfertasExpandida.SelectedIndex);
            }
        }

        public static void GenerarListadoOfertas(List<EntradaOfertasJuego> juegos, int ordenado)
        {
            ObjetosVentana.svOfertasExpandida.ChangeView(null, 0, null);
            ObjetosVentana.spOfertasExpandida.Children.Clear();

            ResourceLoader recursos = new ResourceLoader();           

            if (ordenado == 0)
            {
                juegos = juegos.OrderBy(x => x.titulo).ToList();               
            }
            else if (ordenado == 1)
            {
                juegos = juegos.OrderByDescending(x => x.descuento).ToList();
            }
            else if (ordenado == 2)
            {
                foreach (EntradaOfertasJuego juego in juegos)
                {
                    if (juego.analisisPorcentaje == "null")
                    {
                        juego.analisisPorcentaje = "0";
                    }
                }

                juegos = juegos.OrderByDescending(x => x.analisisPorcentaje.Replace("%", null)).ToList();
            }

            foreach (EntradaOfertasJuego juego in juegos)
            {
                bool añadir = true;

                foreach (Button boton in ObjetosVentana.spOfertasExpandida.Children)
                {
                    string enlace = boton.Tag as string;

                    if (enlace == juego.enlace)
                    {
                        añadir = false;
                    }
                }

                if (añadir == true)
                {
                    SolidColorBrush fondoMaestro = new SolidColorBrush
                    {
                        Color = (Color)Application.Current.Resources["ColorPrimario"],
                        Opacity = 0.6
                    };

                    Grid gridMaestro = new Grid
                    {
                        Margin = new Thickness(0, 0, 0, 0),
                        Background = fondoMaestro,
                        Padding = new Thickness(20, 20, 20, 20)
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
                        Margin = new Thickness(10, 10, 30, 10),
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    Image imagenJuego = new Image
                    {
                        MaxWidth = 250,
                        MaxHeight = 150,
                        Source = new BitmapImage(new Uri(juego.imagen))
                    };

                    spIzquierda.Children.Add(imagenJuego);

                    spIzquierda.SetValue(Grid.ColumnProperty, 0);
                    gridMaestro.Children.Add(spIzquierda);

                    //-----------------------------------------------

                    StackPanel spDerecha = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Margin = new Thickness(10, 10, 30, 10),
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    TextBlock tbTitulo = new TextBlock
                    {
                        Text = juego.titulo,
                        Foreground = new SolidColorBrush(Colors.White),
                        FontSize = 17,
                        TextWrapping = TextWrapping.Wrap,
                        Margin = new Thickness(0, 0, 0, 20)
                    };

                    spDerecha.Children.Add(tbTitulo);

                    StackPanel spDatos = new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    };

                    TextBlock tbDescuento = new TextBlock
                    {
                        Text = juego.descuento,
                        Foreground = new SolidColorBrush(Colors.White),
                        FontSize = 17
                    };

                    StackPanel spDescuento = new StackPanel
                    {
                        Background = new SolidColorBrush(Colors.ForestGreen),
                        Padding = new Thickness(10, 6, 8, 8)
                    };

                    spDescuento.Children.Add(tbDescuento);

                    spDatos.Children.Add(spDescuento);

                    TextBlock tbPrecio = new TextBlock
                    {
                        Text = juego.precio,
                        Foreground = new SolidColorBrush(Colors.White),
                        FontSize = 17
                    };

                    StackPanel spPrecio = new StackPanel
                    {
                        Background = new SolidColorBrush(Colors.Black),
                        Padding = new Thickness(10, 6, 8, 8)
                    };

                    spPrecio.Children.Add(tbPrecio);

                    spDatos.Children.Add(spPrecio);

                    if (juego.precio2 != null)
                    {
                        TextBlock tbPrecio2 = new TextBlock
                        {
                            Text = juego.precio2,
                            Foreground = new SolidColorBrush(Colors.White),
                            FontSize = 17
                        };

                        StackPanel spPrecio2 = new StackPanel
                        {
                            Background = new SolidColorBrush(Colors.Black),
                            Padding = new Thickness(10, 6, 8, 8),
                            Margin = new Thickness(20, 0, 0, 0)
                        };

                        spPrecio2.Children.Add(tbPrecio2);

                        spDatos.Children.Add(spPrecio2);
                    }

                    if (juego.drm != null)
                    {
                        string stringdrm = String.Empty;

                        if (juego.drm.ToLower().Contains("steam"))
                        {
                            stringdrm = "drm_steam2.png";
                        }
                        else if (juego.drm.ToLower().Contains("uplay") || juego.drm.ToLower().Contains("ubisoft") || juego.drm.ToLower().Contains("ubiconnect"))
                        {
                            stringdrm = "drm_ubi2.png";
                        }
                        else if (juego.drm.ToLower().Contains("origin") || juego.drm.ToLower().Contains("ea play") || juego.drm.ToLower().Contains("eaplay"))
                        {
                            stringdrm = "drm_eaplay2.png";
                        }
                        else if (juego.drm.ToLower().Contains("gog"))
                        {
                            stringdrm = "drm_gog2.png";
                        }
                        else if (juego.drm.ToLower().Contains("battle") || juego.drm.ToLower().Contains("blizzard"))
                        {
                            stringdrm = "drm_battlenet2.png";
                        }
                        else if (juego.drm.ToLower().Contains("epic"))
                        {
                            stringdrm = "drm_epic2.png";
                        }
                        else if (juego.drm.ToLower().Contains("microsoft"))
                        {
                            stringdrm = "drm_microsoft2.png";
                        }
                        else if (juego.drm.ToLower().Contains("rockstar"))
                        {
                            stringdrm = "drm_rockstar2.png";
                        }

                        if (stringdrm != String.Empty)
                        {
                            stringdrm = "ms-appx:///Assets/DRMs/" + stringdrm;
                       
                            Image imagenDRM = new Image
                            {
                                Source = new BitmapImage(new Uri(stringdrm)),
                                Margin = new Thickness(20, 0, 0, 0),
                                VerticalAlignment = VerticalAlignment.Center,
                                MaxHeight = 30
                            };

                            spDatos.Children.Add(imagenDRM);
                        }
                    }

                    if (juego.analisisPorcentaje != null)
                    {
                        if (juego.analisisPorcentaje.Trim().Length > 0)
                        {
                            if (juego.analisisPorcentaje != "null" && juego.analisisPorcentaje != "0")
                            {
                                Image imagenAnalisis = new Image
                                {
                                    MaxHeight = 30,
                                    Margin = new Thickness(20, 0, 0, 0),
                                    VerticalAlignment = VerticalAlignment.Center
                                };

                                if (int.Parse(juego.analisisPorcentaje) > 74)
                                {
                                    imagenAnalisis.Source = new BitmapImage(new Uri("ms-appx:///Assets/Reviews/review_positive.png"));
                                }
                                else if (int.Parse(juego.analisisPorcentaje) > 49 && int.Parse(juego.analisisPorcentaje) < 75)
                                {
                                    imagenAnalisis.Source = new BitmapImage(new Uri("ms-appx:///Assets/Reviews/review_mixed.png"));
                                }
                                else if (int.Parse(juego.analisisPorcentaje) < 50)
                                {
                                    imagenAnalisis.Source = new BitmapImage(new Uri("ms-appx:///Assets/Reviews/review_negative.png"));
                                }

                                spDatos.Children.Add(imagenAnalisis);

                                TextBlock tbAnalisis = new TextBlock
                                {
                                    Foreground = new SolidColorBrush(Colors.White),
                                    FontSize = 13,
                                    Text = juego.analisisPorcentaje + "% • " + juego.analisisCantidad + " " + recursos.GetString("Reviews"),
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Margin = new Thickness(10, 0, 0, 2)
                                };

                                spDatos.Children.Add(tbAnalisis);
                            }
                        }                        
                    }

                    spDerecha.Children.Add(spDatos);

                    spDerecha.SetValue(Grid.ColumnProperty, 1);
                    gridMaestro.Children.Add(spDerecha);

                    Button2 boton = new Button2
                    {
                        Content = gridMaestro,
                        Margin = new Thickness(0, 0, 0, 20),
                        Padding = new Thickness(12),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        HorizontalContentAlignment = HorizontalAlignment.Stretch,
                        Background = new SolidColorBrush(Colors.Transparent),
                        Tag = juego.enlace,
                        CornerRadius = new CornerRadius(5),
                        RequestedTheme = ElementTheme.Dark,
                        BorderThickness = new Thickness(0)
                    };

                    boton.Click += BotonAbrirJuego;
                    boton.PointerEntered += Animaciones.EntraRatonBoton2;
                    boton.PointerExited += Animaciones.SaleRatonBoton2;

                    ObjetosVentana.spOfertasExpandida.Children.Add(boton);
                }
            }

            if (ObjetosVentana.spOfertasExpandida.Children.Count > 1)
            {
                if (juegos.Count == ObjetosVentana.spOfertasExpandida.Children.Count)
                {
                    Button boton = ObjetosVentana.spOfertasExpandida.Children[ObjetosVentana.spOfertasExpandida.Children.Count - 1] as Button;
                    boton.Margin = new Thickness(0);
                }
            }
        }

        public static void BotonCerrarExpandida(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();
            ObjetosVentana.nvItemVolver.Visibility = Visibility.Collapsed;
            Pestañas.Visibilidad(ObjetosVentana.gridEntradas, true);
            ScrollViewers.EnseñarSubir(ObjetosVentana.svEntradas);

            int volver = int.Parse(ObjetosVentana.nvPrincipal.Tag.ToString());

            if (volver == 0)
            {                              
                BarraTitulo.CambiarTitulo(null);                
            }
            else
            {
                BarraTitulo.CambiarTitulo(recursos.GetString("Deals"));
            }

            NavigationViewItem menu = ObjetosVentana.nvPrincipal.MenuItems[0] as NavigationViewItem;
            menu.Visibility = Visibility.Visible;

            Pestañas.CreadorItems(recursos.GetString("Subscriptions"), null);
            Pestañas.CreadorItems(recursos.GetString("Free"), null);
            Pestañas.CreadorItems(recursos.GetString("Bundles"), null);
            Pestañas.CreadorItems(recursos.GetString("Deals"), null);
            Pestañas.CreadorItems(recursos.GetString("All"), null);
        }

        public static void CambiarOrdenado(object sender, SelectionChangedEventArgs e)
        {
            List<EntradaOfertasJuego> juegos = ObjetosVentana.cbOrdenarOfertasExpandida.Tag as List<EntradaOfertasJuego>;
            GenerarListadoOfertas(juegos, ObjetosVentana.cbOrdenarOfertasExpandida.SelectedIndex);
        }
    }
}
