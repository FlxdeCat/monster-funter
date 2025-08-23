using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFiring : BossState
{
    private float attackTime = 0f;
    public override void enterState(Boss controller)
    {
        attackTime = 0f;
        this.attack = 5f;
        foreach (CapsuleCollider s in this.attackCollider)
        {
            if(s.name == "Fire_Collider") s.tag = "Boss_Fire";
        }
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
            this.animator.CrossFadeInFixedTime("Fire Attack", 0.2f);
        }
        if (attackTime >= 1.6f && attackTime < 4.9f) this.flamethrower.SetActive(true);
        else if (attackTime >= 4.9f) this.flamethrower.SetActive(false);
        if (attackTime >= 5.4f) controller.swap(controller.stay);
    }

    public override void endState(Boss controller)
    {
        foreach (CapsuleCollider s in this.attackCollider)
        {
            if (s.name == "Fire_Collider") s.tag = "Boss";
        }
    }
}
