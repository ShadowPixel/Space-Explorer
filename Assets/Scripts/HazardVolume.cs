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
            playerShip.Kill();
        }
    }
}
