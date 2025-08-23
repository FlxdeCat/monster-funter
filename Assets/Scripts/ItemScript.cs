using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private Vector3 lookRotate;
    private Quaternion lookRotation;
    private GameObject player;
    [SerializeField] private GameObject itemHUD;

    public bool nearPlayer()
    {
        Collider[] collider = Physics.OverlapSphere(itemHUD.transform.position, 1f);
        foreach (Collider c in collider)
        {
            if (c.TryGetComponent(out CharacterMovement cm))
            {
                return true;
            }
        }
        return false;
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (nearPlayer())
        {
            itemHUD.SetActive(true);
            lookRotate = new Vector3(this.player.transform.position.x - itemHUD.transform.position.x, 0f, this.player.transform.position.z - itemHUD.transform.position.z);
            lookRotation = Quaternion.LookRotation(lookRotate);
            itemHUD.transform.rotation = Quaternion.RotateTowards(itemHUD.transform.rotation, lookRotation, 600f * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.C)) StartCoroutine(CharacterMovement.pickupItem(gameObject));
        }
        else
        {
            itemHUD.SetActive(false);
        }
    }



}
