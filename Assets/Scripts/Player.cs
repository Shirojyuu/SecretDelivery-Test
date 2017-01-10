using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public int speed;
    public int jumpStrength;

    private Rigidbody2D rb;
    private Animator anim;

    private bool grounded;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float xMove = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("XSpeed", Mathf.Abs(xMove * speed));
        Vector3 totalMovement = new Vector3(xMove * speed, 0, 0);
        rb.AddForce(totalMovement);

        if(Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpStrength, 0);
            anim.SetTrigger("Jump");
            grounded = false;
            anim.SetBool("Grounded", grounded);

        }

        RaycastHit2D[] groundCheck = Physics2D.RaycastAll(transform.position, Vector2.up * -5.0f, -5.0f, LayerMask.NameToLayer("Environment"));

        for (int i = 0; i < groundCheck.Length; i++)
        {
            if (groundCheck[i].transform.tag.Equals("Walkable"))
            {
                Debug.Log(groundCheck[i].collider);
                grounded = true;
                anim.SetBool("Grounded", grounded);
            }
        }
        Debug.Log(grounded);
	}
}
