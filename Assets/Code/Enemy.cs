using System.Collections;
using UnityEngine;

public class Enemy : GameBehaviour
{
    public EnemyRange range;
    public float attackCooldown = 1;
    public float attackDamage = 10;
    private float attackTime;
    private int deadTargets;
    private Vector3? endDirection;

    private Transform closestTarget;
    private Transform[] targets;

    private bool CanAttack => Time.time > attackTime;

    private void Awake()
    {
        health.onDeath += () => Destroy(gameObject);
    }

    private void Start()
    {
        targets = new[]
        {
            Princess.instance.transform,
            PlayerInput.instance.transform
        };

        Princess.instance.health.onDeath += OnMainDeath;
        PlayerInput.instance.health.onDeath += OnMainDeath;
    }

    private void Update()
    {
        if (endDirection.HasValue)
        {
            movement.UpdateDesiredVelocity(endDirection.Value);
            return;
        }

        TryUpdateUpdateTarget();
        HandleAttack();

        var pos = transform.GetFlatPosition();
        if (closestTarget != null)
        {
            var dir = closestTarget.position - pos;
            movement.UpdateDesiredVelocity(dir);
        }
    }

    private void TryUpdateUpdateTarget()
    {
        if (Time.frameCount % 30 != 0)
        {
            return;
        }
        closestTarget = transform.GetClosestTarget(targets);
    }

    private void OnMainDeath()
    {
        deadTargets++;
        if (deadTargets >= targets.Length)
        {
            endDirection = Random.insideUnitCircle.To3D().normalized;
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