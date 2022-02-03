using CommunityToolkit.WinUI.UI.Controls;
using Interfaz;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Windows.ApplicationModel.Resources;
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

        public static void BotonAbrirExpandida(object sender, RoutedEventArgs e)
        {
            ScrollViewers.EnseñarSubir(ObjetosVentana.svOfertasExpandida);
            Pestañas.Visibilidad(ObjetosVentana.gridOfertasExpandida, true);
            BarraTitulo.CambiarTitulo(null);

            string gridVisible = string.Empty;

            if (ObjetosVentana.gridEntradasTodo.Visibility == Visibility.Visible)
            {
                gridVisible = ObjetosVentana.gridEntradasTodo.Name;
            }
            else if (ObjetosVentana.gridEntradasOfertas.Visibility == Visibility.Visible)
            {
                gridVisible = ObjetosVentana.gridEntradasOfertas.Name;
            }

            ObjetosVentana.nvItemVolver.Visibility = Visibility.Visible;
            ObjetosVentana.nvItemVolver.Tag = gridVisible;

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
                //juegos = juegos.Sort((e1, e2) =>
                //{
                //    return e2.titulo.CompareTo(e1.titulo);
                //});

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

                    ImageEx imagenJuego = new ImageEx
                    {
                        MaxWidth = 250,
                        MaxHeight = 150,
                        Source = new BitmapImage(new Uri(juego.imagen)),
                        EnableLazyLoading = true,
                        IsCacheEnabled = true,
                        CornerRadius = new CornerRadius(3)
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
                        else if (juego.drm.ToLower().Contains("bethesda"))
                        {
                            stringdrm = "drm_bethesda2.png";
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
                            stringdrm = "/Assets/DRMs/" + stringdrm;
                       
                            ImageEx imagenDRM = new ImageEx
                            {
                                Source = stringdrm,
                                Margin = new Thickness(20, 0, 0, 0),
                                VerticalAlignment = VerticalAlignment.Center,
                                EnableLazyLoading = true,
                                IsCacheEnabled = true,
                                MaxHeight = 30
                            };

                            spDatos.Children.Add(imagenDRM);
                        }
                    }

                    if (juego.analisisPorcentaje != null)
                    {
                        if (juego.analisisPorcentaje != "null" && juego.analisisPorcentaje != "0")
                        {
                            ImageEx imagenAnalisis = new ImageEx
                            {
                                MaxHeight = 30,
                                IsCacheEnabled = true,
                                EnableLazyLoading = true,
                                Margin = new Thickness(20, 0, 0, 0),
                                VerticalAlignment = VerticalAlignment.Center
                            };

                            if (int.Parse(juego.analisisPorcentaje) > 74)
                            {
                                imagenAnalisis.Source = "/Assets/Reviews/review_positive.png";
                            }
                            else if (int.Parse(juego.analisisPorcentaje) > 49 && int.Parse(juego.analisisPorcentaje) < 75)
                            {
                                imagenAnalisis.Source = "/Assets/Reviews/review_mixed.png";
                            }
                            else if (int.Parse(juego.analisisPorcentaje) < 50)
                            {
                                imagenAnalisis.Source = "/Assets/Reviews/review_negative.png";
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

                    spDerecha.Children.Add(spDatos);

                    spDerecha.SetValue(Grid.ColumnProperty, 1);
                    gridMaestro.Children.Add(spDerecha);

                    Button boton = new Button
                    {
                        Content = gridMaestro,
                        Margin = new Thickness(0, 0, 0, 30),
                        Padding = new Thickness(0, 0, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        HorizontalContentAlignment = HorizontalAlignment.Stretch,
                        Background = new SolidColorBrush(Colors.Transparent),
                        Tag = juego.enlace,
                        CornerRadius = new CornerRadius(5)
                    };

                    boton.Click += BotonAbrirJuego;

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

            string gridVolver = ObjetosVentana.nvItemVolver.Tag as string;
           
            if (gridVolver == "gridEntradasOfertas")
            {
                Pestañas.Visibilidad(ObjetosVentana.gridEntradasOfertas, true);              
                BarraTitulo.CambiarTitulo(recursos.GetString("Deals"));
                ScrollViewers.EnseñarSubir(ObjetosVentana.svEntradasOfertas);
            }
            else
            {
                Pestañas.Visibilidad(ObjetosVentana.gridEntradasTodo, true);
                BarraTitulo.CambiarTitulo(null);
                ScrollViewers.EnseñarSubir(ObjetosVentana.svEntradasTodo);
            }

            NavigationViewItem menu = ObjetosVentana.nvPrincipal.MenuItems[0] as NavigationViewItem;
            menu.Visibility = Visibility.Visible;

            Pestañas.CreadorItems(recursos.GetString("Subscriptions"), null);
            Pestañas.CreadorItems(recursos.GetString("Free"), null);
            Pestañas.CreadorItems(recursos.GetString("Bundles"), null);
            Pestañas.CreadorItems(recursos.GetString("Deals"), null);
            Pestañas.CreadorItems(recursos.GetString("All"), null);
        }
    }
}
