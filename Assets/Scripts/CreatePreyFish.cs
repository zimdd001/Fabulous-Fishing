using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePreyFish : MonoBehaviour
{
    FishGenerator FG;
     void OnTriggerEnter2D(Collider2D collision )
     {
        if(collision.gameObject.tag =="Wall")
        {
         FG = GameObject.Find("FishGenerator").GetComponent<FishGenerator>();
         FG.FishWave();
        }
     }
}
