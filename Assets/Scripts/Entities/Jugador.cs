public class Jugador
{
    public int Id;
    public string Nombre;
    public int Gemas;
    public int IdCasilla;
    public float X;
    public float Y;
    public bool PierdeTurno;

    public Jugador(int id, string nombre, int gemas, int idCasilla, float x, float y)
    {
        Id = id;
        Nombre = nombre;
        Gemas = gemas;
        IdCasilla = idCasilla;
        X = x;
        Y = y;
        PierdeTurno = false;
    }
}