using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerScene : MonoBehaviour
{
    private int CurrentScore;
    [SerializeField] private int ClaimScore;
    private Score Cs;

    public GameObject LoseScreen;
    public GameObject WinScreen;
    private bool LWScreen = false;

    public delegate void StopIt();
    public static event StopIt StopGame;

    public Button[] buttons;

    void Start()
    {
        // Подписываемся на событие от SimpleTimer
        SimpleTimer.OnTimerEnd += Fail;

        // Получаем компонент Score
        if (GameObject.Find("Score") != null)
        {
            Cs = GameObject.Find("Score").GetComponent<Score>();
        }
    }

    void FixedUpdate()
    {
        if (GameObject.Find("Score") != null)
        {
            Cs = GameObject.Find("Score").GetComponent<Score>();
            CurrentScore = Score.score;
        }

        if (CurrentScore >= ClaimScore)
        {
            Invoke("Victory", 0.5f);
        }
    }

    public void Fail()
    {
        if (!LWScreen)
        {
            StopGame?.Invoke();
            Debug.Log("Lose");
            Instantiate(LoseScreen, transform.position, Quaternion.identity);
            LWScreen = true;
        }
    }

    private void Victory()
    {
        if (!LWScreen)
        {
            StopGame?.Invoke();
            Debug.Log("Win");
            Instantiate(WinScreen, transform.position, Quaternion.identity);
            UnLockLevel();
            LWScreen = true;
        }
    }

    public void UnLockLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel >= PlayerPrefs.GetInt("level"))
        {
            PlayerPrefs.SetInt("level", currentLevel + 1);
        }
    }

    public void LoadMenu()
    {
        string sceneName = SceneUtility.GetScenePathByBuildIndex(0);
        if (!string.IsNullOrEmpty(sceneName))
        {
            sceneName = System.IO.Path.GetFileNameWithoutExtension(sceneName);
            Debug.Log($"ManagerScene: Loading menu scene '{sceneName}'");
            SceneFader.instance.FadeToScene(sceneName);
        }
        else
        {
            Debug.LogError("ManagerScene: MainMenu scene not found in Build Settings");
        }
    }

    public void LoadNextLevel()
    {
        int currentLevelindex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelindex + 1;

        if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0; // Возвращаемся к MainMenu
        }

        string scenePath = SceneUtility.GetScenePathByBuildIndex(nextLevelIndex);
        if (!string.IsNullOrEmpty(scenePath))
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (!string.IsNullOrEmpty(sceneName))
            {
                Debug.Log($"ManagerScene: Loading next scene '{sceneName}' (index: {nextLevelIndex})");
                SceneFader.instance.FadeToScene(sceneName);
            }
            else
            {
                Debug.LogError($"ManagerScene: Scene name is empty for index {nextLevelIndex}");
            }
        }
        else
        {
            Debug.LogError($"ManagerScene: No scene found at index {nextLevelIndex}. Scene count: {SceneManager.sceneCountInBuildSettings}");
        }
    }

    public void ReloadLevel()
    {
        int currentSceneindex = SceneManager.GetActiveScene().buildIndex;
        string scenePath = SceneUtility.GetScenePathByBuildIndex(currentSceneindex);

        if (!string.IsNullOrEmpty(scenePath))
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (!string.IsNullOrEmpty(sceneName))
            {
                Debug.Log($"ManagerScene: Reloading scene '{sceneName}' (index: {currentSceneindex})");
                SceneFader.instance.FadeToScene(sceneName);
            }
            else
            {
                Debug.LogError($"ManagerScene: Scene name is empty for index {currentSceneindex}");
            }
        }
        else
        {
            Debug.LogError($"ManagerScene: No scene found at index {currentSceneindex}");
        }
    }

    private void OnDestroy()
    {
        // Отписываемся от события, чтобы избежать ошибок при перезагрузке сцены
        SimpleTimer.OnTimerEnd -= Fail;
    }
}