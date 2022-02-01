using Entradas;
using Interfaz;
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
        public int[] categories;
        public string store_name;
        public string store_logo;
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

        var cliente = new WordPressClient("https://pepeizqdeals.com/wp-json/")
        {
            AuthMethod = WordPressPCL.Models.AuthMethod.JWT
        };

        var entradas = await cliente.CustomRequest.Get<IEnumerable<Entrada>>("wp/v2/posts?per_page=100&categories=3,4,12,13,1208");
       

        foreach (Entrada entrada in entradas)
        {          
            if (entrada.categories[0] == 3)
            {
                ObjetosVentana.spEntradasTodo.Children.Add(Ofertas.GenerarEntrada(entrada));
                ObjetosVentana.lvEntradasOfertas.Items.Add(GenerarEntrada(entrada));
            }
            else if (entrada.categories[0] == 4)
            {
                ObjetosVentana.lvEntradasBundles.Items.Add(GenerarEntrada(entrada));
            }
            else if (entrada.categories[0] == 12)
            {
                ObjetosVentana.lvEntradasGratis.Items.Add(GenerarEntrada(entrada));
            }
            else if (entrada.categories[0] == 13)
            {
                ObjetosVentana.lvEntradasSuscripciones.Items.Add(GenerarEntrada(entrada));
            }
        }

        ObjetosVentana.nvPrincipal.SelectedItem = ObjetosVentana.nvPrincipal.MenuItems[0];
        Pestañas.Visibilidad(ObjetosVentana.gridEntradasTodo, true);
    }

    public static ListViewItem GenerarEntrada(Entrada entrada)
    {
        ListViewItem lvItem = new ListViewItem();
        lvItem.Content = entrada.title.rendered;

        return lvItem;
    }
}