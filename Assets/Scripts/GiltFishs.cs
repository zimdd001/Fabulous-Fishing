using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiltFishs : MonoBehaviour
{
public Material GoldMaterial;
private bool GiltFish = false;
private int GiltScore;
private InteractFish InterFish;
   void OnParticleCollision(GameObject other)
   {
    if(gameObject.tag != "Pike" && gameObject.tag !="SmallPike" && !GiltFish)
              {
                InterFish = this.gameObject.GetComponent<InteractFish>();
                InterFish.fishScoreAdd +=5;
                InterFish.Gildedfish = true;
                print("Gold");
                GiltFish = true;
                gameObject.GetComponent<SpriteRenderer>().material = GoldMaterial;
              } 
   }

}
