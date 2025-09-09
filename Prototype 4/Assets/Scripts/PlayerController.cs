using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player settings")]
    public float speed = 5f;
    private Rigidbody playerRb;

    [Header("Camera settings")]
    private GameObject focalPoint;

    [Header("Player powerup settings")]
    private float powerupStrength = 40f;

    public GameObject powerupIndicator;
    public PowerUpType currentPowerUp = PowerUpType.None;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        powerupIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3.forward -> A constant vector: (0, 0, 1)
            // Always points along the world Z+ axis
            // It doesn't change with object's rotation

        // transform.forward
            // A dynamic vector based on the object's rotation
            // local "forward" direction of that object expressed in the world space
            // equivalent to: transform.rotation * Vector3.forward
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position;

        if(currentPowerUp == PowerUpType.Smash && Input.GetKey(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            currentPowerUp = other.GetComponent<Powerup>().powerUpType;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());

            if(currentPowerUp == PowerUpType.Projectile)
            {
                SpawnManager.instance.LaunchProjectiles();
            }
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("awayFromPlayer: " + awayFromPlayer);
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + currentPowerUp);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        powerupIndicator.SetActive(false);
        currentPowerUp = PowerUpType.None;
    }

    [Header("SmashPowerup properties")]
    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;

    bool smashing = false;
    float floorY = 0;

    IEnumerator Smash()
    {
        float jumpTime = Time.time + hangTime;

        while(Time.time < jumpTime)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, smashSpeed);
            yield return null;
        }

        while(playerRb.position.y > floorY)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, -smashSpeed * 2);
            yield return null;
        }

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0f, ForceMode.Impulse);
        }

        smashing = false;

    }
}
