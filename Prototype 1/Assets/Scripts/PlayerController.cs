using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField]private float speed = 10;
    [SerializeField]private float turnSpeed = 10;

    [Header("Input Details")]
    [SerializeField] private float horizontalInput;
    [SerializeField] private float forwardInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Get -1 for left and 1 for right
        //it is async float value, so you may get the values between -1 and 1 as well.
        horizontalInput = Input.GetAxis("Horizontal");

        //same goes for the forward inputs
        forwardInput = Input.GetAxis("Vertical");

        // Move the vehicle forward
        //transform.Translate(0, 0, 1);

        //Time.deltaTime -> time between frames.
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        //Moves the car based on horizontal input
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
    }
}
