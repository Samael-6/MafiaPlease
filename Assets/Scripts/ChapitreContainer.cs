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
        
    }

    IEnumerator LoopWithCondition()
    {
        foreach (var cardsContainer in listCardsContainer)
        {
            Debug.Log("Chapitre en cours : " + i);

            // Attendre que canContinue soit true avant de poursuivre
            yield return new WaitUntil(() => cardsContainer.i <= cardsContainer.Cards.Count - 1);
        }
    }
}
