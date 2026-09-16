using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
 public static bool  GamePaused;
 public  GameObject PauseMenu;
 private AudioSource PauseSound;
 public  AudioSource CallPauseSound;
 public AudioClip Paused;
 public AudioClip UnPaused;
//  }
 private void Start()
 {
   PauseSound = GameObject.FindGameObjectWithTag("ThemePlayer").GetComponent<AudioSource>();
 }
 private void Update()
 {
   if(Input.GetKeyDown(KeyCode.Escape))
   {
      if(GamePaused)
      {
         Continue();
      }
      else
      {
         PauseGame();
      }
   }
 }
 public void CallPause()
 {
   if(GamePaused)
      {
         Continue();
      }
      else
      {
         PauseGame();
      }
 }
 public void  Continue()
 {
  CallPauseSound.PlayOneShot(UnPaused);
  PauseMenu.SetActive(false);
  Time.timeScale = 1f;
  GamePaused = false;
  PauseSound.Play();
 }
 public void PauseGame()
 {
  PauseMenu.SetActive(true);
  Time.timeScale = 0f;
  GamePaused = true;
  PauseSound.Stop();
  CallPauseSound.PlayOneShot(Paused);
 }
    public void LoadMeinMenu()
    {
        Time.timeScale = 1f;
        SceneFader.instance.FadeToScene("MainMenu"); // »ÎË SceneManager.GetSceneByBuildIndex(0).name
    }
    public void ReloadScene()
    {
        int currentSceneindex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        SceneFader.instance.FadeToScene(SceneManager.GetSceneByBuildIndex(currentSceneindex).name);
        Continue();
    }
}
