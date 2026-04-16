using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(-2f, 0f, -10f);

    private Vector3 targetPosition;

    void LateUpdate()
    {
        // Calculate where the camera wants to be
        targetPosition = player.position + offset;

        // Smoothly move the camera toward that position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}