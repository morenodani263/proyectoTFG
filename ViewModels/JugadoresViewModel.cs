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
    internal partial class JugadoresViewModel:ObservableObject
    {
        [ObservableProperty]
        private JugadoresModel jugador;

        [ObservableProperty]
        private ObservableCollection<JugadoresModel> listaJugadores;

        public JugadoresViewModel() { 
            Jugador = new JugadoresModel();
            ListaJugadores = new ObservableCollection<JugadoresModel>();

            ListaJugadores.Add(new JugadoresModel(1, "Pedro", "Diaz", "DC", "", new EquipoModel(){}, "España"));
            ListaJugadores.Add(new JugadoresModel(1, "Juanito", "Juan", "MC", "", new EquipoModel() { }, "España"));

        }
    }
}
