using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Food_Allocation : MonoBehaviour
{
    [SerializeField] private Sprite Allow_Allocation;
    [SerializeField] private Sprite Not_Allow_Allocation;
    [SerializeField] private Button Food_Allocation_Button;

    public bool allow_Food_Allocation = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeButtonImage()
    {
        if (allow_Food_Allocation)
        {
            allow_Food_Allocation = false;
            Food_Allocation_Button.image.sprite = Not_Allow_Allocation;
        }
        else
        {
            allow_Food_Allocation = true;
            Food_Allocation_Button.image.sprite = Allow_Allocation;
        }
    }
}
