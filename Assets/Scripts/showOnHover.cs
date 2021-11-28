using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showOnHover : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private void Start()
    {
        target.SetActive(false);
    }
    void OnMouseOver()
    {
        target.SetActive(true);
        Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        target.SetActive(false);
    }
}
