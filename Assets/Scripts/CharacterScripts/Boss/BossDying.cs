using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDying : BossState
{
    public override void enterState(Boss controller)
    {
        controller.death();
    }

    public override void whileState(Boss controller)
    {

    }

    public override void endState(Boss controller)
    {

    }
}
