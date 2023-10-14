using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using UnityEngine.SceneManagement;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    [SerializeField] private TMP_InputField textEditor;
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private GameObject errorContainter;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject mainGame;
    [SerializeField] private GameObject Container;

    [SerializeField] private Sprite[] sprite;

    // ЦВета
    [SerializeField] private Sprite[] makePassSprites;
    [SerializeField] private Sprite[] inputFields;
    [SerializeField] private Sprite[] trueOrFalse;
    [SerializeField] private Sprite[] colorOfPanels;
    [SerializeField] private Sprite[] reFreshButton;
    [SerializeField] private Sprite[] slidersSprites;
    [SerializeField] private Sprite[] imageButtonsSprites;

    // Цвета для меню
    [SerializeField] private Sprite[] menuBackground;

    [SerializeField] private Sprite[] buttonBackground;

    // Для других скриптов
    [SerializeField] private GameObject buttonWithTranslate;

    private string[] manth = new[]
    {
        "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november",
        "december"
    };

    private string[] random_words = new[]
    {
        "fox", "wolf", "rider", "megamind", "purple", "building", "bike", "children", "boys", "girls", "skibidi",
        "camera", "car", "grass",
        "lion", "sky", "dog", "cat", "teacher", "school", "play", "witch"
    };

    private string[] rim = new[] { "I", "V", "X", "L", "C", "D", "M" };
    private string capcha;
    private string notReverseWord;
    private string reverseWord = "";
    private List<ErrorBlock> errors = new List<ErrorBlock>();
    private int shift;

    private int
        langShift = 2; // 0 - ru, 2 - eng Здесь задаётся изначальное значение. То есть параметр пользователя из браузера(не уверен, что из браузера)

    private void Start()
    {
        langShift = langShift == 2 ? langShift = 0 : langShift = 2;
        buttonWithTranslate.GetComponent<EngToRus>().Translate();
        capcha = RandomCapcha();
        ReverseWord();
        buttonWithTranslate.GetComponent<EngToRus>().makeHash();
    }

    public void CheckPassword()
    {
        string password = textEditor.text;
        Hashtable currentHash = langShift == 2
            ? buttonWithTranslate.GetComponent<EngToRus>().EngErrors
            : buttonWithTranslate.GetComponent<EngToRus>().RuErrors;
        Debug.ClearDeveloperConsole();
        Write_Text((string)currentHash[0], 0, password.Length < 5); // 1
        Write_Text((string)currentHash[1], 1, !password.Any(p => "1234567890".Contains(p))); // 2
        Write_Text((string)currentHash[2], 2, !password.Any(p => char.IsUpper(p))); // 3
        Write_Text((string)currentHash[3], 3, !password.Any(p => !char.IsLetterOrDigit(p))); // 3
        Write_Text((string)currentHash[4], 4,
            password.Sum(p => "123456789".Contains(p) ? Convert.ToInt16((Convert.ToString(p))) : 0) != 45);
        Write_Text((string)currentHash[5], 5, triple_check(password)); // 5
        Write_Text((string)currentHash[6], 6, !manth.Any(m => password.ToLower().Contains(m))); // 6
        Write_Text((string)currentHash[7], 7, !rim.Any(r => password.Contains(r))); // 7
        Write_Text((string)currentHash[8] + capcha, 8, !password.Contains(capcha), true); // 8
        Write_Text((string)currentHash[9], 9, !password.Contains(DateTime.Now.Day.ToString())); // 9
        Write_Text((string)currentHash[10], 10, IMGCheck(password, 10), true); // 10
        Write_Text((string)currentHash[11], 11, !password.ToLower().Contains(reverseWord), false); // 11
        Write_Text((string)currentHash[12], 12, !(password.Length <= 40)); // 13
        Write_Text((string)currentHash[13], 13, !password.Contains("Да"));
        Write_Text((string)currentHash[14], 14, !password.Contains("3"));
        Write_Text((string)currentHash[15], 15, !password.ToLower().Contains("москва"));
        Write_Text((string)currentHash[16], 16, !password.Contains("1984"));
        Write_Text((string)currentHash[17], 17, !password.Contains("5"));
        Write_Text((string)currentHash[18], 18, !password.Contains(DateTime.Now.Year.ToString()));
        IsWinning();
    }

    public string NotReverseWordGet
    {
        get => notReverseWord;
    }

    public string Capcha
    {
        get => capcha;
    }

    public Sprite[] getIMGButtonSprite()
    {
        return imageButtonsSprites;
    }

    public Sprite[] getSliders()
    {
        return slidersSprites;
    }

    public Sprite[] getRefreshButton()
    {
        return reFreshButton;
    }

    public Sprite[] getColorOfPanels()
    {
        return colorOfPanels;
    }

    public List<ErrorBlock> GetErrors()
    {
        return errors;
    }

    public void setShift(int shift)
    {
        this.shift = shift;
    }

    public int getShift()
    {
        return this.shift;
    }

    public int LangShift
    {
        get => langShift;
        set => langShift = value;
    }

    public Sprite[] getSpriteMakePass()
    {
        return makePassSprites;
    }

    public Sprite[] getInputFields()
    {
        return inputFields;
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
        var chars = "ABCDEFGHIJKLMNOPQRSTUVZabcdefghijklmnopqrstu0123456789";
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
        if (errors.Count >= index + 1 && password.ToLower().Contains(errors[index].GetCode()))
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
                if (index == 8) //Вариант при создании капчи и необходимости её перегенирировать
                {
                    inst.GetComponentInChildren<Button>().onClick.AddListener(SetCapcha);
                    inst.transform.GetChild(1).GetComponent<Image>().sprite = reFreshButton[shift / 2];
                }

                if (index == 10) // выбор рандомного спрайта
                {
                    inst.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite =
                        imageButtonsSprites[shift / 2];
                    inst.transform.GetChild(1).GetComponent<Image>().sprite = imageButtonsSprites[shift / 2];
                    inst.GetComponentInChildren<Button>().onClick
                        .AddListener(inst.GetComponent<ChangeSize>().changeSize);
                    inst.GetComponentInChildren<Button>().onClick.AddListener(() =>
                        inst.transform.GetChild(2).GetComponent<Image>().GameObject().SetActive(true));
                    inst.GetComponentInChildren<Button>().onClick
                        .AddListener(() => inst.transform.GetChild(3).GameObject().SetActive(false));
                    inst.GetComponentInChildren<Button>().onClick
                        .AddListener(() => inst.transform.GetChild(4).GameObject().SetActive(false));
                    codeInImage = CreateCodeInImage();
                    inst.transform.GetChild(2).GameObject().GetComponent<Image>().sprite = codeInImage;
                }
            }
            else // корректировка размера текста если кнопки НЕТ
            {
                inst.GetComponent<ChangeSize>().changeSizeTextWithNOButton();
            }

            // Общий для всех процесс добавления в массив ошибок
            var errorObject = new ErrorBlock(inst, index);
            errorObject.SetError(is_error, trueOrFalse, colorOfPanels, shift, langShift);
            if (codeInImage != null) // изменение текста и ответа на ошибку если есть изобраджение
            {
                string str = codeInImage.ToString();
                string[] QuestionAnswer = str.Split(" - ");
                errorObject.Prefab.GetComponentInChildren<TextMeshProUGUI>().text = QuestionAnswer[0] + "?";
                foreach (string q in QuestionAnswer)
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
            errors[index].SetError(is_error, trueOrFalse, colorOfPanels, shift, langShift);
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
                    errors[index].Prefab.transform.GetChild(2).GetComponentInChildren<Image>().GameObject()
                        .SetActive(false);
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
        if (errors.All(e => !e.IsError))
        {
            print("No Errors!");
            Time.timeScale = 0f;
            ChangeColorWinMenu();
            winMenu.SetActive(true);
        }
    }

    // Метод перезапуска игры
    private void Restart()
    {
        textEditor.text = "";
        errors.Clear();
        capcha = RandomCapcha();
        winMenu.SetActive(false);
        mainGame.SetActive(true);
        Delete(Container);
        Time.timeScale = 0.1f;
    }

    // Метод удаления объектов из объекта
    private void Delete(GameObject Container)
    {
        for (int i = 0; i < Container.transform.childCount; i++)
        {
            Destroy(Container.transform.GetChild(i).gameObject);
        }
    }

    private void ChangeColorWinMenu() // 21 19 21
    {
        winMenu.transform.GetChild(0).GetComponent<Image>().sprite = menuBackground[shift / 2 + langShift];
        Debug.Log(shift);
        Color color = shift == 2
            ? new Color(185 / 255f, 167 / 255f, 183 / 255f)
            : new Color(197 / 255f, 182 / 255f, 197 / 255f);
        winMenu.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = color;
        winMenu.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = langShift == 2
            ? "Wow!\nYou finally managed to come up with a password\nthat meets the minimum criteria for the reliability of our security system!\nIt took you just a little:"
            : "Ура!\nВы наконец-то умудрились придумать пароль,\nудовлетворяющий минимальным критериям надёжности нашей системы безопастности!\nНа это вам потребовалось всего лишь навсего:";
        winMenu.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().faceColor = color;
        winMenu.transform.GetChild(0).transform.GetChild(2).GetComponent<Image>().sprite = buttonBackground[shift / 2];
        winMenu.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            langShift == 2 ? "Improve the result?" : "Улучшить результат?";
    }
}