using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingSound : MonoBehaviour
{
   public AudioSource EatSound;
   public AudioClip EatFX;
   public AudioClip BigEatFX;

   void OnTriggerEnter2D(Collider2D collision )
   {
    switch(collision.gameObject.tag)
    {
     case "Fish":
       EatSound.PlayOneShot(EatFX);
     break;
     case "UncaughtFish":
       EatSound.PlayOneShot(BigEatFX);
     break;
     case "FishOnHook":
     if(gameObject.tag != "Smallpike")
       EatSound.PlayOneShot(EatFX);
     break;
    }
   }
}
