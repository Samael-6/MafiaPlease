using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "New Card",  menuName = "Card")]
public class Card : ScriptableObject
{
    public new string name;
    public string informations;
    public string rightchoice;
    public string leftchoice;

    public Sprite artwork;

    public int corruption;
    public int famille;
    public int MentalHealth;
    public int argent;

    public bool IsSwap;
    public void print()
    {
        Debug.Log(name + ": " + informations);
    }

}
