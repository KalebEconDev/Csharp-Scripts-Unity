using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{  
    private int t;
    private bool hitEnemy;
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private Collider2D bulletCollider;
    public float damage;

    // Start is called before the first frame update


    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(8, 8, true);
    }
    void Start()
    {
        t = 0;
        rb = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<Collider2D>();
        playerCollider = GameObject.Find("Player").GetComponent<Collider2D>();
        //Physics2D.IgnoreCollision(bulletCollider, playerCollider, true);
        //Physics2D.IgnoreLayerCollision(8,9,true);
    }

     void Update()
    {
        

        //if ()
        //{
          //  hitEnemy = true;
          //  Debug.Log("Hit!");
        //}
        //else {hitEnemy = false;}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t += 1;
        if(t >= 500) { 
            GameObject.Destroy(this.gameObject);
        }

        //bulletFriction();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.layer == "Enemy");
        //Debug.Log("Bullet hit:" + collision.gameObject.name);

        if(collision.gameObject.layer == 10)
        {
            GameObject.Destroy(this.gameObject);
        }
        if(collision.gameObject.layer == 6)
        {
            rb.velocity = Vector2.zero;
        }

    }

    void bulletFriction()
    {
        if (rb.velocity.x > 0)
        {
            float newVelX = rb.velocity.x - .03f;

            if (newVelX >= 0)
            {
                rb.velocity = new Vector2(newVelX, rb.velocity.y);
            }
            else
            {
                newVelX = 0;
                rb.velocity = new Vector2(newVelX, rb.velocity.y);
            }


        }

        if (rb.velocity.y > 0)
        {
            float newVelY = rb.velocity.y - .03f;

            if (newVelY >= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, newVelY);
            }
            else
            {
                newVelY = 0;
                rb.velocity = new Vector2(rb.velocity.x, newVelY);
            }
        }



        if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            float newVelX = rb.velocity.x + .03f;

            if (newVelX <= 0)
            {
                rb.velocity = new Vector2(newVelX, rb.velocity.y);
            }
            else
            {
                newVelX = 0;
                rb.velocity = new Vector2(newVelX, rb.velocity.y);
            }
        }


        if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            float newVelY = gameObject.GetComponent<Rigidbody2D>().velocity.y + .03f;

            if (newVelY <= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, newVelY);
            }
            else
            {
                newVelY = 0;
                rb.velocity = new Vector2(rb.velocity.x, newVelY);
            }
        }
    }
}
