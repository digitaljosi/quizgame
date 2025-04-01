using UnityEngine;

public class ScoreFase2Manager : MonoBehaviour
{
    public static ScoreFase2Manager Instance { get; private set; }

    public int TotalScore { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mant�m o objeto entre cenas
        }
        else
        {
            Destroy(gameObject); // Garante que s� exista um singleton
        }
    }

    public void AddScore(int points)
    {
        TotalScore += points;
    }

    public int getPontuacaoQuiz2(){
        return TotalScore;
    }
}
