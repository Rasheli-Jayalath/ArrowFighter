using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archerScript : MonoBehaviour
{
    public float moveForce = 20f, jumpForce = 550f, maxVelocity = 3f;
    private Rigidbody2D myBody;
    private Animator anim;
    private bool grounded = true;
    private bool grounded2 = true;
    public float h;
    public float v;
    //public float F;

    public float ForceX = 0;
    public float ForceY = 0;
    
  

    private void Awake()
    {
        //Shoot();
        variables();
    }
    void Start()
    {

    }
    void Update()
    {
    
    }

    void FixedUpdate()
    {
        walkKeyboard();
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    void variables()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }  

    void walkKeyboard()
    {
        float ForceX = 0;
        float ForceY = 0;
        float vel = Mathf.Abs(myBody.velocity.x);

        if (h > 0)
        {
            anim.SetBool("walk", true);

            if (vel < maxVelocity)
            {
                ForceX = moveForce;
            }
            Vector3 scale = transform.localScale;
            scale.x = 0.7f;
            transform.localScale = scale;
        }

        else if (h < 0)
        {
            anim.SetBool("walk", true);
            if (vel < maxVelocity)
            {
                ForceX = -moveForce;
            }
            Vector3 scale = transform.localScale;
            scale.x = -0.7f;
            transform.localScale = scale;
        }
        else if (h == 0)
        {
            anim.SetBool("walk", false);
        }

        if (v > 0)
        {
            if (grounded)
            {
                anim.SetBool("jump", true);
                grounded = false;
                ForceY = jumpForce;  
            }
            else
            {
                anim.SetBool("walk", false);
            }
        }

        if (!grounded)
        {
            anim.SetBool("walk", false);
        }
        if (grounded)
        {
            myBody.AddForce(new Vector2(ForceX, ForceY));
        }
        myBody.AddForce(new Vector2(ForceX, ForceY));
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "fireBall")
        {
            //anim.SetBool("damage", true);
            StartCoroutine(healthDown());
            Destroy(GameObject.FindWithTag("fireBall"));
        }

        IEnumerator healthDown()
        {
            yield return new WaitForSeconds(0.3f);
            //anim.SetBool("damage", false);
        }

    }

}
