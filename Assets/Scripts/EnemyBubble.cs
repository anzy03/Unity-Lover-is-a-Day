using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBubble : MonoBehaviour
{
    public bool destory;
    public float DestroyWait;
    // Start is called before the first frame update
    void Start()
    {
        destory = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (destory)
        {
            StartCoroutine(BubbleDestory(DestroyWait));
        }
    }
    IEnumerator BubbleDestory(float seconds)
    {
        destory = false;
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var i = collision.gameObject.GetComponent<Player>();
            if (i != null)
            {
                i.bubbleHit = true;
            }
        }
        GetComponent<Animator>().SetTrigger("Destroy");
    }
    private void Destroy()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Destroy(gameObject);
    }
}
