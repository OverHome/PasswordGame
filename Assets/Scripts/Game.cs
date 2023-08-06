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
    public TextMeshProUGUI text;
    private void Start()
    {
        capcha = RandomCapcha();
    
    }

    public void CheckPassword()
    {
        bool checker = false;
        string passwod = textEditor.text;
        string check_result = null;
        
        Debug.ClearDeveloperConsole();
        if (passwod.Length < 5)
        {
            print("Должен содержать 5 символов");
            checker = true;
            check_result += "<color=red>Должен содержать 5 символов</color>\n";
        } else { check_result += "<color=green>Должен содержать 5 символов</color>\n"; }
        if (!passwod.Any(p => "123456789".Contains(p)))
        {
            print("Должен содержать хотя бы 1 цифру");
            checker = true;
            check_result += "<color=red>Должен содержать хотя бы 1 цифру</color>\n";
        } else { check_result += "<color=green>Должен содержать хотя бы 1 цифру</color>\n"; }
        if (!passwod.Any(p => char.IsUpper(p)))
        {
            print("Должен содержать большую букву");
            checker = true;
            check_result += "<color=red>Должен содержать большую букву</color>\n";
        } else { check_result += "<color=green>Должен содержать большую букву</color>\n"; }
        if (!passwod.Any(p => !char.IsLetterOrDigit(p))) // Починил не рабочий метод на проверку спец. символа
        {
            print("Должен содержать специальный символ");
            checker = true;
            check_result += "<color=red>Должен содержать специальный символ</color>\n";
        } else { check_result += "<color=green>Должен содержать специальный символ</color>\n"; }
        if (passwod.Sum(p => "123456789".Contains(p) ? Convert.ToInt16((Convert.ToString(p))) : 0) != 35)
        {
            print("Сумма всех цифр должна быть равно 35");
            checker = true;
            check_result += "<color=red>Сумма всех цифр должна быть равно 35</color>\n";
        } else { check_result += "<color=green>Сумма всех цифр должна быть равно 35</color>\n"; }
        if (triple_check(passwod))
        {
            print("Не должен содержать три подряд одинаковых числа");
            checker = true;
            check_result += "<color=red>Не должен содержать три подряд одинаковых числа</color>\n";
        } else { check_result += "<color=green>Не должен содержать три подряд одинаковых числа</color>\n"; }
        if (!manth.Any(m => passwod.Contains(m)))
        {
            print("Должен содержать месяц года");
            checker = true;
            check_result += "<color=red>Должен содержать месяц года</color>\n";
        } else { check_result += "<color=green>Должен содержать месяц года</color>\n"; }
        if (!rim.Any(r => passwod.Contains(r)))
        {
            print("Должен содержать римскую цифру");
            checker = true;
            check_result += "<color=red>Должен содержать римскую цифру</color>\n";
        } else { check_result += "<color=green>Должен содержать римскую цифру</color>\n"; }
        if (!passwod.Contains(capcha))
        {
            print($"Должен содержать нашу капчу: \"{capcha}\"");
            checker = true;
            check_result += $"<color=red>Должен содержать нашу капчу: \"{capcha}\"</color>\n";
        } else { check_result += $"<color=green>Должен содержать нашу капчу: \"{capcha}\"</color>\n"; }
        if (!passwod.Contains(DateTime.Now.Day.ToString()))
        {
            print("Должен содержать сегоднишни день");
            checker = true;
            check_result += "<color=red>Должен содержать сегоднишни день</color>\n";
        } else { check_result += "<color=green>Должен содержать сегоднишни день</color>\n"; }
        if (checker == false)
        {
            print("Хороший пароль");
        }
        text.text = check_result;
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
}
