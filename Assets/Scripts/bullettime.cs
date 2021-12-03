using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bullettime : MonoBehaviour
{
    private GameObject hightLightShift;
   
    private float bulletTimeTimer;
    private float maxBulletTime;
    private bool isRechargeTime;
    private bool isBulletTime;


    private Text discription;
    private Image icon;
    private Text countDown;
    private Text decoText;

    // Start is called before the first frame update
    private void Start()
    {
        discription = transform.GetChild(0).gameObject.GetComponent<Text>();
        icon = transform.GetChild(1).gameObject.GetComponent<Image>();
        countDown = transform.GetChild(2).gameObject.GetComponent<Text>();
        decoText = transform.GetChild(3).gameObject.GetComponent<Text>();
        maxBulletTime = 6.0f;
        bulletTimeTimer = maxBulletTime;
        isRechargeTime = false;
        isBulletTime = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
        
        ChangeColor();
        countDown.text = ((int)bulletTimeTimer).ToString();
        if (!PauseMenu.isPaused && Input.GetKeyDown(KeyCode.LeftShift))
        {
            isBulletTime = true;
            isRechargeTime = false;
            Time.timeScale = 0.2f;
        }
        if (isBulletTime && bulletTimeTimer > 0)
        {
            bulletTimeTimer -= Time.deltaTime * (1/Time.timeScale);

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isBulletTime = false;
                isRechargeTime = true;
                Time.timeScale = 1.0f;
            }
        }
        if (bulletTimeTimer < 0)
        {
            isBulletTime = false;
            isRechargeTime = true;
            Time.timeScale = 1.0f;
        }
        if (isRechargeTime) bulletTimeTimer += Time.deltaTime;
        if(bulletTimeTimer > maxBulletTime)
        {
            isRechargeTime = false;
        }
    }

    private void ChangeColor()
    {
        if (isBulletTime)
        {
            discription.color = new Color(0.561f, 0.769f, 0.643f, 1f);
            icon.color = new Color(0.561f, 0.769f, 0.643f, 1f);
            countDown.color = new Color(0.561f, 0.769f, 0.643f, 1f);
            decoText.color = new Color(0.561f, 0.769f, 0.643f, 1f);
        }
        else
        {
            discription.color = new Color(1, 1, 1, 1f);
            icon.color = new Color(1, 1, 1, 1f);
            countDown.color = new Color(1, 1, 1, 1f);
            decoText.color = new Color(1, 1, 1, 1f);
        }
    }
}
