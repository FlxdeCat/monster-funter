using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardFireSkill : MonoBehaviour
{

    private bool missionFire;
    static bool stopFire;
    private Animator animator;
    private CharacterController characterController;
    private CharacterMovement playerCM;
    private CapsuleCollider fireCollider;
    [SerializeField] private AudioSource fireSFX;
    [SerializeField] private GameObject fireBeam;

    void Start()
    {
        missionFire = false;
        animator = GetComponent<Animator>();
        characterController = GetComponentInParent<CharacterController>();
        playerCM = GetComponentInParent<CharacterMovement>();
        fireCollider = fireBeam.GetComponentInChildren<CapsuleCollider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !stopFire && characterController.isGrounded && MainUIScript.hasFSkill())
        {
            if (!missionFire && playerCM.mission.missionIndex == 2 && playerCM.mission.missionActive)
            {
                missionFire = true;
                playerCM.mission.tracker.addProgress();
            }
            StartCoroutine(FireSkill());
        }
    }

    IEnumerator FireSkill()
    {
        stopFire = true;
        CharacterMovement.setstopMove(true);
        WizardAttack.setStopAttack(true);
        WizardFlySkill.setStopFly(true);
        animator.SetBool("fire", true);
        fireSFX.Play();
        yield return new WaitForSeconds(1);
        fireCollider.gameObject.tag = "WizardFire";
        fireCollider.tag = "WizardFire";
        fireBeam.SetActive(true);
        fireBeam.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(5);
        fireCollider.gameObject.tag = "Untagged";
        fireCollider.tag = "Untagged";
        fireBeam.SetActive(false);
        fireBeam.GetComponent<ParticleSystem>().Stop();
        animator.SetBool("fire", false);
        MainUIScript.useFSkill();
        yield return new WaitForSeconds(1);
        stopFire = false;
        CharacterMovement.setstopMove(false);
        WizardAttack.setStopAttack(false);
        WizardFlySkill.setStopFly(false);
    }

    public static void setStopFire(bool setStopFire)
    {
        stopFire = setStopFire;
    }

}
