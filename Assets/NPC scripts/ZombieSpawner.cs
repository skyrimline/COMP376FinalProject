using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    // get a reference to its own zombie, for later spawing
    [SerializeField] private GameObject zombiePrefab = null;

    private GameLogic gameLogic;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.FindGameObjectsWithTag("GameLogic")[0].GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnZombie()
    {
        // when times up, turn into zombie!
        // Instantiate a zombie here and destroy this game object
        Instantiate(zombiePrefab, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
        gameLogic.ChangeRulePower(-5);
    }

    public void restoreMovement()
    {
        transform.parent.gameObject.GetComponent<NPC_Movement>().ResetMoveAndEnableCol();

    }
}
