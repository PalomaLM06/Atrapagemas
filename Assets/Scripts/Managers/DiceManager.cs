

using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public int LanzarDado()
    {
        int resultado = Random.Range(1, 7);
        Debug.Log("Dado: " + resultado);
        return resultado;
    }
}