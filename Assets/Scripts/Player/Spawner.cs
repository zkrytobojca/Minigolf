using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject[] spawnPoints;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        if (spawnPoints.Length > 0)
        {
            int spawnPointId = Random.Range(0, spawnPoints.Length);
            GameObject chosenSpawnPoint = spawnPoints[spawnPointId];
            GameObject player = Instantiate(playerPrefab, chosenSpawnPoint.transform.position, chosenSpawnPoint.transform.rotation);
            PlayerModel modelScript = (PlayerModel)player.GetComponent(typeof(PlayerModel));
            modelScript.InstantiateModel(PlayerModel.skinId);
        }
            
    }
}
