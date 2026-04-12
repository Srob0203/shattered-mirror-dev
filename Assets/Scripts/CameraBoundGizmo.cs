using UnityEngine;

[ExecuteAlways]
public class CameraBoundsGizmo : MonoBehaviour
{
    public Color outlineColor = Color.green;

    void OnDrawGizmos()
    {
        Camera cam = GetComponent<Camera>();
        if (cam == null) return;

        Gizmos.color = outlineColor;

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        Vector3 center = transform.position;
        center.z = 0f;

        Gizmos.DrawWireCube(center, new Vector3(width, height, 0f));
    }
}