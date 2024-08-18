using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Playing, Paused, Won, Lost}
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }
    [SerializeField] int CakeHealthMax;
    int cakeHealth;
    private void Awake()
    {
        if (instance != null && instance != this) instance = null;
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cakeHealth = CakeHealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseCakeHealth(int dmg) 
    {
        cakeHealth -= dmg;
        if (cakeHealth <= 0) Debug.Log("the cake is a lie");
    }
}
