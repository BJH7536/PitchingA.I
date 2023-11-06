using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshUpdater : MonoBehaviour
{
    void Start()
    {
        meshRenderer = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        collider = transform.GetChild(0).GetComponent<MeshCollider>();
    }

    private float time = 0;
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 0.5f)
        {
            time = 0;
            UpdateCollider();
        }
    }

    SkinnedMeshRenderer meshRenderer;
    MeshCollider collider;

    public void UpdateCollider()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        collider.sharedMesh = null;
        collider.sharedMesh = colliderMesh;
    }
}