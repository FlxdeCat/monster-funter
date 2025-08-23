using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePortalController : MonoBehaviour
{
    private Vector3 lookRotate;
    private Quaternion lookRotation;
    [SerializeField] private GameObject mazePortalHUD;
    [SerializeField] private GameObject MainUI;
    [SerializeField] private GameObject player;

    public bool nearPlayer()
    {
        Collider[] collider = Physics.OverlapSphere(mazePortalHUD.transform.position, 3f);
        foreach (Collider c in collider)
        {
            if (c.TryGetComponent(out CharacterMovement cm))
            {
                return true;
            }
        }
        return false;
    }

    void Update()
    {
        if(nearPlayer())
        {
            mazePortalHUD.SetActive(true);
            lookRotate = new Vector3(this.player.transform.position.x - mazePortalHUD.transform.position.x, 0f, this.player.transform.position.z - mazePortalHUD.transform.position.z);
            lookRotation = Quaternion.LookRotation(lookRotate);
            mazePortalHUD.transform.rotation = Quaternion.RotateTowards(mazePortalHUD.transform.rotation, lookRotation, 600f * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.J))
            {
                MainUI.SetActive(false);
                LoadingScreenManager.loadingScreenInstance.LoadNextScene("DungeonMaze");
            }
        }
        else
        {
            mazePortalHUD.SetActive(false);
        }
    }
}
