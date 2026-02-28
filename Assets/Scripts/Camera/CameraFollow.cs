using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow Settings")]
    [SerializeField] private float smoothTime = 0.15f;
    [SerializeField] private Vector2 offset;

    [Header("Look Ahead")]
    [SerializeField] private bool useLookAhead = true;
    [SerializeField] private float lookAheadDistance = 2f;
    [SerializeField] private float lookAheadSmooth = 5f;

    [Header("Bounds")]
    [SerializeField] private bool useBounds = false;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    private Vector3 velocity;
    private Vector3 currentLookAhead;

    private Rigidbody2D targetRb;

    private void Start()
    {
        if (target != null)
            targetRb = target.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + (Vector3)offset;

        // Look Ahead (в сторону движени€)
        if (useLookAhead && targetRb != null)
        {
            Vector2 dir = targetRb.linearVelocity.normalized;
            Vector3 targetLookAhead = (Vector3)(dir * lookAheadDistance);
            currentLookAhead = Vector3.Lerp(
                currentLookAhead,
                targetLookAhead,
                Time.deltaTime * lookAheadSmooth
            );

            desiredPosition += currentLookAhead;
        }

        Vector3 smoothPosition = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );

        // ќграничение камеры по границам
        if (useBounds)
        {
            smoothPosition.x = Mathf.Clamp(smoothPosition.x, minBounds.x, maxBounds.x);
            smoothPosition.y = Mathf.Clamp(smoothPosition.y, minBounds.y, maxBounds.y);
        }

        smoothPosition.z = transform.position.z;
        transform.position = smoothPosition;
    }
}
