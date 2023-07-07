using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public Enemy enemy;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}