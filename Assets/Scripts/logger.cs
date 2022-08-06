using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using UnityEngine.XR;

using System;
using System.IO;
using System.Text;


public class logger : MonoBehaviour
{
    // XR settings
    // Left
    private XRNode xRNode = XRNode.LeftHand;
    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice device;
    // Right
     private XRNode xRNode_Right = XRNode.RightHand;
     private InputDevice device_right;

    //to avoid repeat readings
    private bool triggerIsPressed;
    private bool primary2DAxisIsChosen;
    private Vector2 primary2DAxisValue = Vector2.zero;
    private Vector2 prevPrimary2DAxisValue;

    private bool primary2DAxisIsChosen_right;
    private Vector2 primary2DAxisValue_right = Vector2.zero;
    private Vector2 prevPrimary2DAxisValue_right;

    // Loggers
    public List<float> right0 = new List<float>();
    public List<float> right1 = new List<float>();

    public List<float> left0 = new List<float>();
    public List<float> left1 = new List<float>();


    // XR methods
    void GetDevice()
    {
        // Left
        InputDevices.GetDevicesAtXRNode(xRNode, devices);
        device = devices.FirstOrDefault();

        // Right
        InputDevices.GetDevicesAtXRNode(xRNode_Right, devices);
        device_right = devices.FirstOrDefault();
    }

    void onEnable()
    {
        if (!device.isValid)
        {
            GetDevice();
        }
    }


    void FixedUpdate()
    {
        //XR settings 
        if (!device.isValid)
        {
            GetDevice();
        }

         // RIGHT Joystick
        // capturing primary 2D Axis changes and release
        InputFeatureUsage<Vector2> primary2DAxisUsage_right = CommonUsages.primary2DAxis;
        // make sure the value is not zero and that it has changed
        if (primary2DAxisValue_right != prevPrimary2DAxisValue_right)
        {
            primary2DAxisIsChosen_right = false;
        }

        if (device_right.TryGetFeatureValue(primary2DAxisUsage_right, out primary2DAxisValue_right) && primary2DAxisValue_right != Vector2.zero && !primary2DAxisIsChosen_right)
        {
            prevPrimary2DAxisValue_right = primary2DAxisValue_right;
            primary2DAxisIsChosen_right = true;  
            right0.Add(primary2DAxisValue_right[0]); // Hor;
            right1.Add(primary2DAxisValue_right[1]); // Vertical;
        }
        else if (primary2DAxisValue_right == Vector2.zero && primary2DAxisIsChosen_right)
        {
            prevPrimary2DAxisValue_right = primary2DAxisValue_right;
            primary2DAxisIsChosen_right = false;
        }


        // LEFT Joystick
        // capturing primary 2D Axis changes and release
        InputFeatureUsage<Vector2> primary2DAxisUsage = CommonUsages.primary2DAxis;
        // make sure the value is not zero and that it has changed
        if (primary2DAxisValue != prevPrimary2DAxisValue)
        {
            primary2DAxisIsChosen = false;
        }

        if (device.TryGetFeatureValue(primary2DAxisUsage, out primary2DAxisValue) && primary2DAxisValue != Vector2.zero && !primary2DAxisIsChosen)
        {
            prevPrimary2DAxisValue = primary2DAxisValue;
            primary2DAxisIsChosen = true;
            left0.Add(primary2DAxisValue[0]); // Hor;
            left1.Add(primary2DAxisValue[1]); // Vertical;  
        }
        else if (primary2DAxisValue == Vector2.zero && primary2DAxisIsChosen)
        {
            prevPrimary2DAxisValue = primary2DAxisValue;
            primary2DAxisIsChosen = false;
        }


    }

    void Save()
    {
        // Debug.Log(Application.persistentDataPath);
        // Paths
        string path_rightvertical = @"C:\Users\sibl\Proyecto\SAVES\save_rightvertical.txt";
        string path_righthor = @"C:\Users\sibl\Proyecto\SAVES\save_righthor.txt";
        string path_leftvertical = @"C:\Users\sibl\Proyecto\SAVES\save_leftvertical.txt";
        string path_lefthor = @"C:\Users\sibl\Proyecto\SAVES\save_lefthor.txt";

        // Files name
        string saveRight0 = "Right_Hor";
        string saveRight1 = "Right_Vertical";
        string saveLeft0 = "Left_Hor";
        string saveLeft1 = "Left_Vertical";

        // Right hor
        for(int i=0; i < right0.Count(); i++)
        {
            string saveText = right0[i].ToString();//JsonUtility.ToJson(right1[i]);
            saveRight0 = saveRight0 + ";" + saveText; 
        }
        File.WriteAllText(path_righthor, saveRight0);

        // Right vertical
        for(int i=0; i < right1.Count(); i++)
        {
            string saveText = right1[i].ToString();//JsonUtility.ToJson(right1[i]);
            saveRight1 = saveRight1 + ";" + saveText; 
        }
        File.WriteAllText(path_rightvertical, saveRight1);


        // Left hor
        for(int i=0; i < left0.Count(); i++)
        {
            string saveText = left0[i].ToString();//JsonUtility.ToJson(right1[i]);
            saveLeft0 = saveLeft0 + ";" + saveText; 
        }
        File.WriteAllText(path_lefthor, saveLeft0);

        // Left vertical
        for(int i=0; i < left1.Count(); i++)
        {
            string saveText = left1[i].ToString();//JsonUtility.ToJson(right1[i]);
            saveLeft1 = saveLeft1 + ";" + saveText; 
        }
        File.WriteAllText(path_leftvertical, saveLeft1);

        
    }

    void OnApplicationQuit()
    {
        Save(); 
    }


}