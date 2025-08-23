using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStaying : NPCState
{
    private float stayTimer;
    private float stayTimerCount;

    public override void enterState(NPCController controller)
    {
        stayTimer = Random.Range(3, 5); //ganti 20-61
        stayTimerCount = 0;
    }
    public override void whileState(NPCController controller)
    {
        stayTimerCount += Time.deltaTime;
        if(stayTimerCount >= stayTimer)
        {
            controller.swap(controller.walk);
        }
        else if (NPCController.npcNearPlayer(this.player, this.npc))
        {
            controller.swap(controller.look);
        }
    }

    public override void endState(NPCController controller)
    {
        
    }

}
