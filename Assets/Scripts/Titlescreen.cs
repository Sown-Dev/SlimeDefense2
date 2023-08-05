using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titlescreen : MonoBehaviour
{

    public void playGame(){
        SceneManager.LoadScene(Scenum.CharSelect);
    }

    public void settings(){
        
    }

    public void quit(){
        Application.Quit();
    }
}