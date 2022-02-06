using Entradas;
using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;

//https://blogs.windows.com/windowsdeveloper/2022/01/28/build-your-first-winui-3-app-part-1/
//ms-windows-store://publisher/?name=Microsoft Corporation

namespace Principal
{

    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //----------------------------------

            CargarObjetosVentana();
            Trial.Detectar();
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
            ObjetosVentana.nvItemSteamDeseados = nvItemSteamDeseados;
            ObjetosVentana.nvItemOpciones = nvItemOpciones;

            ObjetosVentana.gridEntradas = gridEntradas;
            ObjetosVentana.gridTrialMensaje = gridTrialMensaje;
            ObjetosVentana.botonTrialComprar = botonTrialComprar;
            ObjetosVentana.svEntradas = svEntradas;
            ObjetosVentana.spEntradas = spEntradas;

            ObjetosVentana.gridSteamDeseados = gridSteamDeseados;
            ObjetosVentana.svSteamDeseados = svSteamDeseados;
            ObjetosVentana.spSteamDeseados = spSteamDeseados;

            ObjetosVentana.gridOpciones = gridOpciones;
            ObjetosVentana.svOpciones = svOpciones;
            ObjetosVentana.spOpciones = spOpciones;

            ObjetosVentana.gridOfertasExpandida = gridOfertasExpandida;
            ObjetosVentana.svOfertasExpandida = svOfertasExpandida;
            ObjetosVentana.spOfertasExpandida = spOfertasExpandida;
            ObjetosVentana.cbOrdenarOfertasExpandida = cbOrdenarOfertasExpandida;
            ObjetosVentana.tbMensajeOfertasExpandida = tbMensajeOfertasExpandida;

            ObjetosVentana.gridAnuncio = gridAnuncio;
            ObjetosVentana.spAnuncio = spAnuncio;
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
            public static NavigationViewItem nvItemSteamDeseados { get; set; }
            public static NavigationViewItem nvItemOpciones { get; set; }
            public static Grid gridEntradas { get; set; }
            public static Grid gridTrialMensaje { get; set; }
            public static Button botonTrialComprar { get; set; }
            public static ScrollViewer svEntradas { get; set; }
            public static StackPanel spEntradas { get; set; }
            public static Grid gridSteamDeseados { get; set; }
            public static ScrollViewer svSteamDeseados { get; set; }
            public static StackPanel spSteamDeseados { get; set; }
            public static Grid gridOpciones { get; set; }
            public static ScrollViewer svOpciones { get; set; }
            public static StackPanel spOpciones { get; set; }
            public static Grid gridOfertasExpandida { get; set; }
            public static ScrollViewer svOfertasExpandida { get; set; }
            public static StackPanel spOfertasExpandida { get; set; }
            public static ComboBox cbOrdenarOfertasExpandida { get; set; }
            public static TextBlock tbMensajeOfertasExpandida { get; set; }
            public static Grid gridAnuncio { get; set; }
            public static StackPanel spAnuncio { get; set; }
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
                    else if (item.Name == "nvItemSteamDeseados")
                    {
                        SteamDeseados.Cargar();
                        Pestañas.Visibilidad(gridSteamDeseados, true);
                        BarraTitulo.CambiarTitulo(recursos.GetString("SteamWishlist"));
                        ScrollViewers.EnseñarSubir(svSteamDeseados);
                    }
                    else if (item.Name == "nvItemOpciones")
                    {
                        Opciones.Cargar();
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
                                Pestañas.Visibilidad(gridEntradas, true);
                                Wordpress.Filtrar(0);
                                BarraTitulo.CambiarTitulo(null);
                                ScrollViewers.EnseñarSubir(svEntradas);
                            }
                            else if (tb.Text == recursos.GetString("Deals"))
                            {
                                Pestañas.Visibilidad(gridEntradas, true);
                                Wordpress.Filtrar(3);
                                BarraTitulo.CambiarTitulo(tb.Text);
                                ScrollViewers.EnseñarSubir(svEntradas);
                            }
                            else if (tb.Text == recursos.GetString("Bundles"))
                            {
                                Pestañas.Visibilidad(gridEntradas, true);
                                Wordpress.Filtrar(4);
                                BarraTitulo.CambiarTitulo(tb.Text);
                                ScrollViewers.EnseñarSubir(svEntradas);
                            }
                            else if (tb.Text == recursos.GetString("Free"))
                            {
                                Pestañas.Visibilidad(gridEntradas, true);
                                Wordpress.Filtrar(12);
                                BarraTitulo.CambiarTitulo(tb.Text);
                                ScrollViewers.EnseñarSubir(svEntradas);
                            }
                            else if (tb.Text == recursos.GetString("Subscriptions"))
                            {
                                Pestañas.Visibilidad(gridEntradas, true);
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
