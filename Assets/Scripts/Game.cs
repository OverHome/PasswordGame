using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using UnityEngine.SceneManagement;
using UnityEditor;
using Unity.VisualScripting;

public class Game : MonoBehaviour
{
    [SerializeField] private TMP_InputField textEditor;
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private GameObject errorContainter;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject mainGame;
    [SerializeField] private GameObject Container;
    [SerializeField] private Sprite[] sprite;

    private string[] manth = new[]
    {
        "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november",
        "december"
    };

    private string[] random_words = new[]
    {
        "fox","wolf","rider","megamind","purple","building","bike","children","boys","girls","skibidi","camera","car","grass",
        "lion","sky"
    };
    private string[] rim = new[] { "I", "V", "X", "L", "C", "D", "M" };
    private string capcha;
    private string notReverseWord;
    private string reverseWord = "";
    private List<ErrorBlock> errors;
    private void Start()
    {
        errors = new List<ErrorBlock>();
        capcha = RandomCapcha();
        ReverseWord();
    }

    public void CheckPassword()
    {
        string password = textEditor.text;
        Debug.ClearDeveloperConsole();
        Write_Text("В пароле должно быть больше 5 символов.", 0, password.Length < 5);
        Write_Text("В пароле должна быть хоть одна цифра.", 1, !password.Any(p => "1234567890".Contains(p)));
        Write_Text("В пароле должна быть заглавная буква.", 2, !password.Any(p => char.IsUpper(p)));
        Write_Text("В пароле должна быть специальный символ (!,~,#,$,%,^,&,*).", 3, !password.Any(p => !char.IsLetterOrDigit(p)));
        Write_Text("В пароле сумма цифр должна быть равна 35.", 4, password.Sum(p => "123456789".Contains(p) ? Convert.ToInt16((Convert.ToString(p))) : 0) != 35);
        Write_Text("В пароле не должно быть трёх идущих подряд символов.", 5, triple_check(password));
        Write_Text("В пароле должен быть написан месяц на английском языке.", 6, !manth.Any(m => password.ToLower().Contains(m)));
        Write_Text("В пароле должна быть минимум одна римская цифра.", 7, !rim.Any(r => password.Contains(r)));
        Write_Text($"Должен содержать нашу капчу: \"{capcha}\"", 8, !password.Contains(capcha), true);
        Write_Text("В пароле должно быть написано сегодняшнее число", 9, !password.Contains(DateTime.Now.Day.ToString()));
        Write_Text("", 10, IMGCheck(password,10), true);
        Write_Text($"Введите слово \"{notReverseWord}\", написанное наоборот",11,!password.ToLower().Contains(reverseWord),false);
        /*Write_Text("", 12, IMGCheck(password,12), true);
        Write_Text("", 13, IMGCheck(password,13), true);
        Write_Text("", 14, IMGCheck(password,14), true);
        */IsWinning();
    }

    // Метод создания слов в обратном порядке
    private void ReverseWord()
    {
        Random rand = new Random();
        notReverseWord = random_words[rand.Next(random_words.Length)];
        for (int i = 0; i < notReverseWord.Length; i++)
        {
            reverseWord += notReverseWord[notReverseWord.Length - i - 1];
        }
        print(reverseWord + " " + reverseWord.Length);
    }
    // Вспомогательный метод для поиска трех подряд символов
    private bool triple_check(string password) 
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
    // Метод генерации капчи с минимум одним числом
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
    // Вспомогательный метод для пересоздания капчи
    private void SetCapcha()
    {
        capcha = RandomCapcha();
        errors[8].Prefab.GetComponentInChildren<TextMeshProUGUI>().text = $"Должен содержать нашу капчу: \"{capcha}\"";
    }
    // Метод для проверки капч и т.д. с изображений
    private bool IMGCheck(string password, int index)
    {
        if (errors.Count >= index+1 && password.ToLower().Contains(errors[index].GetCode()))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    // Метод созданий и записи ошибок
    private void Write_Text(string text, int index, bool is_error = false, bool NeedButton = false)
    {
        if (errors.Count == 0 || (errors.All(p => !p.IsError) && errors.Count == index))
        {
            var inst = Instantiate(errorPanel, errorContainter.transform);
            Sprite codeInImage = null;
            inst.GetComponentInChildren<TextMeshProUGUI>().text = text;
            if (NeedButton)
            {
                inst.GetComponent<ChangeSize>().changeSizeTextWithButton();
                inst.transform.GetChild(1).GameObject().SetActive(true);
                if(index == 8) //Вариант при создании капчи и необходимости её перегенирировать
                {
                    inst.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Пересоздать капчу";
                    inst.GetComponentInChildren<Button>().onClick.AddListener(SetCapcha);
                }
                if(index == 10) // выбор рандомного спрайта
                {
                    inst.GetComponentInChildren<Button>().onClick.AddListener(inst.GetComponent<ChangeSize>().changeSize);
                    inst.GetComponentInChildren<Button>().onClick.AddListener(() => inst.transform.GetChild(2).GetComponent<Image>().GameObject().SetActive(true));
                    codeInImage = CreateCodeInImage();
                    inst.transform.GetChild(2).GameObject().GetComponent<Image>().sprite = codeInImage;
                }
            }
            else // корректировка размера текста если кнопки НЕТ
            {
                inst.GetComponent<ChangeSize>().changeSizeTextWithNOButton();
            }
            // Общий для всех процесс добавления в массив ошибок
            var errorObject = new ErrorBlock(inst);
            errorObject.SetError(is_error);
            if(codeInImage != null) // изменение текста и ответа на ошибку если есть изобраджение
            {
                string str = codeInImage.ToString();
                string[] QuestionAnswer = str.Split(" - ");
                errorObject.Prefab.GetComponentInChildren<TextMeshProUGUI>().text = QuestionAnswer[0] + "?";
                foreach(string q in QuestionAnswer)
                {
                    print(q);
                }
                errorObject.SetCode(QuestionAnswer[1].Remove(QuestionAnswer[1].Length - 21));
                print(QuestionAnswer[1].Remove(QuestionAnswer[1].Length - 21));
            }
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
                if (index == 10) // возврат панели с изображением к нормальному размеру
                {
                    errors[index].Prefab.GetComponent<ChangeSize>().changeSizeBack();
                    errors[index].Prefab.transform.GetChild(2).GetComponentInChildren<Image>().GameObject().SetActive(false);
                }
            }
        }
    }
    // Метод проверки на повторяющееся изображение
    private Sprite CreateCodeInImage()
    {
        Random random = new Random();
        Sprite codeInImage = sprite[random.Next(sprite.Length)];
        foreach (ErrorBlock i in errors)
        {
            if (i.Prefab.transform.GetChild(2).GameObject().GetComponent<Image>().sprite == codeInImage)
            {
                codeInImage = CreateCodeInImage();
            }
        }

        return codeInImage;
    }
    // Метод проверки победы
    private void IsWinning()
    {
        if(errors.All(e => !e.IsError))
        {
            print("No Errors!");
            Time.timeScale = 0f;
            menu.SetActive(true);
            mainGame.SetActive(false);
        }
    }
    // Метод перезапуска игры
    private void Restart()
    {
        textEditor.text = "";
        errors.Clear();
        capcha = RandomCapcha();
        menu.SetActive(false);
        mainGame.SetActive(true);
        Delete(Container);
        Time.timeScale = 0.1f;
    }
    // Метод удаления объектов из объекта
    private void Delete(GameObject Container)
    {
        for(int i = 0; i < Container.transform.childCount; i++)
        {
            Destroy(Container.transform.GetChild(i).gameObject);
        }
    }
}