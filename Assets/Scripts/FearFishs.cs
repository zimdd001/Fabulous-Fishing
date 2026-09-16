using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearFishs : MonoBehaviour
{
   private bool OnFear = false;
   void OnParticleCollision(GameObject other)
   {
    if(gameObject.tag != "DredFish" && gameObject.tag !="Pike" && !OnFear)
      {
        float SpeedX = gameObject.GetComponent<fishmove>().fishSpeed;
        gameObject.GetComponent<fishmove>().fishSpeed = Mathf.Sign(SpeedX) * -14f;
        gameObject.GetComponent<Transform>().Rotate(0f,180f,0f);
        OnFear = true;
      }
  }
}
