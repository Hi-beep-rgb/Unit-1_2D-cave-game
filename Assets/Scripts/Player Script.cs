using UnityEngine;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    bool isGrounded;
    public Animator anim;
    public bool flip;
    HelperScript helper;
    LayerMask groundLayerMask;
    public GameObject weapon;
    public float hp;
    GameObject Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundLayerMask = LayerMask.GetMask("Ground");
        helper = gameObject.AddComponent<HelperScript>();
        hp = 150;
    }

    // Update is called once per frame
    void Update()
    {
        float xvel, yvel;

        Projectile();

        xvel = rb.linearVelocity.x;
        yvel = rb.linearVelocity.y;

        if (Input.GetKey("a"))
        {
            xvel = -8;
        }

        if (Input.GetKey("d"))
        {
            xvel = 8;
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            yvel = 8;
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);

        if (xvel >= 0.1f || xvel <= -0.1f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (DoRayCollisionCheck() == true)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

       if (xvel < 0)
        {
            helper.DoFlipObject(true);
        }
        if (xvel > 0)
        {
            helper.DoFlipObject(false);
        }

        if (hp <= 0)
        {
            Destroy(Player);
        }
    }
    public bool DoRayCollisionCheck()
    {
        float rayLength = 0.8f; //Length of the raycast

        //casting the raycast downwards
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayerMask);

        Color hitColor = Color.white;

        if (hit.collider != null)
        {
            /* print("Player has collided with the Ground layer"); */
            hitColor = Color.green;
        }

        //Drawing the debug ray to show the rays position
        //Turn on gizmos in game editor to see
        Debug.DrawRay(transform.position, Vector2.down * rayLength, hitColor);
        return hit.collider;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", true);
        }
    }

    void Projectile()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //This code will Instantiate the arrow at the players position and rotation
            GameObject arrow;
            arrow = Instantiate(weapon, transform.position, transform.rotation);

            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            SpriteRenderer sr = arrow.GetComponent<SpriteRenderer>();

            //if statment to flip the projectile of the player
            if ( helper.IsFlipped() ==  true )
            {
                //setting the velocity of the projectile
                rb.linearVelocity = new Vector2(-15, 0);

            }
            else
            {
                //setting the velocity of the projectile
                rb.linearVelocity = new Vector2(15, 0);

            }

            //setting the position close to the players position
            rb.transform.position = new Vector3(transform.position.x + 2, transform.position.y + 3, transform.position.z + 1);
        }
    }
}
