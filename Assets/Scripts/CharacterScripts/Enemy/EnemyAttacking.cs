using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : EnemyState
{
    private Vector3 playerPosition;
    private int randomAttack;
    private System.Random rand = new System.Random();
    public override void enterState(Minion controller)
    {
        foreach (CapsuleCollider s in this.attackCollider){
            s.tag = "Ettackables";
        }
        randomAttack = rand.Next(1, 4);
    }
    public override void whileState(Minion controller)
    {
        playerPosition = this.player.transform.position;
        playerPosition.y = this.enemy.transform.position.y;
        this.enemy.transform.LookAt(playerPosition);

        if (this.health <= 0)
        {
            controller.swap(controller.die);
        }
        else if(this.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle" && !this.animator.IsInTransition(0))
        {
            if (!Minion.nearPlayer(this.player, this.enemy)) controller.swap(controller.stay);
            else if (randomAttack == 1) this.animator.CrossFadeInFixedTime("Attack 1", 0.2f);
            else if (randomAttack == 2) this.animator.CrossFadeInFixedTime("Attack 2", 0.2f);
            else if (randomAttack == 3) this.animator.CrossFadeInFixedTime("Attack 3", 0.2f);
        }
        randomAttack = rand.Next(1, 4);
    }
    public override void endState(Minion controller)
    {
        foreach (CapsuleCollider s in this.attackCollider)
        {
            s.tag = "Untagged";
        }
    }
}
