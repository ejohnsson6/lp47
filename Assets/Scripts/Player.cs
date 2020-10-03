using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float jumpForce = 5f;
    private new Rigidbody2D rigidbody;

    [SerializeField] private GameObject cloud;

    private float minAllowedY = -10f;

    private Vector2 startPosition;
    private Quaternion startRotation;

    private Vector2 leftRaycastOrigin;
    private Vector2 rightRaycastOrigin;

    private Vector2 rayCastVector;

    private BoxCollider2D boxCollider;
    private static bool doubleJump;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        rayCastVector = Vector2.up * 0.001f;
        doubleJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        leftRaycastOrigin = new Vector2(transform.position.x - (boxCollider.size.x / 2) * 0.99f, transform.position.y - (boxCollider.size.y / 2) * 0.99f - rayCastVector.y);
        rightRaycastOrigin = new Vector2(transform.position.x + (boxCollider.size.x / 2) * 0.99f, transform.position.y + (boxCollider.size.y / 2) * 0.99f - rayCastVector.y);

        var onGround = OnGround();

        if (onGround)
        {
            doubleJump = true;
        }

        if (transform.position.y <= minAllowedY)
        {
            Reset();
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * (Time.deltaTime * speed));
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (onGround)
            {
                print("That sad single jump :(");
                rigidbody.AddForce(Vector2.up * jumpForce);
            }
            else if (doubleJump)
            {
                print("DOUBLE JUMP!");
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                rigidbody.AddForce(Vector2.up * jumpForce);
                doubleJump = false;
                Instantiate(cloud, transform.position - new Vector3(0, boxCollider.size.y * 0.5f, 0), new Quaternion());
            }

        }
    }

    private bool OnGround()
    {
        // Am on on the ground?
        Vector3 halfSize = (boxCollider.size * 0.51f);
        var hitLeft = Physics2D.Raycast(transform.position - halfSize, Vector2.up);
        var hitRight = Physics2D.Raycast(transform.position - new Vector3(-halfSize.x, halfSize.y, 0), Vector2.up);

        Debug.DrawRay(transform.position - new Vector3(-halfSize.x, halfSize.y, 0), Vector2.up);
        Debug.DrawRay(transform.position - halfSize, Vector2.up);
        return (hitLeft.collider != null) || (hitRight.collider != null);
    }

    private void Reset()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        rigidbody.velocity = Vector2.zero;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == null)
        {
            return;
        }

        if (collision.gameObject.tag.Contains("enemy_"))
        {
            // We hit an enemy, if we hit it from above, kill it
            // Otherwise we should take damage or something
            RaycastHit2D left = Physics2D.Raycast(leftRaycastOrigin, rayCastVector);
            RaycastHit2D right = Physics2D.Raycast(rightRaycastOrigin, rayCastVector);

            if (left.collider?.gameObject == collision.gameObject || right.collider?.gameObject == collision.gameObject)
            {
                // Kill it!
            }
        }

    }
}
