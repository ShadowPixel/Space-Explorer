using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ShrinkPowerup : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] float _sizeChange = 5;
    [SerializeField] float _powerupDuration = 5;

    [Header("Setup")]
    [SerializeField] GameObject _visualsToDeactivate = null;
    [SerializeField] AudioClip _ShrinkSound = null;

    Collider _colliderToDeactivate = null;
    bool _smaller = false;

    private void Awake()
    {
        _colliderToDeactivate = GetComponent<Collider>();

        EnableObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();
        //check if valid player and not already small
        if (playerShip != null && _smaller == false)
        {
            //start timer or restart if already started
            AudioHelper.PlayClip2D(_ShrinkSound, 1);
            StartCoroutine(PowerupSequence(playerShip));
        }
    }

    IEnumerator PowerupSequence(PlayerShip playerShip)
    {
        //set bool for lockout detection
        _smaller = true;

        ActivatePowerup(playerShip);
        //simulate object disable
        DisableObject();

        //wait required time
        yield return new WaitForSeconds(_powerupDuration);
        //reset
        DeactivatePowerup(playerShip);
        EnableObject();

        //bool to release lockout
        _smaller = false;
    }

    void ActivatePowerup(PlayerShip playerShip)
    {
        if (playerShip != null)
        {
            //powerup the player
            playerShip.transform.localScale -= new Vector3(_sizeChange, _sizeChange, _sizeChange);
        }
    }

    void DeactivatePowerup(PlayerShip playerShip)
    {
        //revert player back to normal
        playerShip.transform.localScale += new Vector3(_sizeChange, _sizeChange, _sizeChange);
    }

    public void DisableObject()
    {
        //disable collider
        _colliderToDeactivate.enabled = false;
        //disable visuals
        _visualsToDeactivate.SetActive(false);
    }

    public void EnableObject()
    {
        //enable collider
        _colliderToDeactivate.enabled = true;
        //enable visuals
        _visualsToDeactivate.SetActive(true);
    }
}
