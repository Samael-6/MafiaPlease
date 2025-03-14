using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public CardsContainer cardsContainer;
    public Card card;

    public TMP_Text informations;
    public TMP_Text rightchoice;
    public TMP_Text leftchoice;

    public Image artwork;
    public int index;
    public bool IsUpdate = false;

    public bool IsChapterEnd = false;
    public void BeginPlay()
    {
        index = 0;
        CardUpdate();
    }

    public void CardUpdate()
    {
        //Debug.Log("Index before update : " + index);
        //Debug.Log("``` CardUpdate : cardsContairner.Cards.Count - 1 : " + (cardsContainer.Cards.Count - 1));
        if (index <= cardsContainer.Cards.Count - 1)
        {
            card = cardsContainer.Cards[index];
            informations.text = card.informations;
            artwork.sprite = card.artwork;
            rightchoice.text = card.rightchoice;
            leftchoice.text = card.leftchoice;
            IsUpdate = true;
        }

        else
        {
            gameObject.SetActive(false);
            IsChapterEnd = true;
            //Debug.Log("Chapitre finit !!!!!!!!!");
        }
        index++;
    }
}
