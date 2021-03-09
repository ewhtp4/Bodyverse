using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    //public GameObject[] Infectedcells;

    bool paused = false;

    public GameObject LoseScreen;
    public Slider ImmunityHealth;
    public int immunity = 28;
    //public GameObject LoseScreen;

    public void LoadLevel()
    {
        SceneManager.LoadScene(2);
        
    }


    public void OpenCredits(GameObject credits)
    {
        credits.SetActive(!credits.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        LoseScreen.SetActive(false);
        SceneManager.LoadScene(2);
    }

    public void Pause()
    {
        if(paused)
        {
            Time.timeScale = 1;
            paused = false;
        }
        else
        {
            Time.timeScale = 0;
            paused = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Menu();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        ImmunityHealth.value = immunity;

        
        GameObject Infectedcells = GameObject.FindGameObjectWithTag("Infectible");
        //Debug.Log(Infectedcells);
        //ImmunityHealth.value = 

        if(immunity <1 && LoseScreen != null)
        {
            PlayerLose();
        }

    }

    public void PlayerLose()
    {
        Pause();
        LoseScreen.SetActive(true);
        //DisableLoseScreen();
    }

    public void LowerImmunity()
    {
        immunity--;
    }
    //void DisableLoseScreen()
    //{

    //}
}
