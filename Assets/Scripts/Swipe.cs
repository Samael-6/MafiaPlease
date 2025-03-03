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

    private Swipe otherSwipe; // R�f�rence � l'autre carte
    private Vector3 originalPosition; // Position d'origine de la carte

    private RectTransform canvasRect; // R�f�rence � la taille du canevas

    void Start()
    {
        startPos = transform.position;
        originalPosition = transform.position;  // Sauvegarder la position d'origine
        canvasRect = cardParent.GetComponent<RectTransform>(); // Obtenez la taille du canevas
        FindOtherSwipe();
        UpdateCardImage(); // Affiche la premi�re image
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
        print(canvasRect);
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        // D�placement libre dans toutes les directions
        Vector2 newPos = (Vector2)transform.position + eventData.delta;
        transform.position = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        float swipeDistance = Vector2.Distance(transform.position, startPos);

        // V�rification si la carte sort du canevas
        if (IsOutOfCanvas())
        {
            HideAndResetCard(); // Rendre invisible et r�initialiser
        }
        else
        {
            if (swipeDistance > swipeThreshold)
            {
                SwipeAway();
            }
            else
            {
                ResetPosition(); // Retour au centre si le swipe n'est pas assez grand
            }
        }
    }

    // V�rification si la carte est sortie du canevas
    bool IsOutOfCanvas()
    {
        Vector3[] canvasCorners = new Vector3[4];
        canvasRect.GetWorldCorners(canvasCorners); // Obtenir les coins du canevas en coordonn�es mondiales

        // V�rifie si la carte d�passe les bords du canevas
        if (transform.position.x < canvasCorners[0].x || transform.position.x > canvasCorners[2].x ||
            transform.position.y < canvasCorners[0].y || transform.position.y > canvasCorners[2].y)
        {
            return true;
        }
        return false;
    }

    void SwipeAway()
    {
        LeanTween.move(gameObject, transform.position * 2, moveSpeed)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(HideAndResetCard);
    }

    void HideAndResetCard()
    {
        gameObject.SetActive(false); // Rendre la carte invisible
        Invoke(nameof(ResetCardPosition), 0.3f); // La faire r�appara�tre apr�s 0.3s
    }

    void ResetCardPosition()
    {
        transform.position = originalPosition;  // R�initialisation de la position
        gameObject.SetActive(true); // Rendre la carte visible � nouveau

        // Change l'image de la carte en prenant le prochain �l�ment de la liste
        IncrementIndex();
        UpdateCardImage();

        // Met cette carte en arri�re et active l'autre
        transform.SetAsFirstSibling();
        otherSwipe.transform.SetAsLastSibling();
    }

    void ResetPosition()
    {
        LeanTween.move(gameObject, originalPosition, 0.2f).setEase(LeanTweenType.easeOutBounce); // Retour rapide au centre
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

    // Fonction pour ajouter dynamiquement une image � la liste
    public static void AddNewImage(Sprite newSprite)
    {
        spriteList.Add(newSprite);
    }
}
