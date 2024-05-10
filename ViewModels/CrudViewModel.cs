using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateMatch.Models;

namespace UltimateMatch.ViewModels
{
    //kbkbu
    internal partial class CrudViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<JugadoresModel> listadoJugadores;

        public CrudViewModel() {
            ListadoJugadores = new ObservableCollection<JugadoresModel>();
            RellenoListado();
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
