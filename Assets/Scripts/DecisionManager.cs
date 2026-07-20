using UnityEngine;

public class DecisionManager : MonoBehaviour
{
    public GameObject decisionPanel;
    public GameObject losePanel;
    public GameTimer gameTimer;
    public GameManager gameManager;
    public float loseDelayBeforeRestart = 2f;

    private DecisionPoint activeDecisionPoint;

    public void ShowDecision(DecisionPoint decisionPoint)
    {
        activeDecisionPoint = decisionPoint;
        decisionPanel.SetActive(true);
        if (gameTimer != null) gameTimer.StopTimer();
    }

    public void ChooseLeft()
    {
        Choose(DecisionPoint.Direction.Left);
    }

    public void ChooseRight()
    {
        Choose(DecisionPoint.Direction.Right);
    }

    private void Choose(DecisionPoint.Direction chosen)
    {
        decisionPanel.SetActive(false);

        bool correct = activeDecisionPoint != null && chosen == activeDecisionPoint.correctDirection;
        activeDecisionPoint = null;

        if (correct)
        {
            if (gameTimer != null) gameTimer.StartTimer();
        }
        else
        {
            losePanel.SetActive(true);
            Invoke(nameof(RestartAfterLoss), loseDelayBeforeRestart);
        }
    }

    private void RestartAfterLoss()
    {
        losePanel.SetActive(false);
        if (gameManager != null) gameManager.RestartGame();
    }

    public void ManualRestart()
    {
        CancelInvoke(nameof(RestartAfterLoss));
        RestartAfterLoss();
    }
}
