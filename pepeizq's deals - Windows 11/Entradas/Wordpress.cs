using Entradas;
using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
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

        WordPressClient cliente = new WordPressClient("https://pepeizqdeals.com/wp-json/")
        {
            AuthMethod = WordPressPCL.Models.AuthMethod.JWT
        };

        IEnumerable<Entrada> entradas = await cliente.CustomRequest.Get<IEnumerable<Entrada>>("wp/v2/posts?per_page=100&categories=3,4,12,13,1208");

        ObjetosVentana.spEntradas.Children.Clear();

        foreach (Entrada entrada in entradas)
        {
            if (entrada.categories[0] == 3)
            {
                ObjetosVentana.spEntradas.Children.Add(Ofertas.GenerarEntrada(entrada));
            }
            else if (entrada.categories[0] == 4)
            {
                ObjetosVentana.spEntradas.Children.Add(Bundles.GenerarEntrada(entrada));;
            }
            else if (entrada.categories[0] == 12)
            {

            }
            else if (entrada.categories[0] == 13)
            {

            }
        }

        ObjetosVentana.nvPrincipal.Tag = 0;
        ObjetosVentana.nvPrincipal.SelectedItem = ObjetosVentana.nvPrincipal.MenuItems[1];
        Pestañas.Visibilidad(ObjetosVentana.gridEntradas, true);
    }

    public static void Filtrar(int categoria)
    {
        ObjetosVentana.nvPrincipal.Tag = categoria;

        foreach (Grid grid in ObjetosVentana.spEntradas.Children)
        {
            Entrada entrada = grid.Tag as Entrada;

            if (categoria != 0)
            {
                if (categoria == entrada.categories[0])
                {
                    grid.Visibility = Visibility.Visible;
                }
                else
                {
                    grid.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                grid.Visibility = Visibility.Visible;
            }
        }
    }
}