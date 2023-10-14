using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class LightToDark : MonoBehaviour
{
    [SerializeField] private GameObject mainGame;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Sprite[] buttons;
    public void ChangeLightToDark()
    {
        var gameScript = canvas.GetComponent<Game>();
        int shift = gameScript.getShift();
        int langShift = gameScript.LangShift;
        List<ErrorBlock> errors = gameScript.GetErrors();
        if (shift == 0)  // От светлой к тёмной
        {
            print("L-D");
            gameScript.setShift(2);
            shift += 2;
            mainGame.transform.GetChild(0).GetComponent<Image>().sprite = gameScript.getSpriteMakePass()[shift / 2 + langShift]; // замена текста
            mainGame.transform.GetChild(2).GetComponent<Image>().sprite = gameScript.getInputFields()[shift/2]; // замена поля ввода
            mainGame.transform.GetChild(5).GetComponent<Image>().sprite = gameScript.getSliders()[shift / 2]; // замена кнопки смены цвета
            panel.GetComponent<Image>().color = new Color(21/255f,19/255f,22/255f,255);
            if (langShift == 2)
            {
                mainGame.transform.GetChild(4).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    "<color=red>*</color><color=#FFFFFF>Essential condition</color>";
            } else if (langShift == 0)
            {
                mainGame.transform.GetChild(4).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    "<color=red>*</color><color=#FFFFFF>Обязательное условие</color>";
            }
            mainGame.transform.GetChild(4).transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 255);
            for(int i =0; i < errors.Count; i++)
            {
                errors[i].Prefab.GetComponent<Image>().sprite = errors[i].IsError
                    ? gameScript.getColorOfPanels()[shift]
                    : gameScript.getColorOfPanels()[shift + 1];
                if (gameScript.getRefreshButton()
                    .Contains(errors[i].Prefab.transform.GetChild(1).GetComponent<Image>().sprite))
                {
                    errors[i].Prefab.transform.GetChild(1).GetComponent<Image>().sprite = gameScript.getRefreshButton()[shift / 2];
                }
                else
                {
                    errors[i].Prefab.transform.GetChild(1).GetComponent<Image>().sprite = gameScript.getIMGButtonSprite()[shift / 2];
                    errors[i].Prefab.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = gameScript.getIMGButtonSprite()[shift / 2];
                }
            }
        }
        else if (shift == 2)
        {
            print("D-L");
            gameScript.setShift(0);
            shift -= 2;
            mainGame.transform.GetChild(0).GetComponent<Image>().sprite = gameScript.getSpriteMakePass()[shift / 2 + langShift]; // замена текста
            mainGame.transform.GetChild(2).GetComponent<Image>().sprite = gameScript.getInputFields()[shift/2]; // замена поля ввода
            mainGame.transform.GetChild(5).GetComponent<Image>().sprite = gameScript.getSliders()[shift / 2]; // замена кнопки смены цвета
            panel.GetComponent<Image>().color = new Color(255/255f,239/255f,224/255f,255);
            mainGame.transform.GetChild(4).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                "<color=red>*</color><color=#000000>Обязательные условия</color>";
            mainGame.transform.GetChild(4).transform.GetChild(1).GetComponent<Image>().color = new Color(0, 0, 0, 255);
            for(int i =0; i < errors.Count; i++)
            {
                errors[i].Prefab.GetComponent<Image>().sprite = errors[i].IsError
                    ? gameScript.getColorOfPanels()[shift]
                    : gameScript.getColorOfPanels()[shift + 1];
                if (gameScript.getRefreshButton()
                    .Contains(errors[i].Prefab.transform.GetChild(1).GetComponent<Image>().sprite))
                {
                    errors[i].Prefab.transform.GetChild(1).GetComponent<Image>().sprite = gameScript.getRefreshButton()[shift / 2];
                }
                else
                {
                    errors[i].Prefab.transform.GetChild(1).GetComponent<Image>().sprite = gameScript.getIMGButtonSprite()[shift / 2];
                    errors[i].Prefab.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = gameScript.getIMGButtonSprite()[shift / 2];

                }
            }
        }
    }
}
