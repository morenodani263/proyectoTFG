
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
    [QueryProperty("User", "User")]
    internal partial class CrudViewModel: ObservableObject
    {
        [ObservableProperty]
        private UsuarioModel user = new UsuarioModel();

        [ObservableProperty]
        private EstadioModel estadio;

        [ObservableProperty]
        private ObservableCollection<JugadoresModel2> listJug;

        [ObservableProperty]
        private bool isModoCompeticionEquipo;

        [ObservableProperty]
        private JugadoresModel jugador;

        [ObservableProperty]
        private JugadoresModel2 jugadorModel2;

        [ObservableProperty]
        private int maxId = 0;

        [ObservableProperty]
        private DetallePartidoModel partidoModel;

        [ObservableProperty]
        private EquipoModel equipoActual;

        [ObservableProperty]
        private EquipoModel2 equipoActualModel2;

        [ObservableProperty]
        private ObservableCollection<JugadoresModel> listadoJugadores;

        [ObservableProperty]
        private ObservableCollection<DetallePartidoModel> listadoPartidos;

        [ObservableProperty]
        private ObservableCollection<CompeticionModel> listaCompeticiones;

        [ObservableProperty]
        private ObservableCollection<EquipoModel> listaEquipos;

        [ObservableProperty]
        private ObservableCollection<EquipoModel2> listaEquipos2;

        [ObservableProperty]
        private ObservableCollection<string> listaEquiposNombre;

        [ObservableProperty]
        private ObservableCollection<string> listaTemporadas;

        [ObservableProperty]
        private ObservableCollection<JugadoresModel2> listajug2 = new ObservableCollection<JugadoresModel2>();

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
        private string nombreEstadio = "";

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

        [ObservableProperty]
        private string nombreEquipoActual = "";

        [ObservableProperty]
        private EquipoModel equipoActualCrear;

        //crud competicion
        [ObservableProperty]
        private bool isModoCrearCompeticion;

        [ObservableProperty]
        private bool isModoGestionPartido;

        [ObservableProperty]
        private bool isModoGestionCompeticion;

        public CrudViewModel() {
            EquipoActualCrear = new EquipoModel();
            PartidoModel = new DetallePartidoModel();
            Jugador = new JugadoresModel();
            EquipoActual = new EquipoModel();
            EquipoActualModel2 = new EquipoModel2();
            ListadoJugadores = new ObservableCollection<JugadoresModel>();
            ListaCompeticiones = new ObservableCollection<CompeticionModel>();
            ListadoPartidos = new ObservableCollection<DetallePartidoModel>();
            ListaEquipos2 = new ObservableCollection<EquipoModel2>();
            JugadorModel2 = new JugadoresModel2();
            ListJug = new ObservableCollection<JugadoresModel2>();
            Estadio = new EstadioModel();
            IsModoCrearEquipo = true;
            IsModoEditarEquipo = false;
            IsModoCrearJugador = true;
            IsModoCompeticionEquipo = false;
            isModoEditarJugador = false;
            AvatarImage64 = "icono_persona.png";
            ListaEquiposNombre = new ObservableCollection<string>();
            /*
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
            */


            ListaPicker = new ObservableCollection<int>();
            ObtenerAllJugadores();
            //ObtenerAllEquipos();
            ObtenerTodosEquipos();
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

            //ListaCompeticiones.Add(new CompeticionModel(9, "Compe cvc"));
            ObtenerTodasCompeticiones();

            IsModoCrearCompeticion = true;
            IsModoGestionCompeticion = false;
            IsModoGestionPartido = false;

            ListaTemporadas = new ObservableCollection<string>();
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
        public async Task CambiarModoJugadorAsync(string modo)
        {
            /*
            if(JugadorModel2.Posicion.Equals("") && JugadorModel2.Apellidos.Equals(""))
            {
                return;
            }
            */
            if(modo == "jugador_edicion")
            {
                //AvatarImage = "icono_persona.png";
                /*if(Jugador.Avatar != null)
                {
                    SetAvatarFromBase64(Jugador.Avatar);
                }
                else
                {
                    AvatarImage = "icono_persona.png";
                }*/
                IsModoEditarJugador = true;
                IsModoCrearJugador = false;
            }
            else if(modo == "jugador_crear")
            {
                JugadorModel2 = new JugadoresModel2();
                IsModoEditarJugador = false;
                IsModoCrearJugador = true;
                
                JugadorModel2.Nombre = "";
                JugadorModel2.Apellidos = "";
                JugadorModel2.Nacionalidad = "";
                JugadorModel2.Posicion = "";
            }
            else
            {
                return;
            }

        }

        //


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
        public void RellenarPickerTemporadas()
        {
            ListaTemporadas.Add("2023/2024");
            ListaTemporadas.Add("2024/2025"); 

        }

        [RelayCommand]
        public async Task CambiarModoCrudCompeticionAsync(string modo)
        {
            if (modo == "crear_competicion")
            {
                CompeticionModel = new CompeticionModel();
                AvatarImage = "icono_persona.png";
                IsModoCrearCompeticion = true;
                IsModoGestionCompeticion = false;
                IsModoGestionPartido = false;
            }
            else if (modo == "edicion_competicion")
            {
                IsModoCrearCompeticion = false;
                IsModoGestionCompeticion = true;
                IsModoGestionPartido = false;
                await ObtenerTodosEquipos();
            }
            else if (modo == "edicion_partido")
            {
                IsModoCrearCompeticion = false;
                IsModoGestionCompeticion = false;
                IsModoGestionPartido = true;
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
        [ObservableProperty]
        private DateTime fechaPartido;

        [RelayCommand]
        public async Task CrearPartidos()
        {
            PartidoModel.NombreCompeticion = CompeticionModel.Nombre;
            PartidoModel.Fecha = FechaPartido.ToString("dd-MM-yyyy");
      
            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/crearPartidosnew",
                                                    data: PartidoModel,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                Application.Current.MainPage.DisplayAlert("Partido creada con éxito", response.Message, "ACEPTAR");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("No se ha podido crear partido", response.Message, "ACEPTAR");
            }

        }

        [RelayCommand]
        public async Task EditarCompeticion()
        {
            if (AvatarImage64 != null)
            {
                CompeticionModel.Avatar = AvatarImage64;
            }
            //CompeticionModel.EsLiga = true;

            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/editarCompeticion",
                                                    data: CompeticionModel,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                Application.Current.MainPage.DisplayAlert("Competición editada con éxito", response.Message, "ACEPTAR");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("No se ha podido editar competición", response.Message, "ACEPTAR");
            }

        }

        [RelayCommand]
        public async Task EliminarCompeticion()
        {
            if (AvatarImage64 != null)
            {
                CompeticionModel.Avatar = AvatarImage64;
            }
            //CompeticionModel.EsLiga = true;

            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/eliminarCompeticion",
                                                    data: CompeticionModel,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                Application.Current.MainPage.DisplayAlert("Competición eliminada con éxito", response.Message, "ACEPTAR");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("No se ha podido eliminar competición", response.Message, "ACEPTAR");
            }

        }

        //establecer un avatar desde una imagen en base4
        public void SetAvatarFromBase64(string base64String)
        {
            if (!string.IsNullOrEmpty(base64String))
            {
                var imageBytes = Convert.FromBase64String(base64String.Split(',')[1]);
                AvatarImage = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
        }

        //obtener una imagen base 64 desde un avatar
        public async Task<string> GetBase64FromAvatarAsync()
        {
            if (AvatarImage != null)
            {
                Stream stream = await GetStreamFromImageSourceAsync(AvatarImage);
                if (stream != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        var imageBytes = memoryStream.ToArray();
                        return Convert.ToBase64String(imageBytes);
                    }
                }
            }
            return string.Empty;
        }

        private async Task<Stream> GetStreamFromImageSourceAsync(ImageSource imageSource)
        {
            if (imageSource is StreamImageSource streamImageSource)
            {
                var stream = await streamImageSource.Stream(CancellationToken.None);
                return stream;
            }
            // Puedes agregar más condiciones aquí si manejas otros tipos de ImageSource
            return null;
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
                await ObtenerAllEquipos();
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

        [RelayCommand]
        public async Task ObtenerEquiposCompeticion2(string nombre)
        {
            CambiarModoPartido();

            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/obtenerEquiposCompeticion/" + nombre,
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

        //obtener todos los equipos
        [RelayCommand]
        public async Task ObtenerAllEquipos()
        {
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/obtenerAllEquipos",
                                                    data: CompeticionModel,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                ListaEquipos =
                        JsonConvert.DeserializeObject<ObservableCollection<EquipoModel>>(response.Data.ToString());
                for(int i = 0; i< ListaEquiposNombre.Count; i++)
                {
                    ListaEquiposNombre.Add(ListaEquipos[i].Nombre);
                }

                
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
                EquipoActual = new EquipoModel();
            }
            else if (modo.Equals("editar"))
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
            else
            {
                return;
            }
        }

        //metodo de navegación entre páginas
        [RelayCommand]
        public async Task Navegar(string nombrePagina)
        {
            await Shell.Current.GoToAsync("//" + nombrePagina, new Dictionary<string, object>()
            {
                ["User"] = User
            });
        }

        public static ImageSource Base64ToImageSource(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                Console.WriteLine("Cadena base64 vacía o nula.");
                return null;
            }

            // Intentar limpiar la cadena Base64 eliminando caracteres no válidos.
            base64String = base64String.Trim();
            base64String = base64String.Replace("\r", "").Replace("\n", "");

            try
            {
                byte[] imageBytes = System.Convert.FromBase64String(base64String);
                return ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error al convertir la cadena base64 en imagen: {ex.Message}");
                return null;
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
                await ObtenerAllEquipos();

            }
            foreach (var e in ListaEquipos)
            {
                ListaEquiposNombre.Add(e.Nombre);
            }
            await ObtenerListaEquiposFoto();
        }

        
        [RelayCommand]
        public void CompararNombreEquipo(string nombreEquipo)
        {
            foreach(var e in ListaEquipos)
            {
                if(e.Nombre.Equals(nombreEquipo))
                {
                    EquipoActual = e;
                }
            }
               

        }

        
        //CRUD EQUIPO

        //Crear equipo
        [RelayCommand]
        public async Task CrearEquipo()
        {
            //EN PRIMER LUGAR REALIZAMOS LA CREACIÓN DEL EQUIPO EN LA BBDD

            //obtener el id que tendrá asociado en la base de datos
            await GetMaxIdEquipo();
            EquipoActualCrear.Equipo_id = MaxId + 1;

            //obtener datos
            string escudo_ = await GetBase64FromAvatarAsync();
            //establecer la imagen en el jugador a editar
            if (escudo_ != null)
            {
                EquipoActualCrear.Escudo = "data:image/jpeg;base64," + escudo_;
            }

            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/crearEquipo",
                                                    data: EquipoActualCrear,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                Application.Current.MainPage.DisplayAlert("Equipo creado con éxito", response.Message, "ACEPTAR");

                //EN SEGUNDO LUGAR REALIZAMOS LA CREACIÓN DEL ESTADIO EN LA BBDD
                await GetMaxIdEstadio();
                Estadio.Estadio_id = MaxId + 1;
                Estadio.Nombre = NombreEstadio;
                Estadio.Equipo_id = EquipoActualCrear.Equipo_id;
                RequestModel request1 = new RequestModel(method: "POST",
                                                    route: "/partidos/crearEstadio",
                                                    data: Estadio,
                                                    server: APIService.URL_API);
                ResponseModel response1 = await APIService.ExecuteRequest(request1);
                if (response1.Success == 0)
                {
                    Application.Current.MainPage.DisplayAlert("Estadio añadido con éxito", response1.Message, "ACEPTAR");
                    await ObtenerTodosEquipos();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("No se ha podido añadir estadio", response1.Message, "ACEPTAR");
                }
            }
            else
            {
                App.Current.MainPage.DisplayAlert("No se ha podido crear equipo", response.Message, "ACEPTAR");
            }

        }

        //editar equipo
        [RelayCommand]
        public async Task EditarEquipo()
        {
            EquipoActual.Equipo_id = EquipoActualModel2.Equipo_id;
            EquipoActual.Nombre = EquipoActualModel2.Nombre;
            //obtener datos
            string escudo_ = await GetBase64FromAvatarAsync();
            //establecer la imagen en el jugador a editar
            if (escudo_ != null)
            {
                EquipoActual.Escudo = "data:image/jpeg;base64," + escudo_;
            }

            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/editarEquipo",
                                                    data: EquipoActual,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                App.Current.MainPage.DisplayAlert("Se ha editado con éxito", response.Message, "ACEPTAR");
                

                //una vez editado el equipo, procedemos a editar el estadio

                Estadio.Nombre = NombreEstadio;
                Estadio.Equipo_id = EquipoActual.Equipo_id;


                RequestModel request1 = new RequestModel(method: "POST",
                                                    route: "/partidos/editarEstadio",
                                                    data: Estadio,
                                                    server: APIService.URL_API);
                ResponseModel response1 = await APIService.ExecuteRequest(request1);
                if(response1.Success == 0)
                {
                    App.Current.MainPage.DisplayAlert("Se ha editado estadio con éxito", response.Message, "ACEPTAR");
                    await ObtenerTodosEquipos();
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Error al editar estadio", "", "ACEPTAR");
                }
                
            }
            else
            {
                App.Current.MainPage.DisplayAlert("No se ha podido editar equipo", response.Message, "ACEPTAR");
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
                //eliminar estadio asociado
                EquipoActual.Equipo_id = EquipoActualModel2.Equipo_id;
                EquipoActual.Nombre = EquipoActualModel2.Nombre;
                EquipoActual.Escudo = "";
                RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/eliminarEstadio",
                                                    data: EquipoActual,
                                                    server: APIService.URL_API);

                ResponseModel response = await APIService.ExecuteRequest(request);
                if (response.Success == 0)
                {
                    App.Current.MainPage.DisplayAlert("Se ha eliminado con éxito", response.Message, "ACEPTAR");

                    //eliminar equipo
                    RequestModel request1 = new RequestModel(method: "POST",
                                                    route: "/partidos/eliminarEquipo",
                                                    data: EquipoActual,
                                                    server: APIService.URL_API);

                    ResponseModel response1 = await APIService.ExecuteRequest(request1);

                    if (response.Success == 0)
                    {
                        App.Current.MainPage.DisplayAlert("Se ha eliminado con éxito", response.Message, "ACEPTAR");
                        await ObtenerTodosEquipos();
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("No se ha podido eliminar equipo", "", "ACEPTAR");
                    }
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("No se ha podido eliminar equipo", "", "ACEPTAR");
                }

                
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

                for (int i = 0; i < ListaEquipos.Count; i++)
                {
                    ListaEquiposNombre.Add(ListaEquipos[i].Nombre);
                }
                await ObtenerListaEquiposFoto();
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
            //pasar de EquipoModel2 a EquipoModel
            EquipoActual.Equipo_id = EquipoActualModel2.Equipo_id;
            EquipoActual.Nombre = EquipoActualModel2.Nombre;
            EquipoActual.Escudo = "";

            //datos a insertar
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

        //eliminar un equipo de una competicion
        [RelayCommand]
        public async Task EliminarEquipoDeCompeticion()
        {
            CompeticionEquipoModel competicion_equipo = new CompeticionEquipoModel();
            competicion_equipo.CompeticionId = CompeticionModel;
            competicion_equipo.EquipoId = EquipoActual;

            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/delEquipoCompeticion",
                                                    data: competicion_equipo,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);


            if (response.Success == 0)
            {
                App.Current.MainPage.DisplayAlert("Se ha eliminado con éxito", response.Message, "ACEPTAR");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("No se ha podido añadir el equipo a la competicion", response.Message, "ACEPTAR");
            }

        }

        /*cuando se obtenga ListaJugadores (que es de tipo JugadoresModel), se usa el metodo siguiente para
        poder obtener el listado con fotos (tipo: JugadoresModel2).*/
        [RelayCommand]
        public async Task ObtenerListaEquiposFoto()
        {
            ListaEquipos2.Clear();
            foreach (var l in ListaEquipos)
            {
                SetAvatarFromBase64(l.Escudo);
                var equipo = new EquipoModel2(l.Equipo_id, l.Nombre, AvatarImage);
                ListaEquipos2.Add(equipo);
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

        public async Task GetMaxIdEstadio()
        {
            RequestModel request = new RequestModel(method: "GET",
                                                route: "/partidos/getMaxIdEstadio",
                                                data: string.Empty,
                                                server: APIService.URL_API);

            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                maxId = JsonConvert.DeserializeObject<int>(response.Data.ToString());
            }
        }

        //CRUD JUGADORES
        [RelayCommand]
        public async Task CrearJugador()
        {
            Jugador.Avatar = AvatarImage64;

            //establecer el equipo elegido al jugador a insertar
            CompararNombreEquipo(nombreEquipoActual);
            Jugador.EquiposModel = EquipoActual;
            //pasar de JugadoresModel2 a JugadoresModel
            Jugador.Nombre = JugadorModel2.Nombre;
            Jugador.Apellidos = JugadorModel2.Apellidos;
            Jugador.Nacionalidad = JugadorModel2.Nacionalidad;
            Jugador.Posicion = JugadorModel2.Posicion;
            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/crearJugador",
                                                    data: Jugador,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                App.Current.MainPage.DisplayAlert("Se ha creado con éxito", response.Message, "ACEPTAR");
                await ObtenerAllJugadores();
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error al crear jugadore", response.Message, "ACEPTAR");
            }
        }

        [RelayCommand]
        public async Task EditarJugador()
        {
            //obtener en AvatarImage la imagen del jugador seleccionado
            AvatarImage = JugadorModel2.Avatar;
          
            //obtener imagen del jugador en base 64
            string imagen_jug = await GetBase64FromAvatarAsync();
            //establecer la imagen en el jugador a editar
            if (imagen_jug != null) {
                Jugador.Avatar = "data:image/jpeg;base64,"+imagen_jug;
            }

            //obtener datos de JugadorModel2 a JugadorModel
            Jugador.Jugador_id = JugadorModel2.Jugador_id;
            Jugador.Nombre = JugadorModel2.Nombre;
            Jugador.Apellidos = JugadorModel2.Apellidos;
            Jugador.Nacionalidad = JugadorModel2.Nacionalidad;
            Jugador.Posicion = JugadorModel2.Posicion;

            //establecer el equipo en el jugador a insertar
            CompararNombreEquipo(Jugador.EquiposModel.Nombre);
            Jugador.EquiposModel = JugadorModel2.EquiposModel;

            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/editarJugador",
                                                    data: Jugador,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                App.Current.MainPage.DisplayAlert("Se ha editado con éxito", response.Message, "ACEPTAR");
                await ObtenerAllJugadores();
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error al editar", response.Message, "ACEPTAR");
            }
        }

        //pasar de una lista de jugadores con avatar en base 64 a
        //una lista con avatar ImageSource
        

        /*cuando se obtenga ListaJugadores (que es de tipo JugadoresModel), se usa el metodo siguiente para
        poder obtener el listado con fotos (tipo: JugadoresModel2).*/
        [RelayCommand]
        public async Task ObtenerJugadoresFoto()
        {
            ListJug.Clear();
            foreach (var l in ListadoJugadores)
            {
                SetAvatarFromBase64(l.Avatar);
                var jugador1 = new JugadoresModel2(l.Jugador_id, l.Nombre, l.Apellidos, l.Posicion, 
                    AvatarImage, l.EquiposModel, l.Nacionalidad);       
                ListJug.Add(jugador1);
            }
        }

        //metodo que cambia de JugadorModel2 a JugadorModel
        public async Task CambiarAJugadorModel(JugadoresModel2 jugModel2)
        {
            Jugador.Jugador_id = jugModel2.Jugador_id;
            Jugador.Nombre = jugModel2.Nombre;
            Jugador.Apellidos = jugModel2.Apellidos;
            Jugador.Posicion = jugModel2.Posicion;       
            Jugador.EquiposModel = jugModel2.EquiposModel;
            Jugador.Nacionalidad = jugModel2.Posicion;
            //obtener imagen a base64 desde un ImageSource
            Jugador.Avatar = await GetBase64FromAvatarAsync();
        }

        [RelayCommand]
        public async Task EliminarJugador()
        {
            await CambiarAJugadorModel(JugadorModel2);
            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/partidos/eliminarJugador",
                                                    data: Jugador,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                App.Current.MainPage.DisplayAlert("Se ha eliminado con éxito", response.Message, "ACEPTAR");
                await ObtenerAllJugadores();
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error al eliminar", response.Message, "ACEPTAR");
            }
        }

        //FILTROS JUGADORES

        //obtener todos los jugadores
        [RelayCommand]
        public async Task ObtenerAllJugadores()
        {
            CambiarModo("jugadores");
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/obtenerAllJugadores",
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                ListadoJugadores =
                          JsonConvert.DeserializeObject<ObservableCollection<JugadoresModel>>(response.Data.ToString());
                await ObtenerJugadoresFoto();
            }
        }

        //obtener jugadores por filtro de nombre
        [RelayCommand]
        public async Task FiltroJugadorNombre(string nombre)
        {
            CambiarModo("jugador");
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/partidos/buscarJugadorNombre/" + nombre,
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);

            if (response.Success == 0)
            {
                ListadoJugadores =
                          JsonConvert.DeserializeObject<ObservableCollection<JugadoresModel>>(response.Data.ToString());
                //cuando se obtenga ListaJugadores (que es de tipo JugadoresModel), se usa el metodo siguiente para
                //poder obtener el listado con fotos (tipo: JugadoresModel2).
                await ObtenerJugadoresFoto();
            }
            else
            {
                await ObtenerAllJugadores();
                await ObtenerJugadoresFoto();
            }

        }

        //filtro para obtener todos los jugadores del mismo equipo usando el nombre del equipo
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
                ListadoJugadores =
                          JsonConvert.DeserializeObject<ObservableCollection<JugadoresModel>>(response.Data.ToString());
                await ObtenerJugadoresFoto();
            }
            else
            {
                await ObtenerAllJugadores();
                await ObtenerJugadoresFoto();
            }
        }
    }
}
