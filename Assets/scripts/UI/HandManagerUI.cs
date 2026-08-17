using System.Collections.Generic;
using CardMaker;
using UnityEngine;
using UnityEngine.XR;

public class HandManagerUI : MonoBehaviour
{
    public Transform handUI;
    public GameObject cardprefab;
    public float fanSpread = 15f;
    public float cardSpace = 150f;
    public float Vertspace = 10f;
    
    [SerializeField] private Transform SpawnPreview;
    [SerializeField] private GameObject acitvePreview;
    public float inspectLiftAmount = 80f;
    public float highlightedScale = 150f;

    private List<GameObject> activeUICards = new();

    private int selectedIndex = 0;
    private int inspectedIndex = -1;

    public static HandManagerUI current;

    public void Awake()
    {
        current = this;
    }

    // REFRESHES THE CURRENT ACTIVE HAND UI BASED
    public void RefreshHand()
    {
        foreach (GameObject obj in activeUICards)
        {
            Destroy(obj);
        }
        activeUICards.Clear();

        int cardCount = DeckManager.current.hand.Count;
        if(cardCount == 0)
        {
            return;
        }

        if (selectedIndex >= cardCount)
        {
            selectedIndex = cardCount - 1;
            if (selectedIndex < 0)
            {
                selectedIndex = 0;
            }
        }

        if (cardCount == 1)
        {
            GameObject ShowCard = Instantiate(cardprefab, handUI);
            activeUICards.Add(ShowCard);

            if(ShowCard.TryGetComponent(out CardDisplayUI displayUI))
            {
                displayUI.SetupCard(DeckManager.current.hand[0]);
            }
            ShowCard.transform. localRotation = Quaternion.Euler(0f, 0f, 0f);
            ShowCard.transform.localPosition = new Vector3(0f, 0f, 0f);

            float verticalOffset = 0f;
            if (inspectedIndex == 0)
            {
                verticalOffset = inspectLiftAmount;
            }
            ShowCard.transform.localPosition = new Vector3(0f, verticalOffset, 0f);
            ShowCard.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);


            float targetScale = 150f;
            if (selectedIndex == 0)
            {
                targetScale = highlightedScale;
            }
            ShowCard.transform.localScale = new Vector3(targetScale, targetScale, 1f);
            return;
        }
        for(int i = 0; i < cardCount; i++)
        {
            GameObject ShowCard = Instantiate(cardprefab, handUI);
            activeUICards.Add(ShowCard);

            if(ShowCard.TryGetComponent(out CardDisplayUI displayUI))
            {
                displayUI.SetupCard(DeckManager.current.hand[i]);
            }


            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            ShowCard.transform. localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (cardSpace * (i - (cardCount - 1) / 2f));

            float normalizedpos = (2f * i / (cardCount - 1) - 1f);
            float verticalOffset = Vertspace * (1 - normalizedpos * normalizedpos);
            ShowCard.transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);


            ShowCard.transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);

            float targetScale = 100f;
            if (i == selectedIndex)
            {
                targetScale = highlightedScale;
            }
            ShowCard.transform.localScale = new Vector3(targetScale, targetScale, 1f);
        }
        
        if (inspectedIndex != -1 && selectedIndex < cardCount)
        {
            PreviewCard(DeckManager.current.hand[selectedIndex]);
        }
    }


    // BASED ON CARD SELECTED THROUGH INDEX RAISE IT TO SHOW THAT IT IS SELECTED THUS CHANIGNG ITS POSITION
    public void UpdateVisualSelection(int index)
    {
        ResetInspect();
        selectedIndex = index;
        RefreshHand();
    }

    public void PreviewCard(CardData data)
    {
        if(SpawnPreview == null)
        {
            return;
        }

        if (acitvePreview != null)
        {
            Destroy(acitvePreview);
        }

        acitvePreview = Instantiate(cardprefab,SpawnPreview);
        
        acitvePreview.transform.localPosition = new Vector3(0, 0, 0);
        acitvePreview.transform.localRotation = Quaternion.identity;

        if (acitvePreview.TryGetComponent(out CardDisplayUI displayUI))
        {
            displayUI.SetupCard(data);
        }
    }

    // PREVIEW BOX TO SHOWCASE THE CARD CLEARLY IN A DIFFERNT CORNER
    public void InspectCard()
    {
        if (inspectedIndex == selectedIndex)
        {
            inspectedIndex = -1;
            if (acitvePreview != null)
            {
                Destroy(acitvePreview);
            }
        }
        else
        {
            inspectedIndex = selectedIndex;
            if (selectedIndex < DeckManager.current.hand.Count)
            {
                PreviewCard(DeckManager.current.hand[selectedIndex]);
            }
        }
        RefreshHand();
    }

    // REMOVE THE BOX TO SHOWCASE THE CARD IN THE CORNER
    public void ResetInspect()
    {
        inspectedIndex = -1;
        if (acitvePreview != null)
        {
            Destroy(acitvePreview);
        }
    }
}
