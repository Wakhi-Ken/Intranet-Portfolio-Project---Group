using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public TMP_Text coinCountText;

    [Range(0f, 1f)]
    [Tooltip("Fraction of the total coins the player must collect to be allowed to win.")]
    public float winThreshold = 0.75f;

    private int collectedCoins = 0;
    private int totalCoins = 0;

    public bool HasEnoughCoins => totalCoins > 0 && collectedCoins >= Mathf.CeilToInt(totalCoins * winThreshold);

    private void Start()
    {
        totalCoins = FindObjectsByType<CoinCollectible>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Length;
        UpdateDisplay();
    }

    public void AddCoin()
    {
        collectedCoins++;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (coinCountText != null)
            coinCountText.text = $"Coins: {collectedCoins} / {totalCoins}";
    }
}
