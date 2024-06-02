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
using UltimateMatch.Resources.Utils;
using UltimateMatch.Services;

namespace UltimateMatch.ViewModels
{
    internal partial class CrudViewModel: ObservableObject
    {
        [ObservableProperty]
        private bool isModoCompeticionEquipo;

        [ObservableProperty]
        private JugadoresModel jugador;

        [ObservableProperty]
        private int maxId = 0;

        [ObservableProperty]
        private DetallePartidoModel partidoModel;

        [ObservableProperty]
        private EquipoModel equipoActual;

        [ObservableProperty]
        private ObservableCollection<JugadoresModel> listadoJugadores;

        [ObservableProperty]
        private ObservableCollection<DetallePartidoModel> listadoPartidos;

        [ObservableProperty]
        private ObservableCollection<CompeticionModel> listaCompeticiones;

        [ObservableProperty]
        private ObservableCollection<EquipoModel> listaEquipos;

        [ObservableProperty]
        ObservableCollection<int> listaPicker;

        [ObservableProperty]
        private bool isModoJugadorEnabled;
        [ObservableProperty]
        private bool isModoCompeticionEnabled;
        [ObservableProperty]
        private bool isModoEquipoEnabled;

        [ObservableProperty]
        private bool isModoPartido;
        [ObservableProperty]
        private bool isModoCompeticion;

        [ObservableProperty]
        private bool isModoGestion;
        [ObservableProperty]
        private bool isModoNoGestion;

        [ObservableProperty]
        private bool botonesActivos;

        [ObservableProperty]
        private ImageSource avatarImage;

        [ObservableProperty]
        private string avatarImage64;

        [ObservableProperty]
        private CompeticionModel competicionModel;

        [ObservableProperty]
        private bool isModoCrearPartido;
        [ObservableProperty]
        private bool isModoEditarPartido;

        [ObservableProperty]
        private bool isModoCrearEquipo;

        [ObservableProperty]
        private bool isModoEditarEquipo;

        [ObservableProperty]
        private bool isModoEditarJugador;

        [ObservableProperty]
        private bool isModoCrearJugador;

        public CrudViewModel() {
            PartidoModel = new DetallePartidoModel();
            Jugador = new JugadoresModel();
            EquipoActual = new EquipoModel();
            ListadoJugadores = new ObservableCollection<JugadoresModel>();
            ListaCompeticiones = new ObservableCollection<CompeticionModel>();
            ListadoPartidos = new ObservableCollection<DetallePartidoModel>();
            IsModoCrearEquipo = true;
            IsModoEditarEquipo = false;
            IsModoCrearJugador = true;
            IsModoCompeticionEquipo = false;
            isModoEditarJugador = false;
            

            DetallePartidoModel d1 = new DetallePartidoModel();
            d1.Fecha = "";
            d1.NombreEquipoLocal = "Betis";
            d1.NombreEquipoVisitante = "Sevilla";
            d1.NombreCompeticion = "1";
            d1.GolesEquipoLocal = 1;
            d1.GolesEquipoVisitante = 2;
            d1.Lugar = "Sevilla";
            DetallePartidoModel d2 = new DetallePartidoModel();
            d2.Fecha = "";
            d2.NombreEquipoLocal = "Villarreal";
            d2.NombreEquipoVisitante = "Valencia";
            d2.NombreCompeticion = "1";
            d2.GolesEquipoLocal = 1;
            d2.GolesEquipoVisitante = 2;
            d1.Lugar = "Valencia";
            d1.Temporada = "2023-2024";
            d2.Temporada = "2023-2024";
            ListadoPartidos.Add(d1);
            ListadoPartidos.Add(d2);
            ListadoPartidos.Add(d1);


            ListaPicker = new ObservableCollection<int>();
            RellenoListado();
            RellenarPicker();
            IsModoJugadorEnabled = true;
            IsModoCompeticionEnabled = false;
            IsModoEquipoEnabled = false;
            IsModoGestion = false;
            IsModoNoGestion = true;
            IsModoPartido = false;
            IsModoCrearPartido = true;
            IsModoEditarPartido = false;
            AvatarImage = "icono_persona.png";
            CompeticionModel = new CompeticionModel();
            BotonesActivos = true;
            //filtroNombre = "";

            ListaCompeticiones.Add(new CompeticionModel(9, "Compe cvc"));
            ObtenerTodasCompeticiones();
        }

        [RelayCommand]
        public void MostrarPartidosInfo()
        {
            IsModoEditarPartido = true;
        }


        [RelayCommand]
        public async Task LoadImage()
        {
            var imagesDict = await ImageUtils.OpenImage();
            if (imagesDict != null)
            {
                AvatarImage = (ImageSource)imagesDict["imageFromStream"];
                AvatarImage64 = (string)imagesDict["imageBase64"];

            }   

        }

        [RelayCommand]
        public void CambiarModoJugador(string modo)
        {
            if(modo == "jugador_edicion")
            {
                IsModoEditarJugador = true;
                IsModoCrearJugador = false;
            }
            else if(modo == "jugador_crear")
            {
                IsModoEditarJugador = false;
                IsModoCrearJugador = true;
            }
        }

            [RelayCommand]
        public void CambiarModo(string modo)
        {
            if(modo == "jugadores")
            { 
                IsModoJugadorEnabled = true ;
                IsModoCompeticionEnabled = false;
                IsModoEquipoEnabled = false;
                IsModoEditarJugador = false;
            }
            else if(modo == "competicion")
            {
                IsModoCompeticionEnabled= true ;
                IsModoJugadorEnabled = false;
                IsModoEquipoEnabled = false;
                IsModoEditarJugador = false;
            }
            else if (modo == "equipo")
            {
                IsModoEquipoEnabled= true ;
                IsModoCompeticionEnabled = false;
                IsModoJugadorEnabled = false;
                IsModoEditarJugador = false;
            }           

        }

        [RelayCommand]
        public void CambiarModoGestion()
        {
            if (IsModoGestion)
            {
                IsModoGestion = false;
                IsModoNoGestion = true;
            }
            else
            {
                IsModoGestion = true;
                IsModoNoGestion = false;
            }
            
        }

        [RelayCommand]
        public void CambiarModoPartido()
        {
            IsModoPartido = true;
            IsModoCompeticion = false;
        }

        [RelayCommand]
        public void ModoCrearPartido()
        {
            IsModoCrearPartido = true;
            IsModoEditarPartido = false;
        }

        [RelayCommand]
        public async Task ModoEditarPartido()
        {
            IsModoCrearPartido = false;
            IsModoEditarPartido = true;

            //obtener lista partidos de una competicion
            await ObtenerPartidosPorCompeticion();
        }

        [RelayCommand]
        public void BotonesActivar()
        {
            BotonesActivos = true;
        }

        [RelayCommand]
        public void CambiarModoCompeticion()
        {
            IsModoPartido = false;
            IsModoCompeticion = true;
        }


        [RelayCommand]
        public void CrearPartido()
        {
            IsModoPartido = false;
        }

        public void RellenarPicker()
        {           
            //ListaPicker.Add("X");
            for (int i = 0; i<=50; i++)
            {
                ListaPicker.Add(i);
            }
            
        }

        public void RellenoListado()
        {
            EquipoModel equipoModel = new EquipoModel(1, "Ciruelos", "");
            ListadoJugadores.Add(new JugadoresModel(1 , "Florian", "Wirtz", "MCO", "", equipoModel, "Alemania") { });
            ListadoJugadores.Add(new JugadoresModel(2, "Jamal", "Musiala", "EI", "", equipoModel, "Alemania") { });
            ListadoJugadores.Add(new JugadoresModel(3, "Lamine", "Yamal", "ED", "", equipoModel, "Alemania") { });
        }

        [RelayCommand]
        public async Task CrearCompeticion()
        {
            if (AvatarImage64 != null)
            {
                CompeticionModel.Avatar = AvatarImage64;
            }     
            //CompeticionModel.EsLiga = true;

            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/crearCompeticion",
                                                    data: CompeticionModel,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                Application.Current.MainPage.DisplayAlert("Competición creada con éxito", response.Message, "ACEPTAR");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("No se ha podido crear competición", response.Message, "ACEPTAR");
            }

        }

        [RelayCommand]
        public async Task ObtenerCompeticionesPorNombre(string filtroNombre)
        {

         RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/filtrarCompeticionNombre/" + filtroNombre,
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                ListaCompeticiones =
                        JsonConvert.DeserializeObject<ObservableCollection<CompeticionModel>>(response.Data.ToString());
                
                /*CompeticionModel =
                        JsonConvert.DeserializeObject<CompeticionModel>(response.Data.ToString());
                ListaCompeticiones.Add(CompeticionModel);*/
            }
            else
            {
                await ObtenerTodasCompeticiones();
            }

        }

        [RelayCommand]
        public async Task ObtenerEquiposCompeticion(CompeticionModel competicion)
        {
            CambiarModoPartido();

            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/obtenerEquiposCompeticion",
                                                    data: CompeticionModel,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                ListaEquipos =
                        JsonConvert.DeserializeObject<ObservableCollection<EquipoModel>>(response.Data.ToString());
            }
            else
            {
                
            }

        }

        [RelayCommand]
        public async Task ObtenerPartidosPorCompeticion()
        {
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/obtenerPartidosCompeticion/"+ CompeticionModel.Competicion_Id,
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                ListadoPartidos =
                        JsonConvert.DeserializeObject<ObservableCollection<DetallePartidoModel>>(response.Data.ToString());

            }
            else
            {
                App.Current.MainPage.DisplayAlert("No se han podido obtener los partidos de esta competición", response.Message, "ACEPTAR");
            }

        }

        [RelayCommand]
        public async Task ObtenerTodasCompeticiones()
        {
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/obtenerCompeticiones",
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                ListaCompeticiones =
                        JsonConvert.DeserializeObject<ObservableCollection<CompeticionModel>>(response.Data.ToString());
                
            }
            else
            {
                App.Current.MainPage.DisplayAlert("No se ha podido obtener competiciones", response.Message, "ACEPTAR");
            }

        }

        [RelayCommand]
        public async Task EditarEliminarPartido(string modo)
        {
            RequestModel request;
            if (modo.Equals("editar"))
            {
                //editar
                request = new RequestModel(method: "GET",
                                                    route: "/editarPartido",
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            }
            else
            {
                //eliminar
                request = new RequestModel(method: "GET",
                                                    route: "/partidos/obtenerCompeticiones",
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            }
            
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                App.Current.MainPage.DisplayAlert("OPERACIÓN COMPLETADA CON ÉXITO", response.Message, "ACEPTAR");

            }
            else
            {
                App.Current.MainPage.DisplayAlert("NO SE HA PODIDO REALIZAR LA OPERACIÓN", response.Message, "ACEPTAR");
            }

        }

        //Crear equipo
        [RelayCommand]
        public async Task CrearEquipo()
        {
            /*if (AvatarImage64 != null)
            {
                CompeticionModel.Avatar = AvatarImage64;
            }*/
            //CompeticionModel.EsLiga = true;
            await GetMaxIdEquipo();
            EquipoActual.Equipo_id = MaxId + 1;
            
                    
            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/crearEquipo",
                                                    data: EquipoActual,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                Application.Current.MainPage.DisplayAlert("Equipo creado con éxito", response.Message, "ACEPTAR");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("No se ha podido crear equipo", response.Message, "ACEPTAR");
            }

        }

        //Cambiar modo en el crud equipo
        [RelayCommand]
        public void CambiarModoCrudEquipo(string modo)
        {
            if (modo.Equals("crear"))
            {
                IsModoCrearEquipo = true;
                IsModoEditarEquipo = false;
                EquipoActual.Nombre = "";
                IsModoCompeticionEquipo = false;
            }
            else if(modo.Equals("editar"))
            {
                IsModoCrearEquipo = false;
                IsModoEditarEquipo = true;
                IsModoCompeticionEquipo = false;
            }
            else if (modo.Equals("competicion"))
            {
                IsModoCrearEquipo = false;
                IsModoEditarEquipo = false;
                IsModoCompeticionEquipo = true;
            }
        }

        //obtener equipos filtro nombre
        [RelayCommand]
        public async Task FiltrarEquiposNombre(string nombreEquipo)
        {
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/filtrarEquiposNombre/" + nombreEquipo,
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
                App.Current.MainPage.DisplayAlert("No se han podido obtener equipos", response.Message, "ACEPTAR");
            }

        }

        //obtener todos los equipos
        [RelayCommand]
        public async Task ObtenerTodosEquipos()
        {
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/obtenerEquipos",
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
                App.Current.MainPage.DisplayAlert("No se han podido obtener equipos", response.Message, "ACEPTAR");
            }

        }

        //editar equipo
        [RelayCommand]
        public async Task EditarEquipo()
        {
            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/editarEquipo",
                                                    data: EquipoActual,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                /* ListaEquipos =
                         JsonConvert.DeserializeObject<ObservableCollection<EquipoModel>>(response.Data.ToString());*/
                App.Current.MainPage.DisplayAlert("Se ha editado con éxito", response.Message, "ACEPTAR");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("No se han podido obtener equipos", response.Message, "ACEPTAR");
            }

        }

        //añadir un equipo a una competicion
        [RelayCommand]
        public async Task AnadirEquipoACompeticion()
        {
            CompeticionEquipoModel competicion_equipo = new CompeticionEquipoModel();
            competicion_equipo.CompeticionId = CompeticionModel;
            competicion_equipo.EquipoId = EquipoActual;

            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/addEquipoCompeticion",
                                                    data: competicion_equipo,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            
            if (response.Success == 0)
            {
                App.Current.MainPage.DisplayAlert("Se ha añadido con éxito", response.Message, "ACEPTAR");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("No se ha podido añadir el equipo a la competicion", response.Message, "ACEPTAR");
            }

        }

        //eliminar equipo
        [RelayCommand]
        public async Task EliminarEquipo()
        {
            bool answer = await App.Current.MainPage.DisplayAlert(
            "Confirmar Eliminación",
            "¿Está seguro de que desea eliminar este elemento?",
            "Eliminar",
            "Cancelar");

            if (answer)
            {
                RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/eliminarEquipo",
                                                    data: EquipoActual,
                                                    server: APIService.URL_API);

                ResponseModel response = await APIService.ExecuteRequest(request);

                if (response.Success == 0)
                {
                    /* ListaEquipos =
                             JsonConvert.DeserializeObject<ObservableCollection<EquipoModel>>(response.Data.ToString());*/
                    App.Current.MainPage.DisplayAlert("Se ha eliminado con éxito", response.Message, "ACEPTAR");
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("No se ha podido eliminar equipo", response.Message, "ACEPTAR");
                }
            }
        }

        
        
        public async Task GetMaxIdEquipo()
        {         
            
            RequestModel request = new RequestModel(method: "GET",
                                                route: "/partidos/getMaxIdEquipo",
                                                data: string.Empty,
                                                server: APIService.URL_API);

            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                maxId =
                         JsonConvert.DeserializeObject<int>(response.Data.ToString());
            }

        }

        //CRUD JUGADORES
        [RelayCommand]
        public async Task CrearJugador()
        {
            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/crearJugador",
                                                    data: Jugador,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                App.Current.MainPage.DisplayAlert("Se ha creado con éxito", response.Message, "ACEPTAR");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error al crear", response.Message, "ACEPTAR");
            }
        }

        [RelayCommand]
        public async Task EditarJugador()
        {
            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/editarJugador",
                                                    data: Jugador,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                App.Current.MainPage.DisplayAlert("Se ha editado con éxito", response.Message, "ACEPTAR");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error al editar", response.Message, "ACEPTAR");
            }
        }
        [RelayCommand]
        public async Task EliminarJugador()
        {
            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/eliminarJugador",
                                                    data: Jugador,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                App.Current.MainPage.DisplayAlert("Se ha eliminado con éxito", response.Message, "ACEPTAR");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error al eliminar", response.Message, "ACEPTAR");
            }
        }
    }
}
