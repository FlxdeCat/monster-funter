using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState
{
    internal float health = 30f;
    internal int attack = 10;
    internal GameObject enemy;
    internal Animator animator;
    internal NavMeshAgent nmAgent;
    internal GameObject player;
    internal BoxCollider enemyCollider;
    internal CapsuleCollider[] attackCollider;
    public abstract void enterState(Minion controller);
    public abstract void whileState(Minion controller);
    public abstract void endState(Minion controller);
}
