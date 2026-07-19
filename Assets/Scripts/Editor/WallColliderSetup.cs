using UnityEngine;
using UnityEditor;

public class WallColliderSetup
{
    [MenuItem("Tools/Add Wall Colliders to Maze")]
    static void AddWallColliders()
    {
        GameObject maze = GameObject.Find("walls");
        if (maze == null)
        {
            Debug.LogError("Could not find GameObject named 'walls'. Make sure it exists in the scene.");
            return;
        }

        MeshFilter[] meshFilters = maze.GetComponentsInChildren<MeshFilter>(true);
        int added = 0;
        int fixedMesh = 0;

        foreach (MeshFilter mf in meshFilters)
        {
            MeshCollider mc = mf.GetComponent<MeshCollider>();
            if (mc == null)
            {
                mc = mf.gameObject.AddComponent<MeshCollider>();
                mc.convex = false;
                added++;
            }

            if (mc.sharedMesh != mf.sharedMesh)
            {
                mc.sharedMesh = mf.sharedMesh;
                fixedMesh++;
            }
        }

        Debug.Log($"Added {added} MeshColliders and fixed {fixedMesh} mismatched mesh references on maze walls.");
        EditorUtility.SetDirty(maze);
    }
}
