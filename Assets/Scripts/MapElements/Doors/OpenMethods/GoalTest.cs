using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GoalTest : MonoBehaviour
{

    public bool successCb;
    public Action[] rewardCbList;

    // Start is called before the first frame update
    void Start()
    {
        successCb = false;
        rewardCbList = new Action[4];
        for (int i = 0; i <= 3; i++)
        {
            subscribeRewardCb(i, _OpenDoor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (successCb)
        {
            foreach(Action cb in rewardCbList)
                cb();
        }
    } 

    public void subscribeRewardCb(int index, Action _callback)
    {
        rewardCbList[index] = _callback;
    }

    private void _OpenDoor()
    {
        Debug.Log("OPEN DOOR");
    }
}
