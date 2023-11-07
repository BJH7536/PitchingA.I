using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public Rigidbody rb;

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 5f, 0), ForceMode.Impulse);
        }
    }
}
