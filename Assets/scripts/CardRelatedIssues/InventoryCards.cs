using UnityEngine;
using CardMaker;
using System.Collections.Generic;

public class InventoryCards : MonoBehaviour
{
    public List<CardData> ownedCards = new();
    [SerializeField] private CardLoader loader;

    public void Awake()
    {
        ownedCards = loader.GetAllCards();
    }

    public void AddCard(CardData card)
    {
        ownedCards.Add(card);
    }

    public void RemoveCard(CardData card)
    {
        ownedCards.Remove(card);
    }

    public bool HasCard(CardData card)
    {
        return ownedCards.Contains(card);
    }
}
