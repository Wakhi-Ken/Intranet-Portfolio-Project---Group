using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CoinAlignmentSetup
{
    [MenuItem("Tools/Coins/Align All Coins To Selected")]
    static void AlignCoinsToSelected()
    {
        GameObject reference = Selection.activeGameObject;
        if (reference == null || reference.GetComponent<CoinCollectible>() == null)
        {
            Debug.LogError("Select one correctly standing coin in the Hierarchy first — its rotation will be copied to every other coin.");
            return;
        }

        Quaternion targetRotation = reference.transform.rotation;
        CoinCollectible[] coins = Object.FindObjectsByType<CoinCollectible>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        int updated = 0;

        foreach (CoinCollectible coin in coins)
        {
            coin.transform.rotation = targetRotation;

            Rigidbody rb = coin.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;

            EditorUtility.SetDirty(coin.gameObject);
            updated++;
        }

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        Debug.Log($"Aligned {updated} coin(s) to match '{reference.name}' and locked their Rigidbody to kinematic so physics won't tip them over again. Save the scene to keep these changes.");
    }
}
