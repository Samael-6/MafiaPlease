using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapitreContainer : MonoBehaviour
{
    public List<CardsContainer> listCardsContainer;
    public int i = 0;
    public int max = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var cardsContainer in listCardsContainer)
        {
            cardsContainer.gameObject.SetActive(true);
            cardsContainer.BeginPlay();
            Debug.Log("Chapitre en cours : " + i);
            while (cardsContainer.i <= cardsContainer.Cards.Count - 1)
            {

            }
            cardsContainer.gameObject.SetActive(false);
            i++;
        }
    }
}
