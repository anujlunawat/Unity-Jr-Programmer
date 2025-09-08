using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 9f;
    public GameObject enemyPrefab;
    
    //public int enemyCount = 0;
    public int waveNumber = 1;

    public GameObject powerupPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
        if (Enemy.enemyCount == 0)
            SpawnEnemyWave(waveNumber++);
    }

    Vector3 GenerateSpawnPosition()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnX, 0, spawnZ);

        return randomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Instantiate a powerup only if no previous powerup is present
        int powerups = GameObject.FindGameObjectsWithTag("Powerup").Length;
        if(powerups == 0)
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Enemy.enemyCount++;
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
