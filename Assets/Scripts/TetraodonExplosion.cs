using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetraodonExplosion : MonoBehaviour
{
   Animator TetraodonAnim;
  
   void Start()
   {
    TetraodonAnim = GetComponent<Animator>();
   }
   void OnTriggerEnter2D(Collider2D collision )
   {
     if(GameObject.Find("bait") == null && collision.gameObject.tag =="Player" )
     {
      TetraodonAnim.SetBool("Explosion",true);
      Invoke("Destrucrion",1.21f);
      Invoke("ActiveCollider",0.6f);
     }
   }
   private void Destrucrion()
   {
    Destroy(gameObject);
   }
   
   private void ActiveCollider()
   {
      transform.GetChild(0).gameObject.SetActive(true);
      if(GameObject.FindGameObjectWithTag("FishOnHook")==null)
      {
       gameObject.GetComponent<InteractFish>().ActivatedHook();
      }
   }
   
}
