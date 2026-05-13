using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardRoller : MonoBehaviour
{
    public Image cardImage;

    public Sprite cardBack;

    public Sprite[] cards;

    public Button rollButton;

    private bool isRolling = false;

    void Start()
    {
        // Mostrar portada al iniciar
        cardImage.sprite = cardBack;
    }

    public void RollCard()
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

        // Animación rápida
        for (int i = 0; i < 20; i++)
        {
            int randomCard = Random.Range(0, cards.Length);

            cardImage.sprite = cards[randomCard];

            yield return new WaitForSeconds(0.05f);
        }

        // Carta final
        int finalCard = Random.Range(0, cards.Length);

        cardImage.sprite = cards[finalCard];

        Debug.Log("Carta final: " + cards[finalCard].name);

        rollButton.interactable = true;

        isRolling = false;
    }
}