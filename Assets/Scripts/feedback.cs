using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using UnityEngine.XR;

public class feedback : MonoBehaviour
{
    // XR settings
    // Left
    private XRNode xRNode = XRNode.LeftHand;
    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice device;
    // Right
    private XRNode xRNode_Right = XRNode.RightHand;
    private InputDevice device_right;

    // Vibrations
    private float int_d1 = 0.15f;
    private float int_d05 = 0.5f;
    private float int_dcrash = 1.0f;

    // Flags
    public bool visualFeedbackOnly;
    private bool gripcol = false;
    private bool d1 = false;
    private bool d05 = false;
    private bool dcrash = false;

    // Counters
    private int count_d1 = 0;
    private int count_d05 = 0;
    private int count_dcrash = 0;

    // Time
    private float time1m = 0.0f;
    private float time05m = 0.0f;
    private float timecrash = 0.0f;

    private float time1_ini;

    private bool silly_flag1 = false;


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

    // Every frame
    void Update()
    {
        if (d1 && !d05 && !dcrash && gripcol)
        {
            contrVibration(int_d1, visualFeedbackOnly);
        }
        if (d1 && d05 && !dcrash && gripcol)
            contrVibration(int_d05, visualFeedbackOnly);
        if (d1 && d05 && dcrash && gripcol)
            contrVibration(int_dcrash, visualFeedbackOnly);


    }

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        Debug.Log("Times log 1 meter :" + count_d1.ToString()); 
        //Debug.Log("Duration log 1 meter :" + time1m + " seconds"); 
        Debug.Log("Times log 0.5 meter :" + count_d05.ToString());
        Debug.Log("Times log crash meter :" + count_dcrash.ToString()); 
    }


    // Collision with gripper
    public void CollisionDetectedEnter(colgripper childScript)
     {
        //Debug.Log("child1 collided"); // Debug
        gripcol = true;
     }
     public void CollisionDetectedExit(colgripper childScript)
     {
        //Debug.Log("child1 collided"); // Debug
        gripcol = false;
     }


    // Collision check for vibration and time 
    public void CollisionDetectedOnStay(feedback1M childScript)
     {
        //Debug.Log("child1 collided"); // Debug
        d1 = true;
     }
     public void CollisionDetectedExit(feedback1M childScript)
     {
        //Debug.Log("child1 collided"); // Debug
        d1 = false;
     }

    public void CollisionDetectedOnStay(feedback05M childScript)
     {
        //Debug.Log("child2 collided"); // Debug
        d05 = true;
     }
    public void CollisionDetectedExit(feedback05M childScript)
     {
        //Debug.Log("child2 collided"); // Debug
        d05 = false;
     }  

    public void CollisionDetectedOnStay(feedbackCrash childScript)
     {
        //Debug.Log("child3 collided"); // Debug
        dcrash = true;
     }
    public void CollisionDetectedExit(feedbackCrash childScript)
     {
        //Debug.Log("child3 collided"); // Debug
        dcrash = false;
     }  


    // Collision counter to get metrics
    public void CollisionDetectedCounter(feedback1M childScript)
     {
        count_d1++;
        //Debug.Log("Times log 1 meter :" + count_d1.ToString()); 
     }

    public void CollisionDetectedCounter(feedback05M childScript)
     {
        count_d05++;
        //Debug.Log("Times log 0.5 meter :" + count_d05.ToString()); 
     } 

    public void CollisionDetectedCounter(feedbackCrash childScript)
     {
        count_dcrash++;
        //Debug.Log("Times log crash meter :" + count_dcrash.ToString()); 
     } 
 

     // Vibration function
     private void contrVibration(float intensity, bool visualFeedbackOnly)
     {
        if (visualFeedbackOnly) // no vibration
            return;

        //XR settings 
        if (!device.isValid)
        {
            GetDevice();
        }

        HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
                if (capabilities.supportsImpulse)
                    device.SendHapticImpulse(0, intensity, 0.5f); // Channel, amplitude (0 -> 1), duration in second

            if (device_right.TryGetHapticCapabilities(out capabilities))
                if (capabilities.supportsImpulse)
                    device_right.SendHapticImpulse(0, intensity, 0.5f); // Channel, amplitude (0 -> 1), duration in second

     }

}