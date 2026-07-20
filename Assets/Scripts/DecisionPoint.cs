using UnityEngine;

public class DecisionPoint : MonoBehaviour
{
    public enum Direction { Left, Right }

    [Tooltip("Which direction is the correct path to take at this junction.")]
    public Direction correctDirection;

    public DecisionManager decisionManager;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (other.GetComponent<CharacterController>() == null) return;

        triggered = true;
        decisionManager.ShowDecision(this);
    }
}
