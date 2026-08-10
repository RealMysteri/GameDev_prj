using System.Collections.Generic;
using CardMaker;
using JetBrains.Annotations;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<CardData> deck = new();
    public List<CardData> drawPile = new();
    public List<CardData> hand = new();
    public List<CardData> discardPile = new();
    [SerializeField] private InventoryCards inventory;
    [SerializeField] public PlayerManager player;
    public int maxhand = 4;
    public int currentDrawAPCost = 1;

    [SerializeField] private CardLoader loader;
    [SerializeField] private HandManagerUI UI;

    void Start()
    {

        LoadDeck();
        ShuffleDeck();
        StartingHand();
    }

    public void Update()
    {

    }

    public void DrawCardAction()
    {
        DrawCard(currentDrawAPCost);
    }

    public void LoadDeck()
    {
        deck.Clear();
        deck.AddRange(inventory.ownedCards);
    }
    public void ShuffleDeck()
    {
        drawPile.Clear();

        drawPile.AddRange(deck);

        for (int i = 0; i < drawPile.Count; i++)
        {
            int random = Random.Range(i, drawPile.Count);

            CardData temp = drawPile[i];
            drawPile[i] = drawPile[random];
            drawPile[random] = temp;
        }

    }

    public void StartingHand()
    {
        for (int i = 0; i < maxhand; i++)
        {
            DrawCard(0);
        }
    }
    
    public void DrawCard(int APcost)
    {
        if (player.CurrentAP <= APcost)
        {
            Debug.Log("Not enough AP");
            return;
        }


        if(hand.Count >= maxhand)
        {
            return;
        }

        if(drawPile.Count == 0)
        {
            RefillDeck();
        }

        CardData drawnCard = drawPile[0];

        drawPile.RemoveAt(0);

        hand.Add(drawnCard);

        player.UseAP(APcost);

        if (currentDrawAPCost == 0)
        {
            currentDrawAPCost = 1;
        }

        UI.RefreshHand();
        
    }

    public void Discard(CardData card)
    {
        hand.Remove(card);

        discardPile.Add(card);
        UI.RefreshHand();
    }

    public void RefillDeck()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear(); 
        ShuffleDeck();
    }

        public void SetNextDrawFree()
    {
        currentDrawAPCost = 0;
        UI.RefreshHand();
    }

}
