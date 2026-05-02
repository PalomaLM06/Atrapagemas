

using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public DiceManager diceManager;
    public BoardManager boardManager;
    public List<PlayerController> jugadoresVisuales;

    private List<Jugador> jugadoresDatos;
    private int turnoActual = 0;

    private void Start()
    {
        jugadoresDatos = DBManager.Instance.SelectJugadores();
        PosicionarJugadoresIniciales();
    }

    private void PosicionarJugadoresIniciales()
    {
        for (int i = 0; i < jugadoresDatos.Count; i++)
        {
            int ordenCasilla = jugadoresDatos[i].IdCasilla;
            Vector3 posicion = boardManager.ObtenerPosicionPorOrden(ordenCasilla);

            jugadoresVisuales[i].idJugador = jugadoresDatos[i].Id;
            jugadoresVisuales[i].casillaActual = ordenCasilla;
            jugadoresVisuales[i].MoverJugador(posicion);
        }
    }

    public void JugarTurno()
    {
        PlayerController jugadorVisual = jugadoresVisuales[turnoActual];
        Jugador jugadorDatos = jugadoresDatos[turnoActual];

        int dado = diceManager.LanzarDado();
        int nuevaCasilla = jugadorVisual.casillaActual + dado;

        if (nuevaCasilla > 32)
        {
            nuevaCasilla = 32;
        }

        Casilla casillaDestino = DBManager.Instance.GetCasillaPorOrden(nuevaCasilla);

        Vector3 nuevaPosicion = boardManager.ObtenerPosicionPorOrden(nuevaCasilla);
        jugadorVisual.MoverJugador(nuevaPosicion);
        jugadorVisual.casillaActual = nuevaCasilla;

        DBManager.Instance.ActualizarPosicionJugador(
            jugadorDatos.Id,
            casillaDestino.Orden,
            casillaDestino.X,
            casillaDestino.Y
        );

        RevisarCasillaEspecial(casillaDestino);

        PasarTurno();
    }

    private void RevisarCasillaEspecial(Casilla casilla)
    {
        Debug.Log("Tipo de casilla: " + casilla.Tipo);

        if (casilla.Tipo == "Normal")
        {
            Debug.Log("Casilla normal.");
        }
        else if (casilla.Tipo == "ReRoll")
        {
            Debug.Log("El jugador lanza otra vez.");
        }
        else if (casilla.Tipo == "SkipTurn")
        {
            Debug.Log("El jugador pierde un turno.");
        }
        else if (casilla.Tipo == "Swap")
        {
            Debug.Log("El jugador intercambia posición.");
        }
        else
        {
            Reto reto = DBManager.Instance.GetRetoPorId(casilla.IdReto);

            if (reto != null)
            {
                Debug.Log("Reto: " + reto.Descripcion);
            }
        }
    }

    private void PasarTurno()
    {
        turnoActual++;

        if (turnoActual >= jugadoresVisuales.Count)
        {
            turnoActual = 0;
        }

        Debug.Log("Siguiente turno: Jugador " + jugadoresVisuales[turnoActual].idJugador);
    }
}