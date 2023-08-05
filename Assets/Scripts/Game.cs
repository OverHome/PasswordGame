using System;
using System.Collections;
using System.Collections.Generic;
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

    private string[] manth = new[]
    {
        "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November",
        "December"
    };

    private string[] rim = new[] { "I", "V", "X", "L", "C", "D", "M" };
    private string capcha;

    private void Start()
    {
        capcha = RandomCapcha();
    
    }

    public void CheckPassword()
    {
        string passwod = textEditor.text;
        Debug.ClearDeveloperConsole();
        if (passwod.Length < 5)
        {
            print("Должен содержать 5 символов");
        }
        else if (!passwod.Any(p=> "123456789".Contains(p)))
        {
            print("Должен содержать хотя бы 1 цифру");
        }
        else if (!passwod.Any(p=> char.IsUpper(p)))
        {
            print("Должен содержать большую букву");
        }
        else if (!passwod.Any(p=> char.IsLetterOrDigit(p)))
        {
            print("Должен содержать специальный символ");
        }else if (passwod.Sum(p=> "123456789".Contains(p)? Convert.ToInt16((Convert.ToString(p))): 0)!=35)
        {
            print("Сумма всех цифр должна быть равно 35");
        }else if (!manth.Any(m=> passwod.Contains(m)))
        {
            print("Должен содержать месяц года");
        }
        else if (!rim.Any(r=> passwod.Contains(r)))
        {
            print("Должен содержать римскую цифру");
        }else if (!passwod.Contains(capcha))
        {
            print($"Должен содержать нашу капчу: \"{capcha}\"");
        }
        else if(!passwod.Contains(DateTime.Now.Day.ToString()))
        {
            print("Должен содержать сегоднишни день");
        }else 
        {
            print("Хороший пароль");
        }
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

        return new String(stringChars);
    }
}
