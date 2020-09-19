using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManger : MonoBehaviour
{
    private static CheckPointManger instance;

    AudioManager audioManager;
    public Vector2 lastCheckpointpos;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
