using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Room objects")]
    public GameObject[] doors;
    public GameObject[] enemis;

    private Action[] _verificationCbList;
    private Action[] _rewardCbList;

    void Start()
    {
        _verificationCbList = new Action[3];
        _rewardCbList       = new Action[2];
        RoomReward roomReward             = transform.GetChild(0).gameObject.GetComponent<RoomReward>();
        RoomVerification roomVerification = transform.GetChild(0).gameObject.GetComponent<RoomVerification>();

        subRewardCb(0, RoomReward.OpenDoor);
        subRewardCb(1, RoomReward.CloseDoor);
        subVerificationCb(0, roomVerification.PlayerHasSmallKey);
        subVerificationCb(1, roomVerification.EnemisDead);
        subVerificationCb(2, roomVerification.PlayerHasBigKey);
    }

    void Update()
    {
        for(var i = 0; i < _verificationCbList.Length; i++)
        {
            if (_verificationCbList[i])
                _rewardCbList[i];
        }
    }

    public void subVerificationCb(int index, Action _callback){
        _verificationCbList[index] = _callback;
    }
    
    public void subRewardCb(int index, Action _callback){
        _rewardCbList[index] = _callback;
    }
    
    public void subRewardGOCb(int index, Action _callback){
        _rewardCbList[index] = _callback;
    }
}
