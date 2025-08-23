using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStaying : EnemyState
{
    public override void enterState(Minion controller)
    {

    }
    public override void whileState(Minion controller)
    {
        if(this.health <= 0)
        {
            controller.swap(controller.die);
        }
        else if (Minion.nearPlayer(this.player, this.enemy))
        {
            controller.swap(controller.attack);
        }
        else if (Minion.seePlayer(this.player, this.enemy))
        {
            controller.swap(controller.run);
        }
    }
    public override void endState(Minion controller)
    {

    }
    
}
