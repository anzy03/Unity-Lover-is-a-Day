using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITween : MonoBehaviour
{
    public float delayTime;
    // Start is called before the first frame update
    public void PanelOpen()
    {
        LeanTween.scale(gameObject, new Vector2(1, 1), delayTime);
    }

    public void PanelClose()
    {
        LeanTween.scale(gameObject, new Vector2(1, 1), delayTime);
    }
}
