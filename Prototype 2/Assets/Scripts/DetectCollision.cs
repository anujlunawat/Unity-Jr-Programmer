using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static int lives = 3;
    public static int score = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if an animal collides with the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Lives: " + --lives);
            if(lives == 0)
                Debug.Log("Game Over!");
            return;
        }

        //if an animal collides with the food
        other.gameObject.SetActive(false);
        Destroy(gameObject);
        Debug.Log("Score: " + ++score);
    }
}
