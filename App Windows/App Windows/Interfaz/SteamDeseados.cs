using Entradas;
using Herramientas;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Windows.Storage;
using static Principal.MainWindow;
using static Wordpress;

namespace Interfaz
{
    public class SteamDeseadoJuego
    {
        public string name;
        public string capsule;
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

            ObjetosVentana.tbSteamDeseados.TextChanged += CambiaCuenta;

            ApplicationDataContainer cuentaSteam = ApplicationData.Current.LocalSettings;        
            
            if (cuentaSteam.Values["Cuenta_Steam"] != null)
            {
                ObjetosVentana.tbSteamDeseados.Text = cuentaSteam.Values["Cuenta_Steam"].ToString();
            }           
        }

        public static async void CambiaCuenta(object sender, TextChangedEventArgs e)
        {
            TextBox tbCuenta = sender as TextBox;
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
                                bool añadir = false;

                                if (entrada.json != null)
                                {
                                    EntradaOfertas json = System.Text.Json.JsonSerializer.Deserialize<EntradaOfertas>(entrada.json);
                                
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
                                                        
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                if (entrada.json_expanded != null)
                                {
                                    EntradaOfertas jsonExpandido = System.Text.Json.JsonSerializer.Deserialize<EntradaOfertas>(entrada.json_expanded);
                                }
                            }

                            
                        }
                    }
                    
                }
            }

            tbCuenta.IsEnabled = true;
        }
    }
}