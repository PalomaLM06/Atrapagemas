using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Jugador> jugadores;
    public List<Casilla> casillas;
    public List<Reto> retos;

    private int turnoActual = 0;
    private bool juegoTerminado = false;

    private string escenaTablero;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        escenaTablero = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        if (jugadores != null && jugadores.Count > 0)
        {
            return;
        }

        if (DBManager.Instance == null)
        {
            Debug.LogError("No existe DBManager en la escena.");
            return;
        }

        if (!DBManager.Instance.EstaConectado())
        {
            Debug.LogError("DBManager existe, pero no está conectado a la base de datos.");
            return;
        }

        DBManager.Instance.ResetearJugadores();
        jugadores = DBManager.Instance.SelectJugadores();
        casillas = DBManager.Instance.SelectCasillas();
        retos = DBManager.Instance.SelectRetos();

        Debug.Log("Juego iniciado usando base de datos SQLite.");
        Debug.Log("Jugadores cargados desde SQLite: " + jugadores.Count);
        Debug.Log("Casillas cargadas desde SQLite: " + casillas.Count);
        Debug.Log("Retos cargados desde SQLite: " + retos.Count);

        if (jugadores.Count > 0)
        {
            Debug.Log("Turno inicial: " + jugadores[turnoActual].Nombre);
        }
        else
        {
            Debug.LogError("No se cargaron jugadores desde la base de datos.");
        }

        Debug.Log("Escena de tablero guardada: " + escenaTablero);
    }

    public void TirarDado()
    {
        if (juegoTerminado)
        {
            Debug.Log("El juego ya terminó.");
            return;
        }

        if (jugadores == null || jugadores.Count == 0)
        {
            Debug.LogError("No hay jugadores cargados.");
            return;
        }

        Jugador jugador = jugadores[turnoActual];

        if (jugador.PierdeTurno)
        {
            Debug.Log(jugador.Nombre + " pierde este turno.");

            jugador.PierdeTurno = false;
            CambiarTurno();
            return;
        }

        Debug.Log("Abriendo escena Dados para: " + jugador.Nombre);

        SceneManager.LoadScene("Dados");
    }

    public void ProcesarResultadoDado(int dado)
    {
        if (juegoTerminado)
        {
            Debug.Log("El juego ya terminó.");
            return;
        }

        if (jugadores == null || jugadores.Count == 0)
        {
            Debug.LogError("No hay jugadores cargados.");
            return;
        }

        Jugador jugador = jugadores[turnoActual];

        Debug.Log("--------------------------------");
        Debug.Log("Turno de: " + jugador.Nombre);
        Debug.Log(jugador.Nombre + " tiró el dado y sacó: " + dado);

        MoverJugador(jugador, dado);
        AplicarEfectoCasilla(jugador);
        RevisarVictoria(jugador);

        if (!juegoTerminado)
        {
            CambiarTurno();
        }

        Debug.Log("Regresando a escena de tablero: " + escenaTablero);

        SceneManager.LoadScene(escenaTablero);
    }

    void MoverJugador(Jugador jugador, int pasos)
    {
        int nuevaCasilla = jugador.IdCasilla + pasos;

        if (nuevaCasilla > casillas.Count)
        {
            nuevaCasilla = casillas.Count;
        }

        jugador.IdCasilla = nuevaCasilla;

        Casilla casillaActual = casillas.Find(c => c.Id == jugador.IdCasilla);

        if (casillaActual != null)
        {
            jugador.X = casillaActual.X;
            jugador.Y = casillaActual.Y;

            Debug.Log(jugador.Nombre + " se movió a la casilla " + casillaActual.Orden + " de tipo " + casillaActual.Tipo);

            GuardarPosicionJugador(jugador);
        }
        else
        {
            Debug.LogError("No se encontró la casilla con Id: " + jugador.IdCasilla);
        }
    }

    void AplicarEfectoCasilla(Jugador jugador)
    {
        Casilla casillaActual = casillas.Find(c => c.Id == jugador.IdCasilla);

        if (casillaActual == null)
        {
            Debug.LogError("No se encontró la casilla actual.");
            return;
        }

        Reto retoActual = retos.Find(r => r.Id == casillaActual.IdReto);

        Debug.Log(jugador.Nombre + " cayó en la casilla " + casillaActual.Orden + " de tipo " + casillaActual.Tipo);

        if (retoActual != null)
        {
            Debug.Log("Tipo de reto: " + retoActual.TipoCasilla);
            Debug.Log("Descripción del reto: " + retoActual.Descripcion);
        }
        else
        {
            Debug.LogWarning("Esta casilla no tiene reto encontrado. IdReto: " + casillaActual.IdReto);
        }

        switch (casillaActual.Tipo)
        {
            case "Start":
                Debug.Log("Inicio del tablero.");
                break;

            case "Normal":
                Debug.Log("Casilla normal. No pasa nada.");
                break;

            case "1vs1":
                Ejecutar1vs1(jugador);
                break;

            case "DesafioPersonal":
                EjecutarDesafioPersonal(jugador);
                break;

            case "TodosVsTodos":
                EjecutarTodosVsTodos(jugador);
                break;

            case "MitadvsMitad":
                EjecutarMitadvsMitad(jugador);
                break;

            case "ReRoll":
                EjecutarReRoll(jugador);
                break;

            case "Portal":
                EjecutarPortal(jugador);
                break;

            case "Swap":
                EjecutarSwap(jugador);
                break;

            case "SkipTurn":
                EjecutarSkipTurn(jugador);
                break;

            case "Cofre":
                EjecutarCofre(jugador);
                break;

            default:
                Debug.LogWarning("Tipo de casilla no reconocido: " + casillaActual.Tipo);
                break;
        }
    }

    void Ejecutar1vs1(Jugador jugador)
    {
        Debug.Log("Inicia reto 1vs1.");

        List<Jugador> rivales = jugadores.FindAll(j => j.Id != jugador.Id);

        if (rivales.Count == 0)
        {
            Debug.Log("No hay rivales disponibles.");
            return;
        }

        Jugador rival = rivales[Random.Range(0, rivales.Count)];

        int numeroSistema = Random.Range(1, 11);
        int numeroJugador = Random.Range(1, 11);
        int numeroRival = Random.Range(1, 11);

        int diferenciaJugador = Mathf.Abs(numeroSistema - numeroJugador);
        int diferenciaRival = Mathf.Abs(numeroSistema - numeroRival);

        Debug.Log("Número del sistema: " + numeroSistema);
        Debug.Log(jugador.Nombre + " eligió/sacó: " + numeroJugador);
        Debug.Log(rival.Nombre + " eligió/sacó: " + numeroRival);

        if (diferenciaJugador < diferenciaRival)
        {
            jugador.Gemas += 3;
            GuardarGemasJugador(jugador);

            Debug.Log(jugador.Nombre + " ganó el 1vs1 y recibe 3 gemas. Total: " + jugador.Gemas);
        }
        else if (diferenciaRival < diferenciaJugador)
        {
            rival.Gemas += 3;
            GuardarGemasJugador(rival);

            Debug.Log(rival.Nombre + " ganó el 1vs1 y recibe 3 gemas. Total: " + rival.Gemas);
        }
        else
        {
            Debug.Log("El 1vs1 terminó en empate. Nadie gana gemas.");
        }
    }

    void EjecutarDesafioPersonal(Jugador jugador)
    {
        Debug.Log("Inicia desafío personal.");

        int resultado = Random.Range(1, 7);

        Debug.Log(jugador.Nombre + " obtuvo resultado de desafío: " + resultado);

        if (resultado >= 4)
        {
            jugador.Gemas += 2;
            GuardarGemasJugador(jugador);

            Debug.Log(jugador.Nombre + " superó el desafío personal y ganó 2 gemas. Total: " + jugador.Gemas);
        }
        else
        {
            RetrocederJugador(jugador, 1);
            Debug.Log(jugador.Nombre + " falló el desafío personal y retrocede 1 casilla.");
        }
    }

    void EjecutarTodosVsTodos(Jugador jugadorActual)
    {
        Debug.Log("Inicia reto TodosVsTodos.");

        Jugador ganador = null;
        int mejorDado = 0;
        bool empate = false;

        foreach (Jugador jugador in jugadores)
        {
            int dado = Random.Range(1, 7);
            Debug.Log(jugador.Nombre + " sacó " + dado);

            if (dado > mejorDado)
            {
                mejorDado = dado;
                ganador = jugador;
                empate = false;
            }
            else if (dado == mejorDado)
            {
                empate = true;
            }
        }

        if (empate)
        {
            Debug.Log("Hubo empate en TodosVsTodos. Nadie gana gemas.");
        }
        else if (ganador != null)
        {
            ganador.Gemas += 4;
            GuardarGemasJugador(ganador);

            Debug.Log(ganador.Nombre + " ganó TodosVsTodos y recibe 4 gemas. Total: " + ganador.Gemas);
        }
    }

    void EjecutarMitadvsMitad(Jugador jugadorActual)
    {
        Debug.Log("Inicia reto MitadvsMitad.");

        int equipo1 = 0;
        int equipo2 = 0;

        for (int i = 0; i < jugadores.Count; i++)
        {
            int dado = Random.Range(1, 7);

            if (i % 2 == 0)
            {
                equipo1 += dado;
                Debug.Log(jugadores[i].Nombre + " suma " + dado + " para Equipo 1.");
            }
            else
            {
                equipo2 += dado;
                Debug.Log(jugadores[i].Nombre + " suma " + dado + " para Equipo 2.");
            }
        }

        Debug.Log("Total Equipo 1: " + equipo1);
        Debug.Log("Total Equipo 2: " + equipo2);

        if (equipo1 > equipo2)
        {
            DarGemasAEquipo(0, 2);
            Debug.Log("Equipo 1 ganó. Sus jugadores reciben 2 gemas.");
        }
        else if (equipo2 > equipo1)
        {
            DarGemasAEquipo(1, 2);
            Debug.Log("Equipo 2 ganó. Sus jugadores reciben 2 gemas.");
        }
        else
        {
            Debug.Log("Empate en MitadvsMitad. Nadie gana gemas.");
        }
    }

    void EjecutarReRoll(Jugador jugador)
    {
        int dadoExtra = Random.Range(1, 7);

        Debug.Log(jugador.Nombre + " obtuvo ReRoll y lanzó otra vez: " + dadoExtra);

        MoverJugador(jugador, dadoExtra);

        Casilla nuevaCasilla = casillas.Find(c => c.Id == jugador.IdCasilla);

        if (nuevaCasilla != null)
        {
            Debug.Log(jugador.Nombre + " terminó el ReRoll en la casilla " + nuevaCasilla.Orden + " de tipo " + nuevaCasilla.Tipo);
        }
    }

    void EjecutarPortal(Jugador jugador)
    {
        Debug.Log(jugador.Nombre + " cayó en Portal.");

        List<Casilla> portales = casillas.FindAll(c => c.Tipo == "Portal");

        if (portales.Count <= 1)
        {
            Debug.Log("No hay otro portal disponible.");
            return;
        }

        Casilla portalActual = casillas.Find(c => c.Id == jugador.IdCasilla);

        if (portalActual == null)
        {
            Debug.LogWarning("No se encontró el portal actual.");
            return;
        }

        Casilla portalDestino = null;

        foreach (Casilla portal in portales)
        {
            if (portal.Id != portalActual.Id)
            {
                portalDestino = portal;
                break;
            }
        }

        if (portalDestino != null)
        {
            jugador.IdCasilla = portalDestino.Id;
            jugador.X = portalDestino.X;
            jugador.Y = portalDestino.Y;

            GuardarPosicionJugador(jugador);

            Debug.Log(jugador.Nombre + " fue transportado a la casilla " + portalDestino.Orden);
        }
    }

    void EjecutarSwap(Jugador jugador)
    {
        Debug.Log(jugador.Nombre + " cayó en Swap.");

        Jugador jugadorMasCercano = null;
        int menorDistancia = int.MaxValue;

        foreach (Jugador otroJugador in jugadores)
        {
            if (otroJugador.Id == jugador.Id)
            {
                continue;
            }

            int distancia = Mathf.Abs(otroJugador.IdCasilla - jugador.IdCasilla);

            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                jugadorMasCercano = otroJugador;
            }
        }

        if (jugadorMasCercano == null)
        {
            Debug.Log("No hay jugador para hacer swap.");
            return;
        }

        int casillaTemporal = jugador.IdCasilla;
        float xTemporal = jugador.X;
        float yTemporal = jugador.Y;

        jugador.IdCasilla = jugadorMasCercano.IdCasilla;
        jugador.X = jugadorMasCercano.X;
        jugador.Y = jugadorMasCercano.Y;

        jugadorMasCercano.IdCasilla = casillaTemporal;
        jugadorMasCercano.X = xTemporal;
        jugadorMasCercano.Y = yTemporal;

        GuardarPosicionJugador(jugador);
        GuardarPosicionJugador(jugadorMasCercano);

        Debug.Log(jugador.Nombre + " intercambió posición con " + jugadorMasCercano.Nombre);
    }

    void EjecutarSkipTurn(Jugador jugador)
    {
        jugador.PierdeTurno = true;
        Debug.Log(jugador.Nombre + " perderá su siguiente turno.");
    }

    void EjecutarCofre(Jugador jugador)
    {
        int gemasGanadas = Random.Range(3, 8);

        jugador.Gemas += gemasGanadas;
        GuardarGemasJugador(jugador);

        Debug.Log(jugador.Nombre + " abrió un cofre y ganó " + gemasGanadas + " gemas. Total: " + jugador.Gemas);
    }

    void RetrocederJugador(Jugador jugador, int pasos)
    {
        int nuevaCasilla = jugador.IdCasilla - pasos;

        if (nuevaCasilla < 1)
        {
            nuevaCasilla = 1;
        }

        jugador.IdCasilla = nuevaCasilla;

        Casilla casillaActual = casillas.Find(c => c.Id == jugador.IdCasilla);

        if (casillaActual != null)
        {
            jugador.X = casillaActual.X;
            jugador.Y = casillaActual.Y;

            GuardarPosicionJugador(jugador);

            Debug.Log(jugador.Nombre + " retrocedió a la casilla " + casillaActual.Orden);
        }
    }

    void DarGemasAEquipo(int inicio, int cantidad)
    {
        for (int i = inicio; i < jugadores.Count; i += 2)
        {
            jugadores[i].Gemas += cantidad;
            GuardarGemasJugador(jugadores[i]);

            Debug.Log(jugadores[i].Nombre + " ahora tiene " + jugadores[i].Gemas + " gemas.");
        }
    }

    void RevisarVictoria(Jugador jugador)
    {
        if (jugador.IdCasilla >= casillas.Count)
        {
            juegoTerminado = true;
            Debug.Log(jugador.Nombre + " llegó al final del tablero y ganó la partida.");
        }
    }

    void CambiarTurno()
    {
        turnoActual++;

        if (turnoActual >= jugadores.Count)
        {
            turnoActual = 0;
        }

        Debug.Log("Ahora es turno de: " + jugadores[turnoActual].Nombre);
    }

    void GuardarPosicionJugador(Jugador jugador)
    {
        if (DBManager.Instance != null && DBManager.Instance.EstaConectado())
        {
            DBManager.Instance.ActualizarPosicionJugador(
                jugador.Id,
                jugador.IdCasilla,
                jugador.X,
                jugador.Y
            );
        }
    }

    void GuardarGemasJugador(Jugador jugador)
    {
        if (DBManager.Instance != null && DBManager.Instance.EstaConectado())
        {
            DBManager.Instance.ActualizarGemasJugador(
                jugador.Id,
                jugador.Gemas
            );
        }
    }
}