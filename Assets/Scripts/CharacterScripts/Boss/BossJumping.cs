using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumping : BossState
{
    private float attackTime = 0f;
    public override void enterState(Boss controller)
    {
        attackTime = 0f;
        this.attack = 50f;
    }

    public override void whileState(Boss controller)
    {
        Vector3 lookRotate = new Vector3(this.player.transform.position.x - this.boss.transform.position.x, 0f, this.player.transform.position.z - this.boss.transform.position.z);
        Quaternion lookRotation = Quaternion.LookRotation(lookRotate);
        this.boss.transform.rotation = Quaternion.RotateTowards(this.boss.transform.rotation, lookRotation, 100f * Time.deltaTime);

        attackTime += Time.deltaTime;
        if (this.health <= 0)
        {
            controller.swap(controller.die);
        }
        else if (this.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle" && !this.animator.IsInTransition(0))
        {
            //if (!Boss.nearPlayer(this.player, this.boss)) controller.swap(controller.stay);
            this.animator.CrossFadeInFixedTime("Jump Attack", 0.2f);
        }
        if (attackTime >= 3.8f) controller.swap(controller.stay);
        else if (attackTime >= 3f)
        {
            this.boss.tag = "Ettackable";
            this.bossCollider.tag = "Ettackable";
            foreach (CapsuleCollider s in this.attackCollider)
            {
                s.tag = "Ettackable";
            }
        }
        else if (attackTime >= 1f)
        {
            this.boss.tag = "Ettackable";
            this.bossCollider.tag = "Ettackable";
            foreach (CapsuleCollider s in this.attackCollider)
            {
                s.tag = "Ettackable";
            }
        }
    }

    public override void endState(Boss controller)
    {
        this.boss.tag = "Boss";
        this.bossCollider.tag = "Boss";
        foreach (CapsuleCollider s in this.attackCollider)
        {
            s.tag = "Boss";
        }
    }
}
