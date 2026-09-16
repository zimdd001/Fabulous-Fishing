using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed = 5.5f;
    [SerializeField] private Canvas canvas;

    public static SceneFader instance;

    // Новый метод для проверки сцен
    public static bool IsValidSceneIndex(int index)
    {
        return index >= 0 && index < SceneManager.sceneCountInBuildSettings;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    private void Initialize()
    {
        if (fadeImage == null)
        {
            Debug.LogError("FadeImage is not assigned!");
            enabled = false;
            return;
        }

        canvas ??= GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas not found!");
            enabled = false;
            return;
        }

        canvas.sortingOrder = 100;
        fadeImage.color = Color.clear;
    }

    public void FadeToScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is empty!");
            return;
        }

        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning("SceneFader is inactive!");
            return;
        }

        StartCoroutine(FadeRoutine(sceneName));
    }

    private IEnumerator FadeRoutine(string sceneName)
    {
        // Fade Out
        yield return StartCoroutine(Fade(0f, 1f));

        // Load Scene
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
            yield return null;

        // Fade In
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float startAlpha, float targetAlpha)
    {
        float alpha = startAlpha;
        fadeImage.color = new Color(0, 0, 0, alpha);

        while (Mathf.Abs(alpha - targetAlpha) > 0.01f)
        {
            alpha = Mathf.MoveTowards(alpha, targetAlpha,
                   Time.unscaledDeltaTime * fadeSpeed);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
}