using System;
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
    private new BoxCollider2D boxcollider;

    private float minAllowedY = -10f;

    private Vector2 startPosition;
    private Quaternion startRotation;

    private static bool doubleJump;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        doubleJump = true;
    }

    // Update is called once per frame
    void Update()
    {
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
            transform.Translate(Vector3.right * (Time.deltaTime * speed));
        } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * (Time.deltaTime * speed));
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (onGround)
            {
                rigidbody.AddForce(Vector2.up * jumpForce);
            
            } else if (doubleJump)
            {
                rigidbody.AddForce(Vector2.up * jumpForce);
                doubleJump = false;
                
            }
            
        }
    }

    private bool OnGround()
    {
        // Am on on the ground?
        var hit = Physics2D.Raycast(transform.position - Vector3.up * (boxcollider.size.y * 0.51f), Vector2.up);
        print(hit.collider == boxcollider);
        return (hit.collider != boxcollider);
    }

    private void Reset()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        rigidbody.velocity = Vector2.zero;
        
    }
}
