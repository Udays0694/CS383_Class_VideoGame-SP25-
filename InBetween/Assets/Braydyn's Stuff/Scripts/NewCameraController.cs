using UnityEngine;

public class NewCameraController: MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Camera Settings")]
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);

    private Vector3 velocity = Vector3.zero;

private void LateUpdate()
{
    if (player == null) return;

    Vector3 targetPosition = player.position + offset;
    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
}

public void SetTarget(Transform newTarget)
{
    player = newTarget;
}
}
