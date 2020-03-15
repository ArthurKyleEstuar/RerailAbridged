using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    [SerializeField] private GameSceneController    gameController;

    [Header("Spawn Timing")]
    [SerializeField] private Vector2                spawnDelay      = new Vector2(10.0f, 15.0f);
    
    [Header("Spawn Parameters")]
    [SerializeField] private int                    maxTrainSetSize = 3;
    [SerializeField] private float                  zSpawn          = -1.0f;

    [Header("SpawnObjects")]
    [SerializeField] private List<GameObject>       trainPrefabs    = new List<GameObject>();

    private const float TRAIN_SIZE      = 6.63f;

    private float       currTimer       = 0;
    private float       currSpawnDelay;


    private void Start()
    {
        Vector3 adjustedPos = this.transform.position;
        adjustedPos.z = zSpawn;
        this.transform.position = adjustedPos;
        RandomizeSpawnTime();

        SpawnTrain();
    }

    private void RandomizeSpawnTime()
    {
        currSpawnDelay = Random.Range(spawnDelay.x, spawnDelay.y);
    }

    private void Update()
    {
        currTimer += Time.deltaTime;
        
        if(currTimer >= currSpawnDelay)
        {
            currTimer = 0;
            RandomizeSpawnTime();
            SpawnTrain();
        }
    }

    private void SpawnTrain()
    {
        int spawnSize = Random.Range(1, maxTrainSetSize + 1);

        for (int i = 0; i < spawnSize; i++)
        {
            float spawnX = this.transform.position.x - (TRAIN_SIZE * i);
            int spawnIndex = 0;

            Vector3 spawnPos = this.transform.position;
            spawnPos.x = spawnX;

            if (i != 0)
                spawnIndex = Random.Range(1, trainPrefabs.Count);

            GameObject go = Instantiate(trainPrefabs[spawnIndex]
                , spawnPos
                , Quaternion.identity);

            Train t = go.GetComponent<Train>();

            if (t == null) continue;

            t.Initialize(gameController, i == 0, i == spawnSize - 1);
        }
    }
}
