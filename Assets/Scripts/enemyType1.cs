using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform pointA;
    public Transform pointB;
    public Transform player;
    public GameObject projectilePrefab;

    
    public float moveSpeed = 2f;
    public float throwCooldown = 2f;
    public float projectileForce = 5f;
    public bool isThrowing = false;

    private Animator anim;
    private Rigidbody2D rb;
    private Vector3 nextPoint;
    private bool facingRight = true;
    private bool canThrow = true;


    public int health = 3;
    public GameObject[] hearts;
    private bool isDie = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        nextPoint = pointB.position;
    }

    private void Update()
    {
        if (!isDie)
        {
            if (isThrowing)
            {
                rb.velocity = Vector2.zero;
                anim.SetBool("walk", false);

                FacePlayer();

                if (canThrow)
                    StartCoroutine(ThrowRoutine());
            }
            else
            {
                Patrol();
            }

       
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].SetActive(true);
        }
        for (int j = health; j < hearts.Length; j++)
        {
            hearts[j].SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("arrow"))
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
    private void Patrol()
    {
        anim.SetBool("walk", true);

        transform.position = Vector2.MoveTowards(transform.position, nextPoint, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, nextPoint) < 0.1f)
        {
           
            if (nextPoint == pointA.position)
            {
                nextPoint = pointB.position;
                FaceDirection(false);
            }
            else
            {
                nextPoint = pointA.position;
                FaceDirection(true); 
            }
        }
    }

    private void FacePlayer()
    {
        if (player == null) return;

        if (player.position.x > transform.position.x && !facingRight)
            Flip();
        else if (player.position.x < transform.position.x && facingRight)
            Flip();
    }

    private void FaceDirection(bool faceRight)
    {
        if (facingRight != faceRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * (facingRight ? 1 : -1);
        transform.localScale = localScale;
    }


    IEnumerator ThrowRoutine()
    {
        canThrow = false;
        anim.SetBool("throw",true);

        yield return new WaitForSeconds(0.3f); // sync with throw animation

        Vector3 spawnPos = transform.position + new Vector3(facingRight ? 1f : -1f, 0.5f, 0);
        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
        projRb.velocity = new Vector2(facingRight ? 1 : -1, 0) * projectileForce;
        anim.SetBool("throw", false);
        yield return new WaitForSeconds(throwCooldown);
        canThrow = true;

    }
}
