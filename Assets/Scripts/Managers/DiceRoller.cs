using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public Image diceImage;
    public Sprite[] diceFaces;
    public Button rollButton;

    private bool isRolling = false;

    void Awake()
    {
        if (rollButton != null)
        {
            rollButton.onClick.RemoveAllListeners();
            rollButton.onClick.AddListener(RollDice);
            Debug.Log("Botón Tirar conectado automáticamente.");
        }
        else
        {
            Debug.LogError("DiceRoller: falta asignar Roll Button.");
        }
    }

    public void RollDice()
    {
        if (isRolling)
        {
            return;
        }

        if (diceImage == null)
        {
            Debug.LogError("DiceRoller: falta asignar Dice Image.");
            return;
        }

        if (diceFaces == null || diceFaces.Length < 6)
        {
            Debug.LogError("DiceRoller: faltan las 6 caras del dado.");
            return;
        }

        StartCoroutine(RollAnimation());
    }

    IEnumerator RollAnimation()
    {
        isRolling = true;

        if (rollButton != null)
        {
            rollButton.interactable = false;
        }

        for (int i = 0; i < 15; i++)
        {
            int randomFace = Random.Range(0, 6);

            if (diceImage != null)
            {
                diceImage.sprite = diceFaces[randomFace];
            }

            yield return new WaitForSeconds(0.05f);
        }

        int finalNumber = Random.Range(0, 6);
        int resultadoDado = finalNumber + 1;

        if (diceImage != null)
        {
            diceImage.sprite = diceFaces[finalNumber];
        }

        Debug.Log("Resultado del dado: " + resultadoDado);

        yield return new WaitForSeconds(0.7f);

        isRolling = false;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ProcesarResultadoDado(resultadoDado);
        }
        else
        {
            Debug.LogError("No se encontró GameManager.Instance. Abre la escena Dados desde el tablero.");
        }
    }
}