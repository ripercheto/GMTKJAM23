using System;
using System.Collections;
using UnityEngine;
public class Movement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 10f;

    [SerializeField]
    private float maxAcceleration = 10f;
    [SerializeField]
    private Rigidbody body;

    private Vector3? overrideVelocity;
    private Vector3 velocity;
    private Vector2 desiredVelocity;
    private Coroutine dashCoroutine;

    public void UpdateDesiredVelocity(Vector3 newDesiredVelocity)
    {
        var flatVelocity = new Vector2(newDesiredVelocity.x, newDesiredVelocity.z);
        UpdateDesiredVelocity(flatVelocity);
    }

    public void UpdateDesiredVelocity(Vector2 newDesiredVelocity)
    {
        desiredVelocity = Vector2.ClampMagnitude(newDesiredVelocity, 1f) * maxSpeed;
    }

    public void StartDash(Vector3 direction, float power, float duration)
    {
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }
        dashCoroutine = StartCoroutine(OverrideVelocity(direction * power, duration, null));
    }

    private void FixedUpdate()
    {
        if (overrideVelocity.HasValue)
        {
            body.velocity = overrideVelocity.Value;
            return;
        }

        UpdateState();
        AdjustVelocity();
        body.velocity = velocity;
    }

    private void UpdateState()
    {
        velocity = body.velocity;
    }

    private void AdjustVelocity()
    {
        var xAxis = Vector3.right;
        var zAxis = Vector3.forward;

        var currentX = Vector3.Dot(velocity, xAxis);
        var currentZ = Vector3.Dot(velocity, zAxis);

        var maxSpeedChange = maxAcceleration * Time.deltaTime;

        var newX = Mathf.MoveTowards(currentX, desiredVelocity.x, maxSpeedChange);
        var newZ = Mathf.MoveTowards(currentZ, desiredVelocity.y, maxSpeedChange);

        velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
    }

    private IEnumerator OverrideVelocity(Vector3 newVelocity, float duration, Action onEnd)
    {
        var t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            overrideVelocity = newVelocity;
            yield return null;
        }
        overrideVelocity = null;
        onEnd?.Invoke();
    }
}