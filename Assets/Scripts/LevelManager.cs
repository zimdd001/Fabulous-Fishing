using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int levelUnLock = 1;
    public Button[] buttons;

    void Start()
    {
        levelUnLock = PlayerPrefs.GetInt("level", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < levelUnLock && i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void loadLevel(int levelIndex)
    {
        // Проверка валидности индекса
        if (levelIndex < 0 || levelIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError($"LevelManager: Invalid levelIndex {levelIndex}. Scene count: {SceneManager.sceneCountInBuildSettings}");
            return;
        }

        // Получение имени сцены
        string scenePath = SceneUtility.GetScenePathByBuildIndex(levelIndex);
        if (string.IsNullOrEmpty(scenePath))
        {
            Debug.LogError($"LevelManager: No scene found at index {levelIndex}");
            return;
        }

        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError($"LevelManager: Scene name is empty for index {levelIndex}");
            return;
        }

        Debug.Log($"LevelManager: Loading scene '{sceneName}' (index: {levelIndex})");
        SceneFader.instance.FadeToScene(sceneName);
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        int currLevel = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("level", currLevel + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}