using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using UnityEngine.XR;

public class feedback05M : MonoBehaviour
{
    public CapsuleCollider capsula;
    //public MeshCollider camion;
    public GameObject camion;

    //Collision with camion and vibration
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == camion.name)
        {
            transform.parent.GetComponent<feedback>().CollisionDetectedOnStay(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == camion.name)
        {
            transform.parent.GetComponent<feedback>().CollisionDetectedExit(this);
        }
    }

    //Collision with camion for metrics
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == camion.name)
        {
            transform.parent.GetComponent<feedback>().CollisionDetectedCounter(this);
        }
    }

}