using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APbarUI : MonoBehaviour
{
    [SerializeField] private Sprite activeBox;  
    
    private List<Image> boxImages = new List<Image>();
    public static APbarUI current;

    void Awake()
    {
        current = this;
    }

    void Start()
    {

        foreach (Transform child in transform)
        {
            Image img = child.GetComponent<Image>();
            if (img != null) 
            {
                boxImages.Add(img);
                if(activeBox != null) img.sprite = activeBox;
            }
        }
    }

    public void UpdateApBoxes()
    {
        int currentAP = PlayerManager.current.ActionPoint;

        for (int i = 0; i < boxImages.Count; i++)
        {
            if (i < currentAP)
            {
                boxImages[i].gameObject.SetActive(true);
            }
            else
            {
                boxImages[i].gameObject.SetActive(false); 
            }
        }
    }
}
