using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Смоук-тесты для Fabulous Fishing (EditMode).
/// Проверяют, что после адаптации под Unity 6000.6.0f1 код собирается,
/// а ключевые механики (счёт, крючок, переходы между сценами) работают штатно.
/// </summary>
public class FabulousSmokeTests
{
    [TearDown]
    public void TearDown()
    {
        Score.score = 0;
        Score.FishBaitScore = 0;
    }

    [Test]
    public void CoreTypes_AreLoaded()
    {
        Assert.That(typeof(Score), Is.Not.Null);
        Assert.That(typeof(Hook), Is.Not.Null);
        Assert.That(typeof(ManagerScene), Is.Not.Null);
        Assert.That(typeof(SceneFader), Is.Not.Null);
        Assert.That(typeof(SimpleTimer), Is.Not.Null);
    }

    [Test]
    public void BuildSettings_HaveScenes()
    {
        Assert.That(SceneManager.sceneCountInBuildSettings, Is.GreaterThan(0),
            "В Build Settings должна быть хотя бы одна сцена");
    }

    [Test]
    public void SceneFader_IsValidSceneIndex_ChecksBounds()
    {
        int count = SceneManager.sceneCountInBuildSettings;
        Assert.That(SceneFader.IsValidSceneIndex(-1), Is.False, "Индекс -1 невалиден");
        Assert.That(SceneFader.IsValidSceneIndex(0), Is.True, "Индекс 0 валиден");
        Assert.That(SceneFader.IsValidSceneIndex(count - 1), Is.True, "Последняя сцена валидна");
        Assert.That(SceneFader.IsValidSceneIndex(count), Is.False, "Индекс за границей невалиден");
    }

    [Test]
    public void Score_BaitScore_AccumulatesAndFiresEvent()
    {
        Score.FishBaitScore = 0;
        bool fired = false;
        Score.CallButton handler = () => fired = true;
        Score.ManagerButton += handler;

        var go = new GameObject("score_test");
        var score = go.AddComponent<Score>();

        score.BaitScore(5);

        Assert.That(Score.FishBaitScore, Is.EqualTo(5), "Очки приманки накапливаются");
        Assert.That(fired, Is.True, "Событие ManagerButton срабатывает при очках >= 1");

        Score.ManagerButton -= handler;
        score.ReloadScore();
        Object.DestroyImmediate(go);
    }

    [Test]
    public void Score_ScoreAdd_WithoutTextComponent_DoesNothing()
    {
        Score.score = 0;
        var go = new GameObject("score_test2");
        var score = go.AddComponent<Score>(); // Text нет -> Start не вызывался, scoreText == null

        score.ScoreAdd(10);

        Assert.That(Score.score, Is.EqualTo(0), "Без Text-компонента счёт не меняется и падения нет");

        score.ReloadScore();
        Object.DestroyImmediate(go);
    }

    [Test]
    public void Hook_HasSaneSpeedDefaults()
    {
        var go = new GameObject("hook_test");
        var hook = go.AddComponent<Hook>();

        Assert.That(hook.hookSpeed, Is.EqualTo(0.1f).Within(0.001f));
        Assert.That(hook.returnSpeed, Is.EqualTo(0.5f).Within(0.001f));

        Object.DestroyImmediate(go);
    }
}
