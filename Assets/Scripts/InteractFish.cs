using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractFish : MonoBehaviour
{
   private bool OnHook = false;
   public int fishScoreAdd = 1;
   Score scoreboard;
   ShowAddScore ShowAdd;
   public GameObject fish;
   public GameObject fishbone;
   public bool FishAdd = false;
   [SerializeField] GameObject ToBucket;
   [SerializeField] GameObject ShowAddScore;
   DisabledBait bait;
   Hook Dhook;
   Hook Shook;
   public bool Gildedfish;
   public bool MysteryFish;
   [SerializeField] GameObject[] MysteryBucket;
   
  
   void Start()
   {
    scoreboard = FindObjectOfType<Score>(); 
    ShowAdd = FindObjectOfType<ShowAddScore>();
   }

   private void DestroyFish()
   {
    Destroy(gameObject);
   }
   private void DestroyOverTime()
   {
    if(gameObject.tag =="FishOnHook")
    {
      Destroy(gameObject);
      ActivatedHook();
      OnHook = false;
      CreateBone(fish,fishbone);
    }
   }

   void OnTriggerEnter2D(Collider2D collision )
    {
        switch(collision.gameObject.tag)
        {
            case "Wall":
                DestroyFish();
            break;

            case "Player":
            if(gameObject.tag !="Pike")
            {
             if(GameObject.Find("bait")!=null)
               {
                bait = GameObject.Find("bait").GetComponent<DisabledBait>();
                bait.DisabledOverTime();
                gameObject.tag = "Fish";
                Debug.Log("наживка сработала");
                Shook = GameObject.Find("SingleHook").GetComponent<Hook>();
                Shook.hookSpeed = 8;
                Shook.returnSpeed = 7;
               }
              OnHook = true;
              Invoke("DestroyOverTime",2.5f);
            }
            break;

            case "Pike":
            if(gameObject.tag =="FishOnHook")
            {
              Invoke("AteOFHook",0.1f);
            }
            if(gameObject.tag =="Fish" || gameObject.tag =="UncaughtFish")
            {
             Hunt();
            }
              break;

            case "Smallpike":
            if(gameObject.tag =="FishOnHook")
            {
            Invoke("AteOFHook",0.1f);
            }
            if(gameObject.tag =="Fish")
            {
             Hunt();
            }
            if(GameObject.Find("bait")!=null && gameObject.tag == "Smallpike" && collision.gameObject.tag != "Smallpike")
               {
                bait = GameObject.Find("bait").GetComponent<DisabledBait>();
                bait.DisabledOverTime();
                gameObject.tag = "Fish";
                print("наживка сработала");
               }
              OnHook = true;
              Invoke("DestroyOverTime",2.5f);
              break;
        }
    }

  public void Catching()
   { 
    if(gameObject!=null)
    {
       if(MysteryFish)
    {
      MysteryScore();
    }
     scoreboard.ScoreAdd(fishScoreAdd);
     ShowAdd.ShowScore(fishScoreAdd);
     ShowAdd.EnableShow();
     FishAdd = true; 
     Instantiate(ToBucket);
     DestroyFish(); 
    }
   }
   

   private void Hunt()
   {
    CreateBone(fish,fishbone);
    DestroyFish();
   }

   private void AteOFHook()
   {
     CreateBone(fish,fishbone);
     DestroyFish();
     if(GameObject.FindGameObjectWithTag("FishOnHook")==null)
     {
      ActivatedHook();
     }
    
   }

    public void CreateBone(GameObject fish, GameObject bone)
    {
    var child =  Instantiate(bone,fish.transform.position,Quaternion.identity);
    }
   public void ActivatedHook()
   {
    if(GameObject.Find("DoubleHook")!=null)
    {
    Dhook = GameObject.Find("DoubleHook").GetComponent<Hook>();
    Dhook.HookActivate();
    }
    if(GameObject.Find("SingleHook")!=null)
    {
    Shook = GameObject.Find("SingleHook").GetComponent<Hook>();
    Shook.HookActivate();
    }
   }
   private void MysteryScore()
   {
    
     fishScoreAdd = gameObject.GetComponent<MysteryFish>().MysteryScoreAdd;
     if(fishScoreAdd>=1 && fishScoreAdd<=3)
     {
    //  int commMystery = Random.Range(0,1);
      ToBucket = MysteryBucket[0];
     }
    if(fishScoreAdd>=4 && fishScoreAdd<=6)
     {
    //  int rareMystery = Random.Range(2,3);
      ToBucket = MysteryBucket[1];
     }
    if(fishScoreAdd>=7 && fishScoreAdd<=10)
     {
    //  int epicMystery = Random.Range(4,5);
      ToBucket = MysteryBucket[2]; 
     }
   }
   
}
    