using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "WizardFire" && collision.gameObject.tag == "Enemy")
        {
            Minion minion = collision.gameObject.GetComponent<Minion>();
            minion.fired(10);
        }
        if (gameObject.tag == "WizardFire" && collision.gameObject.tag == "Boss")
        {
            Boss boss = collision.gameObject.GetComponentInParent<Boss>();
            boss.fired(10);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (gameObject.tag == "WizardFire" && collision.gameObject.tag == "Enemy")
        {
            Minion minion = collision.gameObject.GetComponent<Minion>();
            minion.fired(10);
        }
        if (gameObject.tag == "WizardFire" && collision.gameObject.tag == "Boss")
        {
            Boss boss = collision.gameObject.GetComponentInParent<Boss>();
            boss.fired(10);
        }
    }
}
