using UnityEngine.UI;
using UnityEngine;

public class HPbarUI : MonoBehaviour
{
    [SerializeField] private Image hpFillImage; // Drag your HP_Fill Image component here
    public static HPbarUI current;

    void Awake()
    {
        current = this;
    }
    public void UpdateHpBar()
    {
        if (hpFillImage != null && PlayerManager.current.maxhealth > 0)
        {
            hpFillImage.fillAmount = (float)PlayerManager.current.currenthealth / PlayerManager.current.maxhealth;
        }
    }
}
