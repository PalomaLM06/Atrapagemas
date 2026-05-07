public class Casilla
{
    public int Id;
    public int Orden;
    public string Tipo;
    public int IdReto;
    public float X;
    public float Y;

    public Casilla(int id, int orden, string tipo, int idReto, float x, float y)
    {
        Id = id;
        Orden = orden;
        Tipo = tipo;
        IdReto = idReto;
        X = x;
        Y = y;
    }
}