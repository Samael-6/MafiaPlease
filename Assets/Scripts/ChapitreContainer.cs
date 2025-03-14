using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;
using UnityEngine;

public class ChapitreContainer : MonoBehaviour
{
    public List<CardsContainer> listCardsContainer;
    public List<Jauges> jaugesContainer;
    public List<Card> listEnds;
    public int i = 0;
    public int max = 0;

    private List<int> jaugesValues;
    private int j = 0;
    private int indexJ = 0;
    private int argent = 0;
    private int corruption = 0;
    private int famille = 0;
    private int mentalhealth = 0;
    private int CardPosition;

    private Vector3 uiPosition;

    IEnumerator PlayChapters()
    {
        //Debug.Log("LE JEU COMMENCE !!!");
        for (i = 0; i <= listCardsContainer.Count-1; i++)
        {
            listCardsContainer[i].gameObject.SetActive(true);
            listCardsContainer[i].BeginPlay();
            // Attendre que le chapitre soit terminé
            yield return new WaitUntil(() => listCardsContainer[i].cardDisplay.IsChapterEnd);

            //Debug.Log("Chapitre suivant !");
            listCardsContainer[i].gameObject.SetActive(false);
            
        }
        //Debug.Log("LE JEU EST FINI !!!");
    }

    void Start()
    {
        uiPosition = listCardsContainer[0].cardDisplay.artwork.transform.position;
        StartCoroutine(PlayChapters());
    }

    void Update()
    {
        Debug.Log("Position de l'image UI : " + (uiPosition.x - listCardsContainer[0].cardDisplay.artwork.transform.position.x));
        listCardsContainer[i].cardDisplay.IsUpdate = false;
    }

    //private void Update()
    //{
    //    
    //}

    public void JaugesUpdate()
    {
        if ((uiPosition.x - listCardsContainer[i].cardDisplay.artwork.transform.position.x) < 0)
        {
            jaugesValues[0] = listCardsContainer[i].cardDisplay.card.Rargent;

        }

        else
        {
            
        }

        foreach (var jauges in jaugesContainer)
        {
            Debug.Log(jauges.name + " : " + jaugesValues[jaugesContainer.IndexOf(jauges)] + " Index : " + jaugesContainer.IndexOf(jauges));
            jauges.fillImage.fillAmount = jaugesValues[jaugesContainer.IndexOf(jauges)];
        }
        Debug.Log(jaugesContainer[i]);
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
