using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    public CoinManager coinManager;
    public GameTimer gameTimer;
    public GameObject winPanel;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (other.GetComponent<CharacterController>() == null) return;
        if (coinManager == null || !coinManager.HasEnoughCoins) return;

        triggered = true;
        if (gameTimer != null) gameTimer.StopTimer();
        if (winPanel != null) winPanel.SetActive(true);
    }
}
