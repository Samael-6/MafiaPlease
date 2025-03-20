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

    private bool IsDead = false;
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
    //dddddd
    void Start()
    {
        if (listCardsContainer == null || listCardsContainer.Count == 0)
        {
            //Debug.LogError("listCardsContainer est NULL ou VIDE !");
            return;
        }
        uiStartPosition = listCardsContainer[0].cardDisplay.artwork.transform.position;
        jaugesValues.Add(5f);
        jaugesValues.Add(5f);
        jaugesValues.Add(5f);
        jaugesValues.Add(5f);
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
        //if (listCardsContainer[i].cardDisplay.card == listEnds[0] || listCardsContainer[i].cardDisplay.card == listEnds[1] ||
        //    listCardsContainer[i].cardDisplay.card == listEnds[2] || listCardsContainer[i].cardDisplay.card == listEnds[3] ||
        //    listCardsContainer[i].cardDisplay.card == listEnds[4])
        //{
        //    IsDead = true;
        //}


        if (listCardsContainer[i].cardDisplay.choiceRight && !listCardsContainer[i].cardDisplay.neutralPosition)
        {
            jaugesValues[0] = jaugesValues[0] + listCardsContainer[i].cardDisplay.card.Rcorruption;
            jaugesValues[1] = jaugesValues[1] + listCardsContainer[i].cardDisplay.card.Rfamille;
            jaugesValues[2] = jaugesValues[2] + listCardsContainer[i].cardDisplay.card.RMentalHealth;
            jaugesValues[3] = jaugesValues[3] + listCardsContainer[i].cardDisplay.card.Rargent;
        }

        if (!listCardsContainer[i].cardDisplay.choiceRight && !listCardsContainer[i].cardDisplay.neutralPosition)
        {
            jaugesValues[0] = jaugesValues[0] + listCardsContainer[i].cardDisplay.card.Lcorruption;
            jaugesValues[1] = jaugesValues[1] + listCardsContainer[i].cardDisplay.card.Lfamille;
            jaugesValues[2] = jaugesValues[2] + listCardsContainer[i].cardDisplay.card.LMentalHealth;
            jaugesValues[3] = jaugesValues[3] + listCardsContainer[i].cardDisplay.card.Largent;
        }

        jaugesContainer[0].fillImage.fillAmount = jaugesValues[0] / 10f;
        jaugesContainer[1].fillImage.fillAmount = jaugesValues[1] / 10f;
        jaugesContainer[2].fillImage.fillAmount = jaugesValues[2] / 10f;
        jaugesContainer[3].fillImage.fillAmount = jaugesValues[3] / 10f;

        //Debug.Log("Corruption :" + jaugesValues[0]);
        //Debug.Log("Famille : " + jaugesValues[1]);
        //Debug.Log("MentalHealth : " + jaugesValues[2]);
        //Debug.Log("Argent : " + jaugesValues[3]);
        //Debug.Log("Argent : " + jaugesValues[3]);

        Ends();
    }

    public void Ends()
    {
        if (IsDead)
        {
            listCardsContainer[i].gameObject.SetActive(false);
        }

        for (int z = 0; z <= jaugesValues.Count - 1; z++)
        {
            if (jaugesValues[z] <= 0 || (jaugesValues[z] >= 10 && z == 0))
            {
                Debug.Log(" ---  Z  --- " + z);
                Debug.Log("jaugesValues[z] : " + jaugesValues[z]);
                if (z == 0 && jaugesValues[z] <= 0)
                {
                    listCardsContainer[i].Cards[listCardsContainer[i].cardDisplay.index] = listEnds[0];
                    Debug.Log("Corruption 0");
                    IsDead = true;
                    break;
                }

                if (z == 0 && jaugesValues[z] >= 1)
                {
                    listCardsContainer[i].Cards[listCardsContainer[i].cardDisplay.index] = listEnds[1];
                    Debug.Log("Corruption 1");
                    IsDead = true;
                    break;
                }

                if (z == 1)
                {
                    listCardsContainer[i].Cards[listCardsContainer[i].cardDisplay.index] = listEnds[2];
                    Debug.Log("Famille");
                    IsDead = true;
                    break;
                }

                if (z == 2)
                {
                    listCardsContainer[i].Cards[listCardsContainer[i].cardDisplay.index] = listEnds[3];
                    Debug.Log("MentalHealth");
                    IsDead = true;
                    break;
                }

                if (z == 3)
                {
                    listCardsContainer[i].Cards[listCardsContainer[i].cardDisplay.index] = listEnds[4];
                    Debug.Log("Argent");
                    IsDead = true;
                    break;
                }
            }
        }
    }
}
