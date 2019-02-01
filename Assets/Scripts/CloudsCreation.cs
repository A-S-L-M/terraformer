using UnityEngine;
using System.Collections;

public class CloudsCreation : MonoBehaviour
{
    public Camera cam;
    Mesh m, c;
    Vector3[] vertices, verticesInit, verticesClouds;
    int indexClouds = 0;

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
            indexClouds++;
            verticesClouds[indexClouds] = vertices[indexMin];
           //Debug.Log(verticesClouds[indexMin].z);
        }
        else if (Input.GetMouseButton(1) == true)
        {
            vertices[indexMin] -= m.normals[indexMin] / 1000;
        }
        // Debug.Log(vertices[indexMin].x);


        Color[] colors = new Color[verticesClouds.Length];

        for (int i = 0; i < verticesClouds.Length; i++)
            colors[i] = Color.Lerp(Color.white, Color.red, verticesClouds[i].y);

        c = GetComponent<MeshFilter>().mesh;
        c.vertices = verticesClouds;

       // float diff = vertices[indexMin].z - verticesInit[indexMin].z;
      //  Debug.Log(diff);

        m.vertices = vertices;
        m.RecalculateBounds();
        c.RecalculateBounds();
    }
}
