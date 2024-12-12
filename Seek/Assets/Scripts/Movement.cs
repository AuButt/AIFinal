using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Movement")]
    public float speed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyJump = true;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDir;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void FixedUpdate()
    {
        movePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();

        SpeedControl();

        //ground drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(jumpKey) && readyJump && grounded)
        {
            readyJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void movePlayer()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //if(rb.velocity.magnitude < topSpeed)
        if (grounded)
            rb.AddForce(moveDir.normalized * speed, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDir.normalized * speed * airMultiplier, ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 topVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (topVel.magnitude > speed)
        {
            Vector3 limitedVel = topVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    private void ResetJump()
    {
        readyJump = true;
    }
}
