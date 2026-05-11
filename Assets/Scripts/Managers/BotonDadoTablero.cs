using UnityEngine;

public class BotonDadoTablero : MonoBehaviour
{
    public void AbrirDado()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TirarDado();
        }
        else
        {
            Debug.LogError("No se encontró GameManager.Instance.");
        }
    }
}