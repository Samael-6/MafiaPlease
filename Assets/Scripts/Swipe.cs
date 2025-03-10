using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Swipe : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public CardDisplay cardDisplay;

    private Vector3 startPosition;
    private bool isDragging = false;
        
    public float returnSpeed = 0.2f;  // Vitesse du retour au centre
    public float swipeThreshold = 200f;  // Distance pour valider un swipe
    public float outOfScreenX = 1500f;  // Distance pour sortir la carte de l'écran

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Debug.Log("isDragging : " + isDragging);
        Debug.Log("stratPosition" + startPosition);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // La carte suit le doigt sur X et Y
            Vector3 newPosition = transform.position;
            newPosition.x += eventData.delta.x;
            newPosition.y += eventData.delta.y;
            transform.position = newPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Etat objet : " + enabled);
        isDragging = false;
        float swipeDistanceX = transform.position.x - startPosition.x;

        if (Mathf.Abs(swipeDistanceX) < swipeThreshold)
        {
            // Si le swipe est trop court, retour à la position initiale
            Debug.Log("If : " + LeanTween.move(gameObject, startPosition, returnSpeed).setEase(LeanTweenType.easeOutQuad));
            LeanTween.move(gameObject, startPosition, returnSpeed).setEase(LeanTweenType.easeOutQuad);
        }
        else
        {
            // Si le swipe est suffisant, envoie la carte hors de l'écran et reset après
            float targetX = swipeDistanceX > 0 ? outOfScreenX : -outOfScreenX;
            Debug.Log("Else : " + LeanTween.moveX(gameObject, targetX, 0.1f).setEase(LeanTweenType.easeInQuad).setOnComplete(() => StartCoroutine(ResetCard())));
            LeanTween.moveX(gameObject, targetX, 0.1f).setEase(LeanTweenType.easeInQuad).setOnComplete(() => StartCoroutine(ResetCard())); // Appelle la coroutine ResetCard()
        }
    }

    //Fonction pour reset les cartes lorsqu'elles quittent l'écran
    private IEnumerator ResetCard()
    {
                     // Réactiver la carte (au cas où elle est désactivée)
        yield return new WaitForSeconds(0.5f);  // Attendre avant de reset
        cardDisplay.CardUpdate();
        transform.position = startPosition;     // Remettre la carte à sa position d'origine
    }
}
