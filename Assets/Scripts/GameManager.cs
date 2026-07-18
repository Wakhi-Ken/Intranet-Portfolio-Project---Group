using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startMenuCanvas;
    public GameObject ingameCanvas;
    public GameObject gameplayPanel;
    public GameTimer gameTimer;

    void Start()
    {
        ingameCanvas.SetActive(false);
    }

    public void StartGame()
    {
        startMenuCanvas.SetActive(false);
        ingameCanvas.SetActive(true);
        gameplayPanel.SetActive(true);
        gameTimer.StartTimer();
    }
}
