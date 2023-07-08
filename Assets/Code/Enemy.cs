using System.Collections;
using System.Collections.Generic;
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
    private List<Transform> targets;

    private bool CanAttack => Time.time > attackTime;

    private void Awake()
    {
        health.onDeath += OnDeath;
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        targets = new();
        
        if (Princess.instance != null)
        {
            Princess.instance.health.onDeath += OnMainDeath;
            targets.Add(Princess.instance.transform);
        }
        
        if (PlayerInput.instance != null)
        {
            PlayerInput.instance.health.onDeath += OnMainDeath;
            targets.Add(PlayerInput.instance.transform);
        }
        
        CheckIfTargetsDead();
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
        CheckIfTargetsDead();
    }

    private void CheckIfTargetsDead()
    {
        if (deadTargets >= targets.Count)
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