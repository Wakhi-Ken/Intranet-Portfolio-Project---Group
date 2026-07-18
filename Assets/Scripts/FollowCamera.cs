using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform playerCamera;
    public float distance = 2f;
    public Vector3 offset = new Vector3(0, 0, 0);

    void LateUpdate()
    {
        if (playerCamera == null) return;

        Vector3 forward = playerCamera.forward;
        forward.y = 0f;
        if (forward == Vector3.zero) forward = Vector3.forward;
        forward.Normalize();

        transform.position = playerCamera.position + forward * distance + offset;
        transform.rotation = Quaternion.LookRotation(forward);
    }
}
