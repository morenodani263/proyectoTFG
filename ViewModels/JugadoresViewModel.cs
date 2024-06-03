using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateMatch.Models;
using UltimateMatch.Services;

namespace UltimateMatch.ViewModels
{
    [QueryProperty("User", "User")]
    internal partial class JugadoresViewModel:ObservableObject
    {
        [ObservableProperty]
        private UsuarioModel user = new UsuarioModel();

        [ObservableProperty]
        private JugadoresModel jugador;

        [ObservableProperty]
        private EquipoModel equipo;

        [ObservableProperty]
        private ObservableCollection<JugadoresModel> listaJugadores;

        [ObservableProperty]
        private ObservableCollection<EquipoModel> listaEquipos;

        [ObservableProperty]
        private bool isModoJugador;

        [ObservableProperty]
        private bool isModoEquipo;

        public JugadoresViewModel() { 
            Jugador = new JugadoresModel();
            Equipo = new EquipoModel();
            ListaJugadores = new ObservableCollection<JugadoresModel>();
            ListaEquipos =  new ObservableCollection<EquipoModel>();
            IsModoJugador = true;
            IsModoEquipo = false;

            ListaJugadores.Add(new JugadoresModel(1, "Pedro", "Diaz", "DC", "", new EquipoModel(){}, "España"));
            ListaJugadores.Add(new JugadoresModel(1, "Juanito", "Juan", "MC", "", new EquipoModel() { }, "España"));
            ListaEquipos.Add(new EquipoModel(1, "Ciru FS", ""));
            ListaEquipos.Add(new EquipoModel(2, "Yepes FS", ""));

        }

        [RelayCommand]
        public void CambiarModo(string modo)
        {
            if(modo == "jugador")
            {
                IsModoEquipo = false;
                IsModoJugador = true;
            }
            else if (modo == "equipo")
            {
                IsModoEquipo = true;
                IsModoJugador = false;
            }
        }

        //FILTROS BUSQUEDA JUGADORES
        [RelayCommand]
        public async Task FiltroJugadorNombre(string nombre)
        {
            CambiarModo("jugador");
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/buscarJugadorNombre/"+nombre,
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
               ListaJugadores =
                         JsonConvert.DeserializeObject<ObservableCollection<JugadoresModel>>(response.Data.ToString());
            }
            else
            {
                await ObtenerAllJugadores();
            }

        }

        [RelayCommand]
        public async Task FiltroJugadorNombreEquipo(string nombre)
        {
            CambiarModo("jugador");
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/buscarJugadorNombreEquipo/" + nombre,
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                ListaJugadores =
                          JsonConvert.DeserializeObject<ObservableCollection<JugadoresModel>>(response.Data.ToString());
            }
            else
            {
                await ObtenerAllJugadores();
            }

        }

        [RelayCommand]
        public async Task ObtenerAllJugadores()
        {
            CambiarModo("jugador");
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/obtenerAllJugadores",
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                ListaJugadores =
                          JsonConvert.DeserializeObject<ObservableCollection<JugadoresModel>>(response.Data.ToString());
            }          
        }

        //FILTROS BUSQUEDA  EQUIPOS
        [RelayCommand]
        public async Task ObtenerAllEquipos()
        {
            CambiarModo("equipo");
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/obtenerAllEquipos",
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                ListaEquipos =
                          JsonConvert.DeserializeObject<ObservableCollection<EquipoModel>>(response.Data.ToString());
            }
        }

        [RelayCommand]
        public async Task FiltroEquipoNombre(string nombre)
        {
            CambiarModo("equipo");
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/buscarEquipoNombre/" + nombre,
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                ListaEquipos =
                          JsonConvert.DeserializeObject<ObservableCollection<EquipoModel>>(response.Data.ToString());
            }
            else
            {
                await ObtenerAllEquipos();
            }

        }
    }
}
