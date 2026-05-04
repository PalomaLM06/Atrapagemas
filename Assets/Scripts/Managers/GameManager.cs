using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        List<Jugador> jugadores = DBManager.Instance.SelectJugadores();

        foreach (Jugador jugador in jugadores)
        {
            Debug.Log("Jugador: " + jugador.Nombre + " | Casilla: " + jugador.IdCasilla);
        }
    }
}