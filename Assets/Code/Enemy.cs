using UnityEngine;
public class Enemy : MovementBehaviour
{
    public EnemyRange range;
    public float attackCooldown = 1;
    public float attackDamage = 10;
    
    private bool CanAttack => Time.time > attackTime;
    private float attackTime;

    private void Update()
    {
        HandleAttack();

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

    private void HandleAttack()
    {
        if (!CanAttack || !range.HasTargetsInRange)
        {
            return;
        }
        var target = range.GetClosestTarget();
        if (target == null)
        {
            return;
        }
        var health = target.gameObject.GetComponent<Health>();
        if (health == null)
        {
            return;
        }
        health.TakeDamage(attackDamage);
        attackTime = Time.time + attackCooldown;
    }
}