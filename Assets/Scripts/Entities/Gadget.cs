using UnityEngine;

public class Gadget
{
    public int IdGadget;
    public string Descripcion;
    public int Coste;

    public Gadget(int idGadget, string descripcion, int coste)
    {
        IdGadget = idGadget;
        Descripcion = descripcion;
        Coste = coste;
    }
}