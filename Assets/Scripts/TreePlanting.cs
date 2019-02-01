using UnityEngine;
using System.Collections;

public class TreePlanting : MonoBehaviour
{
    public Camera cam;
    Mesh m;
    Vector3[] vertices, verticesInit;
    Vector3 treeSize = new Vector3(0.05f, 0.05f, 0.05f);
    public GameObject myobject;

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
        //Debug.Log(m.vertices[1]);
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
            myobject = Resources.Load("Tree10/Tree10_2") as GameObject;
            
            Instantiate(myobject );
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, m.normals[indexMin]);
            Vector3 position = hit.point;//transform.InverseTransformPoint(hit.point);

            myobject.gameObject.transform.localScale = treeSize;
            //  transform.worldToLocalMatrix(position);
            myobject.transform.SetPositionAndRotation(position, rotation);

           // Debug.Log(position);
           // Debug.Log(myobject.transform.localPosition);

        }
        else if (Input.GetMouseButton(1) == true)
        {
            vertices[indexMin] -= m.normals[indexMin] / 1000;
        }

        //  Debug.Log(pointSphere.x);

        float diff = vertices[indexMin].z - verticesInit[indexMin].z;
        //Debug.Log(diff);

        m.vertices = vertices;
        m.RecalculateBounds();
    }
}
