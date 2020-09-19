using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawn : MonoBehaviour
{
    public GameObject Bubble;
    public bool canSpawn;
    public float waitSeconds;
    Vector2 spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
            if(canSpawn)       
                StartCoroutine(BubbleSpawnTime(waitSeconds));
        
        
    }
    IEnumerator BubbleSpawnTime(float seconds)
    {
        canSpawn = false;
        yield return new WaitForSeconds(seconds);
        Instantiate(Bubble,spawnLocation,Quaternion.identity);
        canSpawn = true;

    }
    
}
