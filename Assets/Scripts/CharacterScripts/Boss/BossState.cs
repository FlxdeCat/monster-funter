using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BossState
{
    internal float health = 600f;
    internal int swipe = 20;
    internal int punch = 20;
    internal int jump = 50;
    internal int fire = 5;
    internal float attack;
    internal GameObject flamethrower;
    internal GameObject boss;
    internal Animator animator;
    internal NavMeshAgent nmAgent;
    internal GameObject player;
    internal BoxCollider bossCollider;
    internal CapsuleCollider[] attackCollider;
    public abstract void enterState(Boss controller);
    public abstract void whileState(Boss controller);
    public abstract void endState(Boss controller);
}
