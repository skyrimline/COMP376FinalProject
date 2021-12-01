using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSequenceController : MonoBehaviour
{
    [SerializeField] private AudioSource Source;
    [SerializeField] private AudioClip Click;
    [SerializeField] private AudioClip ChangeScene;


    List<GameObject> panels = new List<GameObject>();
    Camera mainCam;

    Vector3[] cameraPositions = {
        new Vector3(3.1f, -5.9f, -10f),
        new Vector3(3.1f, -5.9f, -10f),
        new Vector3(3.1f, -5.9f, -10f),
        new Vector3(-25.8f, -14.3f, -10f),
        new Vector3(-25.8f, -14.3f, -10f),
        new Vector3(-25.8f, -14.3f, -10f),
        new Vector3(-16.23f, -0.52f, -10f),
        new Vector3(-16.23f, -0.52f, -10f),
        new Vector3(-16.23f, -0.52f, -10f),
        new Vector3(0.63f, -9.46f, -10f),
        new Vector3(0.63f, -9.46f, -10f),
        new Vector3(-16.79f, 7.96f, -10f),
        new Vector3(-16.79f, 7.96f, -10f),
        new Vector3(-4.13f, 7.96f, -10f),
        new Vector3(-4.13f, 7.96f, -10f),
        new Vector3(-10.85f, 7.96f, -10f),
        new Vector3(-10.85f, 7.96f, -10f),
        new Vector3(-21.67f, -8.4f, -10f),
        new Vector3(-21.67f, -8.4f, -10f),
        new Vector3(-28.96f, -2.95f, -10f),
        new Vector3(30.16f, -7.81f, -10f),
        new Vector3(30.16f, -7.81f, -10f),
        new Vector3(-23.91f, -15.7f, -10f),
        new Vector3(-23.91f, -15.7f, -10f),
        new Vector3(-40.94f, -17.17f, -10f),
        new Vector3(3.1f, -5.9f, -10f),
        new Vector3(3.1f, -5.9f, -10f),
        new Vector3(3.1f, -5.9f, -10f),
        new Vector3(3.1f, -5.9f, -10f),
        new Vector3(3.1f, -5.9f, -10f)
    };

    float[] sizes = {
        23.9f,
        23.9f,
        23.9f,
        12.7f,
        12.7f,
        12.7f,
        10.22f,
        10.22f,
        10.22f,
        10.22f,
        10.22f,
        10.22f,
        10.22f,
        7.93f,
        7.93f, 
        9.66f,
        9.66f, 
        7.6f,
        7.6f,
        7.6f,
        20.4f,
        20.4f,
        14.61f,
        14.61f,
        10.5f,
        23.9f,
        23.9f,
        23.9f,
        23.9f,
        23.9f
    };

    private int currentPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        foreach(Transform t in transform)
        {
            panels.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }

        // set the first one to true
        panels[0].SetActive(true);
        currentPanel = 0;
        mainCam = Camera.main;
        mainCam.transform.position = cameraPositions[currentPanel];
        mainCam.orthographicSize = sizes[currentPanel];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        panels[currentPanel].SetActive(false);
        panels[++currentPanel].SetActive(true);
        mainCam.transform.position = cameraPositions[currentPanel];
        mainCam.orthographicSize = sizes[currentPanel];

        Source.PlayOneShot(Click);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");

        Source.PlayOneShot(ChangeScene);
    }
}
