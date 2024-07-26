﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    internal partial class LoginViewModel:ObservableObject
    {
        [ObservableProperty]
        private UsuarioModel user;

        [ObservableProperty]
        private string base64Image = "data:image/png;base64,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";


        public LoginViewModel() { 
            user = new UsuarioModel();
        }

        [RelayCommand]
        public async Task Login()
        {
            //Le pongo un id predeterminado, ya que luego se le establece uno en la api
            User.Usuario_id = 1;
            RequestModel request = new RequestModel(method: "POST",
                                                    route: "/auth/login",
                                                    data: User,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);
            //si es 0 login exitoso, sino error al iniciar sesion
            if(response.Success == 0)
            {
                await ObtenerUsuario();
                await Navegar("PartidosPage");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("ERROR AL INICIAR SESIÓN",
                response.Message, "ACEPTAR");
            }
            
            
        }

      
        public async Task ObtenerUsuario()
        {
            //Le pongo un id predeterminado, ya que luego se le establece uno en la api
            User.Usuario_id = 1;
            RequestModel request = new RequestModel(method: "Get",
                                                    route: "/auth/user/"+User.NombreUsuario,
                                                    data: string.Empty,
                                                    server: APIService.URL_API);
            ResponseModel response = await APIService.ExecuteRequest(request);
            //si es 0 login exitoso, sino error al iniciar sesion
            if (response.Success == 0)
            {
                User = JsonConvert.DeserializeObject<UsuarioModel>(response.Data.ToString());
            }
            


        }

        [RelayCommand]
        public async Task Navegar(string nombrePagina)
        {
            await Shell.Current.GoToAsync("//" + nombrePagina, new Dictionary<string, object>()
            {
                ["User"] = User
            });
        }
    }
}
