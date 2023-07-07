public class Enemy : MovementBehaviour
{
    private void Update()
    {
        var pos = transform.GetFlatPosition();
        var targetPos = Princess.instance.transform.GetFlatPosition();
        var dir = targetPos - pos;
        movement.UpdateDesiredVelocity(dir);
    }
}