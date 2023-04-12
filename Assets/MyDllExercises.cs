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
        vectorA = new Vec3(a);
        vectorB = new Vec3(b);

        Vector3Debugger.UpdatePosition("A", TransformVec3ToVector3(vectorA));
        Vector3Debugger.UpdatePosition("B", TransformVec3ToVector3(vectorB));
        Vector3Debugger.UpdatePosition("C", TransformVec3ToVector3(vectorC));

        switch (exercise)
        {
            case Exercise.Uno:
                break;
            case Exercise.Dos:
                break;
            case Exercise.Tres:
                break;
            case Exercise.Cuatro:
                break;
            case Exercise.Cinco:
                break;
            case Exercise.Seis:
                break;
            case Exercise.Siete:
                break;
            case Exercise.Ocho:
                break;
            case Exercise.Nueve:
                break;
            case Exercise.Diez:
                break;
            default:
                break;
        }
    }

    Vector3 TransformVec3ToVector3(Vec3 vector)
    {
        return new Vector3(vector.x, vector.y, vector.z);
    }
}
