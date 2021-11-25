using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    private GameObject shop;
    private Animator animator;
    private bool isDay;


    private bool MenuShow = false;
    // Start is called before the first frame update
    void Start()
    {
        shop = GameObject.Find("MovingShop");
        animator = shop.GetComponent<Animator>();
        MenuShow = false;
    }

    // Update is called once per frame
    void Update()
    {
        isDay = animator.GetBool("isDay");
        _menu.transform.GetChild(0).gameObject.SetActive(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));

        if (MenuShow && !isDay)
        {
            //the Upgrade panel will shows
            _menu.transform.GetChild(1).gameObject.SetActive(true);

        }
        else if (!MenuShow || isDay)
        {
            MenuShow = false;
            //the upgrade panel will not show
            _menu.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void MenuClick()
    {
        MenuShow = !MenuShow;
    }
}
