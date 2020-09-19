using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    CheckPointManger gm;

    bool checkPointHit;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<CheckPointManger>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Checkpoint");
            gm.lastCheckpointpos = transform.position;

            GetComponent<Animator>().SetTrigger("CheckPointHit");
        }
    }
}
