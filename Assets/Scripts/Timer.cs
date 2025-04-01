using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion;
    public float fillFraction;

    public bool isAnsweringQuestion;
    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion)
        {
            if (timerValue > 1)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }        
    }
    
    public string converteTempoReacao(float tempo){
        string unidadeMedida = "ms";
        
        if (tempo >= 1f){
            unidadeMedida = "s";
        }

        if(tempo >= 60){
            int minutes = Mathf.FloorToInt(tempo / 60);
            int seconds = Mathf.FloorToInt(tempo % 60);
            return minutes.ToString()+"m "+seconds.ToString()+"s";
        }

        return tempo.ToString()+unidadeMedida;
        
    }

    public string getTimerText(float elapsedTimePhase2){
        if (elapsedTimePhase2 >= 60f)
        {
            int minutes = Mathf.FloorToInt(elapsedTimePhase2 / 60);
            int seconds = Mathf.FloorToInt(elapsedTimePhase2 % 60);
           return string.Format("Tempo passado: {0}m {1}s", minutes, seconds);
        }
        
        return "Tempo: teste " + Mathf.FloorToInt(elapsedTimePhase2) + "s";
    }
}
