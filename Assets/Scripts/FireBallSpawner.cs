using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpawner : MonoBehaviour
{
    public GameObject fireBall;
    public bool canSpawn;
    public float waitSeconds;
    
   
    // Update is called once per frame
    void Update()
    {
        if(fireBall.transform.position.y <= gameObject.transform.position.y)
        {
            fireBall.SetActive(false);
            canSpawn = true;
            fireBall.transform.position = new Vector2(fireBall.transform.position.x,gameObject.transform.position.y);
        }

        if (canSpawn)
        {
            StartCoroutine(FireBallSpawn(waitSeconds));
            
            canSpawn = false;
        }
    }

    IEnumerator FireBallSpawn(float seconds)
    {
     
        yield return new WaitForSeconds(seconds);

        fireBall.SetActive(true);
        
    }
}

