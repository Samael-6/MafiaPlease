using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement; // Importer SceneManager

public class ChapitreContainer : MonoBehaviour
{
    public List<CardsContainer> listCardsContainer;
    public QuitGame mainMenu;
    public List<Jauges> jaugesContainer;
    public List<Card> listEnds;
    public int i = 0;
    public int max = 0;

    public List<float> jaugesValues;
    public Vector3 uiPosition;
    public Vector3 uiStartPosition;

    private bool IsDead = false;
    private bool IsEnd = false;
    //IEnumerator PlayChapters()
    //{
    //    //Debug.Log("LE JEU COMMENCE !!!");
    //    for (i = 0; i <= listCardsContainer.Count - 1; i++)
    //    {
    //        listCardsContainer[i].gameObject.SetActive(true);
    //        listCardsContainer[i].BeginPlay();
    //        // Attendre que le chapitre soit terminé
    //        Debug.Log("Condition pour passer au prochain chapitre : " + (listCardsContainer[i].cardDisplay.IsChapterEnd && listCardsContainer[i].cardDisplay.IsUpdate == false));
    //        yield return new WaitUntil(() => listCardsContainer[i].cardDisplay.IsChapterEnd && listCardsContainer[i].cardDisplay.IsUpdate == false);

    //        Debug.Log("Chapitre suivant !");
    //        listCardsContainer[i].gameObject.SetActive(false);
    //    }
    //    Debug.Log("IsEnd BEFORE : " + IsEnd);
    //    IsEnd = true;
    //    Debug.Log("IsEnd AFTER : " + IsEnd);
    //    Ends();
    //    Debug.Log("LE JEU EST FINI !!!");
    //}

    IEnumerator PlayChapters()
    {
        for (i = 0; i < listCardsContainer.Count; i++) // Utilise `< listCardsContainer.Count` au lieu de `<=`
        {
            listCardsContainer[i].gameObject.SetActive(true);
            listCardsContainer[i].BeginPlay();

            if (listCardsContainer[i].cardDisplay.IsChapterEnd && !listCardsContainer[i].cardDisplay.IsUpdate)
            {
                listCardsContainer[i].gameObject.SetActive(false);
                Debug.Log("Tous les chapitres sont terminés ! ");
                IsEnd = true;
                Ends();
            }
            // Attendre que le chapitre soit terminé
            yield return new WaitUntil(() => listCardsContainer[i].cardDisplay.IsChapterEnd && !listCardsContainer[i].cardDisplay.IsUpdate);
            listCardsContainer[i].gameObject.SetActive(false);
        }
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
        Ends();
    }

    public void Ends()
    {
        Debug.Log("!__ END __!");
        Debug.Log("IsDead || IsEnd : " + (IsDead || IsEnd));
        if (IsDead || IsEnd)
        {
            foreach (var jauge in jaugesContainer)
            {
                jauge.DisableJauges();
            }

            if (i < listCardsContainer.Count)
            {
                listCardsContainer[i].gameObject.SetActive(false);
            }

            Debug.Log(mainMenu == null);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        if (!IsDead)
        {
            for (int z = 0; z < jaugesValues.Count; z++)
            {
                if (jaugesValues[z] <= 0 || (jaugesValues[z] >= 10 && z == 0))
                {
                    Debug.Log(" ---  Z  --- " + z);
                    Debug.Log("jaugesValues[z] : " + jaugesValues[z]);

                    // Vérifier que 'i' est bien dans la limite de listCardsContainer
                    if (i >= listCardsContainer.Count)
                    {
                        //Debug.LogError("Index 'i' hors limite de listCardsContainer !");
                        return;
                    }

                    // Vérifier que listCardsContainer[i] et ses cartes ne sont pas null
                    if (listCardsContainer[i] == null || listCardsContainer[i].Cards == null)
                    {
                        //Debug.LogError("listCardsContainer[i] ou sa liste Cards est null !");
                        return;
                    }

                    // Vérifier que la liste des cartes n'est pas vide
                    if (listCardsContainer[i].Cards.Count == 0)
                    {
                        //Debug.LogWarning("Aucune carte dans listCardsContainer[i].Cards ! Ajout direct de la carte de mort.");
                        listCardsContainer[i].Cards.Add(listEnds[z]);  // Ajout direct de la carte de mort correspondante
                    }

                    // Vérifier et ajuster l'index cardDisplay.index
                    int index = listCardsContainer[i].cardDisplay.index;
                    if (index < 0 || index >= listCardsContainer[i].Cards.Count)
                    {
                        //Debug.LogWarning("Index cardDisplay.index invalide. Réglage sur la dernière carte disponible.");
                        index = listCardsContainer[i].Cards.Count - 1;
                    }

                    // Appliquer la bonne carte de mort selon le type de jauge
                    if (z == 0 && jaugesValues[z] <= 0)
                    {
                        listCardsContainer[i].Cards.Add(listEnds[0]);
                        Debug.Log("Corruption 0");
                    }
                    else if (z == 0 && jaugesValues[z] >= 10)
                    {
                        listCardsContainer[i].Cards.Add(listEnds[1]);
                        Debug.Log("Corruption 1");
                    }
                    else if (z == 1)
                    {
                        listCardsContainer[i].Cards.Add(listEnds[2]);
                        Debug.Log("Famille");
                    }
                    else if (z == 2)
                    {
                        listCardsContainer[i].Cards.Add(listEnds[3]);
                        Debug.Log("MentalHealth");
                    }
                    else if (z == 3)
                    {
                        listCardsContainer[i].Cards.Add(listEnds[4]);
                        Debug.Log("Argent");
                    }

                    IsDead = true;
                    break;
                }
            }
        }
    }
}
