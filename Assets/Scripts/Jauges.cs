using UnityEngine;
using UnityEngine.UI;

public class Jauges : MonoBehaviour
{
    public Image fillImage; // Référence à l'image de remplissage
    private float progress = 0f; // Valeur actuelle (entre 0 et 1)

    void Start()
    {
        UpdateJauges();
    }

    // Met à jour la barre de progression
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
}
