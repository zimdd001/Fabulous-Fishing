using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

/// <summary>
/// PlayMode-тесты: проверяют реальное поведение скриптов во время игры
/// (тикадр, таймер с событиями, загрузка сцены из Build Settings).
/// </summary>
public class FabulousPlayModeTests
{
    [UnityTest]
    public IEnumerator Frames_Advance_AndTimeMoves()
    {
        int startFrame = Time.frameCount;
        yield return new WaitForSeconds(0.2f);
        Assert.That(Time.frameCount, Is.GreaterThan(startFrame), "Кадры должны идти");
    }

    [UnityTest]
    public IEnumerator SimpleTimer_CountsDown_AndFiresOnTimerEnd()
    {
        var go = new GameObject("timer_test");
        var image = go.AddComponent<Image>();
        var timer = go.AddComponent<SimpleTimer>();

        // private [SerializeField] поля — заполняем через reflection,
        // пока не вызвался Start (он пройдёт на следующем кадре)
        const BindingFlags NP = BindingFlags.NonPublic | BindingFlags.Instance;
        typeof(SimpleTimer).GetField("time", NP).SetValue(timer, 0.2f);
        typeof(SimpleTimer).GetField("timerImage", NP).SetValue(timer, image);

        bool fired = false;
        Action handler = () => fired = true;
        SimpleTimer.OnTimerEnd += handler;

        float timeout = Time.unscaledTime + 5f;
        while (!fired && Time.unscaledTime < timeout)
            yield return null;

        SimpleTimer.OnTimerEnd -= handler;
        Assert.That(fired, Is.True, "OnTimerEnd должен сработать, когда время вышло");
        Assert.That(image.fillAmount, Is.EqualTo(0f).Within(0.01f), "Индикатор времени должен опустеть");
        UnityEngine.Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator MainMenuScene_LoadsFromBuildSettings()
    {
        var op = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        Assert.That(op, Is.Not.Null, "Сцена MainMenu должна быть в Build Settings");

        while (!op.isDone)
            yield return null;
        yield return null; // кадр, чтобы отработали Start'ы

        Assert.That(SceneManager.GetActiveScene().name, Is.EqualTo("MainMenu"));
    }
}
