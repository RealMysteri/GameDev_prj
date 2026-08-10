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
        public string spritepath;

        public DamageType[] damageType;
        

        public enum CardType { Quartz, pyrite, howlite, flourite, celestite, moonite }
        public enum DamageType { Quartz, pyrite, howlite, flourite, celestite, moonite }
        public enum CardEffect { SpellAttack, Heal, HeavyAttack, DrawCards, GainAP }
    }

    public class Cards
    {
        public CardData[] cards;
    }
}
