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

            Wordpress.Inicio();
        }

        public void CargarObjetosVentana()
        {
            ObjetosVentana.ventana = ventana;
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
                if (args.InvokedItem.GetType() == typeof(TextBlock))
                {
                    TextBlock tb = args.InvokedItem as TextBlock;

                    if (tb.Text == recursos.GetString("All"))
                    {
                        Pestañas.Visibilidad(gridEntradasTodo);
                    }
                    else if(tb.Text == recursos.GetString("Deals"))
                    {
                        Pestañas.Visibilidad(gridEntradasOfertas);
                    }
                }

            }
        }
    }
}
