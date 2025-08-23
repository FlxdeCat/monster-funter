using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class WizardAttack : MonoBehaviour
{

    public float shootDelay;
    public float speed;
    public float knockbackTime;
    private bool isKnockback;
    static bool stopAttack;
    private float shootDelayCount;
    private float knockbackTimeCount;
    private float forceGroundTime;
    private Vector3 target;
    private Vector3 targetAim;
    private Vector3 targetAimDirection;
    private Ray r;
    private RaycastHit hit;
    private CharacterMovement playerCM;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private CinemachineFreeLook mainCam;
    [SerializeField] private CinemachineFreeLook aimCam;
    [SerializeField] private Rig headRig;
    [SerializeField] private Rig armRig;
    [SerializeField] private GameObject crosshairPanel;
    [SerializeField] private AudioSource shootAudio;
    [SerializeField] private ParticleSystem shootEffect;

    // Start is called before the first frame update
    void Start()
    {
        playerCM = GetComponentInParent<CharacterMovement>();
        knockbackTimeCount = knockbackTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isKnockback)
        {
            if (knockbackTimeCount > 0)
            {
                aimCam.m_Lens.FieldOfView = 30;
                knockbackTimeCount -= Time.deltaTime;
            }
            else
            {
                aimCam.m_Lens.FieldOfView = 28;
                knockbackTimeCount = knockbackTime;
                isKnockback = false;
            }
        }

        if (shootDelayCount > 0) shootDelayCount -= Time.deltaTime;

        Ray r = cam.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
        if (Physics.Raycast(r, out RaycastHit hit, 999f)) targetAim = hit.point;

        if (!stopAttack)
        {
            if (Input.GetMouseButton(1))
            {
                if (Input.GetMouseButtonDown(1))
                {
                    aimCam.ForceCameraPosition(mainCam.transform.position, mainCam.transform.rotation);
                    CharacterMovement.setstopMove(true);
                    WizardFireSkill.setStopFire(true);
                    WizardFlySkill.setStopFly(true);
                }
                aimCam.Priority = 20;
                headRig.weight = 1;
                armRig.weight = 1;
                crosshairPanel.SetActive(true);

                targetAim.y = transform.position.y;
                targetAimDirection = (targetAim - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, targetAimDirection, Time.deltaTime * 20f);

                if (Input.GetMouseButtonDown(0) && shootDelayCount <= 0)
                {
                    if (playerCM.mission.missionIndex == 1 && playerCM.mission.missionActive) playerCM.mission.tracker.addProgress();
                    shootDelayCount = shootDelay;
                    shootProjectile();
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(1))
                {
                    forceGroundTime = 0;
                    mainCam.ForceCameraPosition(aimCam.transform.position, aimCam.transform.rotation);
                    CharacterMovement.setstopMove(false);
                    WizardFireSkill.setStopFire(false);
                    WizardFlySkill.setStopFly(false);
                }
                aimCam.Priority = 0;
                headRig.weight = 0;
                armRig.weight = 0;
                crosshairPanel.SetActive(false);
                if(forceGroundTime < 0.1f)
                {
                    forceGroundTime += Time.deltaTime;
                    GetComponent<Animator>().SetBool("ground", true);
                }
            }
        }
    }

    void shootProjectile()
    {
        isKnockback = true;
        shootAudio.Play();
        shootEffect.Play();
        Ray r = cam.ScreenPointToRay(new Vector2(Screen.width/2f, Screen.height/2f));
        if (Physics.Raycast(r, out RaycastHit hit, 999f) && hit.collider.tag != "PlayerGroup")
        {
            target = hit.point;
        }
        else target = r.GetPoint(500);
        var newProjectile = Instantiate(projectile, shootPoint.position, Quaternion.identity) as GameObject;
        newProjectile.GetComponent<Rigidbody>().velocity = (target - shootPoint.position).normalized * speed;
    }

    public static void setStopAttack(bool setStopAttack)
    {
        stopAttack = setStopAttack;
    }

}
