using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightToDark : MonoBehaviour
{
    [SerializeField] private GameObject mainGame;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject canvas;
    public void ChangeLightToDark()
    {
        var gameScript = canvas.GetComponent<Game>();
        int shift = gameScript.getShift();
        if (shift == 0)  // От светлой к тёмной
        {
            print("L-D");
            gameScript.setShift(1);
            shift++;
            mainGame.transform.GetChild(0).GetComponent<Image>().sprite = gameScript.getSpriteMakePass()[shift]; // замена текста
            mainGame.transform.GetChild(2).GetComponent<Image>().sprite = gameScript.getInputFields()[shift]; // замена поля ввода
            panel.GetComponent<Image>().color = new Color(21/255f,19/255f,21/255f,255);
            mainGame.transform.GetChild(4).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                "<color=red>*</color><color=#FFFFFF>Обязательные условия</color>";
        }
        else if (shift == 1)
        {
            print("D-L");
            gameScript.setShift(0);
            shift--;
            mainGame.transform.GetChild(0).GetComponent<Image>().sprite = gameScript.getSpriteMakePass()[shift]; // замена текста
            mainGame.transform.GetChild(2).GetComponent<Image>().sprite = gameScript.getInputFields()[shift]; // замена поля ввода
            panel.GetComponent<Image>().color = new Color(255/255f,239/255f,224/255f,255);
            mainGame.transform.GetChild(4).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                "<color=red>*</color><color=#000000>Обязательные условия</color>";

        }
    }
}
