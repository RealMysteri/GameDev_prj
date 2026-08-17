using UnityEngine;
using CardMaker;
using System.Collections.Generic;

public class InventoryCards : MonoBehaviour
{
    public static InventoryCards current;

    public List<CardData> ownedCards = new();

    // UNPACKS ALL THE CARD DATA FROM JSON FILE USING CARD LAODER AND PUT IT INTO OWNED CARD
    public void Awake()
    {
        current = this;
        ownedCards = CardJson.current.GetAllCards();
    }

    // Can be used to ADD CARD and is able to be used my otehr scripts
    public void AddCard(CardData card)
    {
        ownedCards.Add(card);
    }
    // Can be used to REMOVE CARD and is able to be used my otehr scripts
    public void RemoveCard(CardData card)
    {
        ownedCards.Remove(card);
    }
    // Can be used to FIND CARD and is able to be used my otehr scripts
    public bool HasCard(CardData card)
    {
        return ownedCards.Contains(card);
    }

    // Can be used to get specifict CARD and is able to be used my otehr scripts
    public CardData GetCardFromInventory(int CardId)
    {
        foreach(CardData card in ownedCards)
        {
            if (card.id == CardId)
            {
                return card;
            }
        }

        return null;
    }
}
