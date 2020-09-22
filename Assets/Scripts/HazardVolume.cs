using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // detect if it's the player ship
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();
        // if valid, continue
        if (playerShip != null)
        {
            //execute command
            StartCoroutine(PlayerKill(playerShip));
        }
    }
    IEnumerator PlayerKill(PlayerShip playerShip)
    {
        playerShip.Kill();
        //wait required time
        yield return new WaitForSeconds(3);
        Application.LoadLevel(Application.loadedLevel);
    }
}
