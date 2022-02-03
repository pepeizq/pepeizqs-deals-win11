using Entradas;
using Interfaz;
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

        WordPressClient cliente = new WordPressClient("https://pepeizqdeals.com/wp-json/")
        {
            AuthMethod = WordPressPCL.Models.AuthMethod.JWT
        };

        IEnumerable<Entrada> entradas = await cliente.CustomRequest.Get<IEnumerable<Entrada>>("wp/v2/posts?per_page=100&categories=3,4,12,13,1208");

        ObjetosVentana.spEntradasTodo.Children.Clear();
        ObjetosVentana.spEntradasOfertas.Children.Clear();
        ObjetosVentana.spEntradasBundles.Children.Clear();
        ObjetosVentana.spEntradasGratis.Children.Clear();
        ObjetosVentana.spEntradasSuscripciones.Children.Clear();

        foreach (Entrada entrada in entradas)
        {          
            if (entrada.categories[0] == 3)
            {
                ObjetosVentana.spEntradasTodo.Children.Add(Ofertas.GenerarEntrada(entrada));
                ObjetosVentana.spEntradasOfertas.Children.Add(Ofertas.GenerarEntrada(entrada));
            }
            else if (entrada.categories[0] == 4)
            {
                //ObjetosVentana.lvEntradasBundles.Items.Add(GenerarEntrada(entrada));
            }
            else if (entrada.categories[0] == 12)
            {
                //ObjetosVentana.lvEntradasGratis.Items.Add(GenerarEntrada(entrada));
            }
            else if (entrada.categories[0] == 13)
            {
                //ObjetosVentana.lvEntradasSuscripciones.Items.Add(GenerarEntrada(entrada));
            }
        }

        ObjetosVentana.nvPrincipal.SelectedItem = ObjetosVentana.nvPrincipal.MenuItems[1];
        Pestañas.Visibilidad(ObjetosVentana.gridEntradasTodo, true);
    }
}