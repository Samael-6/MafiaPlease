using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text informations;
    public Text rightchoice;
    public Text leftchoice;

    public Image artwork;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        card.print();
        informations.text = card.informations;
        artwork.sprite = card.artwork;
        rightchoice.text = card.rightchoice;
        leftchoice.text = card.leftchoice;
    }
}
