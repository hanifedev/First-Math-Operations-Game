using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject squarePrefab;

    [SerializeField]
    private Transform squarePanel;

    [SerializeField]
    private Transform questionPanel;

    [SerializeField]
    private Text questionText;

    [SerializeField]
    private Sprite[] squareSprites;

    [SerializeField]
    private GameObject resultPanel;

    [SerializeField]
    AudioSource audioSource;

    public AudioClip trueButtonSound;

    public AudioClip falseButtonSound;

    private GameObject[] squareArrays = new GameObject[25];

    List<int> divisionNumbers = new List<int>();

    int number1, number2;

    int questionIndex;

    int buttonValue;

    bool buttonPermission;

    int trueAnswer;

    //hak sayısı
    int rightNumber;

    //zorluk seviyesi
    string level;

    RightManager rightManager;

    PointManager pointManager;

    AdmobScript admobScript;

    GameObject selectedSquare;

    private void Awake()
    {        
        rightNumber = 3;
        audioSource = GetComponent<AudioSource>();
        resultPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        rightManager = Object.FindObjectOfType<RightManager>();
        pointManager = Object.FindObjectOfType<PointManager>();
        rightManager.controlRights(rightNumber);
    }

    void Start()
    {
        buttonPermission = false;
        questionPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        createSquares();
    }

    //kırmızı kareleri oluşturur
    public void createSquares()
    {
        for(int i = 0; i < 25; i++)
        {
            GameObject square = Instantiate(squarePrefab, squarePanel);
            square.transform.GetChild(1).GetComponent<Image>().sprite = squareSprites[Random.Range(0,squareSprites.Length)];
            square.transform.GetComponent<Button>().onClick.AddListener(() => ClickedSquareButton());
            squareArrays[i] = square;
        }

        setDivisionToText();
        StartCoroutine(DoFadeRoutine());
        Invoke("openQuestionPanel", 3f);
    }

    //kare butona tıklandığında çalışır
    void ClickedSquareButton()
    {
        if(buttonPermission)
        {
            //butona tıklanan değeri al doğruluğunu kontrol et
            selectedSquare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            buttonValue = int.Parse(selectedSquare.transform.GetChild(0).GetComponent<Text>().text);
            checkAnswerIsTrue();
        }   
    }

    void checkAnswerIsTrue()
    {
        if(buttonValue == trueAnswer)
        {
            audioSource.PlayOneShot(trueButtonSound);
            selectedSquare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            selectedSquare.transform.GetChild(0).GetComponent<Text>().text = "";
            selectedSquare.transform.GetComponent<Button>().interactable = false;
            pointManager.increasePoint(level);
            //aynı soru gelmesin diye listeden sil
            divisionNumbers.RemoveAt(questionIndex);
            if(divisionNumbers.Count > 0)
            {
                openQuestionPanel();
            }
            else
            {
                endGame();
            }
        }
        else
        {
            audioSource.PlayOneShot(falseButtonSound);
            //hakları azalt
            rightNumber--;
            rightManager.controlRights(rightNumber);
        }

        if(rightNumber <= 0)
        {
            endGame();
        }
    }

    private void endGame()
    {
        buttonPermission = false;
        admobScript = Object.FindObjectOfType<AdmobScript>();
        admobScript.ShowAd();
        resultPanel.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    //kareleri teker teker açar
    IEnumerator DoFadeRoutine()
    {
        foreach(var square in squareArrays)
        {
            square.GetComponent<CanvasGroup>().DOFade(1, .2f);
            yield return new WaitForSeconds(0.07f);
        }
    }

    //random sayıları karenin içerisine yazar
    void setDivisionToText()
    {
        foreach(var square in squareArrays)
        {
            int random = Random.Range(1,12);
            divisionNumbers.Add(random);
            square.transform.GetChild(0).GetComponent<Text>().text = random.ToString();
        }
    }

    //soru panelini açar
    void openQuestionPanel()
    {
        askQuestion();
        buttonPermission = true;
        questionPanel.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }


    void askQuestion()
    {
        string sign = "";
        number2 = Random.Range(2,11);
        questionIndex = Random.Range(0,divisionNumbers.Count);
        trueAnswer = divisionNumbers[questionIndex];
        int selection = Random.Range(0,3);
        switch(selection)
        {
            case 0:           
                number1 = trueAnswer - number2;   
                sign = "+";       
                if(number1 < 0)
                {
                    sign = "";
                    changeNumbers();
                }
                break;
            case 1:
                number1 = trueAnswer + number2;
                sign = "-";      
                break;
            case 2:
                number1 = number2 * trueAnswer;
                sign = "/";      
                break;             
        }
        //zorluk seviyesi ata
        if(number1 <= 40)
        {
            level = "Kolay";
        }
        else if(number1 > 40 && number1 <= 80)
        {
            level = "Orta";
        }
        else
        {
            level = "Zor";
        }
        questionText.text = number1.ToString() + sign + number2.ToString();
    }

    private void changeNumbers()
    {
        int temp;
        temp = number2;
        number2 = number1;
        number1 = temp;
    }

}
