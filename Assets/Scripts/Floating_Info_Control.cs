using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// move up and disappear
public class Floating_Info_Control : MonoBehaviour
{
    public enum InfoType {PlainText, ImageWithText};

    public InfoType t;
    private Text text = null;
    private Image image = null;

    private float liveTime;
    private float liveTimer;
    private float movingSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        liveTime = 1.0f;
        liveTimer = liveTime;
        movingSpeed = 2.0f;
        if(t == InfoType.PlainText)
        {
            text = GetComponent<Text>();
            image = null;
        }
        else if(t == InfoType.ImageWithText)
        {
            // TODO: get reference of both text and image
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(liveTimer >= 0)
        {
            if(t == InfoType.PlainText)
            {
                ChangeTextAlphaAndPos();
            }
            else if(t == InfoType.ImageWithText)
            {
                ChangeTextAlphaAndPos();
                ChangeImageAlphaAndPos();
            }
            liveTimer -= Time.deltaTime;
            return;
        }

        // destroy game object based on type
        DestroyByType();
    }

    private void ChangeTextAlphaAndPos()
    {
        text.transform.position += new Vector3(0, movingSpeed * Time.deltaTime, 0);
        text.color = new Color(text.color.r, text.color.g, text.color.b, liveTimer / liveTime);
    }

    private void ChangeImageAlphaAndPos()
    {
        // TODO: 
    }

    private void DestroyByType()
    {
        if (t == InfoType.PlainText)
        {
            // text base object is 2 layers up
            Destroy(transform.parent.parent.gameObject);
        }
        else if (t == InfoType.ImageWithText)
        {
            // TODO delete parent object
        }
    }

    public void SetText(string s)
    {
        if(text != null)
        {
            Debug.Log(s);
            text.text = s;
        }
    }
}
