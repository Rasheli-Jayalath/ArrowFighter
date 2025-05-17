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


    private int health;
    public static bool isDie = false;
    private bool dieHasTriggered = false;

    private int Arrows;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Arrows = PlayerPrefs.GetInt("arrows");
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
        if(!isDie)
        {

            health = PlayerPrefs.GetInt("health");
        }
        if(health ==0 )
        {
            dieHasTriggered = true;
            isDie = true;
        }
        if (dieHasTriggered)
        {
            anim.SetBool("die", true);
            dieHasTriggered = false;
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
        if (!isDie && Arrows!=0)
        {
            Arrows--;
            PlayerPrefs.SetInt("arrows",Arrows);
            anim.SetBool("shoot", true);
            arrowReady = false;

            int score = PlayerPrefs.GetInt("score");
            score += PlayerPrefs.GetInt("health") * 2;
            PlayerPrefs.SetInt("score", score);

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
                PlayerPrefs.SetInt("health",health);
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
