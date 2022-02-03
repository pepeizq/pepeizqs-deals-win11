using Interfaz;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
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

            ObjetosVentana.gridEntradasTodo = gridEntradasTodo;
            ObjetosVentana.svEntradasTodo = svEntradasTodo;
            ObjetosVentana.spEntradasTodo = spEntradasTodo;

            ObjetosVentana.gridEntradasOfertas = gridEntradasOfertas;
            ObjetosVentana.svEntradasOfertas = svEntradasOfertas;
            ObjetosVentana.spEntradasOfertas = spEntradasOfertas;

            ObjetosVentana.gridEntradasBundles = gridEntradasBundles;
            ObjetosVentana.svEntradasBundles = svEntradasBundles;
            ObjetosVentana.spEntradasBundles = spEntradasBundles;

            ObjetosVentana.gridEntradasGratis = gridEntradasGratis;
            ObjetosVentana.svEntradasGratis = svEntradasGratis;
            ObjetosVentana.spEntradasGratis = spEntradasGratis;

            ObjetosVentana.gridEntradasSuscripciones = gridEntradasSuscripciones;
            ObjetosVentana.svEntradasSuscripciones = svEntradasSuscripciones;
            ObjetosVentana.spEntradasSuscripciones = spEntradasSuscripciones;

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
            public static Grid gridEntradasTodo { get; set; }
            public static ScrollViewer svEntradasTodo { get; set; }
            public static StackPanel spEntradasTodo { get; set; }
            public static Grid gridEntradasOfertas { get; set; }
            public static ScrollViewer svEntradasOfertas { get; set; }
            public static StackPanel spEntradasOfertas { get; set; }
            public static Grid gridEntradasBundles { get; set; }
            public static ScrollViewer svEntradasBundles { get; set; }
            public static StackPanel spEntradasBundles { get; set; }
            public static Grid gridEntradasGratis { get; set; }
            public static ScrollViewer svEntradasGratis { get; set; }
            public static StackPanel spEntradasGratis { get; set; }
            public static Grid gridEntradasSuscripciones { get; set; }
            public static ScrollViewer svEntradasSuscripciones { get; set; }
            public static StackPanel spEntradasSuscripciones { get; set; }
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
            SolidColorBrush colorNvSeñalar = nvPrincipal.Resources["NavigationViewSelectionIndicatorForeground"] as SolidColorBrush;

            ResourceLoader recursos = new ResourceLoader();

            if (args.InvokedItemContainer != null)
            {
                if (args.InvokedItemContainer.GetType() == typeof(NavigationViewItem))
                {
                    NavigationViewItem item = args.InvokedItemContainer as NavigationViewItem;

                    if (item.Name == "nvItemMenu")
                    {
                        colorNvSeñalar.Color = Colors.Transparent;                       
                    }
                    else if (item.Name == "nvItemActualizar")
                    {
                        colorNvSeñalar.Color = Colors.Transparent;
                        BarraTitulo.CambiarTitulo(null);                        
                        Wordpress.Cargar();
                    }
                    else if (item.Name == "nvItemOpciones")
                    {
                        colorNvSeñalar.Color = (Color)Application.Current.Resources["ColorFuente"];
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
                                colorNvSeñalar.Color = (Color)Application.Current.Resources["ColorFuente"];
                                Pestañas.Visibilidad(gridEntradasTodo, true);
                                BarraTitulo.CambiarTitulo(null);
                                ScrollViewers.EnseñarSubir(svEntradasTodo);
                            }
                            else if (tb.Text == recursos.GetString("Deals"))
                            {
                                colorNvSeñalar.Color = (Color)Application.Current.Resources["ColorFuente"];
                                Pestañas.Visibilidad(gridEntradasOfertas, true);
                                BarraTitulo.CambiarTitulo(tb.Text);
                                ScrollViewers.EnseñarSubir(svEntradasOfertas);
                            }
                            else if (tb.Text == recursos.GetString("Bundles"))
                            {
                                colorNvSeñalar.Color = (Color)Application.Current.Resources["ColorFuente"];
                                Pestañas.Visibilidad(gridEntradasBundles, true);
                                BarraTitulo.CambiarTitulo(tb.Text);
                                ScrollViewers.EnseñarSubir(svEntradasBundles);
                            }
                            else if (tb.Text == recursos.GetString("Free"))
                            {
                                colorNvSeñalar.Color = (Color)Application.Current.Resources["ColorFuente"];
                                Pestañas.Visibilidad(gridEntradasGratis, true);
                                BarraTitulo.CambiarTitulo(tb.Text);
                                ScrollViewers.EnseñarSubir(svEntradasGratis);
                            }
                            else if (tb.Text == recursos.GetString("Subscriptions"))
                            {
                                colorNvSeñalar.Color = (Color)Application.Current.Resources["ColorFuente"];
                                Pestañas.Visibilidad(gridEntradasSuscripciones, true);
                                BarraTitulo.CambiarTitulo(tb.Text);
                                ScrollViewers.EnseñarSubir(svEntradasSuscripciones);
                            }
                        }
                    }                    
                }
            }
        } 
    }
}
