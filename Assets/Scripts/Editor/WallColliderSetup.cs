using UnityEngine;
using UnityEditor;

public class WallColliderSetup
{
    [MenuItem("Tools/Add Wall Colliders to Maze")]
    static void AddWallColliders()
    {
        GameObject maze = GameObject.Find("maze");
        if (maze == null)
        {
            Debug.LogError("Could not find GameObject named 'maze'. Make sure it exists in the scene.");
            return;
        }

        MeshFilter[] meshFilters = maze.GetComponentsInChildren<MeshFilter>(true);
        int added = 0;

        foreach (MeshFilter mf in meshFilters)
        {
            if (mf.GetComponent<Collider>() != null) continue;

            MeshCollider mc = mf.gameObject.AddComponent<MeshCollider>();
            mc.sharedMesh = mf.sharedMesh;
            mc.convex = false;
            added++;
        }

        Debug.Log($"Added {added} MeshColliders to maze walls.");
        EditorUtility.SetDirty(maze);
    }
}
