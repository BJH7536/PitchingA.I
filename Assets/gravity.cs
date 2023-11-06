using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
    public Vector3 acc;
    public Vector3 velocity;
    public Vector3 initialVelocity;

    public Rigidbody rb;

    public bool standOn = false;

    void Start()
    {
        this.velocity = this.initialVelocity;
    }

    void FixedUpdate()
    {
        velocity += Physics.gravity * Time.deltaTime;  // �߷� ����
        transform.position += velocity * Time.deltaTime;  // �߷¿� ���� �ӵ��� ��ġ ������Ʈ
    }

}