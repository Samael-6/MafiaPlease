using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Swipe : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public CardDisplay cardDisplay;

    public Vector3 startPosition;
    public bool isDragging = false;
        
    public float returnSpeed = 0.2f;  // Vitesse du retour au centre
    public float swipeThreshold = 200f;  // Distance pour valider un swipe
    public float outOfScreenX = 1500f;  // Distance pour sortir la carte de l'écran

    void OnEnable()
    {
        LeanTween.cancel(gameObject);
        //Debug.Log(" LeanTween reset !!!");
    }

    void Start()
    {
        if (startPosition == Vector3.zero)
        {
            startPosition = transform.position;
        }
    }

    //void update()
    //{
    //    debug.log("isdragging : " + isdragging);
    //    debug.log("stratposition" + startposition);
    //}

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
        //Debug.Log("OnEndDrag !!!");
        isDragging = false;
        float swipeDistanceX = transform.position.x - startPosition.x;

        if (Mathf.Abs(swipeDistanceX) < swipeThreshold)
        {
            // Si le swipe est trop court, retour à la position initiale
            //Debug.Log("Distance trop courte");
            LeanTween.move(gameObject, startPosition, returnSpeed).setEase(LeanTweenType.easeOutQuad);
        }
        else
        {
            // Si le swipe est suffisant, envoie la carte hors de l'écran et reset après
            //Debug.Log("Distance suffisante");
            float targetX = swipeDistanceX > 0 ? outOfScreenX : -outOfScreenX;
            LeanTween.moveX(gameObject, targetX, 0.1f).setEase(LeanTweenType.easeInQuad).setOnComplete(() => { Debug.Log("LeanTween terminé, ResetCard() appelé !"); StartCoroutine(DelayedRest()); }); // Appelle la coroutine ResetCard()
        }
    }

    //Fonction pour reset les cartes lorsqu'elles quittent l'écran
    private IEnumerator ResetCard()
    {
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("ResetCard() - Objet actif ? " + gameObject.activeInHierarchy);

        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }

        transform.position = startPosition;
        isDragging = false;

        LeanTween.cancel(gameObject);
        cardDisplay.CardUpdate();
    }

    private IEnumerator DelayedRest()
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(ResetCard());
    }
}
