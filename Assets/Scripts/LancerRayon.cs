using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LancerRayon : MonoBehaviour
{
    public Transform hand;
    private LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        print("chibre");
        if ( (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5 && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < 0.1) )
        {
            print("tout court");
            Ray ray = new Ray(hand.position, hand.forward);
            RaycastHit hit;
            line.enabled = true;
            Vector3[] Tab = new Vector3[2];
            Vector3 Depart = new Vector3(hand.position.x + 0.02f, hand.position.y, hand.position.z + 0.02f);
            Tab[0] = Depart;
            Tab[1] = (Depart + hand.forward*10);
            line.SetPositions(Tab);
            line.SetWidth(0.01f, 0.01f);
          
            if(Physics.Raycast(ray, out hit, 1000.0f)){
                if (OVRInput.Get(OVRInput.Button.One))
                {
                    print("Coucou");
                }
            }
        }
        else
        {
            line.enabled = false;
        }
    }
}
