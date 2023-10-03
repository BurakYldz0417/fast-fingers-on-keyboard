using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TypingTest : MonoBehaviour
{
    public TextMeshProUGUI wordDisplay;
    public TMP_InputField inputField;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI trueText;
    public TextMeshProUGUI falseText;
    public TextMeshProUGUI hiighscoreText;


    private string[] wordList = { "tüm", "çekmek", "konuşmak", "para", "anlamak", "anne", "az", "bazı", "baba", "hayat", "sadece", "küçük", "fazla", "bilgi", "an", "sormak", "bunun", "öyle", "yine", "sağlamak", "sonuç", "kullanılmak", "dış", "ad", "yani", "süre", "dönmek", "açmak", "oturmak", "anlatmak", "bırakmak", "hemen", "saat", "yaş", "sorun", "devlet", "sahip", "sıra", "yazmak", "yüzde", "ay", "atmak", "tutmak", "bunu", "olay", "düşmek", "duymak", "söz", "güzel", "sevmek", "biraz", "zor", "çıkarmak", "şu", "koymak", "tek", "sistem", "birlikte", "verilmek", "kim", "alınmak", "genç", "kapı", "kitap", "üzerine", "burada", "gece", "alan", "birbiri", "ışte", "beklemek", "uzun", "hiçbir", "bugün", "dönem", "arkadaş", "ürün", "aile", "üç", "okumak", "erkek", "herkes", "güç", "belki", "gerçek", "tam", "ilgili", "ilişki", "çevre", "eski", "aramak", "yaşam", "halk", "yakın", "sokak", "bey", "tarih", "özellik", "bölüm", "özel", "akıl", "kimse", "pek", "eğer", "gerek", "özellikle", "anlam", "yüksek", "banka", "kez", "ayak", "taşımak", "geri", "toplum", "araç", "madde", "tür", "karar", "görülmek", "hava", "sayı", "farklı", "grup", "oda", "biçim", "oluşmak", "saka kuşu","at sineği","uğur böceği","dil balığı","kedi balığı", "Tuz ruhu","ay taşı","masa saati","dolma kalem","yapma çiçek" };
    private int currentWordIndex = 0;
    private int score = 0;
    private int wrongCount = 0;
    private int totalWords = 40;
    private float gameDuration = 60f;
    private float currentTime = 0f;
    private bool gameStarted = false;

    public GameObject startbutton, retrybutton;

    static int highscore;
    private void Start()
    {
        ShuffleWords();
        DisplayWord();
        Time.timeScale = 0f;
        highscore = 0;
    }

    private void Update()
    {
        if (gameStarted)
        {
            currentTime -= Time.deltaTime;
            timerText.text = "Sure: " + Mathf.Round(currentTime);

            if (currentTime <= 0f)
            {
                EndGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && gameStarted)
        {
            CheckWord();
            inputField.text = "";
            inputField.ActivateInputField();
        }
       

        trueText.text = "true word coun : " + score.ToString();
        falseText.text="false word count : "+wrongCount.ToString();

        if(score>highscore)
        {
            highscore = score;
        }
    }

    private void DisplayWord()
    {
        if (currentWordIndex < totalWords)
        {
            wordDisplay.text = wordList[currentWordIndex];
        }
        else
        {
            EndGame();
        }
    }

    private void CheckWord()
    {
        if (currentWordIndex < totalWords)
        {
            string inputWord = inputField.text.ToLower();
            string targetWord = wordList[currentWordIndex].ToLower();

            if (inputWord == targetWord)
            {
                score++;
            }
            else
            {
                wrongCount++;
            }

            currentWordIndex++;
            DisplayWord();
        }
    }

    private void ShuffleWords()
    {
        for (int i = 0; i < wordList.Length; i++)
        {
            string temp = wordList[i];
            int randomIndex = Random.Range(i, wordList.Length);
            wordList[i] = wordList[randomIndex];
            wordList[randomIndex] = temp;
        }
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        gameStarted = true;
        currentWordIndex = 0;
        score = 0;
        wrongCount = 0;
        inputField.text = "";
        inputField.ActivateInputField();
        currentTime = gameDuration;
        timerText.text = "TİME :  " + Mathf.Round(currentTime);
        DisplayWord();
    }

    private void EndGame()
    {
        gameStarted = false;
        Time.timeScale = 0f;
        retrybutton.SetActive(true);
        hiighscoreText.text = "High Score : " + highscore.ToString();
    }
    public void quit()
    {
            Application.Quit();
    }
    public void startbut()
    {
        StartGame();
        startbutton.SetActive(false);
    }
    public void retrybut()
    {
        StartGame();
        retrybutton.SetActive(false);
    }
}
