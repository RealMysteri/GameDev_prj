using UnityEngine;
using TMPro;

public class PlayerUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI apText;

    // Public method that ANY script can call to refresh the screen
    public void UpdateDisplay(int currentHealth, int maxHealth, int currentAP, int maxAP)
    {
        if (healthText != null)
        {
            healthText.text = $"HP: {currentHealth} / {maxHealth}";
        }

        if (apText != null)
        {
            apText.text = $"AP: {currentAP} / {maxAP}";
        }
    }
}
