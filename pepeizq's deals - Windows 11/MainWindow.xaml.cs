using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.Windows.ApplicationModel.Resources;
using Windows.UI.Core;

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
            Wordpress.Cargar();

        }

        public void CargarObjetosVentana()
        {
            ObjetosVentana.ventana = ventana;
            ObjetosVentana.gridTitulo = gridTitulo;
            ObjetosVentana.tbTitulo = tbTitulo;
            ObjetosVentana.nvPrincipal = nvPrincipal;
            ObjetosVentana.gridCarga = gridCarga;
            ObjetosVentana.gridEntradasTodo = gridEntradasTodo;
            ObjetosVentana.gridEntradasOfertas = gridEntradasOfertas;
            ObjetosVentana.gridEntradasBundles = gridEntradasBundles;
            ObjetosVentana.gridEntradasGratis = gridEntradasGratis;
            ObjetosVentana.gridEntradasSuscripciones = gridEntradasSuscripciones;
            ObjetosVentana.lvEntradasTodo = lvEntradasTodo;
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
            public static NavigationView nvPrincipal { get; set; }
            public static Grid gridCarga { get; set; }
            public static Grid gridEntradasTodo { get; set; }
            public static Grid gridEntradasOfertas { get; set; }
            public static Grid gridEntradasBundles { get; set; }
            public static Grid gridEntradasGratis { get; set; }
            public static Grid gridEntradasSuscripciones { get; set; }
            public static ListView lvEntradasTodo { get; set; }
            public static ListView lvEntradasOfertas { get; set; }
            public static ListView lvEntradasBundles { get; set; }
            public static ListView lvEntradasGratis { get; set; }
            public static ListView lvEntradasSuscripciones { get; set; }
        }

        private void nvPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            ResourceLoader recursos = new ResourceLoader();

            Pestañas.CreadorItems(recursos.GetString("All"));
            Pestañas.CreadorItems(recursos.GetString("Deals"));
            Pestañas.CreadorItems(recursos.GetString("Bundles"));
            Pestañas.CreadorItems(recursos.GetString("Free"));
            Pestañas.CreadorItems(recursos.GetString("Subscriptions"));
        }

        private void nvPrincipal_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            ResourceLoader recursos = new ResourceLoader();

            if (args.IsSettingsInvoked == true)
            {
                //gridOpciones
            }
            else
            {
                if (args.InvokedItem != null)
                {
                    if (args.InvokedItem.GetType() == typeof(TextBlock))
                    {
                        TextBlock tb = args.InvokedItem as TextBlock;

                        if (tb.Text == recursos.GetString("All"))
                        {
                            Pestañas.Visibilidad(gridEntradasTodo);
                            BarraTitulo.CambiarTitulo(null);
                        }
                        else if (tb.Text == recursos.GetString("Deals"))
                        {
                            Pestañas.Visibilidad(gridEntradasOfertas);
                            BarraTitulo.CambiarTitulo(recursos.GetString("Deals"));
                        }
                        else if (tb.Text == recursos.GetString("Bundles"))
                        {
                            Pestañas.Visibilidad(gridEntradasBundles);
                            BarraTitulo.CambiarTitulo(recursos.GetString("Bundles"));
                        }
                        else if (tb.Text == recursos.GetString("Free"))
                        {
                            Pestañas.Visibilidad(gridEntradasGratis);
                            BarraTitulo.CambiarTitulo(recursos.GetString("Free"));
                        }
                        else if (tb.Text == recursos.GetString("Subscriptions"))
                        {
                            Pestañas.Visibilidad(gridEntradasSuscripciones);
                            BarraTitulo.CambiarTitulo(recursos.GetString("Subscriptions"));
                        }
                    }
                }               
            }
        }

    }
}
