using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void CambiarEsc(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }   
}
