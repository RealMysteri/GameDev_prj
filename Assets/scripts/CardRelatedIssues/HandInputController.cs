using CardMaker;
using UnityEngine;
using UnityEngine.XR;

public class HandInputController : MonoBehaviour
{
    [SerializeField] private DeckManager deckmanager;
    [SerializeField] private HandManagerUI handui;  
    [SerializeField] private CardController cardController;

    private int selectedcard = 0;
    private int  maxcardsui = 4;

    void Update()
    {
        HandleAction();
    }


    private void HandleAction()
    {
        int cardCount = deckmanager.hand.Count;

        bool visualChanged = false;

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        { 
            selectedcard = 0; 
            visualChanged = true; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            if (cardCount > 1)
            {
                selectedcard = 1;
                visualChanged = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            if (cardCount > 2)
            {
                selectedcard = 2;
                visualChanged = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) 
        {
            if (cardCount > 2)
            {
                selectedcard = 3;
                visualChanged = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            handui.InspectCard();
            return; 
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedcard < cardCount)
            {
                CardData cardToPlay = deckmanager.hand[selectedcard];
                if (deckmanager.player.CurrentAP >= cardToPlay.actionPointCost)
                {
                    cardController.PlayCard(cardToPlay);

                    handui.ResetInspect();
                    handui.RefreshHand();
                }
                else
                {
                    Debug.Log("Cannot play: Insufficient Action Points!");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            deckmanager.DrawCard(1);
            return;
        }

        if (visualChanged)
        {
            handui.UpdateVisualSelection(selectedcard);
        }
    }
}
