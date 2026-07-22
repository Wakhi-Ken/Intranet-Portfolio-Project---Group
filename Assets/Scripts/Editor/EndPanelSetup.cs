using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndPanelSetup
{
    [MenuItem("Tools/Maze/Setup End Panel")]
    static void SetupEndPanel()
    {
        GameObject losePanel = SceneObjectFinder.FindInactive("lose panel");
        if (losePanel == null)
        {
            Debug.LogError("Could not find 'lose panel' in the open scene to use as a template for the end panel.");
            return;
        }

        GameObject endPanel = SceneObjectFinder.FindInactive("end panel");
        if (endPanel == null)
        {
            endPanel = Object.Instantiate(losePanel, losePanel.transform.parent);
            endPanel.name = "end panel";
            endPanel.SetActive(false);

            TMP_Text messageText = endPanel.GetComponentInChildren<TMP_Text>(true);
            if (messageText != null) messageText.text = "YOU ESCAPED!";
        }

        GameManager gameManager = Object.FindFirstObjectByType<GameManager>(FindObjectsInactive.Include);
        GameTimer gameTimer = gameManager != null ? gameManager.GetComponent<GameTimer>() : null;

        Button restartButton = endPanel.GetComponentInChildren<Button>(true);
        if (restartButton != null && gameManager != null)
        {
            Transform stage2ButtonTransform = endPanel.transform.Find("Stage2Button");
            GameObject stage2ButtonObj = stage2ButtonTransform != null
                ? stage2ButtonTransform.gameObject
                : Object.Instantiate(restartButton.gameObject, endPanel.transform);
            stage2ButtonObj.name = "Stage2Button";

            RectTransform rect = stage2ButtonObj.GetComponent<RectTransform>();
            RectTransform restartRect = restartButton.GetComponent<RectTransform>();
            rect.anchorMin = restartRect.anchorMin;
            rect.anchorMax = restartRect.anchorMax;
            rect.pivot = restartRect.pivot;
            rect.sizeDelta = restartRect.sizeDelta;
            rect.anchoredPosition = restartRect.anchoredPosition + new Vector2(0f, -restartRect.sizeDelta.y - 20f);

            TMP_Text stage2Label = stage2ButtonObj.GetComponentInChildren<TMP_Text>(true);
            if (stage2Label != null) stage2Label.text = "Stage 2";

            Button stage2Button = stage2ButtonObj.GetComponent<Button>();
            while (stage2Button.onClick.GetPersistentEventCount() > 0)
                UnityEventTools.RemovePersistentListener(stage2Button.onClick, 0);
            UnityEventTools.AddVoidPersistentListener(stage2Button.onClick, gameManager.GoToStage2);
        }
        else
        {
            Debug.LogWarning("Could not find a Restart button on the end panel to clone into a Stage 2 button, or no GameManager was found.");
        }

        GameObject exitObj = GameObject.Find("End Point");
        if (exitObj == null)
        {
            exitObj = new GameObject("End Point");
            exitObj.transform.position = new Vector3(0f, 1f, 0f);

            BoxCollider box = exitObj.AddComponent<BoxCollider>();
            box.isTrigger = true;
            box.size = new Vector3(2f, 3f, 2f);
        }

        EndPoint endPoint = exitObj.GetComponent<EndPoint>();
        if (endPoint == null) endPoint = exitObj.AddComponent<EndPoint>();
        endPoint.endPanel = endPanel;
        endPoint.gameTimer = gameTimer;

        EditorUtility.SetDirty(endPanel);
        EditorUtility.SetDirty(exitObj);
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

        Debug.Log("End panel set up. Move 'End Point' to the maze exit location, then save the scene.");
    }
}
