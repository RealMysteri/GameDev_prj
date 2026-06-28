using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMaker
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]

    public class Card : ScriptableObject
    {
        public string cardName;
        public CardType cardType;
        public int damage;
        public int heal;
        public  List<DamageType> damageType;
        public Sprite sprite;
        public enum CardType
        {
            Quartz,

            pyrite,

            howlite,

            flourite,

            celestite,

            moonite
        }

        public enum DamageType
        {
            Quartz,

            pyrite,

            howlite,

            flourite,

            celestite,

            moonite
        }
    }
}