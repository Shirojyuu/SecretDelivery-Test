using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public int speed;
    public int jumpStrength;

    private Rigidbody2D rb;
    private Animator anim;

    private bool grounded;

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
        Vector2 checkPos = new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().bounds.size.y / 2);
        RaycastHit2D groundCheck = Physics2D.Raycast(checkPos, -1.0f * Vector2.up, 5.0f);

            if (groundCheck != null && groundCheck.collider.tag.Equals("Walkable"))
            {
                Debug.Log(groundCheck.collider);
                grounded = true;
            }

            anim.SetBool("Grounded", grounded);

        Debug.Log(grounded);
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
        Vector3 totalMovement = new Vector3(xMove * speed, 0, 0);
        rb.AddForce(totalMovement);

        //Jumping
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpStrength, 0);
            anim.SetTrigger("Jump");
            grounded = false;
            anim.SetBool("Grounded", grounded);

        }
    }
}
