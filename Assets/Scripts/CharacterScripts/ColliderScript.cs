using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    public float iFrameFireTime;
    public float iFrameTime;
    private float iFrameFireTimeCount;
    private float iFrameTimeCount;
    private Animator animator;
    private AudioSource hitSFX;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        hitSFX = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (iFrameFireTimeCount > 0) iFrameFireTimeCount -= Time.deltaTime;
        if (iFrameTimeCount > 0) iFrameTimeCount -= Time.deltaTime;
    }
    void OnTriggerEnter(Collider collider)
    {
        if ((collider.tag == "Ettackables") && iFrameTimeCount <= 0)
        {
            iFrameTimeCount = iFrameTime;
            MainUIScript.playerDamaged(10);
            hitSFX.Play();
            if (MainUIScript.health <= 0 && !MainUIScript.isDead)
            {
                MainUIScript.isDead = true;
                StartCoroutine(CharacterMovement.playerDies());
                return;
            }
            StartCoroutine(damageAnimation());
        }
        else if ((collider.tag == "Ettackable") && iFrameTimeCount <= 0)
        {
            iFrameTimeCount = iFrameTime;
            MainUIScript.playerDamaged(Boss.curr.attack);
            hitSFX.Play();
            if (MainUIScript.health <= 0 && !MainUIScript.isDead)
            {
                MainUIScript.isDead = true;
                StartCoroutine(CharacterMovement.playerDies());
                return;
            }
            StartCoroutine(damageAnimation());
        }
        else if(collider.tag == "Boss_Fire" && iFrameFireTimeCount <= 0)
        {
            iFrameFireTimeCount = iFrameFireTime;
            MainUIScript.playerDamaged(Boss.curr.attack);
            if (MainUIScript.health <= 0 && !MainUIScript.isDead)
            {
                MainUIScript.isDead = true;
                StartCoroutine(CharacterMovement.playerDies());
                return;
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Boss_Fire" && iFrameFireTimeCount <= 0)
        {
            iFrameFireTimeCount = iFrameFireTime;
            MainUIScript.playerDamaged(Boss.curr.attack);
            if (MainUIScript.health <= 0 && !MainUIScript.isDead)
            {
                MainUIScript.isDead = true;
                StartCoroutine(CharacterMovement.playerDies());
                return;
            }
        }
    }

    private IEnumerator damageAnimation()
    {
        CharacterMovement.setstopMove(true);
        CharacterMovement.setStopSkills(true);
        animator.CrossFadeInFixedTime("Damaged", 0.2f);
        yield return new WaitForSeconds(iFrameTime);
        animator.CrossFadeInFixedTime("Idle", 0.2f);
        CharacterMovement.setstopMove(false);
        CharacterMovement.setStopSkills(false);
    }
}
