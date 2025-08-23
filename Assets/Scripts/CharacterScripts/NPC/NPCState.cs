using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public abstract class NPCState
{
    internal GameObject npc;
    internal Animator animator;
    internal NavMeshAgent nmAgent;
    internal GameObject player;
    internal Transform[] npcDests;
    internal GameObject interactHUD;
    internal string[] npcMessages;
    internal TMP_Text messageTxt;
    public abstract void enterState(NPCController controller);
    public abstract void whileState(NPCController controller);
    public abstract void endState(NPCController controller);
}
