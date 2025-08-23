using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    
    public float speed = 1f;
    public float degreeOpen = 90f;
    public float forward = 0;
    private float time = 0;
    private bool IsOpen = false;
    private bool fullyClosed = true;
    private Vector3 rotStart;
    private Vector3 posStart;
    private Vector3 forwardVector;
    private Vector3 lookRotate;
    private Quaternion lookRotation;
    private Coroutine doorAnimation;
    private GameObject player;
    [SerializeField] private GameObject interactHUD_Center;
    [SerializeField] private GameObject interactHUD_Left;
    [SerializeField] private GameObject interactHUD_Right;
    [SerializeField] private AudioSource openDoorSFX;
    [SerializeField] private AudioSource closeDoorSFX;

    private void Awake()
    {
        rotStart = transform.rotation.eulerAngles;
        forwardVector = transform.right;
        posStart = transform.position;
        player = GameObject.Find("Player");
    }

    public void Open(Vector3 playerPos)
    {
        if (!IsOpen)
        {
            if (doorAnimation != null) StopCoroutine(doorAnimation);
            float dot = Vector3.Dot(forwardVector, (playerPos - transform.position).normalized);
            doorAnimation = StartCoroutine(openDoor(dot));
        }
    }
    public void Close()
    {
        if (IsOpen)
        {
            if (doorAnimation != null) StopCoroutine(doorAnimation);
            doorAnimation = StartCoroutine(closeDoor());
        }
    }

    private IEnumerator openDoor(float forwardPos)
    {
        fullyClosed = false;
        openDoorSFX.Play();
        Quaternion start = transform.rotation;
        Quaternion end;
        if (forwardPos >= forward) end = Quaternion.Euler(new Vector3(0, rotStart.y + degreeOpen, 0));
        else end = Quaternion.Euler(new Vector3(0, rotStart.y - degreeOpen, 0));
        IsOpen = true;
        time = 0f;
        while (time < 1f)
        {
            transform.rotation = Quaternion.Slerp(start, end, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    private IEnumerator closeDoor()
    {
        closeDoorSFX.Play();
        Quaternion start = transform.rotation;
        Quaternion end = Quaternion.Euler(rotStart);
        time = 0;
        IsOpen = false;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(start, end, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
        fullyClosed = true;
    }

    public bool nearPlayer()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider c in collider)
        {
            if (c.TryGetComponent(out CharacterMovement cm)) return true;
        }
        return false;
    }

    private void Update()
    {
        if (nearPlayer())
        {
            if (!IsOpen && fullyClosed)
            {
                interactHUD_Center.SetActive(false);
                interactHUD_Left.SetActive(true);
                interactHUD_Right.SetActive(true);
            }
            else
            {
                interactHUD_Left.SetActive(false);
                interactHUD_Right.SetActive(false);
                interactHUD_Center.SetActive(true);
            }
            lookRotate = new Vector3(player.transform.position.x - interactHUD_Center.transform.position.x, 0f, player.transform.position.z - interactHUD_Center.transform.position.z);
            lookRotation = Quaternion.LookRotation(lookRotate);
            interactHUD_Center.transform.rotation = Quaternion.RotateTowards(interactHUD_Center.transform.rotation, lookRotation, 600f * Time.deltaTime);
            interactHUD_Left.transform.rotation = Quaternion.RotateTowards(interactHUD_Center.transform.rotation, lookRotation, 600f * Time.deltaTime);
            interactHUD_Right.transform.rotation = Quaternion.RotateTowards(interactHUD_Center.transform.rotation, lookRotation, 600f * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (IsOpen) Close();
                else Open(transform.position);
            }
        }
        else
        {
            interactHUD_Left.SetActive(false);
            interactHUD_Right.SetActive(false);
            interactHUD_Center.SetActive(false);
        }
    }
}
