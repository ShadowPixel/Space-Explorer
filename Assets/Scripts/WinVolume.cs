using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class WinVolume : MonoBehaviour
{
    public UnityEngine.UI.Text winText;

    private void Start()
    {
        winText.text = "";
    }
    private void OnTriggerEnter(Collider other)
    {
        //detect if it's the player ship
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();
        //if vaild, continue
        if (playerShip != null)
        {
            StartCoroutine(Victory(playerShip));
        }
    }

    IEnumerator Victory(PlayerShip playerShip)
    {
        playerShip.Kill();
        winText.text = "You Win!";

        yield return new WaitForSeconds(5);
        Application.LoadLevel(Application.loadedLevel);
    }
}
