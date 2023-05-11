using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class MeshCollider : MonoBehaviour
{
    private List<PlaneCustom> planes;
    public bool isMeshActive;
    public List<Vec3> pointsInsideMesh;
    public List<Vec3> pointsToCheck;
    public Vec3 nearestPoint;

    struct Ray
    {
        public Vec3 origin;
        public Vec3 dest;

        public Ray(Vec3 origin, Vec3 dest)
        {
            this.origin = origin;
            this.dest = dest;
        }
    }

    private void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        planes = new List<PlaneCustom>();
        pointsInsideMesh = new List<Vec3>();
        pointsToCheck = new List<Vec3>();

        for (int i = 0; i < mesh.GetIndices(0).Length; i += 3)
        {
            Vec3 auxA = new Vec3(mesh.vertices[mesh.GetIndices(0)[i]]);
            Vec3 auxB = new Vec3(mesh.vertices[mesh.GetIndices(0)[i + 1]]);
            Vec3 auxC = new Vec3(mesh.vertices[mesh.GetIndices(0)[i + 2]]);

            planes.Add(new PlaneCustom(auxA, auxB, auxC));
        }
        for (int i = 0; i < planes.Count; i++)
        {
            Vec3 aux = new Vec3(mesh.normals[i]);

            planes[i].SetNormalAndPosition(aux, planes[i].normal * planes[i].distance);
        }
    }

    // Summary:
    // Create an empty plane, if there is an already existing plane in the list it gets equalized
    // In the loop the actaul vertex are obtained
    // Afterwards the plane is created with the vertices
    // The normal is flipped so the plane isn't inverted
    // The plane is added to the list
    // Then the nearest point to the plane
    private void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        planes.Clear();

        for (int i = 0; i < mesh.GetIndices(0).Length; i += 3)
        {
            Vec3 vertexA = new Vec3(transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i]]));
            Vec3 vertexB = new Vec3(transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i + 1]]));
            Vec3 vertexC = new Vec3(transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i + 2]]));

            var plane = new PlaneCustom(vertexA, vertexB, vertexC);
            plane.normal *= -1;
            planes.Add(plane);
        }

        for (int i = 0; i < planes.Count; i++)
        {
            planes[i].Flip();
        }

        NearestPoint();

        Debug.Log("Colliding Points: " + pointsInsideMesh.Count + ", " + gameObject);
    }

    // Summary:
    // Gets the nearest point by checking x, y and z points and then assigns them to the nearestPoint Vec3
    public void NearestPoint()
    {
        var nearPoint = nearestPoint;

        var x = ValueNearestPosition(transform.position.x);
        var y = ValueNearestPosition(transform.position.y);
        var z = ValueNearestPosition(transform.position.z);

        nearestPoint = Grid.grid[x, y, z];
    }

    // Summary:
    // Gets the nearest position value
    // The position is divided by the grids delta
    // Then it is checked if the value minus the absolute value of he point (so we get a value between 0 and 0.99), if it is less than half we add 1, if not we return that value
    // After it is checkd if the value is clamped between 0 and the grid size minus 1
    // As how clamp works if the value is between those values it returns the value, if not it returns the maximum or minimum
    // The value is returned as an int
    private int ValueNearestPosition(float position)
    {
        float value;

        var aux = position / Grid.Delta;
        
        if (aux - (int)aux > 0.5f)
        {
            value = aux + 1.0f;
        }
        else
        {
            value = aux;
        }

        value = Mathf.Clamp(value, 0, Grid.Size - 1);

        return (int)value;
    }

    

    private void OnDestroy()
    {
        isMeshActive = false;
    }
}
