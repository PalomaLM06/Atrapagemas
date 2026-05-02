using UnityEngine;

public class Casilla
{
    public int IdCasilla;
    public string Tipo;
    public int Orden;
    public int X;
    public int Y;
    public int IdReto;
    public int IdTablero;

    public Casilla(int idCasilla, string tipo, int orden, int x, int y, int idReto, int idTablero)
    {
        IdCasilla = idCasilla;
        Tipo = tipo;
        Orden = orden;
        X = x;
        Y = y;
        IdReto = idReto;
        IdTablero = idTablero;
    }
}