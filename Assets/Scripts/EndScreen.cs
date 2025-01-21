using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] Text finalScoreText;
    ScoreKeeper scoreKeeper;

    public Transform timekeeper;
    void Awake(){
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }
    public void ShowFinalScore()
    {
        GameObject.FindGameObjectWithTag("Door").SetActive(false);
        finalScoreText.text = "Parabéns!\nPontuação: " + scoreKeeper.CalculateScore() + " pontos\nSeu tempo: "+Mathf.FloorToInt(timekeeper.GetComponent<Quiz>().showTimer)+"s";
    }

}
