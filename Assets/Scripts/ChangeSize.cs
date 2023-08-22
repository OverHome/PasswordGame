using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSize : MonoBehaviour
{
    [SerializeField] private GameObject GameObject;

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
}
