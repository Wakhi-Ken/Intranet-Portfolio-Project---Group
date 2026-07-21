using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(AudioSource))]
public class CoinCollectible : MonoBehaviour
{
    [Tooltip("Sound played when the coin is grabbed.")]
    public AudioClip collectSound;

    private XRGrabInteractable grabInteractable;
    private CoinManager coinManager;
    private bool collected;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        coinManager = FindFirstObjectByType<CoinManager>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (collected) return;
        collected = true;

        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        if (coinManager != null)
            coinManager.AddCoin();

        Destroy(gameObject);
    }
}
