using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField]
    private string question = "Insira o texto da nova pergunta aqui";

    [SerializeField]
    private List<string> answers = new List<string>();  // Usar List para permitir diferentes números de alternativas

    [SerializeField]
    private int correctAnswerIndex;

    [SerializeField]
    private Sprite olhar;

    public string GetQuestion() { return question; }

    public Sprite GetOlhar()
    {
        return olhar;
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
            Debug.LogError("Index out of range in GetAnswer()");
            return null;
        }
    }

    public int AnswerCount => answers.Count;  // Propriedade para obter o número de alternativas
}
