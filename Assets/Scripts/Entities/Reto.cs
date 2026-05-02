using UnityEngine;

public class Reto
{
    public int IdReto;
    public string TipoCasilla;
    public string Descripcion;

    public Reto(int idReto, string tipoCasilla, string descripcion)
    {
        IdReto = idReto;
        TipoCasilla = tipoCasilla;
        Descripcion = descripcion;
    }
}