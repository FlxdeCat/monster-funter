using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunning : EnemyState
{
    private Vector3 playerPosition;
    public override void enterState(Minion controller)
    {
        this.nmAgent.isStopped = false;
        this.animator.SetBool("run", true);
    }
    public override void whileState(Minion controller)
    {
        playerPosition = this.player.transform.position;
        playerPosition.y = this.enemy.transform.position.y;
        this.enemy.transform.LookAt(playerPosition);

        this.nmAgent.destination = this.player.transform.position;
        if (this.health <= 0)
        {
            controller.swap(controller.die);
        }
        else if (Minion.nearPlayer(this.player, this.enemy))
        {
            controller.swap(controller.attack);
        }
        else if (!Minion.seePlayer(this.player, this.enemy))
        {
            controller.swap(controller.stay);
        }
    }
    public override void endState(Minion controller)
    {
        this.animator.SetBool("run", false);
        this.nmAgent.destination = this.enemy.transform.position;
        this.nmAgent.isStopped = true;
    }
}
