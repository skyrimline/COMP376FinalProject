using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Progress_bar : MonoBehaviour
{
    public float max;
    public float current;

    public Image progress;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = current / max;
        progress.fillAmount = fillAmount;
    }


}
