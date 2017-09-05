﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Grabber tool enables the user to select objects and drag them around the scene using the WiiMote's orientation.
/// </summary>
public class GrabberTool : MonoBehaviour, ITool
{
    //Initialize 
    public GameObject holder;
    public GameObject wandObject;
    private RaycastHit hit;
    public int rayLength = 200;
    public Vector3 origin, direction, previousOrigin, previousDirection;

    //Initializes all the necessary fields while rendering the scene
    private void Start()
    {
        //Get all necessary game objects
        if (wandObject == null)
        {
            Debug.LogError("Need to set wand object!");
        }

        if (holder == null)
        {
            Debug.LogError("Need to set top level UniCAVE object!");
        }
    }

    /// <summary>
    /// Handles the button input for the grabberTool 
    /// Takes in every button and whether it is currently being pressed or not 
    /// While pointing at the object the user can increase or decrease the size of the object 
    /// </summary>
    public void ButtonClick(TrackerButton button, Vector3 origin_, Vector3 direction_)
    {
        //Check to see if the raycast has intersected with an object
        if ((Physics.Raycast(origin, direction, out hit)))
        {
            //Get the current scale of the object
            Vector3 scale = hit.transform.localScale;
            if (button == TrackerButton.Button2)
            {
                hit.transform.localScale = new Vector3(scale.x + scale.x / 7, scale.y + scale.y / 7, scale.z + scale.z / 7);
            }

            else if (button == TrackerButton.Button3)
            {
                hit.transform.localScale = new Vector3(scale.x - scale.x / 7, scale.y - scale.y / 7, scale.z - scale.z / 7);
            }

        }
    }

    /// <summary>
    /// Asyncrounous method that moves whatever object the raycast from the wand has hit.
    /// </summary>
    /// <returns></returns>
    public void ButtonDrag(RaycastHit hit_, Vector3 offset_, Vector3 origin_, Vector3 direction_)
    {
            //Set the objects rotation equal to the wands 
            hit_.transform.eulerAngles = wandObject.transform.eulerAngles;
            
            //Set the direction of the wand.
            direction = wandObject.transform.forward;   //NOTE: hit.point does not update...

            //offset = hit.transform.position - hit.point;
            //Set the transform of the object hit
            Vector3 number = origin_ + (direction * hit_.distance) + offset_;
            hit_.transform.position = origin_ + (direction * hit_.distance) + offset_;
    }

   
    ///////////Unimplemented Functions /////////////////// 

    public void init()
    {
        throw new NotImplementedException();
    }

    public void shutDown()
    {
        //throw new NotImplementedException();
    }

    public void Analog(double x, double y)
    {
        throw new NotImplementedException();
    }

    public string ToolName
    {
        get
        {
            return "Grabber";
        }
    }
}




