using UnityEngine;

public class TrapSwitch : MonoBehaviour
{
    [Header("Trap To Control")]
    public TrapDamage trap;

    [Header("Trap Visuals")]
    public GameObject trapVisual;

    [Header("Switch Settings")]
    public bool trapIsOn = true;

    public void ToggleTrap()
    {
        // Toggle the current state
        trapIsOn = !trapIsOn;

        // Turn trap damage on/off
        trap.SetTrapActive(trapIsOn);

        // Turn trap visuals on/off
        if (trapVisual != null)
        {
            trapVisual.SetActive(trapIsOn);
        }

        Debug.Log("Trap is now: " + (trapIsOn ? "ON" : "OFF"));
    }
}