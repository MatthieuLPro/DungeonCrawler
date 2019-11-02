using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [Header("Dungeon objects")]
    public GameObject[] ColorSwitches;
    public GameObject[] RedBlocks;
    public GameObject[] BlueBlocks;

    private Predicate<GameObject>[] _verificationCbList;
    private Action<GameObject>[]    _rewardCbList;

    private DungeonVerification _dungeonVerification;
    private DungeonReward       _dungeonReward;     
    private GameObject          _player; 


    void Start()
    {
        _verificationCbList = new Predicate<GameObject>[1];
        _rewardCbList       = new Action<GameObject>[4];

        _dungeonVerification = transform.GetChild(0).gameObject.GetComponent<DungeonVerification>();
        _dungeonReward       = transform.GetChild(1).gameObject.GetComponent<DungeonReward>();

        _SubAllVerifications();
        _SubAllRewards();        
        
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (_VerifyAllObjectsSame(ColorSwitches, _verificationCbList[0]) != -1)
        {
            int i = _VerifyAllObjectsSame(ColorSwitches, _verificationCbList[0]);

            if (ColorSwitches[i].GetComponent<SwitchColor>().color)
            {
                _RewardAllObjects(ColorSwitches, _rewardCbList[0]);
                _RewardAllObjects(RedBlocks, _rewardCbList[2]);
                _RewardAllObjects(BlueBlocks, _rewardCbList[3]);
            }
            else
            {
                _RewardAllObjects(ColorSwitches, _rewardCbList[1]);
                _RewardAllObjects(RedBlocks, _rewardCbList[3]);
                _RewardAllObjects(BlueBlocks, _rewardCbList[2]);
            }
        }
    }

    /* ************************************************ */
    /* Functions verify on All Objects */
    /* ************************************************ */    
    /* Verify if rules are same for all objects */
    private int _VerifyAllObjectsSame(GameObject[] objectArray, Predicate<GameObject> rule)
    {
        if (rule(objectArray[1]) == rule(objectArray[2]) && rule(objectArray[1]) != rule(objectArray[0]))
            return 0;

        if (rule(objectArray[0]) == rule(objectArray[2]) && rule(objectArray[0]) != rule(objectArray[1]))
            return 1;

        if (rule(objectArray[0]) == rule(objectArray[1]) && rule(objectArray[1]) != rule(objectArray[2]))
            return 2;

        for(var i = 3; i < objectArray.Length; i++)
        {
            if (rule(objectArray[i]) != rule(objectArray[i - 1]))
                return i;
        }

        return -1;
    }
    
    /* ************************************************ */
    /* Functions reward on All Objects */
    /* ************************************************ */
    /* Reward for all object  */
    private void _RewardAllObjects(GameObject[] objectArray, Action<GameObject> reward)
    {
        for(var i = 0; i < objectArray.Length; i++)
            reward(objectArray[i]);
    }

    /* ************************************************ */
    /* Subscribe Predicates and Actions */
    /* ************************************************ */
    /* Sub all Predicates (Verifications) */
    private void _SubAllVerifications()
    {
        /* color of the switch */
        _SubVerification(0, _dungeonVerification.ColorSwitch);
    }
    
    /* Sub all Actions (Rewards) */
    private void _SubAllRewards()
    {
        /* Open all doors */
        _SubReward(0, _dungeonReward.SwitchRedColor);

        /* Close all doors */
        _SubReward(1, _dungeonReward.SwitchBlueColor);

        /* Close all doors */
        _SubReward(2, _dungeonReward.BlockUp);

        /* Close all doors */
        _SubReward(3, _dungeonReward.BlockDown);
    }
    
    /* Sub 1 Predicate (Verification) */
    private void _SubVerification(int index, Predicate<GameObject> callback){
        _verificationCbList[index] = callback;
    }

    /* Sub 1 Action (Reward) */
    private void _SubReward(int index, Action<GameObject> callback){
        _rewardCbList[index] = callback;
    }
}
