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
        private ObservableCollection<JugadoresModel> listadoJugadores;

        [ObservableProperty]
        private ObservableCollection<CompeticionModel> listaCompeticiones;

        [ObservableProperty]
        private ObservableCollection<EquipoModel> listaEquipos;

        [ObservableProperty]
        ObservableCollection<string> listaPicker;

        [ObservableProperty]
        private bool isModoJugadorEnabled;
        [ObservableProperty]
        private bool isModoCompeticionEnabled;
        [ObservableProperty]
        private bool isModoEquipoEnabled;
        [ObservableProperty]
        private bool isModoPartido;
        [ObservableProperty]
        private bool isModoGestion;
        [ObservableProperty]
        private bool isModoNoGestion;

        [ObservableProperty]
        private ImageSource avatarImage;

        [ObservableProperty]
        private string avatarImage64;

        [ObservableProperty]
        private CompeticionModel competicionModel;

        //[ObservableProperty]
        //private string filtroNombre;

        public CrudViewModel() {
            ListadoJugadores = new ObservableCollection<JugadoresModel>();
            ListaCompeticiones = new ObservableCollection<CompeticionModel>();
            ListaPicker = new ObservableCollection<string>();
            RellenoListado();
            RellenarPicker();
            IsModoJugadorEnabled = true;
            IsModoCompeticionEnabled = false;
            IsModoEquipoEnabled = false;
            IsModoGestion = false;
            IsModoNoGestion = true;
            IsModoPartido = false;
            AvatarImage = "icono_persona.png";
            CompeticionModel = new CompeticionModel();
            //filtroNombre = "";
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
            IsModoPartido = !IsModoPartido;
            
        }

        [RelayCommand]
        public void CrearPartido()
        {
            IsModoPartido = false;
        }

        public void RellenarPicker()
        {
            
            ListaPicker.Add("X");
            for (int i = 0; i<=50; i++)
            {
                ListaPicker.Add(i.ToString());
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
    }
}
