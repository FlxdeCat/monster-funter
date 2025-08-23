using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{

    void Update()
    {
        if (!GameObject.Find("Enemy") && !GameObject.Find("Boss"))
        {
            StartCoroutine(CharacterMovement.playerWins());
        }
    }

}
