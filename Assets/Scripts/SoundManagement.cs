using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagement : MonoBehaviour
{

    public List<AudioSource> SonsJeu;
    public List<AudioSource> Musiques;

    public Slider sliderSon;
    public Slider sliderMusique;

    private AudioSource menuMusic;

    public void volumeUpdateM()
    {
        foreach (AudioSource musique in Musiques)
        {
            //Debug.Log("Volume before : " + musique.volume);
            musique.volume = sliderMusique.value;
            //Debug.Log("Volume after : " + musique.volume);
        }
    }

    public void volumeUpdateS()
    {
        //Slider slider = GetComponent<Slider>();
        //Debug.Log(slider.name);
        foreach (AudioSource source in SonsJeu)
        {
            //Debug.Log("Volume before : " + source.volume);
            source.volume = sliderSon.value;
            //Debug.Log("Volume after : " + source.volume);
        }
    }

    public void BouttonSound()
    {
        //Debug.Log("Nom son 6 : " + SonsJeu[6].name);
        SonsJeu[7].Play();
    }
}
