using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    public Transform bulletPos;
    Rigidbody2D rb;
    Animator anim;
    Vector2 direction;
    public GameObject AttackBullet;
    public ParticleSystem Dust;
    public AudioManager audioManager;
    public GameManager gm;
    CheckPointManger cm;

    [Header("Bools")]
    public bool isGrounded;
    public bool facingRight, flip, canFlip,bubbleHit,death = false,canMove;
    public bool canShoot;

    [Header("Floats")]
    public float charSpeed;
    public float gravity = 1f, fallMultiplyer = 5f, jumpHeight, maxSpeed = 7f, linerDrag = 4f,jumpDelay = 0.25f,jumpTimer, slowSecond;
    
    
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        canFlip = true;
        anim = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        jumpTimer = 0f;
        canMove = true;
        cm = GameObject.FindGameObjectWithTag("GM").GetComponent<CheckPointManger>();
        transform.position = cm.lastCheckpointpos;
        canShoot = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Horizontal"));

            MoveChar(direction.x);

            updatedPhysics();

            if (flip == true && canFlip == true)
            {
                Flip();
            }

            if (jumpTimer > Time.time && isGrounded)
            {
                Jump();
            }
        }
        
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpTimer = Time.time + jumpDelay;
        }
        if(Input.GetKeyDown(KeyCode.F) && canShoot == true)
        {
            Debug.Log("Fire");
            Vector2 bullPos = bulletPos.transform.position;
            Instantiate(AttackBullet,bullPos,bulletPos.rotation,gameObject.transform);
            StartCoroutine(BulletReload(3f));
        }

        if(bubbleHit)
        {
            StartCoroutine(SlowPlayer(slowSecond));
        }
        if (death)
            Death();
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            gm.ReloadScene();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        //rb.velocity = Vector2.up * jumpHeight;
        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        isGrounded = false;
        anim.SetBool("Jump", true);
        jumpTimer = 0;
    }

    void MoveChar(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * charSpeed);

        anim.SetFloat("magnitude", Mathf.Abs(rb.velocity.x)); 

        if((horizontal > 0 && !facingRight)||(horizontal < 0 && facingRight))
        {
            flip = true;
        }
        
        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    void updatedPhysics()
    {
        if (isGrounded)
        {
            if (Mathf.Abs(direction.x) < 0.4f)
            {
                rb.drag = linerDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0f;
        }
        else if(!isGrounded)
        {
            rb.gravityScale = 1f;
            rb.drag = linerDrag * 0.15f;
            if(rb.velocity.y<0)
            {
                rb.gravityScale = gravity * fallMultiplyer;
            }
            else if(rb.velocity.y>0 && !Input.GetKey(KeyCode.W))
            {
                rb.gravityScale = gravity * (fallMultiplyer / 2);
            }
        }
    }

    void Flip()
    {
        
        if (facingRight == true)
        {
            Quaternion newRoatation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRoatation, 10f * Time.deltaTime);

            float angle = Quaternion.Angle(transform.rotation, newRoatation);
            if (angle == 0)
            {
                
                transform.rotation = newRoatation;
                facingRight = false;
                flip = false;
            }
        }
        else if (facingRight == false)
        {
            Quaternion newRoatation = Quaternion.identity;
            newRoatation = Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRoatation, 10f * Time.deltaTime);
            float angle = Quaternion.Angle(transform.rotation, newRoatation);
            if (angle == 0)
            {
                transform.rotation = newRoatation;
                facingRight = true;
                flip = false;
            }

        }
    }

    void Death()
    {
        rb.simulated = false;
        death = false;
        canMove = false;
        CinamacineShake.Instance.ShakeCamara(1f, 1f);
        StartCoroutine(DeathReload(2f));
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "E_Peach")
        {
            rb.velocity = Vector2.up * jumpHeight / 1.5f;
            col.gameObject.GetComponent<EnemyPeach>().death = true;
            CinamacineShake.Instance.ShakeCamara(1f, 0.05f);
            
        }

        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("Jump", false);
            CreateDust();
            audioManager.Play("LandingSound");
            CinamacineShake.Instance.ShakeCamara(0.5f, 0.2f);
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("NoKillEnemy"))
        {
            death = true;
            anim.SetTrigger("Death");
            audioManager.Play("PlayerDeath");
        }

        if (col.CompareTag("MoveableObject"))
        {
            this.transform.parent = col.transform;
            isGrounded = true;
            anim.SetBool("Jump", false);
            CreateDust();
            audioManager.Play("LandingSound");
            CinamacineShake.Instance.ShakeCamara(0.5f, 0.2f);

        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = false;
            anim.SetBool("Jump", true);
        }

        if(col.CompareTag("MoveableObject"))
        {
            this.transform.parent = null;
            isGrounded = false;
            anim.SetBool("Jump", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("NoKillEnemy"))
        {
            anim.SetTrigger("Death");
            death = true;
            audioManager.Play("PlayerDeath");
        }

    }

    void CreateDust()
    {
        Dust.Play();
    }
    IEnumerator SlowPlayer(float seconds)
    {
        
        charSpeed = 2f;
        Debug.Log("Char slowed");
        yield return new WaitForSeconds(seconds);

        charSpeed = 10f;
        bubbleHit = false;
    }

    IEnumerator DeathReload(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gm.ReloadScene();
    }

    IEnumerator BulletReload(float seconds)
    {
        canShoot = false;
        yield return new WaitForSeconds(seconds);
        canShoot = true;
    }
}
