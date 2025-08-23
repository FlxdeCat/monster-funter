using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinRollSkill : MonoBehaviour
{
    public float rollSpeed;
    private bool roll;
    private bool rollMove;
    private bool missionRoll;
    static bool stopRoll;
    private CharacterController characterController;
    private Animator animator;
    private CharacterMovement playerCM;
    [SerializeField] Transform cam;
    [SerializeField] AudioSource rollSFX;

    void Start()
    {
        roll = false;
        rollMove = false;
        missionRoll = false;
        characterController = GetComponentInParent<CharacterController>();
        playerCM = GetComponentInParent<CharacterMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !roll & !stopRoll && characterController.isGrounded && MainUIScript.hasFSkill())
        {
            if (!missionRoll && playerCM.mission.missionIndex == 2 && playerCM.mission.missionActive)
            {
                missionRoll = true;
                playerCM.mission.tracker.addProgress();
            }
            StartCoroutine(rollSkill());
        }
        if (rollMove)
        {
            characterController.Move(Quaternion.Euler(0f, cam.eulerAngles.y, 0f) * Vector3.forward * rollSpeed * Time.deltaTime);
        }
    }

    IEnumerator rollSkill()
    {
        roll = true;
        animator.SetBool("roll", true);
        yield return new WaitForSeconds(0.1f);
        CharacterMovement.setstopMove(true);
        PaladinRageSkill.setStopRage(true);
        PaladinBasicAttack.setStopAttack(true);
        yield return new WaitForSeconds(0.4f);
        rollSFX.Play();
        rollMove = true;
        yield return new WaitForSeconds(1);
        rollMove = false;
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("roll", false);
        roll = false;
        MainUIScript.useFSkill();
        yield return new WaitForSeconds(0.1f);
        CharacterMovement.setstopMove(false);
        PaladinRageSkill.setStopRage(false);
        PaladinBasicAttack.setStopAttack(false);

    }

    public static void setStopRoll(bool setStopRoll)
    {
        stopRoll = setStopRoll;
    }

}
