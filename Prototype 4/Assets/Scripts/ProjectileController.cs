using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Transform target;
    private float speed = 15f;
    private bool homing = false;

    [SerializeField]private float rocketStrength = 20f;
    private float aliveTimer = 5f;

    // Update is called once per frame
    void Update()
    {
        if(homing && (target != null))
        {
            Vector3 moveDirection = (target.position - transform.position);
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (target != null && collision.gameObject.CompareTag(target.tag)) {
            Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 away = -collision.GetContact(0).normal;
            targetRb.AddForce(away * rocketStrength, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}
