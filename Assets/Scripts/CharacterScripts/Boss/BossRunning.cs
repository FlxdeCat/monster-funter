using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRunning : BossState
{
    private Vector3 playerPosition;
    private int randomAttack;
    private System.Random rand = new System.Random();
    public override void enterState(Boss controller)
    {
        this.attack = 0f;
        randomAttack = rand.Next(1, 5);
        this.nmAgent.isStopped = false;
        this.animator.SetBool("run", true);
    }

    public override void whileState(Boss controller)
    {
        Vector3 lookRotate = new Vector3(this.player.transform.position.x - this.boss.transform.position.x, 0f, this.player.transform.position.z - this.boss.transform.position.z);
        Quaternion lookRotation = Quaternion.LookRotation(lookRotate);
        this.boss.transform.rotation = Quaternion.RotateTowards(this.boss.transform.rotation, lookRotation, 100f * Time.deltaTime);

        this.nmAgent.destination = this.player.transform.position;
        if (this.health <= 0)
        {
            controller.swap(controller.die);
        }
        else if (Boss.nearPlayer(this.player, this.boss))
        {
            if (randomAttack == 1) controller.swap(controller.swipe);
            else if (randomAttack == 2) controller.swap(controller.punch);
            else if (randomAttack == 3) controller.swap(controller.jump);
            else if (randomAttack == 4) controller.swap(controller.fire);
        }
        else if (!Boss.seePlayer(this.player, this.boss))
        {
            controller.swap(controller.stay);
        }
    }

    public override void endState(Boss controller)
    {
        this.animator.SetBool("run", false);
        this.nmAgent.destination = this.boss.transform.position;
        this.nmAgent.isStopped = true;
    }
}
