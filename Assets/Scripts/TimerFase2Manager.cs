using UnityEngine;

public class TimerFase2Manager : MonoBehaviour
{
    public static TimerFase2Manager Instance { get; private set; }

    public float TotalElapsedTime { get; private set; } = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddTime(float time)
    {
        TotalElapsedTime += time;
    }

    public void ResetTime()
    {
        TotalElapsedTime = 0f;
    }
}
