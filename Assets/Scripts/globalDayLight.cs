using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.Rendering.Universal;

public class globalDayLight : MonoBehaviour
{
    private Color spriteTC = new Color(0.188f, 0.094f, 0.094f);
    private Color tileTC = new Color(0.596f, 0.396f, 0.396f);
    private Color dayLightTC = new Color(0.125f, 0, 0.012f);

    private Color dLDefault = new Color(0.624f, 0.675f, 0.753f);
    private Color white = new Color(1.0f, 1.0f, 1.0f);

    private List<SpriteRenderer> spriteColors = new List<SpriteRenderer>();
    private List<Tilemap> tilemapColors = new List<Tilemap>();
    private List<Light2D> dayLightColors = new List<Light2D>();
    private List<Light2D> lightEmitIntensitys = new List<Light2D>();
    private List<Light2D> nightLightIntensitys = new List<Light2D>();

    private GameLogic gL;
    private float dTime;
    private float dTimer;
    private float midNight;
    private float nightInterval = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        gL = GetComponent<GameLogic>();
        dTime = gL.getTime();
        midNight = dTime / 2;
        GameObject[] bg_sprite = GameObject.FindGameObjectsWithTag("bg_sprite");
        GameObject[] bg_tilemap = GameObject.FindGameObjectsWithTag("bg_tilemap");
        GameObject[] bg_daylight = GameObject.FindGameObjectsWithTag("bg_daylight");
        GameObject[] bg_lightEmit = GameObject.FindGameObjectsWithTag("bg_roomLightEmit");
        GameObject[] bg_nightLight = GameObject.FindGameObjectsWithTag("bg_nightLight");
        foreach (GameObject g in bg_sprite){ spriteColors.Add(g.GetComponent<SpriteRenderer>());}
        foreach (GameObject g in bg_tilemap){ tilemapColors.Add(g.GetComponent<Tilemap>());}
        foreach (GameObject g in bg_daylight) { dayLightColors.Add(g.GetComponent<Light2D>());}
        foreach (GameObject g in bg_lightEmit) { lightEmitIntensitys.Add(g.GetComponent<Light2D>()); }
        foreach (GameObject g in bg_nightLight) { nightLightIntensitys.Add(g.GetComponent<Light2D>()); }
    }

    // Update is called once per frame
    void Update()
    {
        dTimer = gL.getTimer();
        float t = Mathf.Abs(dTimer - midNight)/midNight;
        Color tempSpriteColor = new Color(Mathf.Lerp(white.r, spriteTC.r, t), Mathf.Lerp(white.g, spriteTC.g, t), Mathf.Lerp(white.b, spriteTC.b, t));
        Color tempTilemapColor = new Color(Mathf.Lerp(white.r, tileTC.r, t), Mathf.Lerp(white.g, tileTC.g, t), Mathf.Lerp(white.b, tileTC.b, t));
        Color tempDayLightColor = new Color(Mathf.Lerp(dLDefault.r, dayLightTC.r, t), Mathf.Lerp(dLDefault.g, dayLightTC.g, t), Mathf.Lerp(dLDefault.b, dayLightTC.b, t));
        foreach (SpriteRenderer s in spriteColors)
        {
            s.color = tempSpriteColor;
        }
        foreach (Tilemap tm in tilemapColors)
        {
            tm.color = tempTilemapColor;
        }
        foreach (Light2D dLight in dayLightColors)
        {
            dLight.color = tempDayLightColor;
        }
        foreach (Light2D le in lightEmitIntensitys)
        {
            le.intensity = Mathf.Lerp(0.0f, 0.5f, t);
        }
        foreach (Light2D nl in nightLightIntensitys)
        {
            
            if (dTimer < dTime * nightInterval)
            {
                nl.intensity = 1.0f;
            }
            else if (dTimer > dTime - dTime * nightInterval)
            {
                nl.intensity = 1.0f;
            }
            else
            {
                nl.intensity = 0.0f;
            }
            

        }
    }
}
