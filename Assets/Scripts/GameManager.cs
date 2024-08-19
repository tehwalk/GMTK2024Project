using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    [SerializeField] Slider cakeHealthSlider;
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
        cakeHealthSlider.minValue = 0;
        cakeHealthSlider.maxValue = CakeHealthMax;
        cakeHealthSlider.value = cakeHealth;
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
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (timer <= 0)
        {
            state = GameState.Lost;
        }
    }

    public void LoseCakeHealth(int dmg)
    {
        cakeHealth -= dmg;
        cakeHealthSlider.value = cakeHealth;
        if (cakeHealth <= 0) Debug.Log("the cake is a lie");
    }
}
