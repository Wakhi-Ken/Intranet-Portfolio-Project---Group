using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float startTime = 600f; // 10 minutes
    private float currentTime;

    public TMP_Text timerText;

    private bool timerRunning = false;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        if (!timerRunning)
            return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            currentTime = 0;
            timerRunning = false;

            Debug.Log("Time's Up!");
            // Later we'll show the Lose Panel here
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void ResetTimer()
    {
        currentTime = startTime;
        timerRunning = false;
        UpdateTimerDisplay();
    }
}