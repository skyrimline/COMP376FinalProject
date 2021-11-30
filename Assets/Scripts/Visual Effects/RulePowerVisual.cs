using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RulePowerVisual : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Progress_bar progress;

    private GameLogic gameLogic;

    private int maxRulePower;

    private Color highColor = new Color(0.1377925f, 0.6981132f, 0);
    private Color lowColor = new Color(0.6980392f, 0, 0.006834764f);
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
        maxRulePower = gameLogic.rulePower;
        progress.max = maxRulePower;
    }

    // Update is called once per frame
    void Update()
    {
        progress.current = gameLogic.rulePower;
        float percentage = gameLogic.rulePower / (float)maxRulePower;
        fillImage.color = new Color(Mathf.Lerp(lowColor.r, highColor.r, percentage),
                                    Mathf.Lerp(lowColor.g, highColor.g, percentage),
                                    Mathf.Lerp(lowColor.b, highColor.b, percentage));
    }
}
