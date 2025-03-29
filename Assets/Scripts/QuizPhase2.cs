using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class QuizPhase2 : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private List<QuestionSO2> questions = new List<QuestionSO2>();
    private QuestionSO2 currentQuestion;

    [Header("Answers")]
    [SerializeField] private GameObject[] answerButtons;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI timerTextFase2;


    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Button Colors")]
    [SerializeField] private Sprite defaultAnswerSprite;
    [SerializeField] Sprite selectedAnswerSprite;

    [Header("Quiz Panel")]
    [SerializeField] private GameObject quizPanel;

    [Header("Next Button")]
    [SerializeField] private Button nextButton;

    [Header("Quit Button")]
    [SerializeField] private Button quitButton;
    private Dictionary<string, int> selectedEmotions = new();
    private List<string> emotions = new(){
        "Alegria",
        "Raiva",
        "Surpresa",
        "Medo",
        "Nojo",
        "Tristeza",
        "Neutro"
    };
    private bool timerStarted = false;
    private bool isQuizActive = false;


    // [SerializeField]
    // private GerenciadorRepositorioRespostas gerenciadorRepositorioRespostas;

    public GameObject Balao;

    private float startTimePhase2;

    private int totalScore = 0;

    void Awake()
    {
        if (questionText == null)
        {
            Debug.LogError("questionText não atribuído no Inspector");
        }

        if (quizPanel == null)
        {
            Debug.LogError("quizPanel não atribuído no Inspector");
        }

        if (answerButtons == null || answerButtons.Length == 0)
        {
            Debug.LogError("answerButtons não atribuídos no Inspector");
        }

        if (questions == null || questions.Count == 0)
        {
            Debug.LogError("questions não atribuídas no Inspector ou a lista está vazia");
        }

        if (nextButton == null)
        {
            Debug.LogError("nextButton não atribuído no Inspector");
        }

        if (quitButton == null)
        {
            Debug.LogError("quitButton não atribuído no Inspector");
        }

        quizPanel?.SetActive(false);
        nextButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }

    void Start()
    {
        quizPanel?.SetActive(false);
        
    }
    
    void Update()
    {
        if (quizPanel.activeSelf)
        {
            TimerFase2Manager.Instance.AddTime(Time.deltaTime);

            float totalElapsedTime = TimerFase2Manager.Instance.TotalElapsedTime;

            int minutes = Mathf.FloorToInt(totalElapsedTime / 60);
            int seconds = Mathf.FloorToInt(totalElapsedTime % 60);
            timerTextFase2.text = string.Format("Tempo Fase 2: {0}m {1}s", minutes, seconds);
        }
    }
    public void ShowQuiz()
    {
        if (!timerStarted)
        {
            startTimePhase2 = Time.time; // Marca o início do quiz atual
            timerStarted = true;
        }

        isQuizActive = true;
        quizPanel?.SetActive(true);

        float totalElapsedTime = TimerFase2Manager.Instance.TotalElapsedTime;
        int minutes = Mathf.FloorToInt(totalElapsedTime / 60);
        int seconds = Mathf.FloorToInt(totalElapsedTime % 60);
        timerTextFase2.text = string.Format("Tempo Fase 2: {0}m {1}s", minutes, seconds);

        Debug.Log("Show Quiz na fase 2");
        GetNextQuestion();
    }

    void DisplayAnswer(int index)
        {
            SetButtonState(false);

            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (index >= 0 && index < answerButtons.Length)
                {
                    var selectedButton = answerButtons[index];
                    selectedButton.SetActive(true);

                    // Alterar o sprite para o botão selecionado
                    var buttonImage = selectedButton.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        buttonImage.sprite = selectedAnswerSprite;
                    }
                }

            }

        if (questions.Count > 0)
            {
                nextButton.gameObject.SetActive(true);
            }
            else
            {
                quitButton.gameObject.SetActive(true);
            }
        }

    private void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            currentQuestion = questions[0];
            questions.RemoveAt(0);

            SetButtonState(true);
            SetDefaultButtonSprites();
            DisplayQuestion();
            nextButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
        }
        else
        {
            HideQuiz();
        }
    }

    private void DisplayQuestion()
    {
        if (currentQuestion == null)
        {
            Debug.LogError("Pergunta atual não foi carregada.");
            return;
        }

        questionText.text = currentQuestion.question;
        Debug.Log("Exibindo questão: " + currentQuestion.question);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            var button = answerButtons[i];
            if (button != null)
            {
                var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    if (i < currentQuestion.GetAnswersCount())
                    {
                        button.SetActive(true);
                        buttonText.text = currentQuestion.GetAnswer(i);
                        Debug.Log("Ativando botão " + i + " com resposta: " + currentQuestion.GetAnswer(i));

                        int index = i;
                        button.GetComponent<Button>().onClick.RemoveAllListeners();
                        button.GetComponent<Button>().onClick.AddListener(() => OnAnswerSelected(index));
                    }
                    else
                    {
                        button.SetActive(false);
                        Debug.Log("Desativando botão " + i);
                    }
                }
            }
        }
    }

    public void OnAnswerSelected(int index)
    {
        string[] resposta = new string[]{currentQuestion.question, currentQuestion.answers[index]};
         
        GetComponentInParent<GerenciadorRepositorioRespostas>().AddResposta(resposta);
        
        Debug.Log(string.Join(",", resposta));
        
        int points = index + 1;
        ScoreFase2Manager.Instance.AddScore(points);
        totalScore += points;

        if (scoreText != null)
        {
            scoreText.text = "Pontuação Fase 2: " + ScoreFase2Manager.Instance.TotalScore.ToString();
        }
        else
        {
            Debug.LogWarning("Score Display Text não está atribuído no Inspector.");
        }

        var textAnswer = currentQuestion.answers[index];
        if (IsAnEmotion(textAnswer))
        {
            Debug.Log(textAnswer);
            if (selectedEmotions.ContainsKey(textAnswer))
                selectedEmotions[textAnswer]++;
            else
                selectedEmotions.Add(textAnswer, 1);
        }

        DisplayAnswer(index);
        Debug.Log("Resposta selecionada: " + index + " - Pontos atribuídos: " + points);
        SetButtonState(false);

        if (questions.Count > 0)
        {
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            quitButton.gameObject.SetActive(true);
        }

        B_Counter bCounter = FindAnyObjectByType<B_Counter>();
        
        bool dadosSalvos = bCounter.VerificaSeDadosEstaoSalvos();
         Debug.Log(dadosSalvos);
        int baloes = bCounter.GetBoloesEncontrados();

        if(baloes == 12 && dadosSalvos == false){
            bCounter.SetDadosSalvos();
            GameManager.instance.Save();
        }
    }
    private bool IsAnEmotion(string answer)
    {
        return emotions.Any(x => x.Equals(answer, System.StringComparison.InvariantCultureIgnoreCase));
    }

    public void OnNextQuestionButtonClicked()
    {
        HideQuiz();
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Botão Quit pressionado");
        HideQuiz();
    }

    private void SetButtonState(bool state)
    {
        foreach (var button in answerButtons)
        {
            var btnComponent = button?.GetComponent<Button>();
            if (btnComponent != null)
            {
                btnComponent.interactable = state;
                Debug.Log("Definindo estado do botão " + button.name + " para " + state);
            }
        }
    }

    private void SetDefaultButtonSprites()
    {
        foreach (var button in answerButtons)
        {
            var imageComponent = button?.GetComponent<Image>();
        }
    }

    public void HideQuiz()
    {
        isQuizActive = false;
        quizPanel?.SetActive(false);
        Balao.SetActive(false);
        foreach(var emotion in selectedEmotions){
            Debug.Log($"{emotion.Key}; {emotion.Value}");
        }
    }
}
