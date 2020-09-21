using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] float _speedIncreaseAmount = 20;
    [SerializeField] float _powerupDuration = 5;

    [Header("Setup")]
    [SerializeField] GameObject _visualsToDeactivate = null;

    Collider _colliderToDeactivate = null;
    bool _poweredUp = false;

    private void Awake()
    {
        _colliderToDeactivate = GetComponent<Collider>();

        EnableObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();
        //check to see if player is valid and not powered up already
        if (playerShip != null && _poweredUp == false)
        {
            //start the timer for the power up, restart if already started
            StartCoroutine(PowerupSequence(playerShip));
        }
    }

    IEnumerator PowerupSequence(PlayerShip playerShip)
    {
        //set bool for lockout detection
        _poweredUp = true;

        ActivatePowerup(playerShip);
        //simulate disable of object
        DisableObject();

        //wait required time
        yield return new WaitForSeconds(_powerupDuration);
        //reset
        DeactivatePowerup(playerShip);
        EnableObject();

        //bool to release lockout
        _poweredUp = false;
    }

    void ActivatePowerup(PlayerShip playerShip)
    {
        if (playerShip != null)
        {
            //powerup the player
            playerShip.SetSpeed(_speedIncreaseAmount);
            //visuals
            playerShip.SetBoosters(true);
        }
    }

    void DeactivatePowerup(PlayerShip playerShip)
    {
        //revert player to normal
        playerShip?.SetSpeed(-_speedIncreaseAmount);
        //deactivate visuals
        playerShip?.SetBoosters(false);
    }

    public void DisableObject()
    {
        //disable collider
        _colliderToDeactivate.enabled = false;
        //disable visuals
        _visualsToDeactivate.SetActive(false);
        //particle flash/audio
    }

    public void EnableObject()
    {
        //enable collider
        _colliderToDeactivate.enabled = true;
        //enable visuals
        _visualsToDeactivate.SetActive(true);
        //particle flash/audio
    }
}
