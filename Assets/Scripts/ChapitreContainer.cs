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
        StartCoroutine(PlayChapters());
        uiPosition = listCardsContainer[0].cardDisplay.artwork.transform.position;
        jaugesValues.Add(50);
        jaugesValues.Add(50);
        jaugesValues.Add(50);
        jaugesValues.Add(50);
        Debug.Log("jaugesValues : " + jaugesValues.Count);
        Debug.Log("jaugesContainers : " + jaugesContainer.Count);
        JaugesUpdate();
    }

    //private void Update()
    //{
    //    
    //}

    public void JaugesUpdate()
    {
        Debug.Log("jaugesValues : " + jaugesValues[0]);
        Debug.Log("jaugesValues : " + jaugesValues[1]);
        Debug.Log("jaugesValues : " + jaugesValues[2]);
        Debug.Log("jaugesValues : " + jaugesValues[3]);

        Debug.Log("jaugesContainer : " + jaugesContainer[0].fillImage.fillAmount);
        Debug.Log("fillAmount : " + jaugesContainer[1].fillImage.fillAmount);
        Debug.Log("fillAmount : " + jaugesContainer[2].fillImage.fillAmount);
        Debug.Log("fillAmount : " + jaugesContainer[3].fillImage.fillAmount);

        if (0 > (uiPosition.x - listCardsContainer[i].cardDisplay.artwork.transform.position.x))
        {
            jaugesValues[0] = listCardsContainer[i].cardDisplay.card.Rargent;
            jaugesValues[1] = listCardsContainer[i].cardDisplay.card.Rcorruption;
            jaugesValues[2] = listCardsContainer[i].cardDisplay.card.Rfamille;
            jaugesValues[3] = listCardsContainer[i].cardDisplay.card.RMentalHealth;
        }

        if (0 < (uiPosition.x - listCardsContainer[i].cardDisplay.artwork.transform.position.x))
        {
            jaugesValues[0] = listCardsContainer[i].cardDisplay.card.Largent;
            jaugesValues[1] = listCardsContainer[i].cardDisplay.card.Lcorruption;
            jaugesValues[2] = listCardsContainer[i].cardDisplay.card.Lfamille;
            jaugesValues[3] = listCardsContainer[i].cardDisplay.card.LMentalHealth;
        }

        jaugesContainer[0].fillImage.fillAmount = jaugesValues[0] / 100;
        jaugesContainer[1].fillImage.fillAmount = jaugesValues[1] / 100;
        jaugesContainer[2].fillImage.fillAmount = jaugesValues[2] / 100;
        jaugesContainer[3].fillImage.fillAmount = jaugesValues[3] / 100;
        
        Debug.Log(jaugesContainer[i]);
    }

    public void Ends()
    {
        foreach (var jauge in jaugesValues)
        {
            if (jauge <= 0)
            {
                listCardsContainer[i].gameObject.SetActive(false);
            }
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
