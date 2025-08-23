using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionTracker
{
    public int currProgress;
    public int goalProgress;

    public bool isFinished()
    {
        return currProgress >= goalProgress;
    }

    public void addProgress()
    {
        currProgress++;
    }
}
