using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSize : MonoBehaviour
{
    [SerializeField] private GameObject GameObject;
    [SerializeField] private GameObject Text;

    public void changeSize()
    {
        RectTransform rectTransform = GameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(400f, 500f);
    }
    public void changeSizeBack()
    {
        RectTransform rectTransform = GameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(440f, 115f);
    }
    public void changeSizeTextWithButton()
    {
        RectTransform rectTransform = Text.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(240f, 28f);
    }
    public void changeSizeTextWithNOButton()
    {
        RectTransform rectTransform = Text.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(400f, 28f);
    }
}
