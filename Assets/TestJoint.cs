using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJoint : MonoBehaviour
{
    public Rigidbody rb;

    public Vector3 Force;

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Force, ForceMode.Impulse);
        }
    }
}
