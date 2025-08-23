using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    public float attackDelay;
    private float attackDelayCount = 0f;
    internal EnemyState curr;
    internal EnemyState stay = new EnemyStaying();
    internal EnemyState run = new EnemyRunning();
    internal EnemyState attack = new EnemyAttacking();
    internal EnemyState die = new EnemyDying();
    private Vector3 lookRotate;
    private Quaternion lookRotation;
    private GameObject enemy;
    private Animator animator;
    private NavMeshAgent nmAgent;
    private BoxCollider enemyCollider;
    private CapsuleCollider[] attackCollider;
    private IEnumerator previousCoroutine;
    private GameObject player;
    private System.Random rand = new System.Random();
    [SerializeField] private AudioSource hit1;
    [SerializeField] private AudioSource hit2;
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject itemSpawn;

    void Start()
    {
        player = GameObject.Find("Player");
        previousCoroutine = null;
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<BoxCollider>();
        attackCollider = GetComponentsInChildren<CapsuleCollider>(true);
        enemy = gameObject;
        animator = GetComponent<Animator>();
        nmAgent = GetComponent<NavMeshAgent>();
        stay.health = 30;
        stay.attack = 10;
        stay.enemy = enemy;
        stay.animator = animator;
        stay.nmAgent = nmAgent;
        stay.player = player;
        stay.enemyCollider = enemyCollider;
        stay.attackCollider = attackCollider;
        curr = stay;
        curr.enterState(this);
    }

    void Update()
    {
        lookRotate = new Vector3(curr.player.transform.position.x - curr.enemy.transform.position.x, 0f, curr.player.transform.position.z - curr.enemy.transform.position.z);
        lookRotation = Quaternion.LookRotation(lookRotate);
        curr.enemy.transform.rotation = Quaternion.RotateTowards(curr.enemy.transform.rotation, lookRotation, 600f * Time.deltaTime);
        
        healthBar.value = curr.health / 30f;
        if (attackDelayCount > 0)
        {
            attackDelayCount -= Time.deltaTime;
        }
        curr.whileState(this);
    }

    public void swap(EnemyState state)
    {
        float h = curr.health;
        int a = curr.attack;
        curr.endState(this);
        curr = state;
        curr.health = h;
        curr.attack = a;
        curr.animator = animator;
        curr.nmAgent = nmAgent;
        curr.player = player;
        curr.enemy = enemy;
        curr.enemyCollider = enemyCollider;
        curr.attackCollider = attackCollider;
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

    public static bool seePlayer(GameObject player, GameObject enemy)
    {
        Collider[] collider = Physics.OverlapSphere(enemy.transform.position, 20f);
        foreach (Collider c in collider)
        {
            if (c.TryGetComponent(out CharacterMovement cm))
            {
                return true;
            }
        }
        return false;
    }

    public static bool nearPlayer(GameObject player, GameObject enemy)
    {
        Collider[] collider = Physics.OverlapSphere(enemy.transform.position, 1f); //tembak
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
        enemyCollider.enabled = false;
        animator.Play("Death");
        yield return new WaitForSeconds(3f);
        if (rand.Next(0, 2) == 0)
        {
            GameObject item = ItemFactory.createItem(itemSpawn.transform);
            item.transform.position = new Vector3(curr.enemy.transform.position.x, curr.enemy.transform.position.y + 0.5f, curr.enemy.transform.position.z);
        }
        Destroy(gameObject);
    }

}
