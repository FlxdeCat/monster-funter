using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "Attackable" && collision.gameObject.tag == "Enemy")
        {
            Minion minion = collision.gameObject.GetComponent<Minion>();
            minion.damaged(10, "Paladin");
        }
        if (gameObject.tag == "Attackable" && collision.gameObject.tag == "Boss")
        {
            Boss boss = collision.gameObject.GetComponentInParent<Boss>();
            boss.damaged(10, "Paladin");
        }
    }
}
