using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;

public class DBManager : MonoBehaviour
{
    public static DBManager Instance;

    private string dbPath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dbPath = "URI=file:" + Application.dataPath + "/Database/juego.db";
    }

    private IDbConnection CrearConexion()
    {
        IDbConnection conexion = new SqliteConnection(dbPath);
        conexion.Open();
        return conexion;
    }

    private void AgregarParametro(IDbCommand comando, string nombre, object valor)
    {
        IDbDataParameter parametro = comando.CreateParameter();
        parametro.ParameterName = nombre;
        parametro.Value = valor;
        comando.Parameters.Add(parametro);
    }

    public List<Jugador> SelectJugadores()
    {
        List<Jugador> jugadores = new List<Jugador>();

        using (IDbConnection conexion = CrearConexion())
        {
            using (IDbCommand comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT Id, Nombre, Gemas, IdCasilla, x, y FROM Jugadores;";

                using (IDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Jugador jugador = new Jugador(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetInt32(2),
                            reader.GetInt32(3),
                            reader.GetInt32(4),
                            reader.GetInt32(5)
                        );

                        jugadores.Add(jugador);
                    }
                }
            }
        }

        return jugadores;
    }

    public List<Casilla> SelectCasillas()
    {
        List<Casilla> casillas = new List<Casilla>();

        using (IDbConnection conexion = CrearConexion())
        {
            using (IDbCommand comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT Id_casilla, Tipo, Orden, x, y, Id_reto, Id_tablero FROM Casilla ORDER BY Orden;";

                using (IDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Casilla casilla = new Casilla(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetInt32(2),
                            reader.GetInt32(3),
                            reader.GetInt32(4),
                            reader.GetInt32(5),
                            reader.GetInt32(6)
                        );

                        casillas.Add(casilla);
                    }
                }
            }
        }

        return casillas;
    }

    public Casilla GetCasillaPorOrden(int orden)
    {
        using (IDbConnection conexion = CrearConexion())
        {
            using (IDbCommand comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT Id_casilla, Tipo, Orden, x, y, Id_reto, Id_tablero FROM Casilla WHERE Orden = @orden;";
                AgregarParametro(comando, "@orden", orden);

                using (IDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Casilla(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetInt32(2),
                            reader.GetInt32(3),
                            reader.GetInt32(4),
                            reader.GetInt32(5),
                            reader.GetInt32(6)
                        );
                    }
                }
            }
        }

        return null;
    }

    public Reto GetRetoPorId(int idReto)
    {
        using (IDbConnection conexion = CrearConexion())
        {
            using (IDbCommand comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT Id_reto, Tipo_casilla, Descripcion FROM Retos WHERE Id_reto = @idReto;";
                AgregarParametro(comando, "@idReto", idReto);

                using (IDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Reto(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2)
                        );
                    }
                }
            }
        }

        return null;
    }

    public void ActualizarPosicionJugador(int idJugador, int nuevaCasilla, int x, int y)
    {
        using (IDbConnection conexion = CrearConexion())
        {
            using (IDbCommand comando = conexion.CreateCommand())
            {
                comando.CommandText = @"
                    UPDATE Jugadores
                    SET IdCasilla = @nuevaCasilla, x = @x, y = @y
                    WHERE Id = @idJugador;
                ";

                AgregarParametro(comando, "@nuevaCasilla", nuevaCasilla);
                AgregarParametro(comando, "@x", x);
                AgregarParametro(comando, "@y", y);
                AgregarParametro(comando, "@idJugador", idJugador);

                comando.ExecuteNonQuery();
            }
        }
    }

    public void ActualizarGemasJugador(int idJugador, int nuevasGemas)
    {
        using (IDbConnection conexion = CrearConexion())
        {
            using (IDbCommand comando = conexion.CreateCommand())
            {
                comando.CommandText = "UPDATE Jugadores SET Gemas = @gemas WHERE Id = @idJugador;";

                AgregarParametro(comando, "@gemas", nuevasGemas);
                AgregarParametro(comando, "@idJugador", idJugador);

                comando.ExecuteNonQuery();
            }
        }
    }
}

