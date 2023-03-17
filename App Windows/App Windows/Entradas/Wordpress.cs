using Entradas;
using Herramientas;
using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using WordPressPCL;
using static Principal.MainWindow;

public static class Wordpress
{
    public class Entrada
    {
        public int id;
        public EntradaTitulo title;
        public string title2;
        public int[] categories;
        public string store_name;
        public string store_logo;
        public string redirect;
        public string fifu_image_url;
        public string json;
        public string json_expanded;
    }

    public class EntradaTitulo
    {
        public string rendered;
    }

    public static async void Cargar()
    {
        Pestañas.Visibilidad(ObjetosVentana.gridCarga, false);
        ObjetosVentana.spEntradas.Children.Clear();

        IEnumerable<Entrada> entradas = null;

        await Task.Run(async () =>
        {
            WordPressClient cliente = new WordPressClient("https://pepeizqdeals.com/wp-json/")
            {
                AuthMethod = WordPressPCL.Models.AuthMethod.JWT
            };

            entradas = await cliente.CustomRequest.Get<IEnumerable<Entrada>>("wp/v2/posts?per_page=100&categories=3,4,12,13,1208");
        });

        ApplicationDataContainer datos = ApplicationData.Current.LocalSettings;

        if (entradas != null)
        {
            foreach (Entrada entrada in entradas)
            {
                await Task.Delay(100);

                if (entrada.categories[0] == 3)
                {
                    ObjetosVentana.spEntradas.Children.Add(Ofertas.GenerarEntrada(entrada));
                }
                else if (entrada.categories[0] == 4)
                {
                    ObjetosVentana.spEntradas.Children.Add(Bundles.GenerarEntrada(entrada));
                }
                else if (entrada.categories[0] == 12)
                {
                    ObjetosVentana.spEntradas.Children.Add(Gratis.GenerarEntrada(entrada));
                }
                else if (entrada.categories[0] == 13)
                {
                    ObjetosVentana.spEntradas.Children.Add(Suscripciones.GenerarEntrada(entrada));
                }
                else if (entrada.categories[0] == 1208)
                {
                    if (datos.Values["OpcionesNotificaciones"] == null)
                    {
                        Anuncio.CargarEntrada(entrada);
                    }
                    else if (datos.Values["OpcionesNotificaciones"] is true)
                    {
                        Anuncio.CargarEntrada(entrada);
                    }
                }
            }
        }

        if (ObjetosVentana.spAnuncio.Children.Count > 0)
        {
            ObjetosVentana.gridAnuncio.Visibility = Visibility.Visible;
        }

        Filtrar(0);
        ObjetosVentana.nvPrincipal.Tag = 0;
        ObjetosVentana.nvPrincipal.SelectedItem = ObjetosVentana.nvPrincipal.MenuItems[1];
        Pestañas.Visibilidad(ObjetosVentana.gridEntradas, true);

        if (ObjetosVentana.spMensajes.Visibility == Visibility.Visible)
        {
            ObjetosVentana.spCarga.Visibility = Visibility.Collapsed;
        }
        else
        {
            ObjetosVentana.gridCarga.Visibility = Visibility.Collapsed;
        }
    }

    public static void Filtrar(int categoria)
    {
        ObjetosVentana.nvPrincipal.Tag = categoria;

        int todo = 0;
        int ofertas = 0;
        int bundles = 0;
        int gratis = 0;
        int suscripciones = 0;

        int j = 0;
        foreach (Grid grid in ObjetosVentana.spEntradas.Children)
        {
            grid.Margin = new Thickness(0, 0, 0, 40);
            Entrada entrada = grid.Tag as Entrada;

            if (categoria != 0)
            {
                if (categoria == entrada.categories[0])
                {
                    grid.Visibility = Visibility.Visible;

                    if (categoria == 3)
                    {
                        ofertas = j;
                    }
                    else if (categoria == 4)
                    {
                        bundles = j;
                    }
                    else if (categoria == 12)
                    {
                        gratis = j;
                    }
                    else if (categoria == 13)
                    {
                        suscripciones = j;
                    }
                }
                else
                {
                    grid.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                grid.Visibility = Visibility.Visible;
                todo = j;
            }

            j += 1;
        }

        if (ObjetosVentana.spEntradas.Children.Count > 1)
        {
            if (todo > 0)
            {
                Grid grid = ObjetosVentana.spEntradas.Children[todo] as Grid;
                grid.Margin = new Thickness(0);
            }
            else if (ofertas > 0)
            {
                Grid grid = ObjetosVentana.spEntradas.Children[ofertas] as Grid;
                grid.Margin = new Thickness(0);
            }
            else if (bundles > 0)
            {
                Grid grid = ObjetosVentana.spEntradas.Children[bundles] as Grid;
                grid.Margin = new Thickness(0);
            }
            else if (gratis > 0)
            {
                Grid grid = ObjetosVentana.spEntradas.Children[gratis] as Grid;
                grid.Margin = new Thickness(0);
            }
            else if (suscripciones > 0)
            {
                Grid grid = ObjetosVentana.spEntradas.Children[suscripciones] as Grid;
                grid.Margin = new Thickness(0);
            }
        }
    }
}