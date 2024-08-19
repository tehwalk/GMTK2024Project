using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState { Playing, Paused, Won, Lost }
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    GameState state;
    [Header("General Properties")]
    [SerializeField] int CakeHealthMax;
    [SerializeField] float TimerMax;
    [SerializeField] int MaxHealth;
    [Header("UI Properties")]
    [SerializeField] TextMeshProUGUI timerText;
    int cakeHealth;
    int lvl = 1;
    float timer = 0;
    private void Awake()
    {
        if (instance != null && instance != this) instance = null;
        instance = this;
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        cakeHealth = CakeHealthMax;
        timer = TimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        //ReduceTime();
        switch (state)
        {
            case GameState.Playing:
                Time.timeScale = 1;
                if (Input.GetKeyDown(KeyCode.Escape)) state = GameState.Paused;
                ReduceTime();
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                if (Input.GetKeyDown(KeyCode.Escape)) state = GameState.Playing;
                break;
            case GameState.Won:
                Time.timeScale = 0;
                break;
            case GameState.Lost:
                Time.timeScale = 0;
                break;
        }
    }

    void ReduceTime()
    {
        timer -= Time.deltaTime;
        timerText.text = timer.ToString();
        if (timer <= 0)
        {
            state = GameState.Lost;
        }
    }

    public void LoseCakeHealth(int dmg)
    {
        cakeHealth -= dmg;
        if (cakeHealth <= 0) Debug.Log("the cake is a lie");
    }
}
