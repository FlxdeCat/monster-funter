using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class NPCController : MonoBehaviour
{
    internal NPCState curr;
    internal NPCState stay = new NPCStaying();
    internal NPCState walk = new NPCWalking();
    internal NPCState look = new NPCLooking();
    private GameObject npc;
    private Animator animator;
    private NavMeshAgent nmAgent;
    private IEnumerator previousCoroutine;
    public string[] npcMessages;
    [SerializeField] private Transform[] npcDests;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject interactHUD;
    [SerializeField] private TMP_Text messageTxt;

    void Start()
    {
        previousCoroutine = null;
        npc = gameObject;
        animator = GetComponent<Animator>();
        nmAgent = GetComponent<NavMeshAgent>();
        stay.npc = npc;
        stay.animator = animator;
        stay.nmAgent = nmAgent;
        stay.player = player;
        stay.npcDests = npcDests;
        stay.interactHUD = interactHUD;
        stay.npcMessages = npcMessages;
        stay.messageTxt = messageTxt;
        curr = stay;
        curr.enterState(this);
    }

    void Update()
    {
        curr.whileState(this);
    }

    public void swap(NPCState state)
    {
        curr.endState(this);
        curr = state;
        curr.npc = npc;
        curr.animator = animator;
        curr.nmAgent = nmAgent;
        curr.player = player;
        curr.npcDests = npcDests;
        curr.interactHUD = interactHUD;
        curr.messageTxt = messageTxt;
        curr.npcMessages = npcMessages;
        curr.enterState(this);
    }
    public static bool npcNearPlayer(GameObject player, GameObject npc)
    {
        Collider[] collider = Physics.OverlapSphere(npc.transform.position, 2f);
        foreach (Collider c in collider)
        {
            if (c.TryGetComponent(out CharacterMovement cm))
            {
                return true;
            }
        }
        return false;
    }
    public void showText(string txt)
    {
        if(previousCoroutine != null) StopCoroutine(previousCoroutine);
        previousCoroutine = ShowText(txt);
        StartCoroutine(previousCoroutine);
    }
    IEnumerator ShowText(string txt)
    {
        curr.messageTxt.text = "";
        foreach (char ch in txt)
        {
            curr.messageTxt.text += ch;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
