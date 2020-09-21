using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;
    [SerializeField] GameObject particlesToManipulate;

    [Header("Feedback")]
    [SerializeField] TrailRenderer _trail = null;

    Rigidbody _rb = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _trail.enabled = false;
    }

    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            particlesToManipulate.SetActive(true);
        }
        else
        {
            particlesToManipulate.SetActive(false);
        }
    }

    void MoveShip()
    {
        // S = -1, W = 1, none = 0. Scale using moveSpeed
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical") * _moveSpeed;
        // combine direction with amount
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        // apply the movement
        _rb.AddForce(moveDirection);
    }

    void TurnShip()
    {
        // A = -1, D = 1, none = 0. Scale using turnSpeed
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal") * _turnSpeed;
        // specify which axis you are rotating on
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // apply the rotation
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    public void Kill()
    {
        Debug.Log("Player has been killed!");
        this.gameObject.SetActive(false);
    }

    public void SetSpeed(float speedChange)
    {
        _moveSpeed += speedChange;
        //audio/visuals
    }

    public void SetBoosters(bool activeState)
    {
        _trail.enabled = activeState;
    }
}
