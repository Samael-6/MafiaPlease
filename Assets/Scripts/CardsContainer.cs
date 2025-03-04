using System.Collections.Generic;
using UnityEngine;

public class CardsContainer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int i;
    private int k;
    
    public List<Card> Cards;

    void Start()
    {
        Shuffle();
        i = 0;
    }

    public void Shuffle () 
    { 
        i = Cards.Count;
        while (i > 0)
        {
            i--;
            k = Random.Range(0, i);
            (Cards[k], Cards[i]) = (Cards[i], Cards[k]);            
        }
    }
}
