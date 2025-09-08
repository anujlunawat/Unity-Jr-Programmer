using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] animalPrefabs;
    public float startDelay = 2.0f;
    public float spawnInterval = 1.5f;

    [Header("Vertical Spawn Settings")]
    public float spawnRangeX = 20f;
    public float spawnPosZ = 20f;

    [Header("Horizontal Spawn Settings")]
    public float spawnRangeZMin = 1f;
    public float spawnRangeZMax = 15f;
    public float spawnPosX = -25f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomAnimal), startDelay, spawnInterval);
    }

    // Randomly chooses vertical or horizontal spawn
    void SpawnRandomAnimal()
    {
        if (Random.value < 0.5f)
            SpawnVertical();
        else
            SpawnHorizontal();
    }

    void SpawnVertical()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        Spawn(spawnPos, animalPrefabs[0].transform.rotation); // default prefab rotation
    }

    void SpawnHorizontal()
    {
        Vector3 spawnPos = new Vector3(spawnPosX, 0, Random.Range(spawnRangeZMin, spawnRangeZMax));
        Spawn(spawnPos, Quaternion.Euler(0, 90, 0)); // rotated to face +X
    }

    void Spawn(Vector3 position, Quaternion rotation)
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(animalPrefabs[animalIndex], position, rotation);
    }
}
