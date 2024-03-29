﻿using CommunityToolkit.WinUI.UI.Controls;
using Entradas;
using FontAwesome6.Fonts;
using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;
using Otros;
using System.Threading.Tasks;

namespace Principal
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            //----------------------------------

            CargarObjetosVentana();
            Task.Delay(500);
            Wordpress.Cargar();
            Trial.Cargar();
            BarraTitulo.Generar(this);
            BarraTitulo.CambiarTitulo(null);
            Cerrar.Cargar();
            Interfaz.Menu.Cargar();
            Pestañas.Cargar();
            ScrollViewers.Cargar();           
            Ofertas.Cargar();
            Buscador.Cargar();
            Opciones.CargarDatos();
        }

        public void CargarObjetosVentana()
        {
            ObjetosVentana.ventana = ventana;
            ObjetosVentana.gridTitulo = gridTitulo;
            ObjetosVentana.tbTitulo = tbTitulo;

            //-------------------------------------------------------------------

            ObjetosVentana.nvPrincipal = nvPrincipal;
            ObjetosVentana.nvItemMenu = nvItemMenu;
            ObjetosVentana.menuItemMenu = menuItemMenu;
            ObjetosVentana.nvItemVolver = nvItemVolver;
            ObjetosVentana.nvItemSubirArriba = nvItemSubirArriba;
            ObjetosVentana.nvItemSteamDeseados = nvItemSteamDeseados;
            ObjetosVentana.nvItemOpciones = nvItemOpciones;
            ObjetosVentana.gridCierre = gridCierre;

            //-------------------------------------------------------------------

            ObjetosVentana.botonCerrarAppSi = botonCerrarAppSi;
            ObjetosVentana.botonCerrarAppNo = botonCerrarAppNo;
            ObjetosVentana.iconoCerrarAppAzar = iconoCerrarAppAzar;
            ObjetosVentana.tbCerrarAppAzar = tbCerrarAppAzar;
            ObjetosVentana.botonCerrarAppAzar = botonCerrarAppAzar;

            //-------------------------------------------------------------------

            ObjetosVentana.gridEntradas = gridEntradas;
            ObjetosVentana.gridBuscador = gridBuscador;
            ObjetosVentana.gridSteamDeseados = gridSteamDeseados;
            ObjetosVentana.gridOpciones = gridOpciones;
            ObjetosVentana.gridOfertasExpandida = gridOfertasExpandida;
            ObjetosVentana.gridCarga = gridCarga;

            //-------------------------------------------------------------------

            ObjetosVentana.gridTrialMensaje = gridTrialMensaje;
            ObjetosVentana.botonTrialComprar = botonTrialComprar;
            ObjetosVentana.svEntradas = svEntradas;
            ObjetosVentana.spEntradas = spEntradas;

            //-------------------------------------------------------------------

            ObjetosVentana.tbBuscador = tbBuscador;
            ObjetosVentana.tbBuscadorResultado = tbBuscadorResultado;
            ObjetosVentana.svBuscador = svBuscador;
            ObjetosVentana.spBuscador = spBuscador;
            ObjetosVentana.spBuscadorNoResultados = spBuscadorNoResultados;
            ObjetosVentana.botonBuscadorSteamDB = botonBuscadorSteamDB;
            ObjetosVentana.botonBuscadorGGdeals = botonBuscadorGGdeals;
            ObjetosVentana.botonBuscadorIsthereanydeal = botonBuscadorIsthereanydeal;

            //-------------------------------------------------------------------

            ObjetosVentana.svSteamDeseados = svSteamDeseados;
            ObjetosVentana.spSteamDeseados = spSteamDeseados;
            ObjetosVentana.gridTrialMensajeSteamDeseados = gridTrialMensajeSteamDeseados;
            ObjetosVentana.botonTrialComprarSteamDeseados = botonTrialComprarSteamDeseados;
            ObjetosVentana.expanderSteamDeseados = expanderSteamDeseados;
            ObjetosVentana.prSteamDeseados = prSteamDeseados;
            ObjetosVentana.imagenSteamDeseadosAvatar = imagenSteamDeseadosAvatar;
            ObjetosVentana.tbSteamDeseadosUsuario = tbSteamDeseadosUsuario;
            ObjetosVentana.tbSteamDeseadosEnlaceCuenta = tbSteamDeseadosEnlaceCuenta;

            //-------------------------------------------------------------------

            ObjetosVentana.spOpcionesBotones = spOpcionesBotones;
            ObjetosVentana.svOpciones = svOpciones;
            ObjetosVentana.spOpcionesPestañas = spOpcionesPestanas;
            ObjetosVentana.cbOpcionesIdioma = cbOpcionesIdioma;
            ObjetosVentana.toggleOpcionesNotificaciones = toggleOpcionesNotificaciones;
            ObjetosVentana.botonOpcionesActualizar = botonOpcionesActualizar;
            ObjetosVentana.cbOpcionesPantalla = cbOpcionesPantalla;
            ObjetosVentana.botonOpcionesLimpiar = botonOpcionesLimpiar;
            ObjetosVentana.toggleOpcionesArranque = toggleOpcionesArranque;

            //-------------------------------------------------------------------

            ObjetosVentana.svOfertasExpandida = svOfertasExpandida;
            ObjetosVentana.spOfertasExpandida = spOfertasExpandida;
            ObjetosVentana.cbOrdenarOfertasExpandida = cbOrdenarOfertasExpandida;
            ObjetosVentana.tbMensajeOfertasExpandida = tbMensajeOfertasExpandida;

            //-------------------------------------------------------------------

            ObjetosVentana.spCarga = spCarga;
            ObjetosVentana.spMensajes = spMensajes;

            //-------------------------------------------------------------------

            ObjetosVentana.gridAnuncio = gridAnuncio;
            ObjetosVentana.spAnuncio = spAnuncio;      
        }

        public static class ObjetosVentana
        {
            public static Window ventana { get; set; }
            public static Grid gridTitulo { get; set; }
            public static TextBlock tbTitulo { get; set; }
            public static NavigationView nvPrincipal { get; set; }
            public static NavigationViewItem nvItemMenu { get; set; }
            public static MenuFlyout menuItemMenu { get; set; }
            public static NavigationViewItem nvItemVolver { get; set; }
            public static NavigationViewItem nvItemSubirArriba { get; set; }
            public static NavigationViewItem nvItemSteamDeseados { get; set; }
            public static NavigationViewItem nvItemOpciones { get; set; }
            public static Grid gridCierre { get; set; }

            //-------------------------------------------------------------------

            public static Button botonCerrarAppSi { get; set; }
            public static Button botonCerrarAppNo { get; set; }
            public static FontAwesome iconoCerrarAppAzar { get; set; }
            public static TextBlock tbCerrarAppAzar { get; set; }
            public static Button botonCerrarAppAzar { get; set; }

            //-------------------------------------------------------------------

            public static Grid gridEntradas { get; set; }
            public static Grid gridBuscador { get; set; }
            public static Grid gridSteamDeseados { get; set; }
            public static Grid gridOpciones { get; set; }
            public static Grid gridOfertasExpandida { get; set; }
            public static Grid gridCarga { get; set; }
            public static Grid gridAnuncio { get; set; }

            //-------------------------------------------------------------------

            public static Grid gridTrialMensaje { get; set; }
            public static Button botonTrialComprar { get; set; }
            public static ScrollViewer svEntradas { get; set; }
            public static StackPanel spEntradas { get; set; }

            //-------------------------------------------------------------------
          
            public static TextBox tbBuscador { get; set; }
            public static TextBlock tbBuscadorResultado { get; set; }
            public static ScrollViewer svBuscador { get; set; }
            public static StackPanel spBuscador { get; set; }
            public static StackPanel spBuscadorNoResultados { get; set; }
            public static Button botonBuscadorSteamDB { get; set; }
            public static Button botonBuscadorGGdeals { get; set; }
            public static Button botonBuscadorIsthereanydeal { get; set; }

            //-------------------------------------------------------------------
                  
            public static ScrollViewer svSteamDeseados { get; set; }
            public static StackPanel spSteamDeseados { get; set; }
            public static Grid gridTrialMensajeSteamDeseados { get; set; }
            public static Button botonTrialComprarSteamDeseados { get; set; }
            public static Microsoft.UI.Xaml.Controls.Expander expanderSteamDeseados { get; set; }
            public static ProgressRing prSteamDeseados { get; set; }
            public static Image imagenSteamDeseadosAvatar { get; set; }
            public static TextBlock tbSteamDeseadosUsuario { get; set; }
            public static TextBox tbSteamDeseadosEnlaceCuenta { get; set; }

            //-------------------------------------------------------------------

            public static StackPanel spOpcionesBotones { get; set; }
            public static ScrollViewer svOpciones { get; set; }
            public static StackPanel spOpcionesPestañas { get; set; }
            public static ComboBox cbOpcionesIdioma { get; set; }
            public static ToggleSwitch toggleOpcionesNotificaciones { get; set; }
            public static ComboBox cbOpcionesPantalla { get; set; }
            public static Button botonOpcionesActualizar { get; set; }
            public static Button botonOpcionesLimpiar { get; set; }
            public static ToggleSwitch toggleOpcionesArranque { get; set; }

            //-------------------------------------------------------------------

            public static ScrollViewer svOfertasExpandida { get; set; }
            public static StackPanel spOfertasExpandida { get; set; }
            public static ComboBox cbOrdenarOfertasExpandida { get; set; }
            public static TextBlock tbMensajeOfertasExpandida { get; set; }

            //-------------------------------------------------------------------

            public static StackPanel spCarga { get; set; }
            public static StackPanel spMensajes { get; set; }

            //-------------------------------------------------------------------

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
                if (args.InvokedItemContainer.GetType() == typeof(NavigationViewItem2))
                {
                    NavigationViewItem2 item = args.InvokedItemContainer as NavigationViewItem2;

                    if (item.Name == "nvItemMenu")
                    {
                                       
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
                        Opciones.CargarPestaña();
                        Pestañas.Visibilidad(gridOpciones, true);
                        BarraTitulo.CambiarTitulo(recursos.GetString("Options"));
                        ScrollViewers.EnseñarSubir(svOpciones);
                    }
                }
            }
                
            if (args.InvokedItem != null)
            {                           
                if (args.InvokedItem.GetType() == typeof(StackPanel2))
                {
                    StackPanel2 sp = (StackPanel2)args.InvokedItem;

                    if (sp.Children[0] != null)
                    {
                        if (sp.Children[0].GetType() == typeof(TextBlock))
                        {
                            TextBlock tb = sp.Children[0] as TextBlock;

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
