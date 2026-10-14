using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Configuração")]
    public int maxLives = 3;
    public string menuScene = "Menu";
    public string endScene = "Fim";
    public string[] levels = { "Fase1", "Fase2" };   

    public int Score { get; private set; }
    public int Lives { get; private set; }
    public int Collected { get; private set; }
    public int TotalItems { get; private set; }
    public bool Won { get; private set; }

    int levelIndex;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap()
    {
        if (Instance == null)
            new GameObject("GameManager").AddComponent<GameManager>();
    }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Lives = maxLives;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int idx = Array.IndexOf(levels, scene.name);
        if (idx < 0) return;                 // Menu / Fim: nada a contar
        levelIndex = idx;
        Collected = 0;
        TotalItems = 0;
        foreach (var c in FindObjectsByType<Collectible>(FindObjectsSortMode.None))
            if (c.countsForGoal) TotalItems++;
    }

    public void NewGame()
    {
        Score = 0;
        Lives = maxLives;
        Won = false;
        SceneManager.LoadScene(levels[0]);
    }

    public void AddScore(int points) => Score += points;

    public void AddLife() => Lives++;

    public void ItemCollected()
    {
        Collected++;
        if (Collected >= TotalItems) LevelComplete();
    }

    public void LoseLife()
    {
        Lives--;
        if (Lives <= 0) EndGame(false);
    }

    void LevelComplete()
    {
        if (levelIndex + 1 < levels.Length) SceneManager.LoadScene(levels[levelIndex + 1]);
        else EndGame(true);
    }

    void EndGame(bool won)
    {
        Won = won;
        SceneManager.LoadScene(endScene);
    }

    public void GoToMenu() => SceneManager.LoadScene(menuScene);
}
