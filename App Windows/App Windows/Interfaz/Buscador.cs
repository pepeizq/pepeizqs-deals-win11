using Entradas;
using Herramientas;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;
using static Principal.MainWindow;
using static Wordpress;

namespace Interfaz
{
    public static class Buscador
    {
        public static void Busca(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.Trim().Length > 4)
            {
                ObjetosVentana.spBuscadorNoResultados.Visibility = Visibility.Collapsed;
                ObjetosVentana.tbBuscadorResultado.Text = tb.Text.Trim();

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

                BarraTitulo.CambiarTitulo(tb.Text.Trim());
                Pestañas.Visibilidad(ObjetosVentana.gridBuscador, true);
                ScrollViewers.EnseñarSubir(ObjetosVentana.svBuscador);

                ObjetosVentana.spBuscador.Children.Clear();

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
                                        if (Limpieza.Limpiar(juego.titulo).Contains(Limpieza.Limpiar(tb.Text.Trim())))
                                        {
                                            SteamDeseados.GeneralXamlOferta(entrada, juego, mensaje, ObjetosVentana.spBuscador);
                                        }
                                    }
                                }
                            }
                        }
                        else if (entrada.categories[0] == 4 || entrada.categories[0] == 12 || entrada.categories[0] == 13)
                        {
                            bool añadir = false;

                            if (Limpieza.Limpiar(entrada.title.rendered).Contains(Limpieza.Limpiar(tb.Text.Trim())))
                            {
                                añadir = true;
                            }
                            else if (Limpieza.Limpiar(entrada.title2).Contains(Limpieza.Limpiar(tb.Text.Trim())))
                            {
                                añadir = true;
                            }

                            if (añadir == true)
                            {
                                if (entrada.categories[0] == 4)
                                {
                                    ObjetosVentana.spBuscador.Children.Add(Bundles.GenerarEntrada(entrada));
                                }
                                else if (entrada.categories[0] == 12)
                                {
                                    ObjetosVentana.spBuscador.Children.Add(Gratis.GenerarEntrada(entrada));
                                }
                                else if (entrada.categories[0] == 13)
                                {
                                    ObjetosVentana.spBuscador.Children.Add(Suscripciones.GenerarEntrada(entrada));
                                }
                            }
                        }
                    }
                }
            }

            if (ObjetosVentana.spBuscador.Children.Count > 0)
            {
                Grid grid = ObjetosVentana.spBuscador.Children[ObjetosVentana.spBuscador.Children.Count - 1] as Grid;
                grid.Margin = new Thickness(0);
            }
            else if (ObjetosVentana.spBuscador.Children.Count == 0)
            {
                ObjetosVentana.spBuscadorNoResultados.Visibility = Visibility.Visible;
            }

            tb.Focus(FocusState.Programmatic);
        }
    }
}
