using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EngToRus : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject mainMenu;
    

    private Hashtable ruErrors = new Hashtable();
    private Hashtable engErrors = new Hashtable();

    public Hashtable RuErrors
    {
        get => ruErrors;
    }
    public Hashtable EngErrors
    {
        get => engErrors;
    }
    public void makeHash()
    {
        var gameScript = canvas.GetComponent<Game>();
        ruErrors.Add(0,"В пароле должно быть больше 5 символов." );
        ruErrors.Add(1, "В пароле должна быть хоть одна цифра.");
        ruErrors.Add(2, "В пароле должна быть заглавная буква.");
        ruErrors.Add(3, "В пароле должна быть специальный символ (!,~,#,$,%,^,&,*).");
        ruErrors.Add(4, "В пароле сумма цифр должна быть равна 45.");
        ruErrors.Add(5, "В пароле не должно быть трёх идущих подряд символов.");
        ruErrors.Add(6,"В пароле должен быть написан месяц на английском языке.");
        ruErrors.Add(7,"В пароле должна быть минимум одна римская цифра.");
        ruErrors.Add(8,"Должен содержать нашу капчу:");
        ruErrors.Add(9,"В пароле должно быть написано сегодняшнее число");
        ruErrors.Add(10, "Сколько здесь людей (ответ запишите цифровй)?");
        ruErrors.Add(11, $"Введите слово \"{gameScript.NotReverseWordGet}\", написанное наоборот");
        ruErrors.Add(12,"Размер пароля должен быть больше 40 символов");
        ruErrors.Add(13,"Напишите слово 'Да', если вы согласны что этот пароль хороший");
        ruErrors.Add(14,"Для доказательства, что вы не робот - разгадайте загадку и запишите ответ в пароль\nЦифра эта без очков, состоит из двух крючков");
        ruErrors.Add(15,"Напишите в пароль ответ: Столица России?");
        ruErrors.Add(16,"Напишите в пароль ответ: В каком году была создана игрушка Хаги Ваги?");
        ruErrors.Add(17,"Введите ответ на загадку: Мышь считала дырки в сыре, три плюс две ровно?");
        ruErrors.Add(18,"Вы точно не машина? Введите текущий год.");
        engErrors.Add(0,"The password must have more than 5 characters." );
        engErrors.Add(1, "The password must have at least one digit.");
        engErrors.Add(2, "The password must have a capital letter.");
        engErrors.Add(3, "The password must have a special character (!,~,#,$,%,^,&,*).");
        engErrors.Add(4, "In the password, the sum of the digits must be equal to 45.");
        engErrors.Add(5, "The password must not contain three consecutive characters.");
        engErrors.Add(6,"The password must contain the month in English.");
        engErrors.Add(7,"The password must contain at least one Roman numeral.");
        engErrors.Add(8,"The password must contain our captcha:");
        engErrors.Add(9,"Today's date should be written in the password.");
        engErrors.Add(10, "How many people are here (write down the answer with a number)?");
        engErrors.Add(11, $"Enter word \"{gameScript.NotReverseWordGet}\", written in reverse.");
        engErrors.Add(12,"The password size must be more than 40 characters.");
        engErrors.Add(13,"Write the word 'Yes' if you agree that this password is good.");
        engErrors.Add(14,"To prove that you are not a robot, solve the riddle and write answer in password\nThis figure is without glasses, consists of two hooks.");
        engErrors.Add(15,"Write answer in password: The capital of Russia?");
        engErrors.Add(16,"Write answer in password: In what year was the Hagi Wagi toy created?");
        engErrors.Add(17,"Enter the answer to the riddle: The mouse counted the holes in the cheese. Three plus two exactly?");
        engErrors.Add(18,"Are you sure you're not a car? Enter the current year.");
    }
    public void Translate()
    {
        var gameScript = canvas.GetComponent<Game>();
        List<ErrorBlock> errors = gameScript.GetErrors();
        int langShift = gameScript.LangShift;
        int shift = gameScript.getShift();
        if (langShift == 0) // ru ---> eng
        {
            langShift = 2; // eng
            mainMenu.transform.GetChild(0).GetComponent<Image>().sprite =
                gameScript.getSpriteMakePass()[shift / 2 + langShift]; // замена картинки "Придумай пароль"
            mainMenu.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text =
                "Enter password"; // замена плейсхолдера
            mainMenu.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = shift == 2
                ? "<color=red>*</color><color=#FFFFFF>Essential conditions</color>"
                : "<color=red>*</color><color=#000000>Essential conditions</color>";
            for (int i = 0; i < errors.Count; i++)
            {
                errors[i].Prefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    (string)engErrors[errors[i].Index];
                errors[i].Prefab.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text =
                    errors[i].IsError ? "Condition is not met" : "Condition is met";
            }
            gameScript.LangShift = langShift;
        }
        else if (langShift == 2) // eng ---> ru
        {
            langShift = 0; // ru
            mainMenu.transform.GetChild(0).GetComponent<Image>().sprite =
                gameScript.getSpriteMakePass()[shift / 2 + langShift]; // замена картинки "Придумай пароль"
            mainMenu.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text =
                "Введите пароль"; // замена плейсхолдера
            mainMenu.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = shift == 2
                ? "<color=red>*</color><color=#FFFFFF>Обязательные условия</color>"
                : "<color=red>*</color><color=#000000>Обязательные условия</color>";
            for (int i = 0; i < errors.Count; i++)
            {
                errors[i].Prefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    (string)ruErrors[errors[i].Index];
                errors[i].Prefab.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text =
                    errors[i].IsError ? "Условие не выполнено" : "Условие выполнено";
            }
            gameScript.LangShift = langShift;
        }
    }
}