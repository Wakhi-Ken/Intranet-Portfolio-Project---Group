using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using TMPro;

public class CoinCounterUISetup
{
    [MenuItem("Tools/Coins/Add Coin Counter UI")]
    static void AddCoinCounterUI()
    {
        GameObject gameplayPanel = SceneObjectFinder.FindInactive("GamePlayPanel");
        if (gameplayPanel == null)
        {
            Debug.LogError("Could not find 'GamePlayPanel' in the open scene.");
            return;
        }

        Transform timerTextTransform = gameplayPanel.transform.Find("TimerText");
        if (timerTextTransform == null)
        {
            Debug.LogError("Could not find 'TimerText' under GamePlayPanel to copy its style from.");
            return;
        }

        Transform existing = gameplayPanel.transform.Find("CoinText");
        GameObject coinTextObj = existing != null
            ? existing.gameObject
            : Object.Instantiate(timerTextTransform.gameObject, gameplayPanel.transform);
        coinTextObj.name = "CoinText";

        RectTransform rect = coinTextObj.GetComponent<RectTransform>();
        RectTransform timerRect = timerTextTransform.GetComponent<RectTransform>();
        rect.anchorMin = timerRect.anchorMin;
        rect.anchorMax = timerRect.anchorMax;
        rect.pivot = timerRect.pivot;
        rect.sizeDelta = timerRect.sizeDelta;
        rect.anchoredPosition = timerRect.anchoredPosition + new Vector2(0, -160f);

        TMP_Text coinText = coinTextObj.GetComponent<TMP_Text>();
        coinText.text = "Coins: 0";

        GameManager gameManager = Object.FindFirstObjectByType<GameManager>(FindObjectsInactive.Include);
        if (gameManager == null)
        {
            Debug.LogError("Could not find a GameManager in the scene to attach the CoinManager to.");
            return;
        }

        GameObject managerObj = gameManager.gameObject;
        CoinManager coinManager = managerObj.GetComponent<CoinManager>();
        if (coinManager == null) coinManager = managerObj.AddComponent<CoinManager>();
        coinManager.coinCountText = coinText;

        EditorUtility.SetDirty(gameplayPanel);
        EditorUtility.SetDirty(managerObj);
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

        Debug.Log("Coin counter UI added next to the timer. Save the scene to keep these changes.");
    }
}
