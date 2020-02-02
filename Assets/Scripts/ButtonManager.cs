using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Play(string sceneName)
    { // Player Clicked the Play button
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);    
    }

    public void Quit()
    { // Player clicked the Quit Button 
        Application.Quit();
    }
}
