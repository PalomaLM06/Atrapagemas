using UnityEngine;
public class Jugador
{
    public int Id;
    public string Nombre;
    public int Gemas;
    public int IdCasilla;
    public int X;
    public int Y;

    public Jugador(int id, string nombre, int gemas, int idCasilla, int x, int y)
    {
        Id = id;
        Nombre = nombre;
        Gemas = gemas;
        IdCasilla = idCasilla;
        X = x;
        Y = y;
    }
}