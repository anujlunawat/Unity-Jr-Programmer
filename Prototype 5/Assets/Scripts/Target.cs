using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    private GameManager gameManager;

    public int pointValue;
    private int good1Points = 2;
    private int good2Points = 5;
    private int good3Points = 3;
    private int bad1Points = -5;

    public ParticleSystem explosionParticle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        transform.position = RandomSpawnPos();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        assignPointValues();
    }

    private void assignPointValues()
    {
        if (gameObject.CompareTag("Good 1"))
            pointValue = good1Points;
        else if (gameObject.CompareTag("Good 2"))
            pointValue = good2Points;
        else if (gameObject.CompareTag("Good 3"))
            pointValue = good3Points;
        else if (gameObject.CompareTag("Bad 1"))
            pointValue = bad1Points;
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    //private void OnMouseDown()
    //{
    //    if (gameManager.isGameActive && !gameManager.paused)
    //    {
    //        gameManager.UpdateScore(pointValue);
    //        Destroy(gameObject);
    //        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Bad 1"))
            gameManager.UpdateLives();
        Destroy(gameObject);
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive && !gameManager.paused)
        {
            gameManager.UpdateScore(pointValue);
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }
}
