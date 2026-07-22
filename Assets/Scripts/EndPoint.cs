using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public GameObject endPanel;
    public GameTimer gameTimer;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (other.GetComponent<CharacterController>() == null) return;

        triggered = true;
        endPanel.SetActive(true);
        if (gameTimer != null) gameTimer.StopTimer();
    }
}
