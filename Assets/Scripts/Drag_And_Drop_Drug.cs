using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag_And_Drop_Drug : MonoBehaviour, 
    IEndDragHandler, IDragHandler, IInitializePotentialDragHandler, IBeginDragHandler, IPointerUpHandler
{

    public enum DrugType { vaccine, serum};

    public DrugType drugType;

    private Camera cam;
    [SerializeField] Vaccine_Lab lab;
    [SerializeField] ICU_Room icu;

    private bool draggable = false;

    private Vector3 startPos;
    private CanvasGroup cvs;

    private void Awake()
    {
        cam = Camera.main;
        cvs = GetComponent<CanvasGroup>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            //cvs.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10));
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        
        // reset position anyways
        transform.position = startPos;
        //cvs.blocksRaycasts = false;

    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        startPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDraggable();
    }

    private void CheckDraggable()
    {
        if(drugType == DrugType.vaccine)
        {
            if(lab.GetVaccineNum() > 0)
            {
                draggable = true;
            }
            else
            {
                draggable = false;
            }
        }
        else if(drugType == DrugType.serum)
        {
            if (lab.GetSerumNum() > 0)
            {
                draggable = true;
            }
            else
            {
                draggable = false;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
