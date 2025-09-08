using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody playerRb;

    private GameObject focalPoint;

    public bool hasPowerup = false;
    private float powerupStrength = 40f;

    public GameObject powerupIndicator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        //powerupIndicator = GameObject.Find("Powerup Indicator");

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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log(awayFromPlayer);
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        powerupIndicator.SetActive(false);
        hasPowerup = false;
    }
}
