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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
