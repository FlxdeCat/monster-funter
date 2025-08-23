using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWalking : NPCState
{
    private Vector3 randDir;
    private Vector3 randDest;
    private Vector3 finalDest;

    public override void enterState(NPCController controller)
    {
        this.animator.SetBool("walk", true);
        do
        {
            randDest = npcDests[Random.Range(0, npcDests.Length)].position;
        } while (finalDest == randDest);
        finalDest = randDest;
        this.nmAgent.destination = finalDest;
    }
    public override void whileState(NPCController controller)
    {
        if((int)this.npc.transform.position.x == (int)finalDest.x && (int)this.npc.transform.position.z == (int)finalDest.z)
        {
            controller.swap(controller.stay);
        }
        else if (NPCController.npcNearPlayer(this.player, this.npc))
        {
            controller.swap(controller.look);
        }
    }
    public override void endState(NPCController controller)
    {
        this.animator.SetBool("walk", false);
    }
}
