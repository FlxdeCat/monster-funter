using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float attackDelay;
    private float attackDelayCount = 0f;
    internal static BossState curr;
    internal BossState stay = new BossStaying();
    internal BossState run = new BossRunning();
    internal BossState swipe = new BossSwiping();
    internal BossState punch = new BossPunching();
    internal BossState jump = new BossJumping();
    internal BossState fire = new BossFiring();
    internal BossState die = new BossDying();
    private Vector3 lookRotate;
    private Quaternion lookRotation;
    private GameObject boss;
    private Animator animator;
    private NavMeshAgent nmAgent;
    private BoxCollider bossCollider;
    private CapsuleCollider[] attackCollider;
    private IEnumerator previousCoroutine;
    private GameObject player;
    [SerializeField] private Slider healthBar;
    [SerializeField] private AudioSource hit1;
    [SerializeField] private AudioSource hit2;
    [SerializeField] private GameObject flamethrower;

    void Start()
    {
        player = GameObject.Find("Player");
        previousCoroutine = null;
        animator = GetComponent<Animator>();
        bossCollider = GetComponentInChildren<BoxCollider>(true);
        attackCollider = GetComponentsInChildren<CapsuleCollider>(true);
        boss = gameObject;
        animator = GetComponent<Animator>();
        nmAgent = GetComponent<NavMeshAgent>();
        stay.health = 600f;
        stay.swipe = 20;
        stay.punch = 20;
        stay.jump = 50;
        stay.fire = 5;
        stay.boss = boss;
        stay.animator = animator;
        stay.nmAgent = nmAgent;
        stay.player = player;
        stay.bossCollider = bossCollider;
        stay.attackCollider = attackCollider;
        stay.flamethrower = flamethrower;
        curr = stay;
        curr.enterState(this);
    }

    void Update()
    {
        healthBar.value = curr.health / 600f;
        if (attackDelayCount > 0)
        {
            attackDelayCount -= Time.deltaTime;
        }
        curr.whileState(this);
    }

    public void swap(BossState state)
    {
        float h = curr.health;
        int a = curr.swipe;
        int b = curr.punch;
        int c = curr.jump;
        int d = curr.fire;
        curr.endState(this);
        curr = state;
        curr.health = h;
        curr.swipe = a;
        curr.punch = b;
        curr.jump = c;
        curr.fire = d;
        curr.animator = animator;
        curr.nmAgent = nmAgent;
        curr.player = player;
        curr.boss = boss;
        curr.bossCollider = bossCollider;
        curr.attackCollider = attackCollider;
        curr.flamethrower = flamethrower;
        curr.enterState(this);
    }

    public void damaged(int damage, string source)
    {
        if (attackDelayCount <= 0)
        {
            attackDelayCount = attackDelay;
            if (source == "Paladin") hit2.Play();
            else if (source == "Wizard") hit1.Play();
            curr.health -= damage;
        }
    }
    public void fired(int damage)
    {
        if (attackDelayCount <= 0)
        {
            attackDelayCount = attackDelay;
            curr.health -= damage;
        }
    }

    public static bool seePlayer(GameObject player, GameObject boss)
    {
        Collider[] collider = Physics.OverlapSphere(boss.transform.position, 30f);
        foreach (Collider c in collider)
        {
            if (c.TryGetComponent(out CharacterMovement cm))
            {
                return true;
            }
        }
        return false;
    }

    public static bool nearPlayer(GameObject player, GameObject boss)
    {
        Collider[] collider = Physics.OverlapSphere(boss.transform.position, 3f); //tembak
        foreach (Collider c in collider)
        {
            if (c.TryGetComponent(out CharacterMovement cm))
            {
                return true;
            }
        }
        return false;
    }

    public void death()
    {
        if (previousCoroutine != null) StopCoroutine(previousCoroutine);
        previousCoroutine = Death();
        StartCoroutine(previousCoroutine);
    }

    private IEnumerator Death()
    {
        bossCollider.enabled = false;
        animator.Play("Death");
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

}
