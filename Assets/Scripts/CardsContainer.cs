using System.Collections.Generic;
using UnityEngine;

public class CardsContainer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int i;
    private int k;
    
    public List<Card> ChapterCards;
    public List<Card> EventCards;

    private List<Card> Cards;

    void Start()
    {
        Shuffle();
        i = 0;
    }

    public void Shuffle () 
    { 
        i = ChapterCards.Count;
        while (i > 0)
        {
            i--;
            k = Random.Range(0, i);
            (ChapterCards[k], ChapterCards[i]) = (ChapterCards[i], ChapterCards[k]);            
        }
    }

    public void AddEvent()
    {
        int j = 0;
        int c = EventCards.Count - 1;
        for (int x = (ChapterCards.Count - 1) + c; x >= 0; x--)
        {
            j = 0;
        }

    }
}
