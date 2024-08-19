using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContraptionManager : MonoBehaviour
{
    private static ContraptionManager instance;
    public static ContraptionManager Instance {  get { return instance; } }
    int weightSum = 0;
    [SerializeField] int weightLimitStart;
    List<ContraptionBehaviour> pickedContraptions = new List<ContraptionBehaviour>();
    [SerializeField] TextMeshProUGUI weightText;
    [SerializeField] GameObject contraptionPrefab;
    ContraptionBehaviour pendingContaption = null;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null && instance != this) instance = null;
        instance = this;
    }
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateUI();
    }

    public void PickContraption(Contraption con)
    {
        if (weightSum + con.c_weight <= weightLimitStart && pendingContaption == null) 
        { 
            //go pick a position
            //instantiate prefab and put turret
            var instance = Instantiate(contraptionPrefab);
            var conInstance = instance.GetComponent<ContraptionBehaviour>();
            conInstance.Contraption = con;
            pickedContraptions.Add(conInstance);
            pendingContaption = conInstance;
            UpdateUI();
        }
    }

    public void SetContraption() 
    {
       // pickedContraptions.Add(pendingContaption);
        pendingContaption = null;
    }

    int ReturnWeightSum() 
    {
        int w = 0;
        foreach(var con in pickedContraptions) 
        {
            w += con.Contraption.c_weight;
        }
        return w;
    }

    public void RemoveContraption(ContraptionBehaviour con) 
    { 
        pickedContraptions.Remove(con);
        pendingContaption = null;
        UpdateUI() ;
    }

    void UpdateUI()
    {
        weightSum = ReturnWeightSum();
        weightText.text = weightSum.ToString() + "/" + weightLimitStart.ToString();
    }
}
