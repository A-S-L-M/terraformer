using UnityEngine;
using System.Collections;

public class CloudsCreation : MonoBehaviour
{
    public Camera cam;
    Mesh m, c;
    Vector3[] vertices, verticesInit, verticesClouds;
    int indexClouds = 0;

    Color[] colors;

    void Start()
    {
        m = GetComponent<MeshFilter>().mesh;
        vertices = m.vertices;
        verticesClouds = m.vertices;
        verticesInit = m.vertices;
        colors = new Color[verticesClouds.Length];


    }

    void Update()
    {
        //cam = GetComponent<Camera>();
        char clicked = LancerRayon.clicked;
        if (clicked != 'C')
            return;

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            return;

        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;





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
    
            verticesClouds[indexClouds].z = vertices[indexMin].z;
           
            Debug.Log(vertices[indexMin].z);
            Debug.Log(verticesClouds[indexClouds].z);
             indexClouds++;
        }
        else if (Input.GetMouseButton(1) == true)
        {
            vertices[indexMin] -= m.normals[indexMin] / 1000;
        }
         Debug.Log(verticesClouds.Length);



        for (int i = 0; i < verticesClouds.Length; i++)
            colors[i] = Color.white;

        c = GetComponent<MeshFilter>().mesh;
        m.colors = colors;

       // float diff = vertices[indexMin].z - verticesInit[indexMin].z;
      //  Debug.Log(diff);

        m.vertices = vertices;
        m.RecalculateBounds();
        c.RecalculateBounds();
    }
}
