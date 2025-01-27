using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : Agent
{
    private new void Start()
    {
        base.Start();
        SubGoal subGoal1 = new SubGoal("isWaiting", 1, true);
        goals.Add(subGoal1, 3);
    }
    
}
