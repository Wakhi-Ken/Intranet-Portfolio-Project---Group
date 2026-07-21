using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndGamePanelWiring
{
    [MenuItem("Tools/UI/Wire End Game Panel")]
    static void WireEndGamePanel()
    {
        GameObject endGamePanel = SceneObjectFinder.FindInactive("EndGamePanel");
        if (endGamePanel == null)
        {
            Debug.LogError("Could not find 'EndGamePanel' in the open scene(s). Listing what is actually loaded below - check the name/parent and update accordingly.");
            SceneObjectFinder.LogAllRootObjects();
            return;
        }

        GameManager gameManager = Object.FindFirstObjectByType<GameManager>(FindObjectsInactive.Include);
        if (gameManager == null)
        {
            Debug.LogError("Could not find a GameManager in the scene.");
            return;
        }

        Button restartButton = FindButton(endGamePanel.transform, "RestartButton");
        Button quitButton = FindButton(endGamePanel.transform, "QuitButton");
        Button levelButton = FindButton(endGamePanel.transform, "Level 3");

        if (restartButton != null)
        {
            ClearPersistentListeners(restartButton.onClick);
            UnityEventTools.AddVoidPersistentListener(restartButton.onClick, gameManager.RestartGame);
            UnityEventTools.AddBoolPersistentListener(restartButton.onClick, endGamePanel.SetActive, false);
        }
        else
        {
            Debug.LogWarning("RestartButton not found under EndGamePanel.");
        }

        if (quitButton != null)
        {
            ClearPersistentListeners(quitButton.onClick);
            UnityEventTools.AddVoidPersistentListener(quitButton.onClick, gameManager.QuitGame);
        }
        else
        {
            Debug.LogWarning("QuitButton not found under EndGamePanel.");
        }

        if (levelButton != null)
        {
            ClearPersistentListeners(levelButton.onClick);
            UnityEventTools.AddVoidPersistentListener(levelButton.onClick, gameManager.LoadNextLevel);
        }
        else
        {
            Debug.LogWarning("'Level 3' button not found under EndGamePanel.");
        }

        GameObject startMenuCanvas = SceneObjectFinder.FindInactive("StartMenuCanvas");
        if (startMenuCanvas != null)
        {
            Button startMenuQuit = FindButton(startMenuCanvas.transform, "QuitButton");
            if (startMenuQuit != null)
            {
                ClearPersistentListeners(startMenuQuit.onClick);
                UnityEventTools.AddVoidPersistentListener(startMenuQuit.onClick, gameManager.QuitGame);
            }
        }

        EditorUtility.SetDirty(endGamePanel);
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

        Debug.Log("End Game Panel buttons wired: Restart -> GameManager.RestartGame() + hide panel, Quit -> GameManager.QuitGame(), Level 3 -> GameManager.LoadNextLevel() (loads scene \"Stage 3\" - create that scene and add both Stage 2 and Stage 3 to Build Settings before this will work). Save the scene to keep these changes.");
    }

    private static Button FindButton(Transform root, string name)
    {
        foreach (Button button in root.GetComponentsInChildren<Button>(true))
        {
            if (button.gameObject.name == name) return button;
        }
        return null;
    }

    private static void ClearPersistentListeners(UnityEvent evt)
    {
        while (evt.GetPersistentEventCount() > 0)
            UnityEventTools.RemovePersistentListener(evt, 0);
    }
}
