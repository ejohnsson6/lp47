using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float jumpForce = 5f;
    private new Rigidbody2D rigidbody;

    private float minAllowedY = -10f;

    private Vector2 startPosition;
    private Quaternion startRotation;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= minAllowedY)
        {
            Reset();
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            rigidbody.AddForce(Vector2.up * jumpForce);
        }
    }

    private void Reset()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        rigidbody.velocity = Vector2.zero;
    }
}
