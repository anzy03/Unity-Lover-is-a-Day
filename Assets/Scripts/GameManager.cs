using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject Pause;
    
    AudioManager audioManager;

    private void Start()
    { 
      
        audioManager = FindObjectOfType<AudioManager>();   
    }

    public void ReloadScene()
    {
        audioManager.Play("ReloadCheckpoint");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        Pause.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause.SetActive(true);
        }
    }


}
