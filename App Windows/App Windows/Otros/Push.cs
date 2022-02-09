using CommunityToolkit.WinUI.Helpers;
using FireSharp;
using FireSharp.Config;
using FireSharp.Response;
using Herramientas;
using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Windows.Storage;
using static Principal.MainWindow;
using static Wordpress;

namespace Otros
{
    public class MensajePush
    {
        public MensajePush(string titulo, string enlace, string imagen, string dia)
        {
            this.titulo = titulo;
            this.enlace = enlace;
            this.imagen = imagen;
            this.dia = dia;
        }

        public string titulo { get; set; }
        public string enlace { get; set; }
        public string imagen { get; set; }
        public string dia { get; set; }
    }

    public static class Push
    {
        public static async void Escuchar()
        {
            FirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "Shoh5YD3VlmXrlrLpuJ3IM7EtOdFC6hyLi94xu2y",
                BasePath = "https://pepeizq-s-deals.firebaseio.com"
            };

            FirebaseClient cliente = new FirebaseClient(config);

            ApplicationDataStorageHelper helper = new ApplicationDataStorageHelper(ApplicationData.Current);

            List<MensajePush> listaNotificaciones = new List<MensajePush>();

            try
            {
                listaNotificaciones = System.Text.Json.JsonSerializer.Deserialize<List<MensajePush>>(await helper.ReadFileAsync<string>("listaNotificaciones"));
            }
            catch (Exception)
            {

            }

            bool primeraVez = false;

            if (listaNotificaciones != null)
            {
                if (listaNotificaciones.Count == 0)
                {
                    primeraVez = true;
                }
            }
            else
            {
                primeraVez = true;
            }

            FirebaseResponse respuesta = null;

            try
            {
                respuesta = await cliente.GetAsync("mensajes/");
            }
            catch (Exception)
            {

            }

            if (respuesta != null)
            {
                string contenido = respuesta.Body;

                var notificaciones2 = new Dictionary<string, MensajePush>();

                try
                {
                    notificaciones2 = JsonConvert.DeserializeObject<Dictionary<string, MensajePush>>(contenido);
                }
                catch (Exception)
                {
                  
                };

                if (notificaciones2 != null)
                {
                    foreach (var notificacion2 in notificaciones2)
                    {
                        bool añadir = true;

                        foreach (var listaNotificacion in listaNotificaciones)
                        {
                            if (listaNotificacion.enlace == notificacion2.Value.enlace)
                            {
                                añadir = false;
                            }
                        }

                        if (añadir == true)
                        {
                            listaNotificaciones.Add(notificacion2.Value);
                        }
                    }

                    if (listaNotificaciones.Count > 0)
                    {
                        await helper.CreateFileAsync<string>("listaNotificaciones", System.Text.Json.JsonSerializer.Serialize(listaNotificaciones));
                    }
                }

                MensajePush nuevoMensaje = null;
                int i = 0;
                EventStreamResponse respuesta2  = await cliente.OnAsync("mensajes/", async (s, args, context) => {
                    
                    if (i == 0)
                    {
                        nuevoMensaje = new MensajePush(null, null, null, args.Data);
                    }
                    else if (i == 1)
                    {
                        nuevoMensaje.enlace = args.Data;
                    }
                    else if (i == 2)
                    {
                        nuevoMensaje.imagen = args.Data;
                    }
                    else if (i == 3)
                    {
                        nuevoMensaje.titulo = args.Data;
                        i = -1;
                    }

                    if (i == -1)
                    {
                        bool añadir = true;

                        if (listaNotificaciones.Count > 0)
                        {
                            foreach (var notificacion in listaNotificaciones)
                            {
                                if (notificacion.enlace == nuevoMensaje.enlace)
                                {
                                    añadir = false;
                                }
                            }
                        }

                        if (nuevoMensaje.dia != DateTime.Today.DayOfYear.ToString())
                        {
                            añadir = false;
                        }

                        if (añadir == true)
                        {
                            listaNotificaciones.Add(nuevoMensaje);

                            try
                            {
                                await helper.CreateFileAsync<string>("listaNotificaciones", System.Text.Json.JsonSerializer.Serialize(listaNotificaciones));
                            }
                            catch (Exception)
                            {

                            }

                            if (primeraVez == false)
                            {
                                Notificaciones.Toast(nuevoMensaje.titulo, nuevoMensaje.enlace, nuevoMensaje.imagen);
                            }
                        }
                    }
                    i += 1;
                });
            }
        }
    }
}
