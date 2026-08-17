using System;
using UnityEngine;
using CardMaker;

public class CardController : MonoBehaviour
{
    public static CardController current;
    private int freedraws;

    void Awake()
    {
        current = this;
    }

    public void Update()
    {
        
    }

    // checks if theres any free cards draw left to be used from the reasource pool
    public bool FreeDrawActive()
    {
        return freedraws > 0;
    }

    //If the card is a standard draw card and the hand is full, execution breaks. It checks if a free draw is available; if true, it decrements the counter, otherwise it deducts Action Points from PlayerManager and updates the UI boxes before routing the card data to UseCard and safely discarding non-basic actions.
    public void PlayCard(CardData card)
    {
        /*if (player.CurrentAP < card.actionPointCost)
        {
            Debug.Log("Not enough AP");
            return;
        }
        */


        if (card.id == 0 && DeckManager.current.hand.Count >= DeckManager.current.maxhand)
        {
            Debug.Log("hand full");
            return; 
        }

        bool DrawActionFree = (card.id == 0 && freedraws > 0);

        if (DrawActionFree)
        {
            freedraws--;
           // Debug.Log("test"+freedraws);
        }
        else
        {
            PlayerManager.current.UseAP(card.actionPointCost);
            APbarUI.current.UpdateApBoxes();
        }

        //Debug.Log("Executing Effect " + card.id + card.effect);
        UseCard(card);

        if (card.id != 0)
        {
            DeckManager.current.Discard(card);
        }
        
    }

    //Passes the specific CardEffect type assigned to the card using a switch statement to trigger corresponding game actions. It reads values like damage, heal, or drawAmount directly from the passed JSON card data, handles AP changes for updated draw ap cost, and passes commands downstream to PlayerManager to activate damaging ablilites.
    public void UseCard(CardData card)
    {
        switch (card.effect)
        {
            case CardData.CardEffect.SpellAttack:
                LightAttack(card.damage);
                return;
            case CardData.CardEffect.Heal:
                PlayerManager.current.Heal(card.heal);
                Debug.Log("heal");
                return;
            case CardData.CardEffect.HeavyAttack:
                HeavyAttack(card.damage);
                return;
            case CardData.CardEffect.GainAP:
                PlayerManager.current.GainAP(card.drawAmount);
                Debug.Log("AP gained");
                return;
            case CardData.CardEffect.FreeDraws:
                Debug.Log("drew");
                freedraws = card.drawAmount; 
                return;
            case CardData.CardEffect.OriginalDraw:

                if (PlayerManager.current.CurrentAP <= card.actionPointCost && !(card.id == 0 && freedraws > 0))
                {
                    {
                        Debug.Log("Not enough AP");
                        return;
                    }
                }
                else    
                {
                    for (int i = 0; i < card.drawAmount; i++)
                    {
                        DeckManager.current.DrawCard(1);
                    } 
                }

                return;
        }
    }

    // Checks if playermanager is active  and valid in the scenemanager , and if it succeeds fowards the specific damage from the json card data , and execute the heavy attack pattern following.
    void HeavyAttack(int damage)
    {
        if (PlayerManager.current != null)
        {

            PlayerManager.current.ExecuteHeavyAttackMesh(damage);
        }
    }

    // Checks if playermanager is active  and valid in the scenemanager , and if it succeeds fowards the specific damage from the json card data , and execute the Light attack pattern following.
    void LightAttack(int damage)
    {
        if (PlayerManager.current != null)
        {

            PlayerManager.current.ExecuteLightAttackMesh(damage);
        }
    }
}
