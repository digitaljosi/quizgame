using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    [SerializeField]
    GerenciadorRepositorioRespostas quizPhase2;
    [SerializeField]
    GerenciadorRepositorioRespostas gerenciadorRepositorioRespostasQuizPhase1;
    EndScreen endScreen;
    private bool isQuizComplete = false; // Flag to track quiz completion


    [System.Serializable]
    class SaveData{
        public int pontucaoQuiz1;
        public List<string[,]> respostasQuiz2;
    }

    public static GameManager instance;

    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }

        quiz = FindFirstObjectByType<Quiz>();
        //quizPhase2 = FindFirstObjectByType<GerenciadorRepositorioRespostas>();
        endScreen = FindFirstObjectByType<EndScreen>();
    }

    void Start()
    {
        quiz.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
    }

    public void Save(){
        
        int pontucaoQuiz1 = quiz.getPontuacaoQuiz();
        List<string[]> respostasQuiz1 = gerenciadorRepositorioRespostasQuizPhase1.GetRespostas();
        List<string[]> respostasQuiz2 = quizPhase2.GetRespostas();
        

        //string json = JsonUtility.ToJson(data);
        string numeroSave = UnityEngine.Random.Range(1000,100000).ToString();
        string nomeArquivo = "./dados-quiz/"+numeroSave+"save.csv";
        
        TextWriter textWriter = new StreamWriter(nomeArquivo, false);
        textWriter.WriteLine("Pontos Quiz 1 | "+pontucaoQuiz1.ToString());
        textWriter.WriteLine("Respostas Quiz 1 ");
        textWriter.WriteLine("Pergunta | Resposta");
        textWriter.Close();

        foreach (string[] resposta in respostasQuiz1){            
            Debug.Log(resposta[0]);
            textWriter = new StreamWriter(nomeArquivo, true);
            textWriter.WriteLine(resposta[0]+"|"+resposta[1]);
            textWriter.Close();
        }

        textWriter = new StreamWriter(nomeArquivo, true);
        textWriter.WriteLine("Resposta Quiz 2");
        textWriter.WriteLine("Pergunta | Resposta");
        textWriter.Close();

        //textWriter = new StreamWriter(nomeArquivo, true);

        foreach (string[] resposta in respostasQuiz2){            
            Debug.Log(resposta[0]);
            textWriter = new StreamWriter(nomeArquivo, true);
            textWriter.WriteLine(resposta[0]+"|"+resposta[1]);
            textWriter.Close();
        }

        //textWriter.Close();
        
        // for(int i= 0; i< data.respostasQuiz2.Length; i++){
        //     textWriter.WriteLine(data.respostasQuiz2[i,0]+","+data.respostasQuiz2[i,1]);
        // }

        //File.WriteAllText( "./dados-quiz/"+numeroSave+"save.json",json);
        //Debug.Log(Application.persistentDataPath+"/dados-quiz/"+numeroSave+"save.json");
    }

    void Update()
    {
        if (quiz.isComplete && !isQuizComplete) // Check once upon completion
        {
            isQuizComplete = true; // Prevent multiple activations
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    public void OnExitQuiz()
    {
        quiz.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnStartQuiz()
    {
        quiz.gameObject.SetActive(true);
    }

    public void OnEndQuiz()
    {
        quiz.gameObject.SetActive(false);
    }
}
