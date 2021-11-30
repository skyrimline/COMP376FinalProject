using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarVisual : MonoBehaviour
{
    // reference to NPC_Logic to get reference to health info
    private NPC_Logic npc;
    private Progress_bar progress;

    private int maxHealth;

    [SerializeField] Image fillImage;
    private Color lowColor = new Color(0.51f, 0.2f, 0.263f);   // red
    private Color highColor = new Color(0.537f, 0.741f, 0.62f);   // green


    // Start is called before the first frame update
    void Start()
    {
        npc = transform.parent.GetComponent<NPC_Logic>();
        progress = GetComponent<Progress_bar>();
        maxHealth = npc.maxLife;
        progress.max = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        int currHealth = npc.GetLife();
        progress.current = currHealth;

        float percentage = currHealth / (float)maxHealth;
        fillImage.color = new Color(Mathf.Lerp(lowColor.r, highColor.r, percentage),
                                    Mathf.Lerp(lowColor.g, highColor.g, percentage),
                                    Mathf.Lerp(lowColor.b, highColor.b, percentage));

    }
}
