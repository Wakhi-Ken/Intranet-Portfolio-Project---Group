using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameTimer gameTimer;

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        if (gameTimer != null) gameTimer.StopTimer();
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        if (gameTimer != null) gameTimer.StartTimer();
    }
}