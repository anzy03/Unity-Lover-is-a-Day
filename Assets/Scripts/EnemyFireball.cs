using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    public float magnitude;
    public bool movingUp;
    Rigidbody2D rb;

    
    void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * magnitude, ForceMode2D.Impulse);
        
        movingUp = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb.velocity.magnitude <= 1f)
        {
            movingUp = false;
        }
        if (movingUp == false)
        {
            Quaternion newRoatation = Quaternion.Euler(180f, 0f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRoatation, 8f * Time.deltaTime);
        }
        else if (movingUp == true)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var i = collision.gameObject.GetComponent<Player>();
        if (i != null)
        {
            i.death = true;
        }
    }
}
