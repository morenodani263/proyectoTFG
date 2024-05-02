using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using UltimateMatch.Models;
using UltimateMatch.Services;

namespace UltimateMatch.ViewModels
{
    internal partial class PartidosViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<DetallePartidoModel> listaDetallePartido;

        [ObservableProperty]
        private bool isModoPlantilla;

        [ObservableProperty]
        private DetallePartidoModel detallePartidoModel;

        [ObservableProperty]
        private ObservableCollection<ClasificacionDTOModel> listaClasificacion;

        [ObservableProperty]
        private ObservableCollection<JugadoresModel> listaJugadores;

        [ObservableProperty]
        private ObservableCollection<JugadoresModel> listaJugadoresEq1;

        [ObservableProperty]
        private ObservableCollection<JugadoresModel> listaJugadoresEq2;

        [ObservableProperty]
        private string nombreCompeticion;

        public PartidosViewModel() 
        {
            listaDetallePartido = new ObservableCollection<DetallePartidoModel> { new DetallePartidoModel() };
            ListaJugadores = new ObservableCollection<JugadoresModel> {};
            ListaJugadoresEq1 = new ObservableCollection<JugadoresModel>();
            ListaJugadoresEq2 = new ObservableCollection<JugadoresModel>();
            ListaClasificacion = new ObservableCollection<ClasificacionDTOModel> { };
            isModoPlantilla = true;
            NombreCompeticion = "Liga X";
        }

        [RelayCommand]
        public void CambiarModo(string modo)
        {
            if (modo.Equals("plantilla"))
            {
                IsModoPlantilla = true;
            }
            else
            {
                IsModoPlantilla = false;
            }
        }

        //[RelayCommand]
        //public async Task ObtenerPartidosFecha()
        //{
            
        //    RequestModel request = new RequestModel(method: "GET",
        //                                            route: "/partidos/filtrarPorFecha/28-03-2024",
        //                                            data: string.Empty,
        //                                            server: APIService.URL_API);
        //    ResponseModel response = await APIService.ExecuteRequest(request);
        //    //si es 0 login exitoso, sino error al iniciar sesion
        //    if (response.Success == 0)
        //    {
        //        ListaDetallePartido = JsonConvert.DeserializeObject<ObservableCollection<DetallePartidoModel>>(response.Data.ToString());

        //    }
        //    else
        //    {

        //    }
            
        //}

        [RelayCommand]
        public async Task ObtenerPartidosFecha(string fecha)
        {
            if (fecha == null)
            {
                DateTime dt = DateTime.Today;
                fecha = dt.ToString("dd-MM-yyyy");
            }
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/filtrarPorFecha/" + fecha,
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);
  
            if (response.Success == 0)
            {
                ListaDetallePartido = JsonConvert.DeserializeObject<ObservableCollection<DetallePartidoModel>>(response.Data.ToString());

            }
            else
            {

            }

        }



        [RelayCommand]
        public async Task FechaEleccionRapida(string dia)
        {
            string fechaFinal = "";
            DateTime fechaSinFormato = DateTime.Today;
            if (dia.Equals("hoy"))
            {
                
            }
            else if (dia.Equals("ayer"))
            {
                fechaSinFormato = fechaSinFormato.AddDays(-1);
            }
            else if (dia.Equals("mañana"))
            {
                fechaSinFormato = fechaSinFormato.AddDays(1);
            }
            fechaFinal = fechaSinFormato.ToString("dd-MM-yyyy");

            await ObtenerPartidosFecha(fechaFinal);
        }

        //elegir fecha desde el datepicker
        [RelayCommand]
        public async Task FechaEleccion(DateTime dia)
        {
            string fechaString = dia.ToString("dd-MM-yyyy").Replace(" 0:00:00", "");
            await ObtenerPartidosFecha(fechaString);
        }

        //obtener partido en el que se hace doble click
        [RelayCommand]
        public async Task ObtenerPartido()
        {
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/obtenerjugadorespartido",
                                                    data: DetallePartidoModel,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                ListaJugadores = 
                    JsonConvert.DeserializeObject<ObservableCollection<JugadoresModel>>(response.Data.ToString());

                //obtener una lista para cada equipo
                Dictionary<string, ObservableCollection<JugadoresModel>> dictJugadores = new Dictionary<string,ObservableCollection <JugadoresModel>>();
                foreach (var jugador in ListaJugadores)
                {
                    //añadir a diccionario con clave que es nombre de equipo un jugador a la lista
                    string nombreEquipo = jugador.EquiposModel.Nombre;
                    if (dictJugadores.ContainsKey(nombreEquipo))
                    {
                        dictJugadores[nombreEquipo].Add(jugador);
                    }
                    else
                    {
                        dictJugadores.Add(nombreEquipo, new ObservableCollection<JugadoresModel> {});
                        dictJugadores[nombreEquipo].Add(jugador);
                    }
                    
                    
        
                }
                //separar en 2 listas usando la clave del diccionario
                /*
                 * elegir el nombre del equipo del primer jugador de la lista, y obtengo 
                 * todos los que tengan como clave el mismo nombre de equipo, u obtengo
                 * los que tienen el nombre de equipo distinto a ese jugador
                 */
                string nombre_equipo1 = ListaJugadores.ElementAt(0).EquiposModel.Nombre;
                foreach (var dictJug in dictJugadores)
                {
                    string clave = dictJug.Key;
                    ObservableCollection<JugadoresModel> valor = dictJug.Value;

                    if (clave.Equals(nombre_equipo1)){
                        foreach(var jEq1 in valor){
                            ListaJugadoresEq1.Add(jEq1);
                        }                       
                    }
                    else
                    {
                        foreach (var jEq2 in valor)
                        {
                            ListaJugadoresEq2.Add(jEq2);
                        }
                    }

                }

            }
            //despues obtener la clasificacion de la liga donde se juega ese partido
            await ObtenerClasificacion(DetallePartidoModel.NombreCompeticion);
        }

        public async Task ObtenerClasificacion(string nombreCompeticion)
        {
            RequestModel request = new RequestModel(method: "GET",
                                                        route: "/partidos/obtenerClasificacion/" + nombreCompeticion,
                                                        data: string.Empty,
                                                        server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);
            if (response.Success == 0)
            {
                ListaClasificacion =
                        JsonConvert.DeserializeObject<ObservableCollection<ClasificacionDTOModel>>(response.Data.ToString());
                NombreCompeticion = ListaClasificacion.ElementAt(0).NombreCompeticion;
            }
        }
    }

    
    
 }
