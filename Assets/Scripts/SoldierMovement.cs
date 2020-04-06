﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierMovement : MonoBehaviour
{
    [SerializeField]
    private float soldierSpeed = 125f;

    public GameObject inputManager;
    public MouseInput InputScript;
    public GameObject soldierCounter;
    public SoldierCounter soldierCounterScript;

    private Vector3 soldierOneTargetPosition;
    private Vector3 soldierTwoTargetPosition;
    private Vector3 soldierThreeTargetPosition;
    public bool isSoldierMoving = false;
    private bool beginTimer = false;

    public int soldierTimer;
    private bool hasStopped = false;
    public Transform LoadingBar;
    private float currentProgress = 0;
    [SerializeField]
    private float speed = 3;

    private bool hasJoint = false;
    private GameObject connectedMedic;

    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("InputManager");
        InputScript = inputManager.GetComponent<MouseInput>();
        soldierCounter = GameObject.FindGameObjectWithTag("SoldierCounter");
        soldierCounterScript = soldierCounter.GetComponent<SoldierCounter>();
        soldierOneTargetPosition = new Vector3(-302, 230, 0);
        soldierTwoTargetPosition = new Vector3(-67, 183, 0);
        soldierThreeTargetPosition = new Vector3(344, -6, 0);
    }
    void Update()
    {
        if (isSoldierMoving)
        {
            soldierMove();
        }
        if (beginTimer)
        {
            woundingTimer();
        }
        if (hasJoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, connectedMedic.transform.position, 1);
        }
    }

    void soldierMove()
    {
        if (name == "soldierOne")
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, soldierOneTargetPosition, soldierSpeed * Time.deltaTime);
        }
        if (name == "soldierTwo")
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, soldierTwoTargetPosition, soldierSpeed * Time.deltaTime);
        }
        if (name == "soldierThree")
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, soldierThreeTargetPosition, soldierSpeed * Time.deltaTime);
        }
        if (transform.localPosition == soldierOneTargetPosition || transform.localPosition == soldierTwoTargetPosition || transform.localPosition == soldierThreeTargetPosition)
        {
            isSoldierMoving = false;
            hasStopped = true;
            randomiseInjury();
        }
    }

    void randomiseInjury()
    {
        if (hasStopped)
        {
            soldierTimer = Random.Range(1, 10);
            StartCoroutine(WoundingCoroutine());
        }
        
    }
    
    void woundingTimer()
    {
        if (currentProgress < 100)
        {
            currentProgress += speed * Time.deltaTime;
        }
        LoadingBar.GetComponent<Image>().fillAmount = currentProgress / 100;
    }

    IEnumerator WoundingCoroutine()
    {
        yield return new WaitForSeconds(soldierTimer);
        beginTimer = true;
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !hasJoint && beginTimer)
        {
            connectedMedic = collision.gameObject;
            hasJoint = true;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "StartingTrench")
        {
            Destroy(this.gameObject);
            soldierCounterScript.soldiersSaved++;
        }
    }
}
