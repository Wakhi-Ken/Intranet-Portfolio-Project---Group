using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using TMPro;

public class WinConditionSetup
{
    [MenuItem("Tools/Coins/Setup Win Condition")]
    static void SetupWinCondition()
    {
        GameObject losePanel = SceneObjectFinder.FindInactive("lose panel");
        if (losePanel == null)
        {
            Debug.LogError("Could not find 'lose panel' in the open scene to use as a template for the win panel.");
            return;
        }

        GameObject winPanel = SceneObjectFinder.FindInactive("win panel");
        if (winPanel == null)
        {
            winPanel = Object.Instantiate(losePanel, losePanel.transform.parent);
            winPanel.name = "win panel";
            winPanel.SetActive(false);

            TMP_Text messageText = winPanel.GetComponentInChildren<TMP_Text>(true);
            if (messageText != null) messageText.text = "YOU ESCAPED!";
        }

        GameManager gameManager = Object.FindFirstObjectByType<GameManager>(FindObjectsInactive.Include);
        if (gameManager == null)
        {
            Debug.LogError("Could not find a GameManager in the scene.");
            return;
        }

        GameObject managerObj = gameManager.gameObject;
        CoinManager coinManager = managerObj.GetComponent<CoinManager>();
        GameTimer gameTimer = managerObj.GetComponent<GameTimer>();

        if (coinManager == null)
        {
            Debug.LogError("Could not find CoinManager on the GameManager object. Run 'Tools > Coins > Add Coin Counter UI' first.");
            return;
        }

        GameObject exitObj = GameObject.Find("Exit Point");
        if (exitObj == null)
        {
            exitObj = new GameObject("Exit Point");
            exitObj.transform.position = new Vector3(0f, 1f, 0f);

            BoxCollider box = exitObj.AddComponent<BoxCollider>();
            box.isTrigger = true;
            box.size = new Vector3(2f, 3f, 2f);
        }

        ExitPoint exitPoint = exitObj.GetComponent<ExitPoint>();
        if (exitPoint == null) exitPoint = exitObj.AddComponent<ExitPoint>();
        exitPoint.coinManager = coinManager;
        exitPoint.gameTimer = gameTimer;
        exitPoint.winPanel = winPanel;

        EditorUtility.SetDirty(winPanel);
        EditorUtility.SetDirty(exitObj);
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

        Debug.Log("Win condition set up. Move 'Exit Point' to the maze exit location, then save the scene.");
    }
}
