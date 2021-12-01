using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAudio : MonoBehaviour
{
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip start;
    [SerializeField] private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source.PlayOneShot(start);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            source.PlayOneShot(click);
        }
    }

}
