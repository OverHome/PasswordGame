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
        rectTransform.sizeDelta = new Vector2(794.53f, 308.47f);
    }
    public void changeSizeBack()
    {
        RectTransform rectTransform = GameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(800f, 100f);
    }
    public void changeSizeTextWithButton()
    {
        RectTransform rectTransform = Text.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(579.29f, 100f);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x - 92, rectTransform.localPosition.y, 0);

    }
    public void changeSizeTextWithNOButton()
    {
        RectTransform rectTransform = Text.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(762f, 100f);
    }
}
