using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // target yang diikuti (player)
    public Vector3 offset;   // jarak kamera terhadap player
    public float smoothSpeed = 5f; // kehalusan gerakan kamera

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
