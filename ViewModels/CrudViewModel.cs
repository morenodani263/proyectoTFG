using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateMatch.Models;

namespace UltimateMatch.ViewModels
{
    internal partial class CrudViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<JugadoresModel> listadoJugadores;

        [ObservableProperty]
        private bool isModoJugadorEnabled;
        [ObservableProperty]
        private bool isModoCompeticionEnabled;
        [ObservableProperty]
        private bool isModoEquipoEnabled;

        [ObservableProperty]
        private bool isModoGestion;
        public CrudViewModel() {
            ListadoJugadores = new ObservableCollection<JugadoresModel>();
            RellenoListado();
            IsModoJugadorEnabled = true;
            IsModoCompeticionEnabled = false;
            IsModoEquipoEnabled = false;
            IsModoGestion = false;
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
            IsModoGestion = !IsModoGestion;
        }

        public void RellenoListado()
        {
            EquipoModel equipoModel = new EquipoModel(1, "Ciruelos", "");
            ListadoJugadores.Add(new JugadoresModel(1 , "Florian", "Wirtz", "MCO", "", equipoModel) { });
            ListadoJugadores.Add(new JugadoresModel(2, "Jamal", "Musiala", "EI", "", equipoModel) { });
            ListadoJugadores.Add(new JugadoresModel(3, "Lamine", "Yamal", "ED", "", equipoModel) { });
        }
    }
}
