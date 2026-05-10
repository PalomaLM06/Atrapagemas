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

        rollButton.interactable = false;

        // Animación de tirada
        for (int i = 0; i < 15; i++)
        {
            int randomFace = Random.Range(0, 6);

            diceImage.sprite = diceFaces[randomFace];

            yield return new WaitForSeconds(0.05f);
        }

        // Resultado final
        int finalNumber = Random.Range(0, 6);

        diceImage.sprite = diceFaces[finalNumber];

        Debug.Log("Resultado: " + (finalNumber + 1));

        rollButton.interactable = true;

        isRolling = false;
    }
}