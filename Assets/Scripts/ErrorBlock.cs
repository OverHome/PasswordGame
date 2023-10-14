using Unity;
using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ErrorBlock
{
    public GameObject Prefab { get; set; }
    public bool IsError { get; set; }
    private TextMeshProUGUI text;
    private string code = "";
    private int index;
    
    public ErrorBlock(GameObject prefab, int new_index)
    {
        Prefab = prefab;
        text = prefab.GetComponentInChildren<TextMeshProUGUI>();
        index = new_index;
    }

    public int Index{ get => index;}
    public void SetError(bool isError, Sprite[] trueOrFalse, Sprite[] colorOfPanel, int shift)
    {
        IsError = isError;
        text.color = isError ? Color.red : Color.green;
        var upperText = Prefab.transform.GetChild(4).GameObject().GetComponent<TextMeshProUGUI>();
        upperText.text = isError ? "Условие не выполнено!" : "Условие выполнено!";
        upperText.color = isError ? Color.red : Color.green;
        Prefab.transform.GetChild(3).GameObject().GetComponent<Image>().sprite =
            isError ? trueOrFalse[shift + 0] : trueOrFalse[shift + 1];
        Prefab.GetComponent<Image>().sprite = isError ? colorOfPanel[shift + 0] : colorOfPanel[shift + 1];
    }
    public string GetCode()
    {
        return code;
    }
    public void SetCode(string Code)
    {
        code = Code;
    }
}