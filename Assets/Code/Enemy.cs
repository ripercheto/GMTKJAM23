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

    private bool CanAttack => Time.time > attackTime;

    private void Awake()
    {
        health.onDeath += OnDeath;
    }

    private void OnEnable()
    {
        attackTime = Time.time + attackCooldown;
    }

    private void OnDeath()
    {
        range.Clear();
        health.ResetHealth();
        EnemyPool.instance.Deactivate(this);
    }

    private void Start()
    {
        MainCharacters.onMainDeath += OnMainDeath;
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
        closestTarget = transform.GetClosestTarget(MainCharacters.targets);
    }

    private void OnMainDeath()
    {
        deadTargets++;
        CheckIfTargetsDead();
    }

    private void CheckIfTargetsDead()
    {
        if (deadTargets >= MainCharacters.targets.Count)
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

        if ((health.transform.position - transform.position).magnitude > attackDamage)
        {
            Debug.Log(health.gameObject.name + " " + gameObject.name);
        }

        health.TakeDamage(attackDamage);
        attackTime = Time.time + attackCooldown;
    }
}