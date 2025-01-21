using UnityEngine;

public class QuizButtonPhase2 : MonoBehaviour
{
    [SerializeField] QuizPhase2 quizManager;

    public void OnButtonClicked()
    {
        if (quizManager != null)
        {
            quizManager.ShowQuiz();
        }
    }
}
