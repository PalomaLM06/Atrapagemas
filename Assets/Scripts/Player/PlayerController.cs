using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int idJugador;
    public int casillaActual;

    public void MoverJugador(Vector3 nuevaPosicion)
    {
        transform.position = nuevaPosicion;
    }
}