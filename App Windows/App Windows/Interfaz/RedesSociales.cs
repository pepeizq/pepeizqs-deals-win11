using CommunityToolkit.WinUI.Helpers;
using FontAwesome5;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI;
using static Principal.MainWindow;

namespace Interfaz
{
    public static class RedesSociales
    {
        public class RedSocial
        {
            public RedSocial(string nombre, string comentario,
                             string color, EFontAwesomeIcon icono, string enlace)
            {
                this.nombre = nombre;
                this.comentario = comentario;
                this.color = color;
                this.icono = icono;
                this.enlace = enlace;
            }

            public string nombre { get; set; }
            public string comentario { get; set; }
            public string color { get; set; }
            public EFontAwesomeIcon icono { get; set; }
            public string enlace { get; set; }
        }

        public static void Cargar()
        {
            List<RedSocial> redes = new List<RedSocial>
            {
                new RedSocial("SocialTwitter1", "SocialTwitter2", "#55acee", EFontAwesomeIcon.Brands_Twitter, "https://pepeizqdeals.com/follow-the-deals/twitter/"),
                new RedSocial("SocialMail1", "SocialMail2", "#96a2a8", EFontAwesomeIcon.Solid_Envelope, "https://pepeizqdeals.com/follow-the-deals/mail/"),
                new RedSocial("SocialSteam1", "SocialSteam2", "#1b2838", EFontAwesomeIcon.Brands_Steam, "https://pepeizqdeals.com/follow-the-deals/steam-group/"),
                new RedSocial("SocialRss1", "SocialRss2", "#ff9702", EFontAwesomeIcon.Solid_Rss, "https://pepeizqdeals.com/rss-sources/"),
                new RedSocial("SocialTelegram1", "SocialTelegram2", "#0e8ed4", EFontAwesomeIcon.Brands_Telegram, "https://pepeizqdeals.com/follow-the-deals/telegram/"),
                new RedSocial("SocialDiscord1", "SocialDiscord2", "#5865f2", EFontAwesomeIcon.Brands_Discord, "https://pepeizqdeals.com/follow-the-deals/discord/"),
                new RedSocial("SocialReddit1", "SocialReddit2", "#ff4500", EFontAwesomeIcon.Brands_Reddit, "https://pepeizqdeals.com/follow-the-deals/reddit/")
            };

            if (redes.Count > 0)
            {
                ResourceLoader recursos = new ResourceLoader();
                int i = 0;

                foreach (var red in redes)
                {
                    SolidColorBrush fondoMaestro = new SolidColorBrush
                    {
                        Color = red.color.ToColor(),
                        Opacity = 0.8
                    };

                    Grid gridMaestro = new Grid
                    {
                        Background = fondoMaestro,
                        Padding = new Thickness(40, 40, 40, 40),
                        CornerRadius = new CornerRadius(5)
                    };

                    ColumnDefinition col1 = new ColumnDefinition();
                    ColumnDefinition col2 = new ColumnDefinition();

                    col1.Width = new GridLength(1, GridUnitType.Auto);
                    col2.Width = new GridLength(1, GridUnitType.Star);

                    gridMaestro.ColumnDefinitions.Add(col1);
                    gridMaestro.ColumnDefinitions.Add(col2);

                    StackPanel spIzquierda = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Margin = new Thickness(0, 0, 40, 0),
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    FontAwesome icono = new FontAwesome
                    {
                        Icon = red.icono,
                        Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                        VerticalAlignment = VerticalAlignment.Center,
                        FontSize = 30
                    };

                    spIzquierda.Children.Add(icono);

                    spIzquierda.SetValue(Grid.ColumnProperty, 0);
                    gridMaestro.Children.Add(spIzquierda);

                    StackPanel spDerecha = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };

                    TextBlock tb1 = new TextBlock
                    {
                        Text = recursos.GetString(red.nombre),
                        Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                        Margin = new Thickness(0, 0, 0, 15),
                        FontSize = 18  
                    };

                    spDerecha.Children.Add(tb1);

                    TextBlock tb2 = new TextBlock
                    {
                        Text = recursos.GetString(red.comentario),
                        Foreground = new SolidColorBrush((Color)Application.Current.Resources["ColorFuente"]),
                        FontSize = 16,
                        TextWrapping = TextWrapping.Wrap
                    };

                    spDerecha.Children.Add(tb2);

                    spDerecha.SetValue(Grid.ColumnProperty, 1);
                    gridMaestro.Children.Add(spDerecha);

                    Button boton = new Button
                    {
                        Content = gridMaestro,
                        Tag = red.enlace,
                        Padding = new Thickness(12),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        HorizontalContentAlignment = HorizontalAlignment.Stretch,
                        Background = new SolidColorBrush(Colors.Transparent),
                        BorderBrush = new SolidColorBrush(Colors.Transparent),
                        RequestedTheme = ElementTheme.Dark
                    };

                    if (i != redes.Count - 1)
                    {
                        boton.Margin = new Thickness(0, 0, 0, 20);
                    }

                    boton.Click += BotonAbrirRedSocial;

                    ObjetosVentana.spRedesSociales.Children.Add(boton);

                    i += 1;
                }
            }
        }

        public static async void BotonAbrirRedSocial(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            string enlace = boton.Tag as string;

            await Launcher.LaunchUriAsync(new Uri(enlace));
        }
    }
}
