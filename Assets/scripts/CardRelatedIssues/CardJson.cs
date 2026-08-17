using UnityEngine;
using System;
using System.Collections.Generic;

namespace CardMaker
{
    [System.Serializable]
    public class CardData
    {
        public int id;
        public string cardName;
        public CardType cardType;
        public CardEffect effect;
        public int damage;
        public int heal;
        public int actionPointCost;
        public int drawAmount;
        public bool breakEnviroment;
        public string information;


        

        public enum CardType { Quartz, pyrite, howlite, flourite, celestite, moonite }
        public enum CardEffect { OriginalDraw, SpellAttack, Heal, HeavyAttack, FreeDraws, GainAP }

        public enum test {type1, type2}
    }

    public class Cards
    {
        public CardData[] cards;
    }

      public class CardJson : MonoBehaviour
    {
        public TextAsset file;
        public class Cardstat
        {
            public CardData data;
        }
        
        public static CardJson current;

        public void Awake()
        {
            current = this;
        }
        //Takes a specific ID value as and It loads and translates the TEXTASSET ( JSON FILE WITH CARDS ) using JsonUtility, then iterates linearly through the internal cards array collection to extract and return the matching data info;
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
        // extracts all the card data from the TEXT asset json file
        public List<CardData> GetAllCards()
        {
            Cards collection = JsonUtility.FromJson<Cards>(file.text);
            return new List<CardData>(collection.cards);
        }
        }
    
}
