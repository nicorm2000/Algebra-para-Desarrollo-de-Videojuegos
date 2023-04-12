using UnityEngine;
using CustomMath;
using MathDebbuger;
using MathDebbuger.Internals;

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

        //Preguntar que onda

        //Vector3Debugger.AddVector(vectorA, Color.magenta, "A");
        //Vector3Debugger.AddVector(vectorB, Color.yellow, "B");
        //Vector3Debugger.AddVector(vectorC, VectorColor, "C");
    }

    // Update is called once per frame
    void Update()
    {
        vectorA = new Vec3(a);
        vectorB = new Vec3(b);

        //Preguntar que onda

        //Vector3Debugger.UpdatePosition("A", TransformVec3ToVector3(vectorA));
        //Vector3Debugger.UpdatePosition("B", TransformVec3ToVector3(vectorB));
        //Vector3Debugger.UpdatePosition("C", TransformVec3ToVector3(vectorC));

        switch (exercise)
        {
            case Exercise.Uno:

                vectorC = vectorA + vectorB;

                Debug.Log(vectorC);

                break;

            case Exercise.Dos:

                vectorC = vectorB - vectorA;

                Debug.Log(vectorC);

                break;

            case Exercise.Tres:

                vectorC = new Vec3(vectorA.x * vectorB.x, vectorA.y * vectorB.y, vectorA.z * vectorB.z);

                Debug.Log(vectorC);

                break;

            case Exercise.Cuatro:

                vectorC = Vec3.Cross(vectorB, vectorA);

                Debug.Log(vectorC);

                break;

            case Exercise.Cinco:

                time = time > 1 ? 0 : time + Time.deltaTime;

                vectorC = Vec3.Lerp(vectorA, vectorB, time);

                Debug.Log(vectorC);

                break;

            case Exercise.Seis:

                vectorC = Vec3.Max(vectorA, vectorB);

                Debug.Log(vectorC);

                break;

            case Exercise.Siete:

                vectorC = Vec3.Project(vectorA, vectorB);

                Debug.Log(vectorC);

                break;

            case Exercise.Ocho:

                vectorC = Vec3.Normalize(vectorA + vectorB) * Vec3.Distance(vectorA, vectorB);

                Debug.Log(vectorC);

                break;

            case Exercise.Nueve:

                vectorC = Vec3.Reflect(vectorA, Vec3.Normalize(vectorB));

                Debug.Log(vectorC);

                break;

            case Exercise.Diez:

                time = time > timeLimit ? 0 : time + Time.deltaTime;

                vectorC = Vec3.LerpUnclamped(vectorB, vectorA, time);

                Debug.Log(vectorC);

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
