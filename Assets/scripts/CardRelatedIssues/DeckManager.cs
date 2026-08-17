using System.Collections.Generic;
using CardMaker;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<CardData> deck = new();
    public List<CardData> drawPile = new();
    public List<CardData> hand = new();
    public List<CardData> discardPile = new();
    public int maxhand = 4;

    public static DeckManager current;

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        
        LoadDeck();
        ShuffleDeck();
        StartingHand();
    }

    public void Update()
    {

    }

    public void LoadDeck()
    {
        deck.Clear();
        deck.AddRange(InventoryCards.current.ownedCards);
        deck.RemoveAt(0);
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
        hand.Add(InventoryCards.current.GetCardFromInventory(0));
        for (int i = 0; i < maxhand; i++)
        {
            DrawCard(0);
        }
    }
    
    public void DrawCard(int APcost)
    {

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

        hand.Insert(0,drawnCard);



        HandManagerUI.current.RefreshHand();
        
    }

    public void Discard(CardData card)
    {
        hand.Remove(card);

        discardPile.Add(card);
        HandManagerUI.current.RefreshHand();
    }

    public void RefillDeck()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear(); 
        ShuffleDeck();
    }


}
