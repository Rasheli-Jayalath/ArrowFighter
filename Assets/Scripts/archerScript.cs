using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archerScript : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded = true;
    private float h;
    private bool jumpPressed = false;

    private bool arrowReady = true;
    public float arrowForce;
    public GameObject arrow;

    public int health = 3;
    public GameObject[] hearts;
    private bool isDie = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Read input
        h = Input.GetAxisRaw("Horizontal");

        // Trigger jump on key press
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpPressed = true;
        }
        if (Input.GetButtonDown("Fire1") && arrowReady)
        {
            //isShoot = true;
            ArrowShoot();
        }

        HandleAnimations();

        if (!isDie)
        {

            for (int i = 0; i < health; i++)
            {
                hearts[i].SetActive(true);
            }
            for (int j = health; j < hearts.Length; j++)
            {
                hearts[j].SetActive(false);
            }
        }



    }

    void FixedUpdate()
    {
        if (!isDie)
        {
            Move();
            Jump();
        }
      
    }



    public void ArrowShoot()
    {
        if (!isDie)
        {
            anim.SetBool("shoot", true);
            arrowReady = false;
            StartCoroutine(DelayedArrowShoot());
        }
    
    }

    IEnumerator DelayedArrowShoot()
    {
        yield return new WaitForSeconds(0.3f); // Delay for animation sync

        Vector3 arrowInitPos;
        Vector2 arrowVelocity;

        if (transform.localScale.x > 0)
        {
            arrowInitPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            arrowVelocity = transform.right * arrowForce;
        }
        else
        {
            arrowInitPos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            arrowVelocity = transform.right * -arrowForce;
        }

        GameObject ArrowIns = Instantiate(arrow, arrowInitPos, transform.rotation);
        ArrowIns.GetComponent<Rigidbody2D>().velocity = arrowVelocity;

        anim.SetBool("shoot", false);

        // Wait 1 second cooldown before allowing next shot
        yield return new WaitForSeconds(0.4f);
        arrowReady = true;
    }


    private void Move()
    {
        rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);


        if (h != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(h) * 0.6f, 0.6f, 1f);
        }
    }

    private void Jump()
    {
        if (jumpPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            jumpPressed = false;
        }
    }

    private void HandleAnimations()
    {
        anim.SetBool("walk", h != 0);
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("fireball"))
        {
         
            if (!isDie)
            {
                anim.SetBool("damage", true);
                StartCoroutine(HealthDown());
                Destroy(coll.gameObject);
                health--;
            }
            if (health == 0)
            {
                anim.SetBool("die", true);
                isDie = true;
            }
        }
    }

    IEnumerator HealthDown()
    {
  
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("damage", false);
    }
}
