using UnityEngine;
using System.Collections.Generic;
using System.IO;
using SQLite4Unity3d;

public class DBManager : MonoBehaviour
{
    public static DBManager Instance { get; private set; }

    [SerializeField] private string databaseName = "juego.db";

    private SQLiteConnection conexion;
    private bool conectado = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ConectarBaseDatos();
    }

    private void ConectarBaseDatos()
    {
        string dbPath = Path.Combine(Application.streamingAssetsPath, databaseName);

        if (!File.Exists(dbPath))
        {
            Debug.LogError("No se encontró la base de datos en: " + dbPath);
            conectado = false;
            return;
        }
//para que se pueda actualizar la base de datos
        conexion = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        conectado = true;

        Debug.Log("Base de datos conectada correctamente: " + dbPath);
    }

    public bool EstaConectado()
    {
        return conectado && conexion != null;
    }

    public List<Jugador> SelectJugadores()
    {
        List<Jugador> jugadores = new List<Jugador>();

        if (!EstaConectado())
        {
            Debug.LogError("No hay conexión con la base de datos.");
            return jugadores;
        }

        List<JugadorDB> filas = conexion.Query<JugadorDB>(
            "SELECT Id, Nombre, Gemas, IdCasilla, x AS X, y AS Y FROM Jugadores;"
        );

        foreach (JugadorDB fila in filas)
        {
            Jugador jugador = new Jugador(
                fila.Id,
                fila.Nombre,
                fila.Gemas,
                fila.IdCasilla,
                fila.X,
                fila.Y
            );

            jugadores.Add(jugador);
        }

        return jugadores;
    }

    public List<Casilla> SelectCasillas()
    {
        List<Casilla> casillas = new List<Casilla>();

        if (!EstaConectado())
        {
            Debug.LogError("No hay conexión con la base de datos.");
            return casillas;
        }

        List<CasillaDB> filas = conexion.Query<CasillaDB>(
            "SELECT Id_casilla AS Id, Tipo, Orden, x AS X, y AS Y, Id_reto AS IdReto, Id_tablero AS IdTablero FROM Casilla ORDER BY Orden;"
        );

        foreach (CasillaDB fila in filas)
        {
            Casilla casilla = new Casilla(
                fila.Id,
                fila.Orden,
                fila.Tipo,
                fila.IdReto,
                fila.X,
                fila.Y
            );

            casillas.Add(casilla);
        }

        return casillas;
    }

    public List<Reto> SelectRetos()
    {
        List<Reto> retos = new List<Reto>();

        if (!EstaConectado())
        {
            Debug.LogError("No hay conexión con la base de datos.");
            return retos;
        }

        List<RetoDB> filas = conexion.Query<RetoDB>(
            "SELECT Id_reto AS Id, Tipo_casilla AS TipoCasilla, Descripcion FROM Retos;"
        );

        foreach (RetoDB fila in filas)
        {
            Reto reto = new Reto(
                fila.Id,
                fila.TipoCasilla,
                fila.Descripcion
            );

            retos.Add(reto);
        }

        return retos;
    }

    public Casilla GetCasillaPorOrden(int orden)
    {
        if (!EstaConectado())
        {
            Debug.LogError("No hay conexión con la base de datos.");
            return null;
        }

        List<CasillaDB> filas = conexion.Query<CasillaDB>(
            "SELECT Id_casilla AS Id, Tipo, Orden, x AS X, y AS Y, Id_reto AS IdReto, Id_tablero AS IdTablero FROM Casilla WHERE Orden = ?;",
            orden
        );

        if (filas.Count == 0)
        {
            return null;
        }

        CasillaDB fila = filas[0];

        return new Casilla(
            fila.Id,
            fila.Orden,
            fila.Tipo,
            fila.IdReto,
            fila.X,
            fila.Y
        );
    }

    public Reto GetRetoPorId(int idReto)
    {
        if (!EstaConectado())
        {
            Debug.LogError("No hay conexión con la base de datos.");
            return null;
        }

        List<RetoDB> filas = conexion.Query<RetoDB>(
            "SELECT Id_reto AS Id, Tipo_casilla AS TipoCasilla, Descripcion FROM Retos WHERE Id_reto = ?;",
            idReto
        );

        if (filas.Count == 0)
        {
            return null;
        }

        RetoDB fila = filas[0];

        return new Reto(
            fila.Id,
            fila.TipoCasilla,
            fila.Descripcion
        );
    }

    public void ActualizarPosicionJugador(int idJugador, int nuevaCasilla, float x, float y)
    {
        if (!EstaConectado())
        {
            Debug.LogError("No hay conexión con la base de datos.");
            return;
        }

        conexion.Execute(
            "UPDATE Jugadores SET IdCasilla = ?, x = ?, y = ? WHERE Id = ?;",
            nuevaCasilla,
            x,
            y,
            idJugador
        );

        Debug.Log("Posición actualizada en SQLite para jugador " + idJugador);
    }

    public void ActualizarGemasJugador(int idJugador, int nuevasGemas)
    {
        if (!EstaConectado())
        {
            Debug.LogError("No hay conexión con la base de datos.");
            return;
        }

        conexion.Execute(
            "UPDATE Jugadores SET Gemas = ? WHERE Id = ?;",
            nuevasGemas,
            idJugador
        );

        Debug.Log("Gemas actualizadas en SQLite para jugador " + idJugador);
    }

    private void OnDestroy()
    {
        if (conexion != null)
        {
            conexion.Close();
            conexion = null;
        }
    }
}

public class JugadorDB
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Gemas { get; set; }
    public int IdCasilla { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
}

public class CasillaDB
{
    public int Id { get; set; }
    public string Tipo { get; set; }
    public int Orden { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public int IdReto { get; set; }
    public int IdTablero { get; set; }
}

public class RetoDB
{
    public int Id { get; set; }
    public string TipoCasilla { get; set; }
    public string Descripcion { get; set; }
}