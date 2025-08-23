using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinRageSkill : MonoBehaviour
{

    static bool stopRage;
    private bool rage;
    private bool missionRage;
    private Animator animator;
    private CharacterController characterController;
    private CharacterMovement playerCM;
    [SerializeField] AudioSource rageSFX;

    void Start()
    {
        missionRage = false;
        rage = false;
        characterController = GetComponentInParent<CharacterController>();
        playerCM = GetComponentInParent<CharacterMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !rage && !stopRage && characterController.isGrounded && MainUIScript.hasRSkill())
        {
            if (!missionRage && playerCM.mission.missionIndex == 2 && playerCM.mission.missionActive)
            {
                missionRage = true;
                playerCM.mission.tracker.addProgress();
            }
            MainUIScript.useRSkill();
            StartCoroutine(rageSkill());
        }
    }

    IEnumerator rageSkill()
    {
        rage = true;
        CharacterMovement.setstopMove(true);
        PaladinRollSkill.setStopRoll(true);
        PaladinBasicAttack.setStopAttack(true);
        animator.SetBool("rage", true);
        rageSFX.Play();
        yield return new WaitForSeconds(2.4f);
        rage = false;
        CharacterMovement.setstopMove(false);
        PaladinRollSkill.setStopRoll(false);
        PaladinBasicAttack.setStopAttack(false);
        animator.SetBool("rage", false);
    }

    public static void setStopRage(bool setStopRage)
    {
        stopRage = setStopRage;
    }

}
