using UnityEngine;

public class BouncingCircle : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;
    private int frameCounter = 0;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Initialize the direction to a random direction
        direction = Random.insideUnitCircle.normalized;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Move the circle
        transform.Translate(direction * speed * Time.deltaTime);

        // Check for collision with screen boundaries and reverse direction if needed
        Vector2 position = Camera.main.WorldToViewportPoint(transform.position);
        if (position.x <= 0 || position.x >= 1)
        {
            direction.x *= -1;
            Debug.Log("Bouncing ball hit the screen X boundary!");
        }
        if (position.y <= 0 || position.y >= 1)
        {
            direction.y *= -1;
            Debug.Log("Bouncing ball hit the screen Y boundary!");
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                CheckTouch(touch.position);
            }
        }

        frameCounter++;

        if (frameCounter >= 120)
        {
            frameCounter = 0;
            spriteRenderer.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    void OnMouseDown()
    {
        // Log a message to the console when the circle is clicked
        Debug.Log("Bouncing ball clicked!");
    }
    void CheckTouch(Vector2 touchPosition)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        Debug.Log("Touch position: " + touchPosition + ", World position: " + worldPosition);

        Collider2D hit = Physics2D.OverlapPoint(worldPosition);
        if (hit != null)
        {
            Debug.Log("Hit object: " + hit.gameObject.name);
            if (hit.gameObject == gameObject)
            {
                // The touch hit this circle
                Debug.Log("Circle touched!");
            }
        }
        else
        {
            Debug.Log("No collider hit.");
        }
    }
}