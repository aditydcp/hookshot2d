using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public float horizontalInput;
    public float verticalInput;
    private Transform groundCheck;
    //private Transform groundCheckLeft;
    //private Transform groundCheckRight;
    const float groundRadius = .2f;
    public bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D m_Rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        // set up
        groundCheck = transform.Find("GroundCheck");
        //groundCheckLeft = transform.Find("GroundCheck_left");
        //groundCheckRight = transform.Find("GroundCheck_right");
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        GroundCheck();
        Movement();
        Respawn();
    }

    private void GroundCheck()
    {
        isGrounded = false;

        // isGrounded is true whenever something enters within the GroundCheck radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                isGrounded = true;
        }
    }

    private void Movement()
    {
        if (horizontalInput != 0)
        {
            // flips the character
            spriteRenderer.flipX = (horizontalInput > 0 ? true : false);
            // moves the character
            m_Rigidbody2D.velocity = new Vector2(horizontalInput * moveSpeed, m_Rigidbody2D.velocity.y);
        }

        //if((verticalInput > 0 || Input.GetButtonDown("Jump")) && isGrounded)
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Respawn()
    {
        if(transform.position.y < -10)
        {
            transform.position = new Vector2(0, 0);
        }
    }
}
