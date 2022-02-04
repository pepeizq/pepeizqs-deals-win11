using Entradas;
using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using Windows.UI;

//https://blogs.windows.com/windowsdeveloper/2022/01/28/build-your-first-winui-3-app-part-1/

namespace Principal
{

    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //----------------------------------

            CargarObjetosVentana();

            BarraTitulo.Generar(this);
            BarraTitulo.CambiarTitulo(null);
            Pestañas.Cargar();
            ScrollViewers.Cargar();
            Wordpress.Cargar();
            Ofertas.Cargar();

        }

        public void CargarObjetosVentana()
        {
            ObjetosVentana.ventana = ventana;
            ObjetosVentana.gridTitulo = gridTitulo;
            ObjetosVentana.tbTitulo = tbTitulo;
            ObjetosVentana.gridCarga = gridCarga;

            ObjetosVentana.nvPrincipal = nvPrincipal;
            ObjetosVentana.nvItemMenu = nvItemMenu;
            ObjetosVentana.nvItemVolver = nvItemVolver;
            ObjetosVentana.nvItemSubirArriba = nvItemSubirArriba;
            ObjetosVentana.nvItemActualizar = nvItemActualizar;
            ObjetosVentana.nvItemOpciones = nvItemOpciones;

            ObjetosVentana.gridEntradas = gridEntradas;
            ObjetosVentana.svEntradas = svEntradas;
            ObjetosVentana.spEntradas = spEntradas;

            ObjetosVentana.gridOpciones = gridOpciones;
            ObjetosVentana.svOpciones = svOpciones;
            ObjetosVentana.spOpciones = spOpciones;

            ObjetosVentana.gridOfertasExpandida = gridOfertasExpandida;
            ObjetosVentana.svOfertasExpandida = svOfertasExpandida;
            ObjetosVentana.spOfertasExpandida = spOfertasExpandida;
            ObjetosVentana.cbOrdenarOfertasExpandida = cbOrdenarOfertasExpandida;
            ObjetosVentana.tbMensajeOfertasExpandida = tbMensajeOfertasExpandida;
        }

        public static class ObjetosVentana
        {
            public static Window ventana { get; set; }
            public static Grid gridTitulo { get; set; }
            public static TextBlock tbTitulo { get; set; }
            public static Grid gridCarga { get; set; }
            public static NavigationView nvPrincipal { get; set; }
            public static NavigationViewItem nvItemMenu { get; set; }
            public static NavigationViewItem nvItemVolver { get; set; }
            public static NavigationViewItem nvItemSubirArriba { get; set; }
            public static NavigationViewItem nvItemActualizar { get; set; }
            public static NavigationViewItem nvItemOpciones { get; set; }
            public static Grid gridEntradas { get; set; }
            public static ScrollViewer svEntradas { get; set; }
            public static StackPanel spEntradas { get; set; }
            public static Grid gridOpciones { get; set; }
            public static ScrollViewer svOpciones { get; set; }
            public static StackPanel spOpciones { get; set; }
            public static Grid gridOfertasExpandida { get; set; }
            public static ScrollViewer svOfertasExpandida { get; set; }
            public static StackPanel spOfertasExpandida { get; set; }
            public static ComboBox cbOrdenarOfertasExpandida { get; set; }
            public static TextBlock tbMensajeOfertasExpandida { get; set; }
        }

        private void nvPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();
           
            Pestañas.CreadorItems(recursos.GetString("Subscriptions"), null);
            Pestañas.CreadorItems(recursos.GetString("Free"), null);
            Pestañas.CreadorItems(recursos.GetString("Bundles"), null);
            Pestañas.CreadorItems(recursos.GetString("Deals"), null);
            Pestañas.CreadorItems(recursos.GetString("All"), null);
        }

        private void nvPrincipal_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            ResourceLoader recursos = new ResourceLoader();

            if (args.InvokedItemContainer != null)
            {
                if (args.InvokedItemContainer.GetType() == typeof(NavigationViewItem))
                {
                    NavigationViewItem item = args.InvokedItemContainer as NavigationViewItem;

                    if (item.Name == "nvItemMenu")
                    {
                        //colorNvSeñalar.Color = Colors.Transparent;                       
                    }
                    else if (item.Name == "nvItemActualizar")
                    {
                        BarraTitulo.CambiarTitulo(null);                        
                        Wordpress.Cargar();
                    }
                    else if (item.Name == "nvItemOpciones")
                    {
                        Pestañas.Visibilidad(gridOpciones, true);
                        BarraTitulo.CambiarTitulo(recursos.GetString("Options"));
                        ScrollViewers.EnseñarSubir(svOpciones);
                    }
                }
            }
                
            if (args.InvokedItem != null)
            {                           
                if (args.InvokedItem.GetType() == typeof(Grid))
                {
                    Grid grid = (Grid)args.InvokedItem;

                    if (grid.Children[0] != null)
                    {
                        if (grid.Children[0].GetType() == typeof(TextBlock))
                        {
                            TextBlock tb = grid.Children[0] as TextBlock;

                            if (tb.Text == recursos.GetString("All"))
                            {
                                Wordpress.Filtrar(0);
                                BarraTitulo.CambiarTitulo(null);
                                ScrollViewers.EnseñarSubir(svEntradas);
                            }
                            else if (tb.Text == recursos.GetString("Deals"))
                            {
                                Wordpress.Filtrar(3);
                                BarraTitulo.CambiarTitulo(tb.Text);
                                ScrollViewers.EnseñarSubir(svEntradas);
                            }
                            else if (tb.Text == recursos.GetString("Bundles"))
                            {
                                Wordpress.Filtrar(4);
                                BarraTitulo.CambiarTitulo(tb.Text);
                                ScrollViewers.EnseñarSubir(svEntradas);
                            }
                            else if (tb.Text == recursos.GetString("Free"))
                            {
                                Wordpress.Filtrar(12);
                                BarraTitulo.CambiarTitulo(tb.Text);
                                ScrollViewers.EnseñarSubir(svEntradas);
                            }
                            else if (tb.Text == recursos.GetString("Subscriptions"))
                            {
                                Wordpress.Filtrar(13);
                                BarraTitulo.CambiarTitulo(tb.Text);
                                ScrollViewers.EnseñarSubir(svEntradas);
                            }
                        }
                    }                    
                }
            }
        } 
    }
}
