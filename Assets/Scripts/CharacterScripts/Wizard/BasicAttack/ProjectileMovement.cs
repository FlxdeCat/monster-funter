using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{

    private bool collide = false;
    [SerializeField] private GameObject projectileImpact;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "PlayerGroup" && collision.gameObject.tag != "Projectile" && !collide)
        {
            collide = true;
            var impact = Instantiate(projectileImpact, collision.contacts[0].point, Quaternion.identity) as GameObject;
            if (collision.gameObject.tag == "Enemy")
            {
                Minion minion = collision.gameObject.GetComponent<Minion>();
                minion.damaged(10, "Wizard");
            }
            if (collision.gameObject.tag == "Boss")
            {
                Boss boss = collision.gameObject.GetComponentInParent<Boss>();
                boss.damaged(10, "Wizard");
            }
            Destroy(impact, 2);
            Destroy(gameObject);
        }
    }
}
