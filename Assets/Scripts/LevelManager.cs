using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance {  get { return instance; } }
    ContraptionManager contraptionManager;
    [Header("General Properties")]
    [SerializeField] int expRequiredInit;
    int lvl = 1;
    int expGained = 0;
    int expRequired;
    [Header("General Properties")]
    [SerializeField] TextMeshProUGUI lvlText;
    [SerializeField] Slider lvlSlider;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null && instance != this) instance = null;
        instance = this;
    }
    void Start()
    {
        contraptionManager = ContraptionManager.Instance;
        expRequired = expRequiredInit;
        InitializeLevelReq();
    }
    void InitializeLevelReq() 
    { 
        lvlText.text = "LVL " + lvl.ToString();
        expGained = 0;
        lvlSlider.minValue = 0;
        lvlSlider.maxValue = expRequired;
        lvlSlider.value = expGained;
    }

    void LevelUp() 
    {
        lvl++;
        contraptionManager.WeightLimit++;
        expRequired += expRequired / 2;
        ContraptionManager.Instance.UpdateUI();
        InitializeLevelReq();
    }

    public void GainExp(int exp) 
    {
        expGained += exp;
        lvlSlider.value = expGained;
        if(expGained >= expRequired) LevelUp();
    }

   
}
