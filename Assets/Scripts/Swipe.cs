using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class Swipe : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 startPos;
    private bool isDragging = false;

    public float swipeThreshold = 200f;
    public float moveSpeed = 0.3f;  // Vitesse du swipe (plus rapide)
    public Transform cardParent;
    public Image cardImage; // Image de la carte

    public static List<Sprite> spriteList = new List<Sprite>(); // Liste d'images
    private static int currentIndex = 0; // Index global de la liste

    private Swipe otherSwipe; // Référence à l'autre carte
    private Vector3 originalPosition; // Position d'origine de la carte

    private RectTransform canvasRect; // Référence à la taille du canevas

    void Start()
    {
        startPos = transform.position;
        originalPosition = transform.position;  // Sauvegarder la position d'origine
        canvasRect = cardParent.GetComponent<RectTransform>(); // Obtenez la taille du canevas
        FindOtherSwipe();
        UpdateCardImage(); // Affiche la première image
    }

    void FindOtherSwipe()
    {
        foreach (Transform child in cardParent)
        {
            Swipe swipe = child.GetComponent<Swipe>();
            if (swipe != null && swipe != this)
            {
                otherSwipe = swipe;
                break;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        // Déplacement horizontal uniquement
        Vector2 newPos = (Vector2)transform.position + new Vector2(eventData.delta.x, 0);
        transform.position = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        float swipeDistance = transform.position.x - startPos.x;

        // Vérification si la carte sort du canevas
        if (IsOutOfCanvas())
        {
            ResetPosition(); // Retour à la position d'origine si elle sort du canevas
        }
        else
        {
            if (Mathf.Abs(swipeDistance) > swipeThreshold)
            {
                if (swipeDistance > 0)
                    SwipeRight();
                else
                    SwipeLeft();
            }
            else
            {
                ResetPosition(); // Retour au centre si le swipe n'est pas assez grand
            }
        }
    }

    // Vérification si la carte est sortie du canevas
    bool IsOutOfCanvas()
    {
        Vector3[] canvasCorners = new Vector3[4];
        canvasRect.GetWorldCorners(canvasCorners); // Obtenir les coins du canevas en coordonnées mondiales

        // Vérification si le coin de la carte dépasse du canevas
        if (transform.position.x < canvasCorners[0].x || transform.position.x > canvasCorners[2].x)
        {
            return true;
        }
        return false;
    }

    void SwipeRight()
    {
        LeanTween.moveX(gameObject, transform.position.x + 1000, moveSpeed * 5)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(RepositionSwipe);
    }

    void SwipeLeft()
    {
        LeanTween.moveX(gameObject, transform.position.x - 1000, moveSpeed * 5)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(RepositionSwipe);
    }

    void ResetPosition()
    {
        LeanTween.move(gameObject, originalPosition, 0.2f).setEase(LeanTweenType.easeOutBounce); // Retour rapide au centre
    }

    void RepositionSwipe()
    {
        transform.position = originalPosition;  // Réinitialisation de la position à l'originale

        // Change l'image de la carte en prenant le prochain élément de la liste
        IncrementIndex();
        UpdateCardImage();

        // Met cette carte en arrière et active l'autre
        transform.SetAsFirstSibling();
        otherSwipe.transform.SetAsLastSibling();
    }

    void IncrementIndex()
    {
        if (spriteList.Count == 0) return;

        currentIndex++;
        if (currentIndex >= spriteList.Count) currentIndex = 0; // Boucle infinie
    }

    void UpdateCardImage()
    {
        if (spriteList.Count > 0)
        {
            cardImage.sprite = spriteList[currentIndex];
        }
    }

    // Fonction pour ajouter dynamiquement une image à la liste
    public static void AddNewImage(Sprite newSprite)
    {
        spriteList.Add(newSprite);
    }
}
