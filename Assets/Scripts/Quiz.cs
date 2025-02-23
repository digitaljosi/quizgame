using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] Text questionText;
    [SerializeField] Image questionImage;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI timerText2;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Slider")]
    [SerializeField] Slider progressBar;

    public bool isComplete;
    [SerializeField]
    private BoolValue quizFinalizado;
    private float elapsedTime = 0f;
    public float elapsedTimePhase2 = 0f;
    public float showTimer;

    void Awake()
    {
        timer = FindFirstObjectByType<Timer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }
    public float GetElapsedTime()
    {
            if (elapsedTimePhase2 >= 60f)
            {
                int minutes = Mathf.FloorToInt(elapsedTimePhase2 / 60);
                int seconds = Mathf.FloorToInt(elapsedTimePhase2 % 60);
                timerText2.text = string.Format("Tempo passado: {0}m {1}s", minutes, seconds);
            }
            else
            {
                timerText2.text = "Tempo: " + Mathf.FloorToInt(elapsedTimePhase2) + "s";
            }
        return elapsedTimePhase2;
    }
    void Update()
    {
        showTimer = elapsedTime;
        timerImage.fillAmount = timer.fillFraction;
        UpdateTimerText2();
        GetElapsedTime();
        if (timer.loadNextQuestion)
        {

            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                quizFinalizado.RuntimeValue = true;
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }

        if (!isComplete) {
            elapsedTime += Time.deltaTime;
            
            if (elapsedTime >= 60f)
            {
                // Calculando os minutos e segundos
                int minutes = Mathf.FloorToInt(elapsedTime / 60);
                int seconds = Mathf.FloorToInt(elapsedTime % 60);
                
                // Formatando o texto para mostrar minutos e segundos
                timerText.text = string.Format("Tempo: {0}m {1}s", minutes, seconds);
            }
            else
            {
                // Se o tempo for inferior a 60 segundos, exibe em segundos
                timerText.text = "Tempo: " + Mathf.FloorToInt(elapsedTime) + "s";
            }
        }
    }
    public void UpdateTimerText2()
    {
        Debug.Log(elapsedTimePhase2 );
        if (elapsedTimePhase2 >= 60f)
        {
            int minutes = Mathf.FloorToInt(elapsedTimePhase2 / 60);
            int seconds = Mathf.FloorToInt(elapsedTimePhase2 % 60);
            timerText2.text = string.Format("Tempo passado: {0}m {1}s", minutes, seconds);
        }
        else
        {
            timerText2.text = "Tempo: teste " + Mathf.FloorToInt(elapsedTimePhase2) + "s";
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Pontuação: " + scoreKeeper.CalculateScore();

    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correto!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            StartCoroutine(Delay());
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Desculpe, a resposta correta é:\n" + correctAnswer;
            StartCoroutine(Delay());
            //Codigo responsavel por destacar a resposta cer
            //buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            //buttonImage.sprite = correctAnswerSprite;
        }
        Time.timeScale = 1; 
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            //GetRandomQuestion();
            LoadNextQuestion();
            DisplayQuestion();
            progressBar.value++;
            // Descomente para pular fase 1 vvvvv
            // progressBar.value=progressBar.maxValue;
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {            
            questions.Remove(currentQuestion);
        }
    }
    void LoadNextQuestion()
{
    if (questions.Count > 0)
    {
        // Seleciona a próxima pergunta na ordem
        currentQuestion = questions[0];
        
        // Remove a pergunta já usada da lista
        questions.RemoveAt(0);
    }
    else
    {
        // Lista de perguntas esgotada
        currentQuestion = null;
        Debug.Log("Todas as perguntas foram respondidas.");
    }
}


    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        questionImage.sprite = currentQuestion.GetOlhar();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    IEnumerator Delay(){
        Time.timeScale = .2f;
        yield return new WaitForSeconds(2);
               
    }
}
