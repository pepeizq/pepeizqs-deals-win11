using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;

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
            Wordpress.Cargar();


        }

        public void CargarObjetosVentana()
        {
            ObjetosVentana.ventana = ventana;
            ObjetosVentana.gridTitulo = gridTitulo;
            ObjetosVentana.tbTitulo = tbTitulo;
            ObjetosVentana.gridCarga = gridCarga;
            ObjetosVentana.nvPrincipal = nvPrincipal;
            ObjetosVentana.nvItemActualizar = nvItemActualizar;
            ObjetosVentana.nvItemOpciones = nvItemOpciones;
            ObjetosVentana.gridEntradasTodo = gridEntradasTodo;
            ObjetosVentana.svEntradasTodo = svEntradasTodo;
            ObjetosVentana.spEntradasTodo = spEntradasTodo;
            ObjetosVentana.gridEntradasOfertas = gridEntradasOfertas;
            ObjetosVentana.gridEntradasBundles = gridEntradasBundles;
            ObjetosVentana.gridEntradasGratis = gridEntradasGratis;
            ObjetosVentana.gridEntradasSuscripciones = gridEntradasSuscripciones;
            ObjetosVentana.gridOpciones = gridOpciones;
           
            ObjetosVentana.lvEntradasOfertas = lvEntradasOfertas;
            ObjetosVentana.lvEntradasBundles = lvEntradasBundles;
            ObjetosVentana.lvEntradasGratis = lvEntradasGratis;
            ObjetosVentana.lvEntradasSuscripciones = lvEntradasSuscripciones;
        }

        public static class ObjetosVentana
        {
            public static Window ventana { get; set; }
            public static Grid gridTitulo { get; set; }
            public static TextBlock tbTitulo { get; set; }
            public static Grid gridCarga { get; set; }
            public static NavigationView nvPrincipal { get; set; }
            public static NavigationViewItem nvItemActualizar { get; set; }
            public static NavigationViewItem nvItemOpciones { get; set; }
            public static Grid gridEntradasTodo { get; set; }
            public static ScrollViewer svEntradasTodo { get; set; }
            public static StackPanel spEntradasTodo { get; set; }
            public static Grid gridEntradasOfertas { get; set; }
            public static Grid gridEntradasBundles { get; set; }
            public static Grid gridEntradasGratis { get; set; }
            public static Grid gridEntradasSuscripciones { get; set; }
            public static Grid gridOpciones { get; set; }

            public static ListView lvEntradasOfertas { get; set; }
            public static ListView lvEntradasBundles { get; set; }
            public static ListView lvEntradasGratis { get; set; }
            public static ListView lvEntradasSuscripciones { get; set; }
        }

        private void nvPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();

            Pestañas.CreadorItems(recursos.GetString("All"), null);
            Pestañas.CreadorItems(recursos.GetString("Deals"), null);
            Pestañas.CreadorItems(recursos.GetString("Bundles"), null);
            Pestañas.CreadorItems(recursos.GetString("Free"), null);
            Pestañas.CreadorItems(recursos.GetString("Subscriptions"), null);
        }

        private void nvPrincipal_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            ResourceLoader recursos = new ResourceLoader();

            if (args.InvokedItemContainer != null)
            {
                if (args.InvokedItemContainer.GetType() == typeof(NavigationViewItem))
                {
                    NavigationViewItem item = args.InvokedItemContainer as NavigationViewItem;

                    if (item.Name == "nvItemActualizar")
                    {
                        Wordpress.Cargar();
                        BarraTitulo.CambiarTitulo(null);
                    }
                    else if (item.Name == "nvItemOpciones")
                    {
                        Pestañas.Visibilidad(gridOpciones, true);
                        BarraTitulo.CambiarTitulo(recursos.GetString("Options"));
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
                                Pestañas.Visibilidad(gridEntradasTodo, true);
                                BarraTitulo.CambiarTitulo(null);
                            }
                            else if (tb.Text == recursos.GetString("Deals"))
                            {
                                Pestañas.Visibilidad(gridEntradasOfertas, true);
                                BarraTitulo.CambiarTitulo(recursos.GetString("Deals"));
                            }
                            else if (tb.Text == recursos.GetString("Bundles"))
                            {
                                Pestañas.Visibilidad(gridEntradasBundles, true);
                                BarraTitulo.CambiarTitulo(recursos.GetString("Bundles"));
                            }
                            else if (tb.Text == recursos.GetString("Free"))
                            {
                                Pestañas.Visibilidad(gridEntradasGratis, true);
                                BarraTitulo.CambiarTitulo(recursos.GetString("Free"));
                            }
                            else if (tb.Text == recursos.GetString("Subscriptions"))
                            {
                                Pestañas.Visibilidad(gridEntradasSuscripciones, true);
                                BarraTitulo.CambiarTitulo(recursos.GetString("Subscriptions"));
                            }
                        }
                    }                    
                }
            }
        }
    }
}
