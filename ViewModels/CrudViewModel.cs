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
        private DetallePartidoModel partidoModel;

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

        

        public CrudViewModel() {
            PartidoModel = new DetallePartidoModel();
            ListadoJugadores = new ObservableCollection<JugadoresModel>();
            ListaCompeticiones = new ObservableCollection<CompeticionModel>();
            ListadoPartidos = new ObservableCollection<DetallePartidoModel>();

            

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
        public void CambiarModo(string modo)
        {
            if(modo == "jugadores")
            { 
                IsModoJugadorEnabled = true ;
                IsModoCompeticionEnabled = false;
                IsModoEquipoEnabled = false;
            }
            else if(modo == "competicion")
            {
                IsModoCompeticionEnabled= true ;
                IsModoJugadorEnabled = false;
                IsModoEquipoEnabled = false;
            }
            else if (modo == "equipo")
            {
                IsModoEquipoEnabled= true ;
                IsModoCompeticionEnabled = false;
                IsModoJugadorEnabled = false;
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
            ListadoJugadores.Add(new JugadoresModel(1 , "Florian", "Wirtz", "MCO", "", equipoModel) { });
            ListadoJugadores.Add(new JugadoresModel(2, "Jamal", "Musiala", "EI", "", equipoModel) { });
            ListadoJugadores.Add(new JugadoresModel(3, "Lamine", "Yamal", "ED", "", equipoModel) { });
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
           /* else
            {
                await ObtenerTodasCompeticiones();
            }*/

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
    }
}
