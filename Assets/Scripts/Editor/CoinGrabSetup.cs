using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CoinGrabSetup
{
    private const string CoinPrefabsFolder = "Assets/PolyKebap/Stylized Coin Pack/Prefabs";
    private const string DefaultSoundPath = "Assets/Game Sound Solutions - 8 bits Elements/coin/coin_1.wav";

    [MenuItem("Tools/Coins/Add Grab & Collection Components")]
    static void SetupCoinPrefabs()
    {
        AudioClip defaultClip = AssetDatabase.LoadAssetAtPath<AudioClip>(DefaultSoundPath);
        if (defaultClip == null)
            Debug.LogWarning($"Could not find default coin sound at {DefaultSoundPath}. Prefabs will be set up without a sound; assign one manually.");

        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { CoinPrefabsFolder });
        int updated = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject root = PrefabUtility.LoadPrefabContents(path);

            MeshFilter meshFilter = root.GetComponentInChildren<MeshFilter>(true);
            if (meshFilter == null)
            {
                Debug.LogWarning($"Skipped {path}: no MeshFilter found.");
                PrefabUtility.UnloadPrefabContents(root);
                continue;
            }

            Rigidbody rb = root.GetComponent<Rigidbody>();
            if (rb == null) rb = root.AddComponent<Rigidbody>();
            rb.isKinematic = true;

            MeshCollider col = root.GetComponent<MeshCollider>();
            if (col == null) col = root.AddComponent<MeshCollider>();
            col.sharedMesh = meshFilter.sharedMesh;
            col.convex = true;

            XRGrabInteractable grab = root.GetComponent<XRGrabInteractable>();
            if (grab == null) grab = root.AddComponent<XRGrabInteractable>();
            if (!grab.colliders.Contains(col))
                grab.colliders.Add(col);

            AudioSource audioSource = root.GetComponent<AudioSource>();
            if (audioSource == null) audioSource = root.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1f;

            CoinCollectible coin = root.GetComponent<CoinCollectible>();
            if (coin == null) coin = root.AddComponent<CoinCollectible>();
            if (coin.collectSound == null)
                coin.collectSound = defaultClip;

            PrefabUtility.SaveAsPrefabAsset(root, path);
            PrefabUtility.UnloadPrefabContents(root);
            updated++;
        }

        Debug.Log($"Coin grab setup complete: updated {updated} prefab(s) in {CoinPrefabsFolder}.");
    }
}
