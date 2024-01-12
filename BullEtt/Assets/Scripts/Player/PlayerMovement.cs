using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] float moveSpeed = 2;
    [Tooltip("True if the game is " +
        "top dwon, false if a platformer")]
    [SerializeField] bool isTopDown = true;
    [SerializeField] bool allowKeyControls = true;
    [SerializeField] float jumpForce = 5.0f;
    Collider2D collider;
    [SerializeField] Canvas mobileCanvas;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        if (isTopDown)
        {
            rb2d.gravityScale = 0;
        }
        else
        {
            joystick.AxisOptions = AxisOptions.Horizontal;
        }
        if (allowKeyControls)
        {
            mobileCanvas.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(0, 0);
        if (allowKeyControls)
        {
            move.x = Input.GetAxis("Horizontal");
            move.y = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            move.x = joystick.Horizontal;
            move.y = joystick.Vertical;
        }
        if (isTopDown)
        {
            rb2d.velocity = new Vector3(move.x * moveSpeed, move.y * moveSpeed);
        }
        else
        {
            rb2d.velocity = new Vector3(move.x * moveSpeed, rb2d.velocity.y, 0);
        }
    }

    public void Jump()
    {
        if (collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb2d.AddForce(new Vector2(0, 20 * jumpForce));
        }
    }
}
