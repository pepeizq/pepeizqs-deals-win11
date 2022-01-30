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
    }

    public class EntradaTitulo
    {
        public string rendered;
    }

    public static async void Inicio()
    {
        Pestañas.Visibilidad(ObjetosVentana.gridCarga);

        var cliente = new WordPressClient("https://pepeizqdeals.com/wp-json/")
        {
            AuthMethod = WordPressPCL.Models.AuthMethod.JWT
        };

        var entradas = await cliente.CustomRequest.Get<IEnumerable<Entrada>>("wp/v2/posts");
       

        foreach (Entrada entrada in entradas)
        {
            ObjetosVentana.lvEntradasTodo.Items.Add(GenerarEntrada(entrada));

            if (entrada.categories[0] == 3)
            {
                ObjetosVentana.lvEntradasOfertas.Items.Add(GenerarEntrada(entrada));
            }
        }

        Pestañas.Visibilidad(ObjetosVentana.gridEntradasTodo);
    }

    public static ListViewItem GenerarEntrada(Entrada entrada)
    {
        ListViewItem lvItem = new ListViewItem();
        lvItem.Content = entrada.title.rendered;

        return lvItem;
    }
}