using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

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
    public bool choiceRight;
    public bool neutralPosition;

    public Vector3 uiPosition;
    public Vector3 uiStartPosition;

    public void BeginPlay()
    {
        index = 0;
        uiStartPosition = artwork.transform.position;
        CardUpdate();
    }

    public void CardUpdate()
    {
        cardsContainer.chapterContainer.JaugesUpdate();
        //Debug.Log("Index before update : " + index);
        //Debug.Log("``` CardUpdate : cardsContairner.Cards.Count - 1 : " + (cardsContainer.Cards.Count - 1));
        //TKT C pour le fonctionnement;
        if (index <= cardsContainer.Cards.Count - 1)
        {
            card = cardsContainer.Cards[index];
            informations.text = card.informations;
            artwork.sprite = card.artwork;
            rightchoice.text = card.rightchoice;
            leftchoice.text = card.leftchoice;
            uiPosition = artwork.transform.position - uiStartPosition;
            //Debug.Log("uiPosition.x : " + uiPosition.x);
            //Debug.Log("Valeur bool condition ChoiceRight : " + (0 < uiPosition.x));

            if (card.son != null)
            {
                if (card.son.name == "SD-AppelFemme")
                {
                    cardsContainer.chapterContainer.soundManagement.SonsJeu[0].Play();
                }

                else if (card.son.name == "SD-AppelHomme")
                {
                    cardsContainer.chapterContainer.soundManagement.SonsJeu[1].Play();
                }

                else if (card.son.name == "SD-papierEnveloppe" || card.son.name == "SD-papierEnveloppeBis")
                {
                    int x = Random.Range(2, 3);
                    cardsContainer.chapterContainer.soundManagement.SonsJeu[x].Play();
                }
            }
        }

        else
        {
            gameObject.SetActive(false);
            IsChapterEnd = true;
            IsUpdate = true;
            //Debug.Log("Chapitre finit !!!!!!!!!");
        }

        index++;
    }
}

// PROUT