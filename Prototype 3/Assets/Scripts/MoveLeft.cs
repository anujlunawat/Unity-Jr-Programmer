using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 30f;
    private float leftBound = -15f;
    private PlayerController playerControllerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Obstacle") && transform.position.x < leftBound)
            Destroy(gameObject);
        if(!playerControllerScript.gameOver)
            transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
