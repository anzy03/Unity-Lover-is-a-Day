using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPower : MonoBehaviour
{
    public bool isShooting;
    private bool shot;
    public float deathTime = 5f;
   


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Attacked");
        isShooting = true;
    }
    private void FixedUpdate()
    {
        
        if (isShooting)
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * 10f;
            gameObject.transform.parent = null;
            shot = true;
        }

        if(shot)
        {
            StartCoroutine(BulletWait(deathTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "E_Peach")
        {
            collision.gameObject.GetComponent<EnemyPeach>().death = true;
            CinamacineShake.Instance.ShakeCamara(1f, 0.05f);
            Destroy(gameObject); 
            return;
        }
        else if (collision.gameObject.tag == "E_crow")
        {
            CinamacineShake.Instance.ShakeCamara(1f, 0.05f);
            collision.gameObject.GetComponent<EnemyCrow>().death = true;
            Destroy(gameObject);
            return;
        }
        else
        {
            DestroyThis();
        }

        Destroy(gameObject);
    }
    private void DestroyThis()
    {
        Destroy(gameObject,0.2f);
        CinamacineShake.Instance.ShakeCamara(1f, 0.05f);
    }

    IEnumerator BulletWait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DestroyThis();
    }
}
