using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quiz/QuestionSO2")]
public class QuestionSO2 : ScriptableObject
{
    public string question;
    public List<string> answers;
    [SerializeField]
    private int correctAnswerIndex;
    public string GetQuestion()
    {
        return question;
    }
    public int GetCorrectAnswerIndex() 
    {  
        return correctAnswerIndex; 
    }

    public string GetAnswer(int index)
    {
        if (index >= 0 && index < answers.Count)
        {
            return answers[index];
        }
        else
        {
            Debug.LogError("Index de resposta inválido.");
            return string.Empty;
        }
    }

    public int GetAnswersCount()
    {
        return answers.Count;
    }
}
