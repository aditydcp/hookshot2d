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
    public float bulletSpeed = 10f;
    public GameObject fire;
    public Vector3 mousePos;

    public float MaxFuelCapacity = 100f;
    public float CurrentFuelLevel = 100f;
    public float FuelConsumptionRate = 5f;
    public float FuelRegenerationRate = 10f;
    public float JetpackThrust = 25f;
    public float RefuelCooldownSeconds = 2f;

    private IEnumerator _refuelCooldown = null;

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
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        // GroundCheck();
        Movement();
        Respawn();
        Jetpack();
        //Attack();
    }

    /*private void GroundCheck()
    {
        isGrounded = false;

        // isGrounded is true whenever something enters within the GroundCheck radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                isGrounded = true;
        }
    }*/

    private void Jetpack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_refuelCooldown != null)
            {
                StopCoroutine(_refuelCooldown);
                _refuelCooldown = null;
            }

            FireJetpack();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _refuelCooldown = RefuelCooldown();
            StartCoroutine(_refuelCooldown);
        }
    }

    private IEnumerator RefuelCooldown()
    {
        yield return new WaitForSeconds(RefuelCooldownSeconds);

        while (CurrentFuelLevel < MaxFuelCapacity)
        {
            RefuelJetpack();

            yield return null;
        }
    }

    private void FireJetpack()
    {
        if (CurrentFuelLevel > 0)
        {
            m_Rigidbody2D.AddForce(Vector2.up * Time.deltaTime * JetpackThrust, ForceMode2D.Impulse);

            if (CurrentFuelLevel - FuelConsumptionRate * Time.deltaTime > 0)
            {
                CurrentFuelLevel -= FuelConsumptionRate * Time.deltaTime;
            }
            else
            {
                CurrentFuelLevel = 0;
            }
        }
    }

    private void RefuelJetpack()
    {
        if (CurrentFuelLevel + FuelRegenerationRate * Time.deltaTime < MaxFuelCapacity)
        {
            CurrentFuelLevel += FuelRegenerationRate * Time.deltaTime;
        }
        else
        {
            CurrentFuelLevel = MaxFuelCapacity;
        }
    }

    private void Movement()
    {
        if (horizontalInput != 0)
        {
            // moves the character
            m_Rigidbody2D.velocity = new Vector2(horizontalInput * moveSpeed, m_Rigidbody2D.velocity.y);
        }
    }

    private void Respawn()
    {
        if(transform.position.y < -10)
        {
            transform.position = new Vector2(0, 0);
        }
    }

    private Vector3 MousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            GameObject bullet = GameObject.Instantiate(fire, transform.position, Quaternion.identity) as GameObject;
            bullet.transform.SetParent(transform);
            bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, targetPos, bulletSpeed * Time.deltaTime);
        }
    }
}
