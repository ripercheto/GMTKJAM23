using UnityEngine;
public class Movement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 10f;

    [SerializeField]
    private float maxAcceleration = 10f;
    [SerializeField]
    private Rigidbody body;

    private Vector3 velocity;
    private Vector2 desiredVelocity;

    public void UpdateDesiredVelocity(Vector3 newDesiredVelocity)
    {
        var flatVelocity = new Vector2(newDesiredVelocity.x, newDesiredVelocity.z);
        UpdateDesiredVelocity(flatVelocity);
    }

    public void UpdateDesiredVelocity(Vector2 newDesiredVelocity)
    {
        desiredVelocity = Vector2.ClampMagnitude(newDesiredVelocity, 1f) * maxSpeed;
    }

    private void FixedUpdate()
    {
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
}