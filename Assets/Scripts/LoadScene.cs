using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 0;
    [SerializeField] private float delay = 20f;

    void Start()
    {
        Invoke(nameof(LoadSceneDelayed), delay);
    }

    private void LoadSceneDelayed()
    {
        if (!SceneFader.IsValidSceneIndex(sceneIndex))
        {
            Debug.LogError($"Invalid scene index: {sceneIndex}");
            return;
        }

        // Получаем имя сцены для совместимости с текущим SceneFader
        string sceneName = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
        sceneName = System.IO.Path.GetFileNameWithoutExtension(sceneName);

        SceneFader.instance.FadeToScene(sceneName);
    }
}