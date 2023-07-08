public class Enemy : MovementBehaviour
{
    private void Update()
    {
        var pos = transform.GetFlatPosition();
        var targetPos = Princess.instance.transform.GetFlatPosition();
        var dir = targetPos - pos;

        var targetPos2 = PlayerInput.instance.transform.GetFlatPosition();
        var dir2 = targetPos2 - pos;

        if (dir.magnitude < dir2.magnitude)
        {
            movement.UpdateDesiredVelocity(dir);
        }
        else
        {
            movement.UpdateDesiredVelocity(dir2);
        }
    }
}