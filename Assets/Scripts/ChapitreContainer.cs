using System;
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

    public List<float> jaugesValues;
    public Vector3 uiPosition;
    public Vector3 uiStartPosition;

    IEnumerator PlayChapters()
    {
        //Debug.Log("LE JEU COMMENCE !!!");
        for (i = 0; i <= listCardsContainer.Count-1; i++)
        {
            listCardsContainer[i].gameObject.SetActive(true);
            listCardsContainer[i].BeginPlay();
            // Attendre que le chapitre soit terminé
            yield return new WaitUntil(() => listCardsContainer[i].cardDisplay.IsChapterEnd && listCardsContainer[i].cardDisplay.IsUpdate == false);

            //Debug.Log("Chapitre suivant !");
            listCardsContainer[i].gameObject.SetActive(false);
            
        }
        //Debug.Log("LE JEU EST FINI !!!");
        //eddeazfaeedaeda
    }
    void OnEnable()
    {
        if (listCardsContainer == null)
        {
            //Debug.Log("Réinitialisation de listCardsContainer...");
            listCardsContainer = new List<CardsContainer>();
        }
    }

    void Start()
    {
        if (listCardsContainer == null || listCardsContainer.Count == 0)
        {
            //Debug.LogError("listCardsContainer est NULL ou VIDE !");
            return;
        }
        Debug.Log(listCardsContainer[0].cardDisplay);
        uiStartPosition = listCardsContainer[0].cardDisplay.artwork.transform.position;
        jaugesValues.Add(50f);
        jaugesValues.Add(50f);
        jaugesValues.Add(50f);
        jaugesValues.Add(50f);
        StartCoroutine(PlayChapters());
        //Debug.Log("jaugesValues : " + jaugesValues.Count);
        //Debug.Log("jaugesContainers : " + jaugesContainer.Count);

    }

    //private void Update()
    //{
    //    if (i <= listCardsContainer.Count - 1)
    //    {
    //        uiPosition = listCardsContainer[0].cardDisplay.artwork.transform.position - uiStartPosition;
    //        if (0 > uiPosition.x)
    //        {
    //            listCardsContainer[i].cardDisplay.ChoiceRight = true;
    //        }
    //        if (0 < uiPosition.x)
    //        {
    //            listCardsContainer[i].cardDisplay.ChoiceRight = false;
    //        }
    //        Debug.Log("ChoiceRight : " + listCardsContainer[i].cardDisplay.ChoiceRight);
    //        if (listCardsContainer[i].cardDisplay.IsUpdate)
    //        {
    //            JaugesUpdate();
    //            Debug.Log("Jauges updates");
    //            listCardsContainer[i].cardDisplay.IsUpdate = false;
    //        }
    //    }
    //}

    public void JaugesUpdate()
    {
        //Debug.Log("jaugesValues : " + jaugesValues[0]);
        //Debug.Log("jaugesValues : " + jaugesValues[1]);
        //Debug.Log("jaugesValues : " + jaugesValues[2]);
        //Debug.Log("jaugesValues : " + jaugesValues[3]);

        if (listCardsContainer[i].cardDisplay.choiceRight && !listCardsContainer[i].cardDisplay.neutralPosition)
        {
            Debug.Log("DROITE !!!!");
            jaugesValues[0] = jaugesValues[0] + listCardsContainer[i].cardDisplay.card.Rargent;
            jaugesValues[1] = jaugesValues[1] + listCardsContainer[i].cardDisplay.card.Rcorruption;
            jaugesValues[2] = jaugesValues[2] + listCardsContainer[i].cardDisplay.card.Rfamille;
            jaugesValues[3] = jaugesValues[3] + listCardsContainer[i].cardDisplay.card.RMentalHealth;
        }

        if (!listCardsContainer[i].cardDisplay.choiceRight && !listCardsContainer[i].cardDisplay.neutralPosition)
        {
            Debug.Log("GAUCHE !!!!");
            jaugesValues[0] = jaugesValues[0] + listCardsContainer[i].cardDisplay.card.Largent;
            jaugesValues[1] = jaugesValues[1] + listCardsContainer[i].cardDisplay.card.Lcorruption;
            jaugesValues[2] = jaugesValues[2] + listCardsContainer[i].cardDisplay.card.Lfamille;
            jaugesValues[3] = jaugesValues[3] + listCardsContainer[i].cardDisplay.card.LMentalHealth;
        }

        jaugesContainer[0].fillImage.fillAmount = jaugesValues[0] / 100f;
        jaugesContainer[1].fillImage.fillAmount = jaugesValues[1] / 100f;
        jaugesContainer[2].fillImage.fillAmount = jaugesValues[2] / 100f;
        jaugesContainer[3].fillImage.fillAmount = jaugesValues[3] / 100f;
        Debug.Log("C'est UPDATE !!!!!!!!!!!!!!!!!!!!!");
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
}
