using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private int count;

    bool jumpAir = false;

    private Rigidbody rb;

    private float movementX;
    private float movementY;
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText ();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;
        
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 8) 
        {
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
        }
    }

    void GroundCheck()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);

        if(Physics.Raycast(transform.position, dir, out hit, distance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Update()
    {
        
        if(isGrounded)
        {
            if(Input.GetKeyDown("space"))
            {
                Jump();
                jumpAir = true;
            }
        }
        else
        {
            if(Input.GetKeyDown("space"))
            {
                if(jumpAir)
                Jump();
                jumpAir = false;
            }
        }


        
    }


    void FixedUpdate()
    {
        Vector3 movement  = new Vector3(movementX, 0.0f,movementY);

        rb.AddForce(movement*speed);

        GroundCheck();
        //isGrounded = (null !=Physics.OverlapSphere(groundCheck.position,0.5f,groundLayer));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
    


}
