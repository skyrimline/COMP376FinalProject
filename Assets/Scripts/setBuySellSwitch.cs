using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBuySellSwitch : MonoBehaviour
{
    [SerializeField] public Animator buySellPanelAnimator;
    [SerializeField] public GameObject myScrollView;
    [SerializeField] public GameObject oppositeScrollView;
    private static bool clicked;
    private GameObject Content;
    // Start is called before the first frame update
    void Start()
    {
        buySellPanelAnimator.SetBool("isBuy", true);
        clicked = true;
        myScrollView.SetActive(true);
        oppositeScrollView.SetActive(true);
        Content =  myScrollView.transform.GetChild(0).transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        //Since there are too many object within the panel, I decided to inactivate oppositeScollView to achieve better performance
        myScrollView.SetActive(!clicked);
        oppositeScrollView.SetActive(clicked);
    }

    public void switchState()
    {
        clicked = !clicked;
        buySellPanelAnimator.SetBool("isBuy", clicked);
        Vector3 localPos = Content.transform.localPosition;
        localPos.y = 0;
        Content.transform.localPosition = localPos;
    }
}
