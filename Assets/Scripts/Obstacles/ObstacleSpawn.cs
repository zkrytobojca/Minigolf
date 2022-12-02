using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    [Range(0, 100)]
    public int chanceToSpawn = 100;

    public List<GameObject> obstacles = null;

    private void Start()
    {
        int randomSpawn = Random.Range(0, 101);

        if (randomSpawn <= chanceToSpawn && obstacles != null)
        {
            if (obstacles.Count > 0)
            {
                int randomObstacle = Random.Range(0, obstacles.Count);
                Instantiate(obstacles[randomObstacle], transform.position, transform.rotation, transform);
            }
        }
    }
}
