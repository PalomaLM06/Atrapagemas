using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public Image diceImage;
    public Sprite[] diceFaces;
    public Button rollButton;

    private bool isRolling = false;

    public void RollDice()
    {
        if (!isRolling)
        {
            StartCoroutine(RollAnimation());
        }
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

            if (diceImage != null && diceFaces.Length >= 6)
            {
                diceImage.sprite = diceFaces[randomFace];
            }

            yield return new WaitForSeconds(0.05f);
        }

        int finalNumber = Random.Range(0, 6);
        int resultadoDado = finalNumber + 1;

        if (diceImage != null && diceFaces.Length >= 6)
        {
            diceImage.sprite = diceFaces[finalNumber];
        }

        Debug.Log("Resultado del dado: " + resultadoDado);

        yield return new WaitForSeconds(0.7f);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ProcesarResultadoDado(resultadoDado);
        }
        else
        {
            Debug.LogError("No se encontró GameManager.Instance. Abre la escena Dado desde el tablero.");
        }

        isRolling = false;
    }
}