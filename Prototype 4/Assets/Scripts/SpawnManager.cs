using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 9f;
    public GameObject[] enemyPrefabs;
    
    //public int enemyCount = 0;
    public int waveNumber = 1;

    public GameObject[] powerupPrefab;

    public static SpawnManager instance;

    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        player = GameObject.Find("Player");
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
        int powerupIndex = Random.Range(0, powerupPrefab.Length);

        if(powerups == 0)
            Instantiate(powerupPrefab[powerupIndex], GenerateSpawnPosition(), powerupPrefab[powerupIndex].transform.rotation);

        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Enemy.enemyCount++;

            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], GenerateSpawnPosition(), enemyPrefabs[enemyIndex].transform.rotation);
        }
    }

    public GameObject projectilePrefab;
    public void LaunchProjectiles()
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Debug.Log("enemy: " + enemy);
            GameObject projectileTmp = Instantiate(projectilePrefab, player.transform.position + Vector3.up, Quaternion.identity);
            projectileTmp.GetComponent<ProjectileController>().Fire(enemy.transform);
        }
    }
}
