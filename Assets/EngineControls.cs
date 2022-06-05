using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineControls : MonoBehaviour
{
    public Rigidbody2D ParentShip;
    public bool EngageMainThruster = false;
    public bool EngageRetrothrusters = false;
    public bool EngageOverdrive = false;
    public float OverdriveBonus = 0f;
    public Vector2 CurrentVelocity;

    public float MTForceMultiplier = 0f;
    public float RTVelocityMultiplier = 0f;

    private Vector2 ForwardFace;
    private EnemyController enemyController;
    private PlayerController playerController;
    //private List<Component> Components;
    // Start is called before the first frame update
    void Start()
    {
        GameObject _parent = transform.parent.gameObject;
        if (_parent.TryGetComponent(out PlayerController p_controller))
        {
            playerController = p_controller;
        }
        if (_parent.TryGetComponent(out EnemyController e_controller))
        {
            enemyController = e_controller;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ForwardFace = GetComponentInParent<Transform>().up;
        ParentShip = GetComponentInParent<Rigidbody2D>();
        CheckController();
        CurrentVelocity = ParentShip.velocity;
        EngageOverdrive = Input.GetKey(KeyCode.LeftShift);
        if (EngageMainThruster) MainThruster();
        if (EngageRetrothrusters) Retrothrusteres();
    }

    void CheckController()
    {
        if (playerController) EngageMainThruster = playerController.MainThrusterOn;
        if (enemyController) EngageMainThruster = enemyController.MainThrusterOn;
    }

    void MainThruster()
    {

        //EngageMainThruster = GetComponentInParent<EnemyController>().MainThrusterOn;
        //CurrentVelocity = Time.deltaTime * MTVelocityMultiplier * CurrentVelocity;
        if (EngageOverdrive)
        {
            ParentShip.AddForce(Time.deltaTime * MTForceMultiplier * OverdriveBonus * ForwardFace);
        }
        else
        {
            ParentShip.AddForce(Time.deltaTime * MTForceMultiplier * ForwardFace);
        }

    }

    void Retrothrusteres()
    {
        CurrentVelocity = Time.deltaTime * RTVelocityMultiplier * CurrentVelocity;
    }
}
