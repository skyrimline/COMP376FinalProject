using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFlowControl : MonoBehaviour
{
    // control the game flow
    // BE: rule power < 10
    // BE: not meeting requirements
    // HE: game runs to the last

    // scoreboard
    [SerializeField] private GameObject ScoreBoard;
    [SerializeField] private Text titleText;
    [SerializeField] private Text requiredText;
    [SerializeField] private Text savedText;
    [SerializeField] private Text rewardText;

    [SerializeField] private Text targetSaveCountText;

    // some ending and transition panels
    [SerializeField] private GameObject[] phase1EndTalkPanels; // talk panels phase end
    [SerializeField] private GameObject[] phase2EndTalkPanels; // talk panels phase end
    [SerializeField] private GameObject[] phase3EndTalkPanels; // talk panels phase end
    [SerializeField] private GameObject BEPanel_RulePower;  // 1 rule power BE to choose from
    [SerializeField] private GameObject BEPanel_Other;    // 3 BEs to choose from 

    // phase 2 and phase 3 unlock new rooms
    [SerializeField] private Room_Area[] phase2UnlockRooms;
    [SerializeField] private Room_Area[] phase3UnlockRooms;

    private int currentPanelIndex;

    //targated saving number of npcs and present saved npcs
    private int rewardFactor = 200;
    private int reward = 0;

    public int[] phaseDayCount = { 7, 7, 15 };
    private int[] phaseSaveCount = {1, 1, 45};

    private GameLogic gameLogic;

    private bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
        gameLogic.phase = 1;
        gameLogic.setDay(phaseDayCount[gameLogic.phase-1]);
        targetSaveCountText.text = phaseSaveCount[gameLogic.phase-1].ToString("D2");
        ScoreBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            return;
        }

        // check rule power
        if(gameLogic.rulePower < 50)
        {
            triggered = true;
            Camera.main.transform.position = new Vector3(3.1f, -5.9f, -10f);
            Camera.main.orthographicSize = 23.9f;
            Time.timeScale = 0;
            BEPanel_RulePower.SetActive(true);
        }


        // change phase when time runs out
        if(gameLogic.getDay() == 0 && gameLogic.getTimer() < 1 && gameLogic.phase <= 3)
        {
            reward = ShowScoreBoard();
            triggered = true;
            // check saved NPC count and trigger event
            if(gameLogic.countSavedNormalNPC() < phaseSaveCount[gameLogic.phase-1])
            {
                Camera.main.transform.position = new Vector3(3.1f, -5.9f, -10f);
                Camera.main.orthographicSize = 23.9f;
                //var t = ScoreBoard.GetComponent<RectTransform>();
                //t.localPosition = new Vector3(-297f, -238f, 0);
                ScoreBoard.transform.localPosition = new Vector3(-297f, -13f, 0);
                Time.timeScale = 0;
                BEPanel_Other.SetActive(true);
            }
            else
            {
                // continue game and phase++ 
                // 触发过关对话panel
                // set main camera back to default place
                Camera.main.transform.position = new Vector3(3.1f, -5.9f, -10f);
                Camera.main.orthographicSize = 23.9f;
                currentPanelIndex = 0;
                if (gameLogic.phase == 1)
                {
                    phase1EndTalkPanels[0].SetActive(true);
                }
                else if (gameLogic.phase == 2)
                {
                    phase2EndTalkPanels[0].SetActive(true);
                }
                else if(gameLogic.phase == 3)
                {
                    phase3EndTalkPanels[0].SetActive(true);
                }
            }
        }
    }

    private int ShowScoreBoard()
    {
        int savedNormalNPCs = gameLogic.countSavedNormalNPC();
        int levelTargetSavedNPCS = phaseSaveCount[gameLogic.phase-1];
        Time.timeScale = 0;
        // set texts
        if (savedNormalNPCs < levelTargetSavedNPCS)
        {
            titleText.text = "apocalypse".ToUpper();
            titleText.color = new Color(0.576f, 0, 0);
            savedText.color = new Color(0.576f, 0, 0);
        }
        else
        {
            titleText.text = "Genesis".ToUpper();
            titleText.color = new Color(1, 1, 1);
            savedText.color = new Color(0, 1, 0);
        }
        requiredText.text = levelTargetSavedNPCS.ToString("D2");
        savedText.text = savedNormalNPCs.ToString("D2");

        rewardText.text = (savedNormalNPCs - levelTargetSavedNPCS) > 0 ? ((savedNormalNPCs - levelTargetSavedNPCS) * rewardFactor).ToString("D3") : 0.ToString("D3");
        ScoreBoard.SetActive(true);

        return (savedNormalNPCs - levelTargetSavedNPCS) > 0 ? ((savedNormalNPCs - levelTargetSavedNPCS) * rewardFactor) : 0;

    }

    public void NextPanel()
    {
        ++currentPanelIndex;
        if(gameLogic.phase == 1)
        {
            phase1EndTalkPanels[currentPanelIndex-1].SetActive(false);
            phase1EndTalkPanels[currentPanelIndex].SetActive(true);
        }
        else if(gameLogic.phase == 2)
        {
            phase2EndTalkPanels[currentPanelIndex - 1].SetActive(false);
            phase2EndTalkPanels[currentPanelIndex].SetActive(true);
        }
        else if(gameLogic.phase == 3)
        {
            phase3EndTalkPanels[currentPanelIndex - 1].SetActive(false);
            phase3EndTalkPanels[currentPanelIndex].SetActive(true);
        }
    }

    public void NextPhase()
    {
        // set the current active panel to false
        ScoreBoard.SetActive(false);
        if (gameLogic.phase == 1)
        {
            phase1EndTalkPanels[currentPanelIndex].SetActive(false);
            // also enable some rooms.
            foreach(Room_Area r in phase2UnlockRooms)
            {
                r.EnableRoom();
            }
        }
        else if (gameLogic.phase == 2)
        {
            phase2EndTalkPanels[currentPanelIndex].SetActive(false);
            // enable the rest of the rooms.
            foreach (Room_Area r in phase3UnlockRooms)
            {
                r.EnableRoom();
            }
        }
        else if (gameLogic.phase == 3)
        {
            phase3EndTalkPanels[currentPanelIndex].SetActive(false);
        }

        // set new phase
        gameLogic.phase++;
        // set new day in phase
        gameLogic.setDay(phaseDayCount[gameLogic.phase - 1]);
        // set timer
        gameLogic.setTimer(gameLogic.getTime());
        // set new people to save
        targetSaveCountText.text = phaseSaveCount[gameLogic.phase - 1].ToString("D2");

        // give rewrd
        gameLogic.money += reward;


        Time.timeScale = 1;
        triggered = false;

    }

    public void GameWin()
    {
        SceneManager.LoadScene("mainmenu");
    }

}
