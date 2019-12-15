using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Room objects")]
    public GameObject[] doors;
    public GameObject[] enemis;
    public GameObject[] switches;

    private Predicate<GameObject>[] _verificationCbList;
    private Action<GameObject>[]    _rewardCbList;

    private RoomVerification _roomVerification;
    private RoomReward       _roomReward;     
    private GameObject       _player; 


    void Start()
    {
        _verificationCbList = new Predicate<GameObject>[5];
        _rewardCbList       = new Action<GameObject>[3];

        _roomVerification = transform.GetChild(0).gameObject.GetComponent<RoomVerification>();
        _roomReward       = transform.GetChild(1).gameObject.GetComponent<RoomReward>();

        _SubAllVerifications();
        _SubAllRewards();        
        
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (_VerifyAllObjects(switches, _verificationCbList[4]))
            _RewardAllObjects(doors, _rewardCbList[0]);
        else
            _RewardAllObjects(doors, _rewardCbList[1]);
    }

    /* ************************************************ */
    /* Functions on All Objects */
    /* ************************************************ */
    /* Verify rules for all object  */
    private bool _VerifyAllObjects(GameObject[] objectArray, Predicate<GameObject> rule)
    {
        for(var i = 0; i < objectArray.Length; i++)
        {
            if (!rule(objectArray[i]))
                return false;
        }

        return true;
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
    /* Functions on Individual Object */
    /* ************************************************ */
    /* Verify for one object  */
    public bool VerifyOneObject(GameObject myObject, int index){
        return (_verificationCbList[index](myObject));
    }

    /* Reward for one object  */
    public void RewardOneObject(GameObject myObject, int index){
        _rewardCbList[index](myObject);
    }

    /* ************************************************ */
    /* Subscribe Predicates and Actions */
    /* ************************************************ */
    /* Sub all Predicates (Verifications) */
    private void _SubAllVerifications()
    {
        /* if all enemis are dead */
        _SubVerification(0, _roomVerification.EnemisDead);

        /* if player has small key */
        _SubVerification(1, _roomVerification.PlayerHasSmallKey);

        /* if player has big key */
        _SubVerification(2, _roomVerification.PlayerHasBigKey);

        /* if all Floor switches are true */
        _SubVerification(3, _roomVerification.FloorSwitch);

        /* if all Floor toggles are true */
        _SubVerification(4, _roomVerification.FloorToggle);
    }
    
    /* Sub all Actions (Rewards) */
    private void _SubAllRewards()
    {

        /* Open all doors */
        _SubReward(0, _roomReward.OpenDoor);

        /* Close all doors */
        _SubReward(1, _roomReward.CloseDoor);
        
        /* Enemis appears */
        _SubReward(2, _roomReward.EnemyAppear);
    }
    
    /* Sub 1 Predicate (Verification) */
    private void _SubVerification(int index, Predicate<GameObject> callback){
        _verificationCbList[index] = callback;
    }

    /* Sub 1 Action (Reward) */
    private void _SubReward(int index, Action<GameObject> callback){
        _rewardCbList[index] = callback;
    }

    /* ************************************************ */
    /* Template Cases */
    /* ************************************************ */
    /* Enemis Case */
    /* Enemis Appear if player enter in area */
    private void _EnemisAppearInRoom()
    {
        if (switches[0].GetComponent<BoxCollider2D>().enabled)
        {
            if (VerifyOneObject(switches[0], 3))
                _RewardAllObjects(enemis, _rewardCbList[2]);
        }
    }

    /* Doors Case */
    /* Doors open if all switches are ON */
    private void _DoorOpenWithSwitches()
    {
        if (switches[0].GetComponent<BoxCollider2D>().enabled)
        {
            if (_VerifyAllObjects(switches, _verificationCbList[3]))
                _RewardAllObjects(doors, _rewardCbList[0]);
        }
    }
    
    /* Doors open if all toggles are ON */
    private void _DoorOpenWithToggles()
    {
        if (_VerifyAllObjects(switches, _verificationCbList[4]))
            _RewardAllObjects(doors, _rewardCbList[0]);
        else
            _RewardAllObjects(doors, _rewardCbList[1]);
    }
}
