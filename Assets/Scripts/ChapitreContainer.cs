using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ChapitreContainer : MonoBehaviour
{
    public List<CardsContainer> listCardsContainer;
    public List<Card> listEnds;
    public int i = 0;
    public int max = 0;
    
    private int j = 0;
    private int argent;
    private int corruption;
    private int famille;
    private int mentalhealth;

    IEnumerator PlayChapters()
    {
        Debug.Log("LE JEU COMMENCE !!!");
        for (i = 0; i <= listCardsContainer.Count-1; i++)
        {
            listCardsContainer[i].gameObject.SetActive(true);

            // Attendre que le chapitre soit terminé
            yield return new WaitUntil(() => listCardsContainer[i].cardDisplay.index >= listCardsContainer[i].Cards.Count - 1);

            Debug.Log("Chapitre suivant !");
            listCardsContainer[i].gameObject.SetActive(false);
        }
        Debug.Log("LE JEU EST FINI !!!");
    }

    void Start()
    {
        listCardsContainer[i].BeginPlay();
        StartCoroutine(PlayChapters());
    }

    public void JaugesUpdate()
    {
        j = listCardsContainer[i].cardDisplay.index;
        argent = argent +   listCardsContainer[i].Cards[j].argent;
        corruption = corruption + listCardsContainer[i].Cards[j].corruption;
        famille = famille + listCardsContainer[i].Cards[j].famille;
        mentalhealth = mentalhealth + listCardsContainer[i].Cards[j].famille;
    }

    public void Ends()
    {
        if (argent <= 0)
        {
            listCardsContainer[i].gameObject.SetActive(false);
        }
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    Debug.Log("LE JEU COMMENCE !!!");
    //    while (listCardsContainer.Count - 1 >= i & listCardsContainer[i].i <= listCardsContainer[i].Cards.Count)
    //    {
    //        listCardsContainer[i].gameObject.SetActive(true);
    //        listCardsContainer[i].BeginPlay();
    //        Debug.Log("Chapitre en cours : " + i);
    //        if (listCardsContainer[i].i >= listCardsContainer[i].Cards.Count - 1)
    //        {
    //            Debug.Log("Chapitre suivant !");
    //            listCardsContainer[i].gameObject.SetActive(false);
    //            i++;
    //        }   
    //    }
    //    Debug.Log("LE JEU EST FINI !!!");
    //}
}
