using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] Text finalScoreText;
    ScoreKeeper scoreKeeper;

    [SerializeField]
    private GameObject botaoContinuar;
     [SerializeField] private GameObject botaoFinalizar;

    public Transform timekeeper;
    void Awake(){
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }
    public void ShowFinalScore()
    {
        GameObject.FindGameObjectWithTag("Door").SetActive(false);
        finalScoreText.text = "Parabéns!\nPontuação: " + scoreKeeper.CalculateScore() + " pontos\nSeu "+timekeeper.GetComponent<Quiz>().GetTimerQuiz1();
    }

    public void ShowFinalScoreGeral(string pontosQuiz1, string tempoQuiz1, string pontosQuiz2,string tempoQuiz2){
        botaoContinuar.SetActive(false);
        botaoFinalizar.SetActive(true);

        finalScoreText.text = 
        "Parabéns!\nPontuação Fase 1: " + pontosQuiz1
        +"\n"+tempoQuiz1
        +"\nPontuação Fase 2: " + pontosQuiz2
        +"\n"+tempoQuiz2;

    }

}
