﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerCameraController : MonoBehaviour
{
    public static PlatformerCameraController S;

    private Vector3 camPos = new Vector3 (0,0,-10);
    //Floats below set in inspector
    public float camLowerBound;
    public float camUpperBound;
    public float camLeftBound;
    public float camRightBound;

    public Vector3 camOffset;

    public float foreGroundRiseSpeed;
    public float backGroundRiseSpeed;

    public float foreGroundMoveSpeed;
    public float backGroundMoveSpeed;

    public Vector3 foreGroundOffset;
    public Vector3 backGroundOffset;

    public GameObject[] arrayOfBackgrounds;
    public GameObject[] arrayOfForegrounds;

    private void Start()
    {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Sets the camPos varaible to be the same x/y position as the character
        camPos.x = CharacterController2D.S.transform.position.x;
        camPos.y = CharacterController2D.S.transform.position.y;

        //If the camPos.x is going to exceed the bounds of the level set it to the bounds
        if (camPos.x < camLeftBound)
        {
            camPos.x = camLeftBound;
        }
        else if(camPos.x > camRightBound)
        {
            camPos.x = camRightBound;
        }

        //If the camPos.y is going to exceed the bounds of the level set it to the bounds
        if (camPos.y > camUpperBound)
        {
            camPos.y = camUpperBound;
        }
        else if(camPos.y < camLowerBound)
        {
            camPos.y = camLowerBound;
        }

        transform.position = camPos + camOffset; //Set the position of the camera to be that of camPos

        //For each background in the array move it with the character at a slower rate then the camera to simulate paralaxing
        for (int i = 0; i < arrayOfBackgrounds.Length; i++)
        {
            Vector3 backPos = arrayOfBackgrounds[i].transform.position;
            backPos.x = (camPos.x / backGroundMoveSpeed) + (i * 40) + backGroundOffset.x;
            backPos.y = (camPos.y / backGroundRiseSpeed) + backGroundOffset.y; //1.02f
            arrayOfBackgrounds[i].transform.position = backPos + camOffset;
        }

        //For each foreground in the array move it with the character at a slower rate then the camera to simulate paralaxing
        for (int i = 0; i < arrayOfForegrounds.Length; i++)
        {
            Vector3 backPos = arrayOfForegrounds[i].transform.position;
            backPos.x = (camPos.x / foreGroundMoveSpeed) + (i * 40) + foreGroundOffset.x;
            backPos.y = (camPos.y / foreGroundRiseSpeed) + foreGroundOffset.y; //1.1f
            arrayOfForegrounds[i].transform.position = backPos + camOffset;
        }
    }
}
