using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : PrefabPool<Enemy>
{
    public EnemyType type;

    public static EnemyPool bat;
    public static EnemyPool snake;
    
    public static EnemyPool Get(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Bat:
                return bat;
            case EnemyType.Snake:
                return snake;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Awake()
    {
        switch (type)
        {
            case EnemyType.Bat:
                bat = this;
                break;
            case EnemyType.Snake:
                snake = this;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}