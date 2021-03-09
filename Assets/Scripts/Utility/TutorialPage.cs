using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPage: MonoBehaviour
{
    public GameObject tutorialPage;
    
    void Start()
    {
        
    }
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.T))
        {
            ShowTutorial();
        }
    }

    public void ShowTutorial()
    {
        tutorialPage.SetActive(!tutorialPage.activeSelf);
    }
}
