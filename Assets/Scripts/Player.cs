using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float speed;
    public float runningSpeed;
    public float slideForce;
    public float checkDist = 3.0f;
    public int jumpStrength;

    public float defaultColSize = 0.095f;
    public float slideColSize = 0.98f;

    private Rigidbody2D rb;
    private Animator anim;
   

    private float speedMod = 1.0f;

    private bool grounded;
    private bool running;

	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () 
    {
        //Ground Check  
        Vector2 checkPos = new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().bounds.size.y / 2.0f);

        RaycastHit2D groundCheck = Physics2D.Raycast(checkPos, checkDist * Vector2.down, checkDist);
        Debug.DrawRay(checkPos, Vector2.down * checkDist, Color.green);

            if (groundCheck != null && groundCheck.collider.tag.Equals("Walkable"))
            {
                Debug.Log(groundCheck.collider);
                grounded = true;
            }

            anim.SetBool("Grounded", grounded);

        Debug.Log(grounded);

        
	}

    void LateUpdate()
    {
        //Consider revision
        /*
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("VenusSlide"))
        {
            GetComponent<BoxCollider2D>().offset = new Vector2(0, slideColSize);
        }

        else
        {
            GetComponent<BoxCollider2D>().offset = new Vector2(0, defaultColSize);
        } 
        */
    }
    void FixedUpdate()
    {
        //Horizontal Movement
        float xMove = Input.GetAxisRaw("Horizontal");

        if(xMove < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if(xMove > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }
        anim.SetFloat("XSpeed", Mathf.Abs(xMove * speed));
        Vector3 totalMovement = new Vector3(xMove * speed * speedMod, 0, 0);
        rb.AddForce(totalMovement);

        HandleJump();
        HandleRun(xMove);
        HandleSlide();   
    }

    private void HandleJump()
    {
        //Jumping
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpStrength, 0);
            anim.SetTrigger("Jump");
            grounded = false;
            anim.SetBool("Grounded", grounded);

        }
    }

    private void HandleRun(float xMove)
    {
        //Running
        if (Input.GetButtonDown("RunButton") && xMove != 0)
        {
            speedMod = runningSpeed;
            running = true;
            anim.SetBool("Running", running);
        }

        if (Input.GetButtonUp("RunButton"))
        {
            speedMod = 1.0f;
            running = false;
            anim.SetBool("Running", running);
        }
    }

    private void HandleSlide()
    {
        //Slide!
        if (Input.GetButtonDown("SlideButton") && grounded)
        {
            if (GetComponent<SpriteRenderer>().flipX)
                rb.velocity = new Vector3(-slideForce, rb.velocity.y, 0);

            if (!GetComponent<SpriteRenderer>().flipX)
                rb.velocity = new Vector3(slideForce, rb.velocity.y, 0);
            anim.SetTrigger("Slide");

        }
    }
}
