using System;
using UnityEngine;
using CardMaker;

public class CardController : MonoBehaviour
{
    [SerializeField] public PlayerManager player;
    [SerializeField] public DeckManager deck;


    public void Update()
    {
        
    }

    void TestPlayCard()
    {
        if(deck.hand.Count == 0)
        {
            Debug.Log("No cards");
            return;
        }


        CardData card = deck.hand[0];

        string json = JsonUtility.ToJson(card, true);
        Debug.Log(json);
        PlayCard(card);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayCard(CardData card)
    {
        /*if (player.CurrentAP < card.actionPointCost)
        {
            Debug.Log("Not enough AP");
            return;
        }
        */
        player.UseAP(card.actionPointCost);

        UseCard(card);

        deck.Discard(card);
    }

    public void UseCard(CardData card)
    {
        switch (card.effect)
        {
            case CardData.CardEffect.SpellAttack:
                Debug.Log(card.damage);
                return;
            case CardData.CardEffect.Heal:
                player.Heal(card.heal);
                Debug.Log("heal");
                return;
            case CardData.CardEffect.HeavyAttack:
                HeavyAttack();
                return;
            case CardData.CardEffect.GainAP:
                player.GainAP(card.drawAmount);
                Debug.Log("AP gained");
                return;
            case CardData.CardEffect.DrawCards:
                Debug.Log("drew");
                for(int i = 0; i < card.drawAmount; i++)
                {
                    deck.DrawCard(0);
                }
                return;
        }
    }

    void HeavyAttack()
    {
        Debug.Log("works");
    }
}
