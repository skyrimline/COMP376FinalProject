using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Disinfectable : MonoBehaviour, IPointerDownHandler
{
    private Dorm dorm;
    // the corresponding room area script of the dorm
    private Room_Area roomArea;

    private int cost = 50;

    private GameLogic gl;

    // for spawning error messages
    [SerializeField] private GameObject ErrorMessagePrefab;

    private void Start()
    {
        dorm = GetComponent<Dorm>();
        gl = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
        roomArea = dorm.gameObject.GetComponent<Room_Area>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // all conditions must be met in order to do the execution
        // 1. dorm is infected
        // 2. disinfection tool is selected
        // 3. remaining money is enough - Money error effect
        // 4. room area is empty, i.e. no NPC is in the area - room not cleared error message
        // generate error message when error occurs
        if(eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        if(dorm.isDormInfected && Disinfection.disinfectionActive)
        {
            if (gl.money < cost)
            {
                GenerateErrorMessage("No Enough Money");
            }

            if (roomArea.NPCList.Count != 0)
            {
                GenerateErrorMessage("Evacuate People Before Disinfecting");
            }

            if (gl.money >= cost && roomArea.NPCList.Count == 0)
            {
                beingDisinfected();
            }
        }
       
    }

    private void beingDisinfected()
    {
        // deduct money
        gl.money -= cost;
        dorm.Disinfect();
    }

    private void GenerateErrorMessage(string message)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        pos.y += 1f;
        GameObject g = Instantiate(ErrorMessagePrefab, pos, Quaternion.identity);
        var f = g.GetComponentInChildren<Floating_Info_Control>();
        if (f != null)
        {
            f.SetText(message);
        }
    }
}