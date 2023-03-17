using CommunityToolkit.WinUI.UI.Controls;
using Entradas;
using Herramientas;
using Interfaz;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.Windows.ApplicationModel.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using static Principal.MainWindow;
using static Wordpress;

namespace Otros
{
    public class SteamDeseadoJuego
    {
        public string name;
        public string capsule;
    }

    public class SteamCuentaVanidad
    {
        [JsonPropertyName("response")]
        public SteamCuentaVanidadID response { get; set; }
    }

    public class SteamCuentaVanidadID
    {
        [JsonPropertyName("steamid")]
        public string steamid { get; set; }
    }

    public class SteamCuenta
    {
        [JsonPropertyName("response")]
        public SteamCuentaJugadores response { get; set; }
    }

    public class SteamCuentaJugadores
    {
        [JsonPropertyName("players")]
        public List<SteamCuentaJugadoresInfo> players { get; set; }
    }

    public class SteamCuentaJugadoresInfo
    {
        [JsonPropertyName("personaname")]
        public string personaname { get; set; }

        [JsonPropertyName("avatar")]
        public string avatar { get; set; }
    }

    public static class SteamDeseados
    {
        public static void Cargar()
        {
            ResourceLoader recursos = new ResourceLoader();
            NavigationViewItem menu = ObjetosVentana.nvPrincipal.MenuItems[0] as NavigationViewItem;

            if (menu.Visibility == Visibility.Collapsed)
            {
                menu.Visibility = Visibility.Visible;
                ObjetosVentana.nvItemVolver.Visibility = Visibility.Collapsed;

                Pestañas.CreadorItems(recursos.GetString("Subscriptions"), null);
                Pestañas.CreadorItems(recursos.GetString("Free"), null);
                Pestañas.CreadorItems(recursos.GetString("Bundles"), null);
                Pestañas.CreadorItems(recursos.GetString("Deals"), null);
                Pestañas.CreadorItems(recursos.GetString("All"), null);
            }

            ObjetosVentana.tbSteamDeseadosEnlaceCuenta.TextChanged += CambiaCuenta;

            ApplicationDataContainer cuentaSteam = ApplicationData.Current.LocalSettings;        
            
            if (ObjetosVentana.tbSteamDeseadosEnlaceCuenta.IsEnabled == true)
            {
                if (cuentaSteam.Values["Cuenta_Steam"] != null)
                {
                    ObjetosVentana.tbSteamDeseadosEnlaceCuenta.Text = cuentaSteam.Values["Cuenta_Steam"].ToString();
                    ObjetosVentana.expanderSteamDeseados.IsExpanded = false;
                    CambiaCuenta();
                }
            }                   
        }

        public static void CambiaCuenta(object sender, TextChangedEventArgs e)
        {
            CambiaCuenta();
        }

        public static async void CambiaCuenta()
        {
            ObjetosVentana.prSteamDeseados.IsActive = true;
            ObjetosVentana.prSteamDeseados.Visibility = Visibility.Visible;

            if (ObjetosVentana.spSteamDeseados.Children.Count > 0)
            {
                ObjetosVentana.spSteamDeseados.Children.Clear();
            }

            TextBox tbCuenta = ObjetosVentana.tbSteamDeseadosEnlaceCuenta;
            tbCuenta.IsEnabled = false;

            int buscar = 0;

            if (tbCuenta.Text != null)
            {
                if (tbCuenta.Text.Contains("steamcommunity.com/id/"))
                {
                    buscar = 1;
                }
                else if (tbCuenta.Text.Contains("steamcommunity.com/profiles/"))
                {
                    buscar = 2;
                }
            }

            if (buscar > 0)
            {
                string usuario = tbCuenta.Text;
                usuario = usuario.Replace("https://steamcommunity.com/id/", null);
                usuario = usuario.Replace("http://steamcommunity.com/id/", null);
                usuario = usuario.Replace("https://steamcommunity.com/profiles/", null);
                usuario = usuario.Replace("http://steamcommunity.com/profiles/", null);
                usuario = usuario.Replace("/", null);
                usuario = usuario.Trim();
               
                try
                {
                    string steamID = null;

                    if (buscar == 1)
                    {
                        string htmlVanidad = await Decompiladores.CogerHtml("https://api.steampowered.com/ISteamUser/ResolveVanityURL/v1/?key=41F2D73A0B5024E9101F8D4E8D8AC21E&vanityurl=" + usuario);
                        SteamCuentaVanidad vanidad = System.Text.Json.JsonSerializer.Deserialize<SteamCuentaVanidad>(htmlVanidad);
                        steamID = vanidad.response.steamid;
                    }
                    else
                    {
                        steamID = usuario;
                    }

                    string htmlSteamID = await Decompiladores.CogerHtml("https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=41F2D73A0B5024E9101F8D4E8D8AC21E&steamids=" + steamID);
                    SteamCuenta cuenta = System.Text.Json.JsonSerializer.Deserialize<SteamCuenta>(htmlSteamID);

                    ObjetosVentana.imagenSteamDeseadosAvatar.Source = new BitmapImage(new Uri(cuenta.response.players[0].avatar));
                    ObjetosVentana.tbSteamDeseadosUsuario.Text = cuenta.response.players[0].personaname;
                }
                catch (Exception)
                {

                }

                List<SteamDeseadoJuego> juegosDeseadosTodos = new List<SteamDeseadoJuego>();
                int i = 0;
                while (i < 20)
                {
                    string htmlUsuario = string.Empty;

                    if (buscar == 1)
                    {
                        htmlUsuario = await Decompiladores.CogerHtml("https://store.steampowered.com/wishlist/id/" + usuario + "/wishlistdata/?p=" + i.ToString());
                    }
                    else if (buscar == 2)
                    {
                        htmlUsuario = await Decompiladores.CogerHtml("https://store.steampowered.com/wishlist/profiles/" + usuario + "/wishlistdata/?p=" + i.ToString());
                    }

                    if (htmlUsuario != null)
                    {
                        var juegosDeseados = new Dictionary<int, SteamDeseadoJuego>();

                        try
                        {
                            juegosDeseados = JsonConvert.DeserializeObject<Dictionary<int, SteamDeseadoJuego>>(htmlUsuario);
                        }
                        catch (Exception)
                        {
                            break;
                        };

                        if (juegosDeseados != null)
                        {
                            if (juegosDeseados.Count > 0)
                            {
                                foreach (var juegoDeseado in juegosDeseados)
                                {
                                    juegosDeseadosTodos.Add(juegoDeseado.Value);
                                }
                            }
                        }
                    }
                    i += 1;
                }

                if (juegosDeseadosTodos.Count > 0)
                {
                    foreach (Grid grid in ObjetosVentana.spEntradas.Children)
                    {
                        Entrada entrada = grid.Tag as Entrada;

                        if (entrada != null)
                        {
                            if (entrada.categories[0] == 3)
                            {
                                EntradaOfertas json = null;
                                string mensaje = null;

                                if (entrada.json != null)
                                {
                                    json = System.Text.Json.JsonSerializer.Deserialize<EntradaOfertas>(entrada.json);
                                    mensaje = json.mensaje;
                                }

                                if (entrada.json_expanded != null)
                                {
                                    json = System.Text.Json.JsonSerializer.Deserialize<EntradaOfertas>(entrada.json_expanded);
                                    mensaje = null;
                                }

                                if (json != null)
                                {
                                    if (json.juegos != null)
                                    {
                                        foreach (var juego in json.juegos)
                                        {
                                            foreach (SteamDeseadoJuego juegoDeseado in juegosDeseadosTodos)
                                            {
                                                if (Limpieza.Limpiar(juego.titulo) == Limpieza.Limpiar(juegoDeseado.name))
                                                {
                                                    GeneralXamlOferta(entrada, juego, mensaje, ObjetosVentana.spSteamDeseados);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (entrada.categories[0] == 4 || entrada.categories[0] == 12 || entrada.categories[0] == 13)
                            {
                                bool añadir = false;

                                foreach (SteamDeseadoJuego juegoDeseado in juegosDeseadosTodos)
                                {
                                    if (Limpieza.Limpiar(entrada.title.rendered).Contains(Limpieza.Limpiar(juegoDeseado.name)))
                                    {
                                        añadir = true;
                                    }
                                    else if (Limpieza.Limpiar(entrada.title2) != null)
                                    {
                                        if (Limpieza.Limpiar(entrada.title2).Contains(Limpieza.Limpiar(juegoDeseado.name)))
                                        {
                                            añadir = true;
                                        }
                                    }
                                }

                                if (añadir == true)
                                {
                                    if (entrada.categories[0] == 4)
                                    {
                                        ObjetosVentana.spSteamDeseados.Children.Add(Bundles.GenerarEntrada(entrada));
                                    }
                                    else if (entrada.categories[0] == 12)
                                    {
                                        ObjetosVentana.spSteamDeseados.Children.Add(Gratis.GenerarEntrada(entrada));
                                    }
                                    else if (entrada.categories[0] == 13)
                                    {
                                        ObjetosVentana.spSteamDeseados.Children.Add(Suscripciones.GenerarEntrada(entrada));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (ObjetosVentana.spSteamDeseados.Children.Count > 0)
            {
                Grid grid = ObjetosVentana.spSteamDeseados.Children[ObjetosVentana.spSteamDeseados.Children.Count - 1] as Grid;
                grid.Margin = new Thickness(0);

                ApplicationDataContainer cuentaSteam = ApplicationData.Current.LocalSettings;
                cuentaSteam.Values["Cuenta_Steam"] = ObjetosVentana.tbSteamDeseadosEnlaceCuenta.Text;
                ObjetosVentana.expanderSteamDeseados.IsExpanded = false;
            }

            tbCuenta.IsEnabled = true;

            ObjetosVentana.prSteamDeseados.IsActive = false;
            ObjetosVentana.prSteamDeseados.Visibility = Visibility.Collapsed;
        }

        public static void GeneralXamlOferta(Entrada entrada, EntradaOfertasJuego juego, string mensaje, StackPanel sp)
        {
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

            if (mensaje != null)
            {
                mensaje = mensaje.Replace("* ", null);
                mensaje = mensaje.Replace("<b>", null);
                mensaje = mensaje.Replace("</b>", null);
                mensaje = mensaje.Trim();

                TextBlock tb = new TextBlock
                {
                    Text = mensaje,
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

            spIzquierda.SetValue(Grid.ColumnProperty, 0);
            gridMaestro.Children.Add(spIzquierda);

            //------------------------------------

            StackPanel spDerecha = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

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
                MaxWidth = 400,
                MaxHeight = 300
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
                BorderThickness = new Thickness(0, 0, 0, 0),
                CornerRadius = new CornerRadius(5)
            };

            boton.Click += BotonAbrirJuego;
            boton.PointerEntered += Animaciones.EntraRatonBoton2;
            boton.PointerExited += Animaciones.SaleRatonBoton2;

            spDerecha.Children.Add(boton);

            spDerecha.SetValue(Grid.ColumnProperty, 1);
            gridMaestro.Children.Add(spDerecha);

            sp.Children.Add(gridMaestro);
        }

        private static async void BotonAbrirJuego(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            string enlace = boton.Tag as string;

            await Launcher.LaunchUriAsync(new Uri(enlace));
        }
    }
}