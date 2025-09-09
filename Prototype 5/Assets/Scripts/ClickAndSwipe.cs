using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;

    private TrailRenderer trail;
    private BoxCollider col;

    private bool swiping = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        trail.enabled = false;
        col.enabled = false;
    }

    void Update()
    {
        if (gameManager.isGameActive)
        {
            // if the left mouse button is clicked
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }

            //if the right mouse button is clicked
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }

            if (swiping)
            {
                UpdateMousePosition();
            }
        }
    }

    void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        transform.position = mousePos;
    }

    void UpdateComponents()
    {
        trail.enabled = swiping;
        col.enabled = swiping;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
}
