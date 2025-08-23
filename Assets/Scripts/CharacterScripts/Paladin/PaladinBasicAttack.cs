using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinBasicAttack : MonoBehaviour
{

    static bool stopAttack;
    private int clickCount;
    private int attackCount;
    private bool attack;
    private bool secondHit;
    private bool thirdHit;
    private Animator animator;
    private CharacterController characterController;
    private CharacterMovement playerCM;
    private AnimatorStateInfo currentAttack;
    [SerializeField] private AudioSource attackSFX;
    [SerializeField] private GameObject sword;

    void Start()
    {
        attack = false;
        characterController = GetComponentInParent<CharacterController>();
        playerCM = GetComponentInParent<CharacterMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!stopAttack)
        {
            if (Input.GetMouseButtonDown(0) && attackCount == 0 && characterController.isGrounded)
            {
                CharacterMovement.setstopMove(true);
                PaladinRageSkill.setStopRage(true);
                PaladinRollSkill.setStopRoll(true);
                addMissionProgress();
                attackCount = 1;
                clickCount = 1;
                secondHit = true;
                thirdHit = true;
                animator.SetBool("attack", true);
                animator.SetInteger("AttackCount", 1);
                sword.GetComponent<BoxCollider>().enabled = true;
                sword.tag = "Attackable";
                return;
            }
            if (Input.GetMouseButtonDown(0)) clickCount = Mathf.Min(clickCount + 1, attackCount + 1);
            currentAttack = animator.GetCurrentAnimatorStateInfo(0);
            if (currentAttack.IsName("Paladin_FirstAttack"))
            {
                attack = true;
                if (clickCount >= 2)
                {
                    if (secondHit)
                    {
                        addMissionProgress();
                        secondHit = false;
                    }
                    attackCount = 2;
                    animator.SetInteger("AttackCount", 2);
                }
                return;
            }
            else if (currentAttack.IsName("Paladin_SecondAttack"))
            {
                if (clickCount >= 3)
                {
                    if (thirdHit)
                    {
                        addMissionProgress();
                        thirdHit = false;
                    }
                    attackCount = 3;
                    animator.SetInteger("AttackCount", 3);
                }
                return;
            }
            else if (currentAttack.IsName("Paladin_ThirdAttack")) return;
            else if (!attack) return;
            attackCount = 0;
            attack = false;
            secondHit = true;
            thirdHit = true;
            sword.tag = "Untagged";
            sword.GetComponent<BoxCollider>().enabled = false;
            animator.SetBool("attack", false);
            CharacterMovement.setstopMove(false);
            PaladinRageSkill.setStopRage(false);
            PaladinRollSkill.setStopRoll(false);
        }
        else
        {
            attackCount = 0;
            attack = false;
            secondHit = true;
            thirdHit = true;
            sword.tag = "Untagged";
            sword.GetComponent<BoxCollider>().enabled = false;
            animator.SetBool("attack", false);
        }
    }
    public void playSFX()
    {
        attackSFX.Play();
    }
    public static void setStopAttack(bool setStopAttack)
    {
        stopAttack = setStopAttack;
    }

    private void addMissionProgress()
    {
        if (playerCM.mission.missionIndex == 1 && playerCM.mission.missionActive) playerCM.mission.tracker.addProgress();
    }

}
