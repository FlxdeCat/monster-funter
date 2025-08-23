using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WizardAiming : MonoBehaviour
{

    public float aimSpeed;
    [SerializeField] Camera aimCam;
    [SerializeField] LayerMask layerMask;

    private void Update()
    {
        Ray r = aimCam.ScreenPointToRay(new Vector2(Screen.width/2f, Screen.height/2f));
        if(Physics.Raycast(r, out RaycastHit hit, 999f, layerMask))
        {
            transform.position = Vector3.Lerp(transform.position, hit.point, aimSpeed * Time.deltaTime);
        }
    }

}
