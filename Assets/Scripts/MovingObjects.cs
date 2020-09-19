using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    public float time;
    public Vector2 startPos, moveLocation;

    // Start is called before the first frame update
    void Start()
    {
        MoveTo();
    }

    void MoveTo()
    {
        LeanTween.move(gameObject, moveLocation, time).setOnComplete(MoveBack);
    }

    void MoveBack()
    {
        LeanTween.move(gameObject, startPos, time).setOnComplete(MoveTo);
    }
}
