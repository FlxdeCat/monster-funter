using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardFlySkill : MonoBehaviour
{

    public float upSpeed;
    public float flyUpTime;
    public float flySpeed;
    public float flyTime;
    private float flyUpTimeCount;
    private float flyTimeCount;
    private static bool fly = false;
    private bool missionFly;
    static bool stopFly;
    private Vector3 velocity;
    private Animator animator;
    private CharacterController characterController;
    private CharacterMovement playerCM;
    private Vector3 movementCamDirection;
    [SerializeField] private Transform cam;
    [SerializeField] private AudioSource flySFX;

    void Start()
    {
        missionFly = false;
        stopFly = false;
        velocity.y = 0;
        animator = GetComponent<Animator>();
        characterController = GetComponentInParent<CharacterController>();
        playerCM = GetComponentInParent<CharacterMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !stopFly)
        {
            if (!fly && characterController.isGrounded && MainUIScript.hasRSkill())
            {
                if (!missionFly && playerCM.mission.missionIndex == 2 && playerCM.mission.missionActive)
                {
                    missionFly = true;
                    playerCM.mission.tracker.addProgress();
                }
                MainUIScript.useRSkill();
                fly = true;
                velocity.y = upSpeed;
                flyUpTimeCount = flyUpTime;
                flyTimeCount = flyTime;
                flySFX.Play();
                animator.SetBool("fly", true);
                CharacterMovement.setstopMove(true);
                CharacterMovement.disableGravity(true);
                WizardAttack.setStopAttack(true);
                WizardFireSkill.setStopFire(true);
            }
            else
            {
                fly = false;
                velocity.y = 0;
                animator.SetBool("fly", false);
                CharacterMovement.setstopMove(false);
                WizardAttack.setStopAttack(false);
                WizardFireSkill.setStopFire(false);
            }
        }

        if (fly)
        {
            if(flyUpTimeCount > 0)
            {
                characterController.Move(velocity);
                flyUpTimeCount -= Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
                animator.SetBool("ground", false);
                movementCamDirection = Quaternion.Euler(0f, cam.eulerAngles.y, 0f) * Vector3.forward;
                characterController.Move(movementCamDirection * flySpeed * Time.deltaTime);
            }

            if (flyTimeCount > 0 && !wallCollide())
            {
                flyTimeCount -= Time.deltaTime;
            }
            else
            {
                fly = false;
                velocity.y = 0;
                animator.SetBool("fly", false);
                CharacterMovement.setstopMove(false);
                CharacterMovement.disableGravity(true);
                WizardAttack.setStopAttack(false);
                WizardFireSkill.setStopFire(false);
            }
        }
    }

    public static void setStopFly(bool setStopFly)
    {
        stopFly = setStopFly;
    }

    public static bool isFlying()
    {
        return fly;
    }

    public bool wallCollide()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider c in collider)
        {
            if (c.tag == "Ground" || c.tag == "Wall")
            {
                return true;
            }
        }
        return false;
    }

}
