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
    internal partial class JugadoresViewModel : ObservableObject
    {
        [ObservableProperty]
        private UsuarioModel user = new UsuarioModel();

        [ObservableProperty]
        private ObservableCollection<EquipoModel2> listadoEquipos2;

        [ObservableProperty]
        private EquipoModel2 equipoActual;

        [ObservableProperty]
        private ImageSource avatarImage;

        [ObservableProperty]
        private ImageSource avatarImage64;

        [ObservableProperty]
        private JugadoresModel jugador;

        [ObservableProperty]
        private EquipoModel equipo;

        [ObservableProperty]
        private ObservableCollection<JugadoresModel> listaJugadores;

        [ObservableProperty]
        private ObservableCollection<JugadoresModel2> listaJugadores2;

        [ObservableProperty]
        private JugadoresModel2 jugador2;

        [ObservableProperty]
        private ObservableCollection<EquipoModel> listaEquipos;

        [ObservableProperty]
        private bool isModoJugador;

        [ObservableProperty]
        private bool isModoEquipo;

        [ObservableProperty]
        private bool isGestionVisible;

        public JugadoresViewModel() {
            Jugador = new JugadoresModel();
            Equipo = new EquipoModel();
            ListaJugadores = new ObservableCollection<JugadoresModel>();
            ListaEquipos = new ObservableCollection<EquipoModel>();
            IsModoJugador = true;
            IsModoEquipo = false;
            ListadoEquipos2 = new ObservableCollection<EquipoModel2>();
            EquipoActual = new EquipoModel2();

            ListaJugadores.Add(new JugadoresModel(1, "Pedro", "Diaz", "DC", "", new EquipoModel() { }, "España"));
            ListaJugadores.Add(new JugadoresModel(1, "Juanito", "Juan", "MC", "", new EquipoModel() { }, "España"));
            ListaEquipos.Add(new EquipoModel(1, "Ciru FS", ""));
            ListaEquipos.Add(new EquipoModel(2, "Yepes FS", ""));

            

        }

        [RelayCommand]
        public async Task Navegar(string nombrePagina)
        {
            await Shell.Current.GoToAsync("//" + nombrePagina, new Dictionary<string, object>()
            {
                ["User"] = User
            });
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
            if (modo == "jugador")
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
                                                    route: "/partidos/buscarJugadorNombre/" + nombre,
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

        public ImageSource base64ToImageSource (string avatar)
        {
            // Convertir la cadena Base64 en un array de bytes
            //byte[] imageBytes = Convert.FromBase64String(avatar);

            byte[] imageBytes = FromBase64String(avatar);

            //byte[] imageBytes = System.Convert.FromBase64String(avatar);


            // Crear un flujo de memoria a partir del array de bytes
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                // Crear una ImageSource desde el flujo de memoria
                ImageSource imageSource1 = ImageSource.FromStream(() => ms);



                return imageSource1;

                // Agregar el control de Image a tu diseño
                // Por ejemplo, si estás dentro de un StackLayout llamado "stackLayout", puedes agregarlo así:
                // stackLayout.Children.Add(imageControl);
            }
        }

        public static byte[] FromBase64String(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                throw new ArgumentNullException(nameof(base64String), "Base64 string cannot be null or empty.");
            }

            // Eliminar cualquier esquema de URL de datos (por ejemplo, data:image/png;base64,).
            if (base64String.Contains(","))
            {
                base64String = base64String.Substring(base64String.IndexOf(",") + 1);
            }



            int length = base64String.Length;

            // Asegurarse de que la longitud sea un múltiplo de 4
            if (length % 4 != 0)
            {
                throw new FormatException("Invalid Base64 string length.");
            }
            int padding = base64String[length - 2] == '=' ? 2 : base64String[length - 1] == '=' ? 1 : 0;
            int size = (length * 3) / 4 - padding;
            byte[] buffer = new byte[size];

            int offset = 0;
            for (int i = 0; i < length; i += 4)
            {
                int a = Base64CharIndex(base64String[i]);
                int b = Base64CharIndex(base64String[i + 1]);
                int c = Base64CharIndex(base64String[i + 2]);
                int d = Base64CharIndex(base64String[i + 3]);

                buffer[offset++] = (byte)((a << 2) | (b >> 4));
                if (c < 64)
                {
                    buffer[offset++] = (byte)((b << 4) | (c >> 2));
                    if (d < 64)
                    {
                        buffer[offset++] = (byte)((c << 6) | d);
                    }
                }
            }

            return buffer;
        }

        private static int Base64CharIndex(char c)
        {
            if (c >= 'A' && c <= 'Z') return c - 'A';
            if (c >= 'a' && c <= 'z') return c - 'a' + 26;
            if (c >= '0' && c <= '9') return c - '0' + 52;
            if (c == '+') return 62;
            if (c == '/') return 63;
            if (c == '=') return 0; // Padding
            throw new ArgumentException($"Invalid character '{c}' in Base64 string.", nameof(c));
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
    
