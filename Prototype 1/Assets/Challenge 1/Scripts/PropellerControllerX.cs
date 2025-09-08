using UnityEngine;

public class PropellerControllerX : MonoBehaviour
{
    public float speed = 180f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(xAngle, yAngle, zAngle)
        //transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        //transform.Rotate(axis, angle)
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
