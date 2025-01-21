using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen endScreen;
    private bool isQuizComplete = false; // Flag to track quiz completion

    void Awake()
    {
        quiz = FindFirstObjectByType<Quiz>();
        endScreen = FindFirstObjectByType<EndScreen>();
    }

    void Start()
    {
        quiz.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
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
