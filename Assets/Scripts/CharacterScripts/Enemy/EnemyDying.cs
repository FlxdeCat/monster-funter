using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDying : EnemyState
{
    public override void enterState(Minion controller)
    {
        controller.death();
    }
    public override void whileState(Minion controller)
    {

    }
    public override void endState(Minion controller)
    {

    }
}
