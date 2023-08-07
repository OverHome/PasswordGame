using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using TextEditor = UnityEditor.UI.TextEditor;

public class Game : MonoBehaviour
{
    [SerializeField] private TMP_InputField textEditor;
    [SerializeField] private GameObject errorMessage;
    [SerializeField] private GameObject errorContainter;
    [SerializeField] private ScrollRect scrollRect;
    private string[] manth = new[]
    {
        "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November",
        "December"
    };
    private string[] rim = new[] { "I", "V", "X", "L", "C", "D", "M" };
    private string capcha;
    private int last_right;
    private List<ErrorBlock> errors;
    private void Start()
    {
        errors = new List<ErrorBlock>();
        capcha = RandomCapcha();
    }

    public void CheckPassword()
    {
        var ver = scrollRect.transform.GetComponent<ScrollRect>().verticalNormalizedPosition;
        ApplyScrollPosition(scrollRect.transform.GetComponent<ScrollRect>(), 1f);
        ver = scrollRect.transform.GetComponent<ScrollRect>().verticalNormalizedPosition;
        string password = textEditor.text;
        scrollRect.transform.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
        ver = scrollRect.transform.GetComponent<ScrollRect>().verticalNormalizedPosition;
        Debug.ClearDeveloperConsole();
        Write_Text("В пароле должно быть больше 5 символов.", 0, password.Length < 5);
        Write_Text("В пароле должна быть хоть одна цифра.", 1, !password.Any(p => "1234567890".Contains(p)));
        Write_Text("В пароле должна быть заглавная буква.", 2, !password.Any(p => char.IsUpper(p)));
        Write_Text("В пароле должна быть специальный символ (!,~,#,$,%,^,&,*).", 3, !password.Any(p => !char.IsLetterOrDigit(p)));
        Write_Text("В пароле сумма цифр должна быть равна 35.", 4, password.Sum(p => "123456789".Contains(p) ? Convert.ToInt16((Convert.ToString(p))) : 0) != 35);
        Write_Text("В пароле не должно быть трёх идущих подряд цифр.", 5, triple_check(password));
        Write_Text("В пароле должен быть написан месяц на английском языке.", 6, !manth.Any(m => password.Contains(m)));
        Write_Text("В пароле должна быть минимум одна римская цифра.", 7, !rim.Any(r => password.Contains(r)));
        Write_Text($"Должен содержать нашу капчу: \"{capcha}\"", 8, !password.Contains(capcha));
        Write_Text("В пароле должно быть написано сегодняшнее число", 9, !password.Contains(DateTime.Now.Day.ToString()));
        scrollRect.transform.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
    }

    // ВСПОМОГАТЕЛЬНЫЙ МЕТОД ДЛЯ ПРОВЕРКИ ТРИ ИДУЩИХ ПОДРЯД ЧИСЛА
    private static bool triple_check(string password) 
    {
        for (int i = 0; i < (password.Length - 2); i++)
        {
            if (password[i] == password[i + 1] && password[i + 1] == password[i + 2])
            {
                return true;
            }
        }
        return false;
    }

    private string RandomCapcha()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[5];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }
        stringChars[random.Next(0, 4)] = chars[^random.Next(1, 8)]; // гарантированное число
        return new String(stringChars);
    }
    
    private void Write_Text(string text, int index, bool is_error = false)
    {
        if (errors.Count == 0 || (errors.All(p => !p.IsError) && errors.Count == index))
        {
            var inst = Instantiate(errorMessage, errorContainter.transform);
            inst.GetComponentInChildren<TextMeshProUGUI>().text = text;
            var errorObject = new ErrorBlock(inst);
            errorObject.SetError(is_error);
            errors.Add(errorObject);
        }
        if (index < errors.Count)
        {
            errors[index].SetError(is_error);
            if (is_error)
            {
                errors[index].Prefab.transform.SetAsFirstSibling();
            }
            else
            {
                errors[index].Prefab.transform.SetAsLastSibling();
            }
        }
    }
    IEnumerator ApplyScrollPosition(ScrollRect sr, float verticalPos)
    {
        yield return new WaitForEndOfFrame();
        sr.verticalNormalizedPosition = verticalPos;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)sr.transform);
    }
}







/*

       
       
       
        if (checker == false)
        {
            print("Хороший пароль");
        }
        text.text = check_result;*/
