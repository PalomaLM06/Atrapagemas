

using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public List<Transform> posicionesCasillas;

    public Vector3 ObtenerPosicionPorOrden(int orden)
    {
        int index = orden - 1;

        if (index >= 0 && index < posicionesCasillas.Count)
        {
            return posicionesCasillas[index].position;
        }

        Debug.LogError("No existe la casilla con orden: " + orden);
        return Vector3.zero;
    }
}