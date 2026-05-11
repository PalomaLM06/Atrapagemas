using UnityEngine;
using UnityEngine.UI;

public class BotonDadoTablero : MonoBehaviour
{
    private Button boton;

    void Awake()
    {
        boton = GetComponent<Button>();

        if (boton == null)
        {
            Debug.LogError("BotonDadoTablero: este objeto no tiene componente Button.");
            return;
        }

        boton.onClick.RemoveAllListeners();
        boton.onClick.AddListener(AbrirDado);

        Debug.Log("Botón Dado conectado automáticamente.");
    }

    public void AbrirDado()
    {
        Debug.Log("Botón Dado presionado.");

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