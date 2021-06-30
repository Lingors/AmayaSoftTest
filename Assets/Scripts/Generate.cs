using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Generate : MonoBehaviour
{
    public GameObject block, restart, endGame;
    public Text whatToFind;
    private GameObject _clone;
    private Sprite[] _sprites;

    private string _level = "easy", _rightAnswer = "", _typeOfSprites;
    private float _endJ;
    private int _randomSprite;
    private readonly List<string> _listAnswers = new List<string>();

    // возможные ответы
    private readonly string[] _answers =
    {
        "1", "2", "3", "5", "4", "6", "7", "8", "9", "10", "A", "B", "C", "D", "F", "G", "J", "I", "E", "H", "K", "L",
        "M", "R", "S", "U", "V", "O", "Q", "W", "T", "N", "P", "X", "Y", "Z"
    };

    private void Start()
    {
        AllGenerate();
    }

    // функция для понимания какая стадия игры сейчас
    private void SetRules(int endI, string level, int endJ)
    {
        for (var i = 0; i < endI; i++)
        {
            Destroy(GameObject.Find(i.ToString()));
        }

        _level = level;
        _endJ = endJ;
        _rightAnswer = "";
        whatToFind.text = "Find ";
        _typeOfSprites = "";
    }

    // функция установки верного ответа
    private void SetAnswer()
    {
        _rightAnswer = _answers[_randomSprite];
        _listAnswers.Add(_rightAnswer);
        _clone.AddComponent<RightAnswer>();
    }

    public void AllGenerate()
    {
        _sprites = Resources.LoadAll<Sprite>("Sprites"); // получаем все спрайты
        switch (_level) // смотрим какая сейчас стадия игры
        {
            case "easy":
                SetRules(0, "middle", 2);
                break;
            case "middle":
                SetRules(3, "hard", 0);
                break;
            case "hard":
                SetRules(6, "end", -2);
                break;
            case "end":
                SetRules(9, "", -2);
                endGame.GetComponent<Image>().DOFade(1, 1.5f);
                restart.SetActive(true);
                break;
        }

        if (_level != "")
        {
            var k = 0; // для удобства названия объектов
            // бежим по циклу и взависимости от переменной endJ у нас будет либо 1 строка, либо 2, либо 3
            // получается такая сетка
            // -2,2    0,2    2,2
            // -2,0    0,0    2,0
            // -2,-2   0,-2   2,-2
            for (var j = 2; j >= _endJ; j += -2)
            {
                for (var i = -2; i <= 2; i += 2)
                {
                    _clone = Instantiate(block, new Vector3(i, j, 0f), Quaternion.identity); // создаем ячейки
                    _clone.name = k.ToString();
                    var spriteRenderer = _clone.GetComponent<SpriteRenderer>();
                    _randomSprite = Random.Range(0, _sprites.Length);
                    // смотрим чтобы не было повторов
                    while (_sprites[_randomSprite] == null ||
                           !_sprites[_randomSprite].name.ToLower().Contains(_typeOfSprites))
                    {
                        _randomSprite = Random.Range(0, _sprites.Length);
                    }
                    // выбираем какие объекты будут формироваться
                    if (_typeOfSprites == "")
                    {
                        _typeOfSprites = _sprites[_randomSprite].name.ToLower().Contains("cookies")
                            ? "cookies"
                            : "letters";
                    }
                    // присваиваем спрайт
                    spriteRenderer.sprite = _sprites[_randomSprite];
                    _sprites[_randomSprite] = null; // зануляем чтобы больше не использовать
                    // выставляем правильный ответ если такой еще не встречался за сессию
                    if (Random.Range(0, 2) == 1 && _rightAnswer == "" && !_listAnswers.Contains(_rightAnswer))
                    {
                        SetAnswer();
                    }
                    else
                    {
                        _clone.AddComponent<WrongAnswer>();
                    }
                    // костыль для нормального отображения 7 и 8
                    var spriteRendererName = spriteRenderer.sprite.name;
                    if (spriteRendererName == "SD_NC_Cookies_1_6" || spriteRendererName == "SD_NC_Cookies_1_7")
                    {
                        _clone.transform.rotation = new Quaternion(0f, 0f, -90f, 90f);
                    }
                    // подключаем партиклы
                    var emissionModule = _clone.GetComponent<ParticleSystem>().emission;
                    emissionModule.enabled = false;
                    k++;
                }
            }

            if (_rightAnswer == "" && !_listAnswers.Contains(_rightAnswer))
            {
                SetAnswer();
                _listAnswers.Add(_rightAnswer);
                Destroy(_clone.GetComponent<WrongAnswer>());
            }

            whatToFind.text += _rightAnswer;
        }
        else
        {
            whatToFind.text = "";
        }
    }
}