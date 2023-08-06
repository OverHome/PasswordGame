using Unity;
using System;
using UnityEngine;
using TMPro;

class ErrorBlock
{
    public GameObject Prefab { get; set; }
    public bool IsError { get; set; }
    private TextMeshProUGUI text;

    public ErrorBlock(GameObject prefab)
    {
        Prefab = prefab;
        text = prefab.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetError(bool isError)
    {
        IsError = isError;
        text.color = isError ? Color.red : Color.green;
    }
}