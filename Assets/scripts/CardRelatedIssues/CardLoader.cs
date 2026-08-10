using UnityEngine;
using CardMaker;
using System.IO;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

public class CardLoader : MonoBehaviour
{
    public TextAsset file;
    public class Cardstat
    {
        public CardData data;
    }
    
    public void Awake()
    {
        
    }

    public CardData GetCard(int id)
    {
        Cards collection = JsonUtility.FromJson<Cards>(file.text);
        foreach(CardData card in collection.cards)
        {
            if(card.id == id)
            {
                return card;
            }
        }
        return null;
    }

    public List<CardData> GetAllCards()
    {
        Cards collection = JsonUtility.FromJson<Cards>(file.text);
        return new List<CardData>(collection.cards);
    }
}
