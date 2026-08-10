using UnityEngine;
using CardMaker;
using UnityEngine.UI;
using TMPro;

public class CardDisplayUI : MonoBehaviour
{
    public CardData card;
    public Image cardimage;
    public TMP_Text cardname;
    public TMP_Text stat;
    public TMP_Text information;
    [SerializeField] public PlayerManager player;
    [SerializeField] private CardLoader loader;

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

    public void SetupCard(CardData data)
    {
        card = data;
        UpdateCardDisplay();
    }
    
    public void UpdateCardDisplay()
    {
        cardimage.color = typeColors[(int)card.damageType[0]];
        
        cardname.text = card.cardName;
        stat.text = card.damage.ToString();
        information.text = card.information;
    }

}
