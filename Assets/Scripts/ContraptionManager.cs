using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContraptionManager : MonoBehaviour
{
    int weightSum = 0;
    [SerializeField] int weightLimitStart;
    List<ContraptionBehaviour> pickedContraptions = new List<ContraptionBehaviour>();
    [SerializeField] TextMeshProUGUI weightText;
    [SerializeField] GameObject contraptionPrefab;
    // Start is called before the first frame update
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
        if (weightSum + con.c_weight <= weightLimitStart) 
        { 
            //go pick a position
            //instantiate prefab and put turret
            var instance = Instantiate(contraptionPrefab);
            var conInstance = instance.GetComponent<ContraptionBehaviour>();
            conInstance.Contraption = con;
            pickedContraptions.Add(conInstance);
            UpdateUI();
        }
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

    void UpdateUI()
    {
        weightSum = ReturnWeightSum();
        weightText.text = weightSum.ToString() + "/" + weightLimitStart.ToString();
    }
}
