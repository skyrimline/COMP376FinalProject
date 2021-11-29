using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBuySellSwitch : MonoBehaviour
{
    [SerializeField] public Animator buySellPanelAnimator;
    private static bool clicked;
    // Start is called before the first frame update
    void Start()
    {
        buySellPanelAnimator.SetBool("isBuy", true);
        clicked = true;
    }

    public void switchState()
    {
        clicked = !clicked;
        buySellPanelAnimator.SetBool("isBuy", clicked);
    }
}
