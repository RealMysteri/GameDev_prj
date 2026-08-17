using UnityEngine;
using CardMaker;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CardDisplayUI : MonoBehaviour
{
    public CardData card;
    public Image cardimage;
    public Image Textbox;
    public Image cardtype;
    public TMP_Text cardname;
    public TMP_Text actionpoint;
    public TMP_Text information;
    public List<Sprite> spriteChoice;

    // TURNS THE CARDS COLORED BASED ON THEIR CARD TYPE
    private Color[] typeColors = 
    {
        Color.whiteSmoke, 
        Color.yellow,
        Color.burlywood, 
        Color.magenta, 
        Color.antiqueWhite, 
        Color.ghostWhite 
    };
    //public image[] typeimages;

    // GET CARD DATA
    public void SetupCard(CardData data)
    {
        card = data;
        UpdateCardDisplay();
    }
    
    // UPDATES ALL UI INFOMRATION ONTO THE CARD CARD COLOR ,  TEXT BOX COLOR , CARDTPYE SPRITE, CARDNAME , HOW MUCH IT COST AND INFROMATION
    public void UpdateCardDisplay()
    {
        cardimage.color = typeColors[(int)card.cardType];
        Textbox.color = typeColors[(int)card.cardType];
        cardtype.sprite = spriteChoice[(int)card.effect];
        cardname.text = card.cardName;
        actionpoint.text = card.actionPointCost.ToString();
        information.text = card.information;

        bool isCurrentlyFree = (card.id == 0 && CardController.current != null && CardController.current.FreeDrawActive());

        if (isCurrentlyFree)
        {
            actionpoint.text = "0";
        }
        else
        {
            actionpoint.text = card.actionPointCost.ToString();
        }
    }

}
