using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using UnityEngine.XR;

public class colgripper : MonoBehaviour
{
    public CapsuleCollider capsula;
    //public MeshCollider gripper;
    public GameObject gripper;

    //Collision with gripper and vibration
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == gripper.name)
        {
            transform.parent.GetComponent<feedback>().CollisionDetectedExit(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == gripper.name)
        {
            transform.parent.GetComponent<feedback>().CollisionDetectedEnter(this);
        }
    }

}