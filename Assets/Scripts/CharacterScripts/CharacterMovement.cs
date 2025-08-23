using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CharacterMovement : MonoBehaviour
{
    public Mission mission;

    float rotSpeed;
    public float jumpDelay;
    public float walkSpeed;
    public float runSpeed;
    public float jumpSpeed;
    public float dodgeSpeed;
    public float dodgeTime;
    public float grav;
    public float rotTime;
    public float walkFootstepDelay;
    public float runFootstepDelay;
    private float footstepDelay;
    private float lastFootstepTime;
    private float jumpDelayCount = 0;
    private float dodgeTimeCount = 0;
    private float speed = 0;
    private float rotAngle;
    private float angle;
    private float HAxis;
    private float VAxis;
    private bool dodge;
    private bool jump;
    private bool fall;
    private bool ground;
    private bool useItem;
    static bool stopMove;
    static bool noGrav;
    private Vector3 movement;
    private Vector3 velocity;
    private Vector3 movementCamDirection;
    private GameObject player;
    private CharacterController characterController;
    private static Animator animator;
    private GameObject spawnpoint;
    private InventoryController inventory;
    private Coroutine useItemAnimation;
    private static GameObject _deathPanel;
    private static GameObject _winPanel;
    private static AudioSource _deathSound;
    private static AudioSource _winSound;
    [SerializeField] private Transform cam;
    [SerializeField] private AudioSource landSFX;
    [SerializeField] private AudioSource footstepsSFXSource;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioClip[] footstepSFX;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private AudioSource winSound;

    void Start()
    {
        inventory = InventoryController.inventoryInstance;
        dodge = false;
        useItem = false;
        lastFootstepTime = Time.time;
        _deathSound = deathSound;
        _winSound = winSound;
        noGrav = false;
        player = GameObject.FindGameObjectWithTag("Player");
        characterController = GetComponent<CharacterController>();
        if(PlayerPrefs.GetString("chosenCharacter") == "Paladin") animator = transform.GetChild(0).GetComponent<Animator>();
        else animator = transform.GetChild(1).GetComponent<Animator>();
        jumpDelayCount = jumpDelay;
        _deathPanel = deathPanel;
        _winPanel = winPanel;
        setstopMove(false);
        setStopSkills(false);
    }

    void Update()
    {
        if (!stopMove)
        {
            jumpDelayCount -= Time.deltaTime;
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                landSFX.mute = false;
                animator.SetBool("walk", true);
                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && MainUIScript.hasStamina())
                {
                    animator.SetBool("run", true);
                    speed = runSpeed;
                }
                else
                {
                    animator.SetBool("run", false);
                    speed = walkSpeed;
                }
            }
            else
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
            }

            if (characterController.isGrounded || ground)
            {
                animator.SetBool("ground", true);
                ground = true;
                if (fall)
                {
                    landSFX.Play();
                    fall = false;
                }
                if (jump)
                {
                    animator.SetBool("jump", false);
                    jump = false;
                }
                if (Input.GetKeyDown(KeyCode.Space) && jumpDelayCount < 0)
                {
                    landSFX.mute = false;
                    velocity = new Vector3(0f, 0f, 0f);
                    jumpDelayCount = jumpDelay;
                    velocity.y += jumpSpeed;
                    animator.SetBool("jump", true);
                    jump = true;
                    ground = false;
                }
                else
                {
                    velocity.y -= grav * Time.deltaTime;
                }
                if (Input.GetKeyDown(KeyCode.V))
                {
                    if (!dodge)
                    {
                        setStopSkills(true);
                        animator.SetBool("dodge", true);
                        dodge = true;
                        dodgeTimeCount = dodgeTime;
                    }
                }
                if (Input.GetKeyDown(KeyCode.G) && !useItem)
                {
                    if(inventory.getChosenItem() == "Meat" && inventory.getMeatQuantity() > 0)
                    {
                        StartCoroutine(useMeat());
                    }
                    else if(inventory.getChosenItem() == "Potion" && inventory.getPotionQuantity() > 0)
                    {
                        StartCoroutine(usePotion());
                    }
                }
            }
            else
            {
                animator.SetBool("ground", false);
                ground = false;
                fall = true;
                if(!WizardFlySkill.isFlying()) velocity.y -= grav * Time.deltaTime;
            }

            if (dodgeTimeCount > 0)
            {
                characterController.Move(Quaternion.Euler(0f, cam.eulerAngles.y, 0f) * Vector3.forward * -1 * dodgeSpeed * Time.deltaTime);
                dodgeTimeCount -= Time.deltaTime;
            }
            else
            {
                setStopSkills(false);
                animator.SetBool("dodge", false);
                dodge = false;
            }

            VAxis = Input.GetAxisRaw("Vertical");
            HAxis = Input.GetAxisRaw("Horizontal");

            movement = new Vector3(HAxis, 0f, VAxis).normalized;
            rotAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotAngle, ref rotSpeed, rotTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if (movement.magnitude >= 0.1f && !dodge)
            {
                movementCamDirection = Quaternion.Euler(0f, rotAngle, 0f) * Vector3.forward;
                characterController.Move(movementCamDirection * speed * Time.deltaTime);
                //characterController.transform.Rotate(Vector3.up * HAxis * (rotSpeed * Time.deltaTime));
            }
            characterController.Move(velocity);

            if ((animator.GetBool("walk") || animator.GetBool("run")) && (characterController.isGrounded || ground))
            {
                footstepDelay = (animator.GetBool("run")) ? runFootstepDelay : walkFootstepDelay;
                if (Time.time - lastFootstepTime > footstepDelay)
                {
                    PlayFootstep();
                    lastFootstepTime = Time.time;
                }
            }
        }
        else
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            animator.SetBool("ground", true);
            animator.SetBool("jump", false);
            jump = false;
            ground = false;
            if (fall)
            {
                landSFX.Play();
                fall = false;
            }
            if (!noGrav) velocity.y -= grav * Time.deltaTime;
            else velocity.y = 0;
            characterController.Move(velocity);
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref rotSpeed, rotTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            inventory.switchItem();
        }

    }

    public void PlayFootstep()
    {
        AudioClip clip = footstepSFX[Random.Range(0, footstepSFX.Length)];
        footstepsSFXSource.PlayOneShot(clip);
    }

    public static void setStopSkills(bool setStopSkills)
    {
        WizardAttack.setStopAttack(setStopSkills);
        WizardFireSkill.setStopFire(setStopSkills);
        WizardFlySkill.setStopFly(setStopSkills);
        PaladinRollSkill.setStopRoll(setStopSkills);
        PaladinRageSkill.setStopRage(setStopSkills);
        PaladinBasicAttack.setStopAttack(setStopSkills);
    }

    public static void setstopMove(bool setstopMove)
    {
        stopMove = setstopMove;
    }
    public static void disableGravity(bool disableGravity)
    {
        noGrav = disableGravity;
    }

    public IEnumerator useMeat()
    {
        setstopMove(true);
        setStopSkills(true);
        useItem = true;
        animator.SetBool("use", true);
        yield return new WaitForSeconds(2f);
        inventory.useMeat();
        MainUIScript.useMeat();
        animator.SetBool("use", false);
        useItem = false;
        setstopMove(false);
        setStopSkills(false);
    }

    public IEnumerator usePotion()
    {
        setstopMove(true);
        setStopSkills(true);
        useItem = true;
        animator.SetBool("use", true);
        yield return new WaitForSeconds(2f);
        inventory.usePotion();
        MainUIScript.usePotion();
        animator.SetBool("use", false);
        useItem = false;
        setstopMove(false);
        setStopSkills(false);
    }

    public static IEnumerator playerDies()
    {
        setstopMove(true);
        setStopSkills(true);
        animator.enabled = false;
        yield return new WaitForSeconds(0.1f);
        animator.enabled = true;
        animator.Play("Death");
        yield return new WaitForSeconds(2f);
        _deathSound.Play();
        yield return new WaitForSeconds(1f);
        _deathPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public static IEnumerator pickupItem(GameObject item)
    {
        setstopMove(true);
        setStopSkills(true);
        animator.SetBool("pickup", true);
        yield return new WaitForSeconds(3.5f);
        animator.SetBool("pickup", false);
        if(item.name == "Meat" || item.name == "Meat(Clone)") InventoryController.inventoryInstance.addMeat();
        else if(item.name == "Potion" || item.name == "Potion(Clone)") InventoryController.inventoryInstance.addPotion();
        Destroy(item);
        setstopMove(false);
        setStopSkills(false);
    }

    public static IEnumerator playerWins()
    {
        setstopMove(true);
        setStopSkills(true);
        _winSound.Play();
        _winPanel.SetActive(true);
        Time.timeScale = 0;
        yield return null;
    }

}
