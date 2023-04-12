using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using MathDebbuger;

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
    public Color VectorColor = Color.red;
    public Vector3 a;
    public Vector3 b;

    private Vec3 vectorA;
    private Vec3 vectorB;
    private Vec3 vectorC;
    private float time = 0;
    private const int timeLimit = 10;

    // Start is called before the first frame update
    void Start()
    {
        Vector3Debugger.EnableEditorView();

        Vector3Debugger.AddVector(vectorA, Color.magenta, "A");
        Vector3Debugger.AddVector(vectorB, Color.yellow, "B");
        Vector3Debugger.AddVector(vectorC, VectorColor, "C");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
