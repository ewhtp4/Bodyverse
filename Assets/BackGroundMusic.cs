using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    // Start is called before the first frame update
    void Awake()
    {
        BackgroundMusic = GetComponent<AudioSource>();
        BackgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
