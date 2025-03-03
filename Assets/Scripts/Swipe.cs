using UnityEngine;
using UnityEngine.EventSystems;

public class Swipe : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private bool isDragging = false;
    public float returnSpeed = 0.2f;  // Vitesse du retour au centre
    public float swipeThreshold = 200f;  // Distance pour valider un swipe
    public float outOfScreenX = 1000f;  // Distance pour sortir la carte de l'écran

    void Start()
    {
        startPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // La carte suit exactement le doigt sur X et Y
            Vector3 newPosition = transform.position;
            newPosition.x += eventData.delta.x;
            newPosition.y += eventData.delta.y;
            transform.position = newPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        float swipeDistanceX = transform.position.x - startPosition.x;

        if (Mathf.Abs(swipeDistanceX) < swipeThreshold)
        {
            // Si le swipe est trop court, retour à la position initiale
            LeanTween.move(gameObject, startPosition, returnSpeed).setEase(LeanTweenType.easeOutQuad);
        }
        else
        {
            // Si le swipe est suffisant, envoie la carte hors de l'écran
            float targetX = swipeDistanceX > 0 ? outOfScreenX : -outOfScreenX;
            LeanTween.moveX(gameObject, targetX, 0.3f).setEase(LeanTweenType.easeInQuad)
                .setOnComplete(() => gameObject.SetActive(false)); // Désactiver la carte après le swipe
        }
    }
}
