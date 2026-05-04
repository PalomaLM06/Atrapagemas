using UnityEngine;
using System.Collections.Generic;

public class DBManager : MonoBehaviour
{
    public static DBManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 🔹 Simulación de base de datos
    private List<Jugador> jugadores = new List<Jugador>()
    {
        new Jugador { Id = 1, IdCasilla = 0 },
        new Jugador { Id = 2, IdCasilla = 0 }
    };

    private List<Casilla> casillas = new List<Casilla>()
    {
        new Casilla { Orden = 0, Tipo = "Normal", X = 0, Y = 0 },
        new Casilla { Orden = 1, Tipo = "Normal", X = 1, Y = 0 },
        new Casilla { Orden = 2, Tipo = "ReRoll", X = 2, Y = 0 },
        new Casilla { Orden = 3, Tipo = "Reto", IdReto = 1, X = 3, Y = 0 }
    };

    private List<Reto> retos = new List<Reto>()
    {
        new Reto { Id = 1, Descripcion = "Haz 10 flexiones" }
    };

    // 🔹 Métodos que usa tu GameManager

    public List<Jugador> SelectJugadores()
    {
        return jugadores;
    }

    public Casilla GetCasillaPorOrden(int orden)
    {
        return casillas.Find(c => c.Orden == orden);
    }

    public void ActualizarPosicionJugador(int id, int orden, float x, float y)
    {
        Jugador j = jugadores.Find(jug => jug.Id == id);
        if (j != null)
        {
            j.IdCasilla = orden;
            Debug.Log($"Jugador {id} actualizado a casilla {orden}");
        }
    }

    public Reto GetRetoPorId(int id)
    {
        return retos.Find(r => r.Id == id);
    }
}