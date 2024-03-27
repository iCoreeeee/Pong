using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseApplication()
    {
        print("Exit");
        Application.Quit();
    }
    
    void Start()
    {
        
    }
}
