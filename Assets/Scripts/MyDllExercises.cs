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

    void Start()
    {
        Vector3Debugger.AddVector(vectorA, Color.magenta, "A");
        Vector3Debugger.EnableEditorView("A");
        Vector3Debugger.AddVector(vectorB, Color.yellow, "B");
        Vector3Debugger.EnableEditorView("B");
        Vector3Debugger.AddVector(vectorC, VectorColor, "C");
        Vector3Debugger.EnableEditorView("C");
    }

    void Update()
    {
        vectorA = new Vec3(a);
        vectorB = new Vec3(b);

        switch (exercise)
        {
            case Exercise.Uno:
                
                //Summary
                //Sum between two vectors

                vectorC = vectorA + vectorB;

                break;

            case Exercise.Dos:

                //Summary
                //Substraction between two vectors

                vectorC = vectorB - vectorA;

                break;

            case Exercise.Tres:

                //Summary
                //Multiplication between each member of each vector

                vectorC = new Vec3(vectorA.x * vectorB.x, vectorA.y * vectorB.y, vectorA.z * vectorB.z);

                break;

            case Exercise.Cuatro:

                //Summary
                //Cross product between vectors, but from B to A
                //New perpendicular vector

                vectorC = Vec3.Cross(vectorB, vectorA);

                break;

            case Exercise.Cinco:

                //Summary
                //Lerp function that goes from A to B based on time variable
                //Little loop that lets me reset time if it is higher than 1, this is because it is clamped between [0,1]

                time = time > 1 ? 0 : time + Time.deltaTime;

                vectorC = Vec3.Lerp(vectorA, vectorB, time);

                break;

            case Exercise.Seis:

                //Summary
                //Max function that selects the maximum value from each vector and compares them
                //Once selected it chooses the higher value and returns a new vector with these values

                vectorC = Vec3.Max(vectorA, vectorB);

                break;

            case Exercise.Siete:

                //Summary
                //Project function between A and B
                //The result is a vector that is the projection vector A onto the direction of vector B

                vectorC = Vec3.Project(vectorA, vectorB);

                break;

            case Exercise.Ocho:

                //Summary
                //First vector A and vector B are added, the result is the displacement vector between the 2
                //Second the displacement vector is normalized, which means it scales the vetor to have a magnitude of 1 while preserving its direction
                //Third the distance between vector A and B is calculated
                //Fourth the normalized vector is multiplied by the distance, as a result it gives a vector with the same direction as vector A and B, but its magnitude is equal to the distance between both points
                //The displacement of a vector (vector de desplazamiento) is the change of position or the displacement between two points

                vectorC = Vec3.Normalize(vectorA + vectorB) * Vec3.Distance(vectorA, vectorB);//Difiere en valores negativos

                break;

            case Exercise.Nueve:

                //Summary
                //Reflect function between A and the vector B normalized
                //The vector B needs to be normalized in order to have a magnitude of 1, so the exercise works
                //It will basically give us the direction of the vector without being influenced by its length

                vectorC = Vec3.Reflect(vectorA, Vec3.Normalize(vectorB));

                break;

            case Exercise.Diez:

                //Summary
                //Lerp Unclamped function that goes from B to A based on time variable
                //Little loop that lets me reset time if it is higher than timeLimit variablem which I set at 10
                //Same as before but now it goes backwards and with more time

                time = time > timeLimit ? 0 : time + Time.deltaTime;

                vectorC = Vec3.LerpUnclamped(vectorB, vectorA, time);

                break;

            default:

                break;
        }

        Vector3Debugger.UpdatePosition("A", TransformVec3ToVector3(vectorA));
        Vector3Debugger.UpdatePosition("B", TransformVec3ToVector3(vectorB));
        Vector3Debugger.UpdatePosition("C", TransformVec3ToVector3(vectorC));
    }

    Vector3 TransformVec3ToVector3(Vec3 vector)
    {
        return new Vector3(vector.x, vector.y, vector.z);
    }
}
