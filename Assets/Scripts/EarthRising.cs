using UnityEngine;
using System.Collections;

public class EarthRising : MonoBehaviour
{
    public Camera cam;
    Mesh m;
    Vector3[] vertices, verticesInit;

    void Start()
    {
         m = GetComponent<MeshFilter>().mesh;
        vertices = m.vertices;
        verticesInit = m.vertices;
        
    }

    void Update()
    {
        //cam = GetComponent<Camera>();

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            return;

        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        //Debug.Log(hit.collider.gameObject.name);

        /*if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            return;*/

       

        Vector3 pointSphere = transform.InverseTransformPoint(hit.point);
        float distMin = Vector3.Distance(vertices[0], pointSphere);
        int indexMin = 0;

        for (int i = 1; i < vertices.Length; i++)
        {
            if (Vector3.Distance(vertices[i], pointSphere) < distMin)
            {
                //Debug.Log(vertices[i].x);
                distMin = Vector3.Distance(vertices[i], pointSphere);
                indexMin = i;
            }
        }

        if (Input.GetMouseButton(0) == true)
        {
            vertices[indexMin] += m.normals[indexMin] / 1000;
        }
        else if (Input.GetMouseButton(1) == true)
        {
            vertices[indexMin] -= m.normals[indexMin] / 1000;
        }
           // Debug.Log(vertices[indexMin].x);
        // Debug.Log(hit.collider.gameObject.name);
        //  Debug.Log(pointSphere.x);

        float diff = vertices[indexMin].z - verticesInit[indexMin].z;
        Debug.Log(diff);
/*
        if (-(diff) > 0.01)
        {
            m.colors[indexMin] = Color.red;
        }*/

        //Vector3 pointSphereNormal = 

        /*
         m.vertices;
         m.normals;
         */
        m.vertices = vertices;
        m.RecalculateBounds();
    }
}
