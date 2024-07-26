using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UltimateMatch.Models;
using UltimateMatch.Resources.Utils;
using UltimateMatch.Services;
using UltimateMatch.Views.Popups;


namespace UltimateMatch.ViewModels
{
    partial class RegisterViewModel: ObservableObject
    {
        [ObservableProperty]
        private bool paso1;

        [ObservableProperty]
        private bool paso2;

        [ObservableProperty]
        private bool paso3;

        [ObservableProperty]
        private bool paso4;

        [ObservableProperty]
        private string avatarImage64;

        [ObservableProperty]
        private ImageSource avatarImage;

        [ObservableProperty]
        private UsuarioModel usuario;

        [ObservableProperty]
        private string confPasswd;

        [ObservableProperty]
        private string mensajesError;

        [ObservableProperty]
        private string codigoRol;

        [ObservableProperty]
        private PopupRegistroErrores popupErrores;
        public RegisterViewModel()
        {
            Paso1 = true;
            Paso2 = false;
            Paso3 = false;
            Paso4 = false;
            Usuario = new UsuarioModel();
            MensajesError = "errores listado";
           
            
        }

        public async Task ShowPopupErrores()
        {
            PopupErrores = new PopupRegistroErrores();
            await Application.Current.MainPage
                .ShowPopupAsync(PopupErrores);

        }
        
        [RelayCommand]
        public async Task RegistroUsuario()
        {
            //Le pongo un id predeterminado, ya que luego se le establece uno en la api
            Usuario.Usuario_id = 1;
            Usuario.FechaNacimiento = Usuario.FechaNacimiento.Replace(" 0:00:00", "");
            Usuario.Avatar = AvatarImage64;
            Usuario.Rol = "";
            await EstablecerRol();
            RequestModel request = new RequestModel(method: "POST",
                                                    route:"/auth/register",
                                                    data: Usuario,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);
            //si es 0 registro exitoso, sino error al registrar
            if(response.Success == 0)
            {
                await Navegar("LoginPage");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("ERROR AL REGISTRAR",
                           "No se ha podido registrar el usuario, por favor vuelva a intentarlo", "ACEPTAR");
            }
           
        }

        [RelayCommand]
        public async Task Navegar(string nombrePagina)
        {
            await Shell.Current.GoToAsync("//" + nombrePagina, new Dictionary<string, object>()
            {
                ["User"] = Usuario
            });
        }

        [RelayCommand]
        public async Task EstablecerRol()
        {
            
            RequestModel request = new RequestModel(method: "GET",
                                                    route: "/auth/rol/" + CodigoRol,
                                                    data: String.Empty,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);
            //si es 0 registro exitoso, sino error al registrar
            if(response.Success == 0)
            {
                Usuario.Rol = "administrador";
            }
            else
            {
                Usuario.Rol = "invitado";
            }
        }



        public bool ValidarNombre()
        {
            if (string.IsNullOrWhiteSpace(Usuario.Nombre) || Usuario.Nombre.Length > 100)
            {
                //si el campo esta vacío concatena una cadena y sino la otra
                MensajesError += string.IsNullOrWhiteSpace(Usuario.Nombre) ? "El campo nombre no puede estar vacío\n\n" :
                    "El campo nombre no puede tener más de 100 caracteres\n\n";
                return true; // Retorna true si el campo está vacío o excede la longitud máxima
            }
            return false; // Retorna false si el campo es válido
        }


        public bool ValidarApellidos()
        {
            if (string.IsNullOrWhiteSpace(Usuario.Apellidos) || Usuario.Apellidos.Length > 150)
            {
                //si el campo esta vacío concatena una cadena y sino la otra
                MensajesError += string.IsNullOrWhiteSpace(Usuario.Apellidos) ? "El campo apellidos no puede estar vacío\n\n" :
                    "El campo apellidos no puede tener más de 150 caracteres\n\n";
                return true; // Retorna true si el campo está vacío o excede la longitud máxima
            }
            return false; // Retorna false si el campo es válido


        }

        public bool ValidarTfno()
        {
            string patronTfno = @"^\d{9}$";
            Regex regex = new Regex(patronTfno);
            //comparar con el metodo IsMatch
            if (!regex.IsMatch(Usuario.Tfno))
            {
                return true;
            }
            return false;
        }

        public bool ValidarNombreUsuario()
        {
            if (string.IsNullOrWhiteSpace(Usuario.NombreUsuario) || Usuario.NombreUsuario.Length > 100)
            {
                //si el campo esta vacío concatena una cadena y sino la otra
                MensajesError += string.IsNullOrWhiteSpace(Usuario.NombreUsuario) ? "El campo nombre usuario no puede estar vacío\n\n" :
                    "El campo nombre usuario no puede tener más de 100 caracteres\n\n";
                return true; // Retorna true si el campo está vacío o excede la longitud máxima
            }
            return false; // Retorna false si el campo es válido
        }

        public bool ValidarCorreo()
        {
            if (string.IsNullOrWhiteSpace(Usuario.Correo) || Usuario.Correo.Length > 100)
            {
                //si el campo esta vacío concatena una cadena y sino la otra
                MensajesError += string.IsNullOrWhiteSpace(Usuario.Correo) ? "El campo correo no puede estar vacío\n\n" :
                    "El campo correo no puede tener más de 100 caracteres\n\n";
                return true; // Retorna true si el campo está vacío o excede la longitud máxima
            }

            string patronCorreo = "^[\\w\\.-]+@[a-zA-Z\\d-]+(\\.[a-zA-Z\\d-]+)*\\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(patronCorreo);
            //comparar con el metodo IsMatch
            if (!regex.IsMatch(Usuario.Correo))
            {
                return true;
            }
            return false;
        }

        public bool ValidarPassword()
        {
            if (string.IsNullOrWhiteSpace(Usuario.Password) || Usuario.Password.Length > 20)
            {
                //si el campo esta vacío concatena una cadena y sino la otra
                MensajesError += string.IsNullOrWhiteSpace(Usuario.Password) ? "El campo contraseña no puede estar vacío\n\n" :
                    "El campo contraseña no puede tener más de 20 caracteres\n\n";
                return true; // Retorna true si el campo está vacío o excede la longitud máxima
            }
            return false; // Retorna false si el campo es válido
        }

        public bool ValidarConfPasswd()
        {
            if (string.IsNullOrWhiteSpace(ConfPasswd) || ConfPasswd.Length > 20)
            {
                //si el campo esta vacío concatena una cadena y sino la otra
                MensajesError += string.IsNullOrWhiteSpace(ConfPasswd) ? "El campo confirmar contraseña no puede estar vacío\n\n" :
                    "El campo confirmar contraseña no puede tener más de 20 caracteres\n\n";
                return true; // Retorna true si el campo está vacío o excede la longitud máxima
            }else if (!Usuario.Password.Equals(ConfPasswd))
            {
                MensajesError += "Las contraseñas no coinciden";
                return true;              
            }
            return false; // Retorna false si el campo es válido
        }

        public bool ValidarPais()
        {
            if (string.IsNullOrWhiteSpace(Usuario.Pais) || Usuario.Pais.Length > 100)
            {
                //si el campo esta vacío concatena una cadena y sino la otra
                MensajesError += string.IsNullOrWhiteSpace(Usuario.Pais) ? "El campo país no puede estar vacío\n\n" :
                    "El campo país no puede tener más de 100 caracteres\n\n";
                return true; // Retorna true si el campo está vacío o excede la longitud máxima
            }
            return false; // Retorna false si el campo es válido
        }

        public bool ValidarProvincia()
        {
            if (string.IsNullOrWhiteSpace(Usuario.Provincia) || Usuario.Provincia.Length > 100)
            {
                //si el campo esta vacío concatena una cadena y sino la otra
                MensajesError += string.IsNullOrWhiteSpace(Usuario.Provincia) ? "El campo provincia no puede estar vacío\n\n" :
                    "El campo provincia no puede tener más de 100 caracteres\n\n";
                return true; // Retorna true si el campo está vacío o excede la longitud máxima
            }
            return false; // Retorna false si el campo es válido
        }

        public bool ValidarCalle()
        {
            if (string.IsNullOrWhiteSpace(Usuario.Calle) || Usuario.Calle.Length > 120)
            {
                //si el campo esta vacío concatena una cadena y sino la otra
                MensajesError += string.IsNullOrWhiteSpace(Usuario.Calle) ? "El campo calle no puede estar vacío\n\n" :
                    "El campo calle no puede tener más de 120 caracteres\n\n";
                return true; // Retorna true si el campo está vacío o excede la longitud máxima
            }
            return false; // Retorna false si el campo es válido
        }

        public bool ValidarCP()
        {
            if (string.IsNullOrWhiteSpace(Usuario.CP) || Usuario.CP.Length > 30)
            {
                //si el campo esta vacío concatena una cadena y sino la otra
                MensajesError += string.IsNullOrWhiteSpace(Usuario.CP) ? "El campo código postal no puede estar vacío\n\n" :
                    "El campo código postal no puede tener más de 30 caracteres\n\n";
                return true; // Retorna true si el campo está vacío o excede la longitud máxima
            }
            return false; // Retorna false si el campo es válido
        }

        public bool ValidarPantalla1()
        {
            if (ValidarNombre() || ValidarApellidos() || ValidarTfno())
            {
                return true;
            }
            return false;
        }

        public bool ValidarPantalla2()
        {
            if (ValidarNombreUsuario() || ValidarCorreo() || ValidarPassword() || ValidarConfPasswd())
            {
                return true;
            }
            return false;
        }

        public bool ValidarPantalla3()
        {
            if (ValidarPais() || ValidarProvincia() || ValidarCalle() || ValidarCP())
            {
                return true;
            }
            return false;
        }


        [RelayCommand]
        public void CambioPantalla(string cambioPantalla)
        {
            switch (cambioPantalla)
            {
                case "next":
                    if (Paso1)
                    {
                        if (ValidarPantalla1())
                        {
                            ShowPopupErrores();
                            return;
                        }
                        Paso1 = false;
                        Paso2 = true;
                        Paso3 = false;
                        Paso4 = false;
                    }
                    else if (Paso2)
                    {
                        if (ValidarPantalla2())
                        {
                            ShowPopupErrores();
                            return;
                        }
                        Paso1 = false;
                        Paso2 = false;
                        Paso3 = true;
                        Paso4 = false;
                    }
                    else if (Paso3)
                    {
                        if (ValidarPantalla3())
                        {
                            ShowPopupErrores();
                            return;
                        }
                        Paso1 = false;
                        Paso2 = false;
                        Paso3 = false;
                        Paso4 = true;
                    }
                    break;

                case "back":
                    if (Paso2)
                    {
                        Paso1 = true;
                        Paso2 = false;
                        Paso3 = false;
                        Paso4 = false;
                    }
                    else if (Paso3)
                    {
                        Paso1 = false;
                        Paso2 = true;
                        Paso3 = false;
                        Paso4 = false;
                    }
                    else if (Paso4)
                    {
                        Paso1 = false;
                        Paso2 = false;
                        Paso3 = true;
                        Paso4 = false;
                    }
                    break;

                default:
                    break;
            }
        }

        //[RelayCommand]
        //public void CambioPantalla(string cambioPantalla)
        //{
        //    if(Paso1 && cambioPantalla.Equals("next"))
        //    {
        //        if (ValidarPantalla1())
        //        {

        //        }
        //        else
        //        {
        //            Paso1 = false;
        //            Paso2 = true;
        //            Paso3 = false;
        //            Paso4 = false;
        //        }        
        //    }
        //    else if (Paso2 && cambioPantalla.Equals("next"))
        //    {
        //        Paso1 = false;
        //        Paso2 = false;
        //        Paso3 = true;
        //        Paso4 = false;
        //    }
        //    else if (Paso2 && cambioPantalla.Equals("back"))
        //    {
        //        Paso1 = true;
        //        Paso2 = false;
        //        Paso3 = false;
        //        Paso4 = false;
        //    }
        //    else if (Paso3 && cambioPantalla.Equals("back"))
        //    {
        //        Paso1 = false;
        //        Paso2 = true;
        //        Paso3 = false;
        //        Paso4 = false;
        //    }
        //    else if (Paso3 && cambioPantalla.Equals("next"))
        //    {
        //        Paso1 = false;
        //        Paso2 = false;
        //        Paso3 = false;
        //        Paso4 = true;
        //    }
        //    else if (Paso4 && cambioPantalla.Equals("back"))
        //    {
        //        Paso1 = false;
        //        Paso2 = false;
        //        Paso3 = true;
        //        Paso4 = false;
        //    }
        //    else
        //    {

        //    }
        //}


        /*[RelayCommand]
        public async Task Navegar(string nombrePagina)
        {
            await Shell.Current.GoToAsync("//" + nombrePagina, new Dictionary<string, object>()
            {
                ["User"] = User
            });
        }*/

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


    }
}
