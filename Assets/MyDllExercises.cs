using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class MyDllExercises : MonoBehaviour
{
    public enum Exercise
    {
        Uno,
        Dos,
        Tres,
        Cuatro,
        Cinco,
        Seis,
        Siete,
        Ocho,
        Nueve,
        Diez
    }

    public Exercise exercise = Exercise.Uno;
    public Vector3 a;
    public Vector3 b;
    public Color defaultVectorColor = Color.red;

    private Vec3 vectorA;
    private Vec3 vectorB;
    private Vec3 vectorC;
    private float time = 0;
    private const int timeLimit = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
