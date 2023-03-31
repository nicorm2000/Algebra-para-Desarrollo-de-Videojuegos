using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public Vector3 j = new Vector3(0, 0, 5);
    public float m = 2;
    // Start is called before the first frame update
    void Start()
    {
        print(Vector3.Magnitude(j));
        print(Vector3.ClampMagnitude(j, m));
        print(Vector3.Magnitude(j));

        print(Mathf.Acos(3));

        print(Vector3.Reflect(new Vector3(2,4,5), new Vector3(1,2,3)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
