using UnityEngine;
using UnityEngine.UI;

public class Jauges : MonoBehaviour
{
    public Image fillImage; // Référence à l'image de remplissage
    public float AmountFill;
    public float progress = 0f; // Valeur actuelle (entre 0 et 1)

    void Start()
    {
        //fillImage.fillAmount = AmountFill;
        //Debug.Log("AmountFill : " + AmountFill);
    }
    //Met à jour la barre de progression
    public void SetJauges(float value)
    {
        progress = Mathf.Clamp01(value); // Garde la valeur entre 0 et 1
        UpdateJauges();
    }

    private void UpdateJauges()
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = progress;
        }
    }

    public void DisableJauges()
    {
        gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
}
