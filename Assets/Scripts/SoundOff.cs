using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOff : MonoBehaviour
{
    private AudioSource Stopplay;
    
    void Start()=> ManagerScene.StopGame += StopSound;

    public void  StopSound()
     {
        if(Stopplay !=null)
        {
         Stopplay = GetComponent<AudioSource>();
         {
          Stopplay.Stop();
          ManagerScene.StopGame -= StopSound;
         }
        }
      
     } 
}
