using UnityEngine;
public class SimpleMovement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 10f;

    [SerializeField]
    private float maxAcceleration = 10f;
    [SerializeField]
    private Rigidbody2D body;

    private Vector2 velocity, desiredVelocity;

    private void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        desiredVelocity = playerInput * maxSpeed;
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
        var xAxis = Vector2.right;
        var yAxis = Vector2.up;

        var currentX = Vector3.Dot(velocity, xAxis);
        var currentY = Vector3.Dot(velocity, yAxis);

        var maxSpeedChange = maxAcceleration * Time.deltaTime;

        var newX = Mathf.MoveTowards(currentX, desiredVelocity.x, maxSpeedChange);
        var newY = Mathf.MoveTowards(currentY, desiredVelocity.y, maxSpeedChange);

        velocity += xAxis * (newX - currentX) + yAxis * (newY - currentY);
    }
}