using System;
using System.Collections;
using UnityEngine;
public class Movement : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioClip dodgeSound;
    [SerializeField]
    private float maxSpeed = 10f;

    [SerializeField]
    private float maxAcceleration = 10f;
    [SerializeField]
    public Rigidbody body;

    private Vector3? overrideVelocity;
    private Vector3 velocity;
    [HideInInspector]
    public Vector2 desiredVelocity;
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

    public void StartDash(Vector3 direction, float power, float duration, Action onEnd)
    {
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }
        dashCoroutine = StartCoroutine(OverrideVelocity(direction * power, duration, onEnd));
        soundSource.clip = dodgeSound;
        soundSource.Play();
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
        overrideVelocity = Vector3.zero;
        yield return new WaitForFixedUpdate();
        overrideVelocity = null;
        onEnd?.Invoke();
    }
}