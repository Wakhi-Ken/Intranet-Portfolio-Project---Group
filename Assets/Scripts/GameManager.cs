using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startMenuCanvas;
    public GameObject ingameCanvas;
    public GameObject gameplayPanel;
    public GameTimer gameTimer;

    private PauseManager pauseManager;
    private CharacterController playerController;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    void Start()
    {
        ingameCanvas.SetActive(false);

        pauseManager = GetComponent<PauseManager>();
        playerController = FindFirstObjectByType<CharacterController>();
        if (playerController != null)
        {
            spawnPosition = playerController.transform.position;
            spawnRotation = playerController.transform.rotation;
        }
    }

    public void StartGame()
    {
        startMenuCanvas.SetActive(false);
        ingameCanvas.SetActive(true);
        gameplayPanel.SetActive(true);
        gameTimer.StartTimer();
    }

    public void RestartGame()
    {
        if (pauseManager != null && pauseManager.pausePanel != null)
            pauseManager.pausePanel.SetActive(false);

        ResetPlayerPosition();
        gameTimer.ResetTimer();
        StartGame();
    }

    public void ReturnToMainMenu()
    {
        if (pauseManager != null && pauseManager.pausePanel != null)
            pauseManager.pausePanel.SetActive(false);

        ingameCanvas.SetActive(false);
        startMenuCanvas.SetActive(true);
        gameTimer.StopTimer();
        gameTimer.ResetTimer();
        ResetPlayerPosition();
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Stage 3");
    }

    private void ResetPlayerPosition()
    {
        if (playerController == null) return;

        playerController.enabled = false;
        playerController.transform.SetPositionAndRotation(spawnPosition, spawnRotation);
        playerController.enabled = true;
    }
}
