using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPeach : MonoBehaviour
{
    public bool movingRight;
    public float moveSpeed;
    public float timer = 3.0f;
    private float m_timer;
    public bool death;
    // Start is called before the first frame update
    void Start()
    {

        death = false;
        m_timer = timer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!death)
        {
            if (movingRight)
            {
                //transform.rotation = Quaternion.Euler(0, Mathf.Lerp(transform.eulerAngles.y, 180, 8f * Time.deltaTime), 0);
                Quaternion newRoatation = Quaternion.Euler(0f, 180f, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRoatation, 8f * Time.deltaTime);
            }
            else
            {
                //transform.rotation = Quaternion.Euler(0, Mathf.Lerp(transform.eulerAngles.y,0,8f * Time.deltaTime), 0);
                Quaternion newRoatation = Quaternion.Euler(0f, 0f, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRoatation, 8f * Time.deltaTime);
            }
        }
        m_timer -= Time.deltaTime;
        if (m_timer <= 0.0f)
        {
            m_timer += timer;
            movingRight = !movingRight;
        }

        if(death == true)
        {
            Death();
        }
        else
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

    }

    void Death()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Death");
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        Destroy(gameObject,2f);
    }
    void DeathSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Walls")
        {
            movingRight = !movingRight;
        }
    }
}
