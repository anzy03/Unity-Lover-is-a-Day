using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrow : MonoBehaviour
{
    public bool movingRight;
    public float moveSpeed;
    public float timer = 3.0f;
    private float m_timer;
    public bool death = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<Collider2D>().isTrigger = false;
        m_timer = timer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (movingRight)
        {
            //transform.rotation = Quaternion.Euler(0f, Mathf.Lerp(transform.eulerAngles.y, 180, 8f * Time.deltaTime), 0f);
            Quaternion newRoatation = Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRoatation, 8f * Time.deltaTime);
        }
        else
        {
            //transform.rotation = Quaternion.Euler(0f, Mathf.Lerp(transform.eulerAngles.y, 0, 8f * Time.deltaTime), 0f);
            Quaternion newRoatation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRoatation, 8f * Time.deltaTime);
        }

        m_timer -= Time.deltaTime;
        if (m_timer <= 0.0f)
        {
            m_timer += timer;
            movingRight = !movingRight;
        }

        if (death == true)
            Death();
        else
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        

    }

    void DeathSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    void Death()
    {
        GetComponent<Animator>().SetTrigger("Death");
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        GetComponent<Collider2D>().isTrigger = true;
        
        Destroy(gameObject, 2f);
    }
}
