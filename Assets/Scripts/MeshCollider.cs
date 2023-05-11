using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using System.Linq;
using System;

public class MeshCollider : MonoBehaviour
{
    public List<PlaneCustom> planes;
    public bool isMeshActive;
    public List<Vec3> pointsInsideMesh;
    private List<Vec3> pointsToCheck;
    public Vec3 nearestPoint;

    // Summary:
    // Create a Ray struct that will be used later on to check if the point is in the plane
    // Add an origin point, and a dest point as well as a Ray constructor
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

    // Summary:
    // Starts by creating every plane that will be used in the code
    // Checks for 3 vertices so that the plane can be created using the 3 point plane constructor and adds it to the list
    // Sets the plane passed by parameter based on a point inside the plane and the normal to orient it
    // Aux is the normal(that is obtained by calculatingg the normals of the mesh), and the normal multiplied by the distance gives me the point
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
    // Then the nearest point to the plane is checked
    // Then the points to check are added 
    // Lastly if the points are inside of the mesh they are added
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

        AddPointsToCheck();
        Debug.Log("Points: " + pointsToCheck.Count);
        AddPointsInsideOfMesh();

        Debug.Log("Colliding Points: " + pointsInsideMesh.Count);
    }

    // Summary:
    // Gets the nearest point by checking x, y and z points and then assigns them to the nearestPoint Vec3
    private void NearestPoint()
    {
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

    // Summary:
    // Empties the pointsToCheck Vec3 list, and sets the grid size to the grid size minus 1
    // Then an x, y and z max and x, y and z min point are calculated with the MaxGridSize function
    // After this 6 points are checked with the Clamp function so they don't exceed neither the gridSize or go lower than 0
    // Finally there is a loop that goes from the min value up to the max of each x, y and z point and adds thhe point to the pointsToCheck list
    private void AddPointsToCheck()
    {
        pointsToCheck.Clear();

        int maxPointX = MaxGridSize(nearestPoint.x, transform.localScale.x, 3, 1);
        int maxPointY = MaxGridSize(nearestPoint.y, transform.localScale.y, 3, 1);
        int maxPointZ = MaxGridSize(nearestPoint.z, transform.localScale.z, 3, 1);
        int minPointX = MaxGridSize(nearestPoint.x, transform.localScale.x, 3, -1);
        int minPointY = MaxGridSize(nearestPoint.y, transform.localScale.y, 3, -1);
        int minPointZ = MaxGridSize(nearestPoint.z, transform.localScale.z, 3, -1);

        var gridSize = Grid.Size - 1;

        maxPointX = Mathf.Clamp(maxPointX, 0, gridSize);
        maxPointY = Mathf.Clamp(maxPointY, 0, gridSize);
        maxPointZ = Mathf.Clamp(maxPointZ, 0, gridSize);
        minPointX = Mathf.Clamp(minPointX, 0, gridSize);
        minPointY = Mathf.Clamp(minPointY, 0, gridSize);
        minPointZ = Mathf.Clamp(minPointZ, 0, gridSize);

        for (int x = minPointX; x < maxPointX; x++)
        {
            for (int y = minPointY; y < maxPointY; y++)
            {
                for (int z = minPointZ; z < maxPointZ; z++)
                {
                    pointsToCheck.Add(Grid.grid[x, y, z]);
                }
            }
        }
    }

    // Summary:
    // This function needs the nearestPoint, the scale so this can be worked with different scales, the number of points it will check and a value to check positive or negative values
    // Once it has all of them, it makes the nearesPoint as an integer
    // And gets the sum between numberOfPoints that will be checked and the multiplication between scale and the sign which will dtermine if it is positive or negative
    // As a return value both ints get added up
    private int MaxGridSize(float nearestPoint, float scale, int numberOfPoints, int signOfTheNumber)
    {
        int x = (int)nearestPoint;
        
        int y = (numberOfPoints + (int)scale - 1) * signOfTheNumber;
        
        return x + y;
    }

    // Summary:
    // Empties the pointsInsideMesh Vec3 list
    // Begins a loop where every point in pointsToCheck is checked
    // The Ray constructor is used to draw a Ray from the point to a Vec3 forward with a length of my desire
    // Counter is declared, that will later on be used
    // Begins another loop that checks every plane in the planes list
    // Checks if the point is in the plane using the IsPointInPlane function
    // Checks if the plane is valid in the IsValePlane, and if it is, the counter adds one up
    // If the counter is odd then the point is added to the pointsInsideMesh list
    private void AddPointsInsideOfMesh()
    {
        pointsInsideMesh.Clear();

        foreach (var point in pointsToCheck)
        {
            Ray pointRay = new Ray(point, Vec3.Forward * 10f);

            var counter = 0;

            foreach (var plane in planes)
            {
                if (IsPointInPlane(plane, pointRay, out Vec3 t))
                {
                    if (IsValidPlane(plane, t))
                    {
                        counter++;
                    }
                }
            }
            Debug.Log("Counter is: " + counter);
            if (counter % 2 == 1)
            {
                pointsInsideMesh.Add(point);
                Debug.Log("apa");
            }
        }
    }

    // Summary:
    // The function asks for a plane, a ray and a point
    // The function uses an out keyword in one of the parameters to make the program aware that the variable will be initialized and set withitn the function 
    // The point is set to zero because we will need the intersection point
    // The denominator value is the dot product of the normal and the dest of the ray, because, this will give how close both are
    // If the absolute value of the denominator is bigger than epsilon it continues, else, the would be perpendicular
    // Then two aux variables are created for later use, the first one is the multiplication between the normal and the distance
    // These two are multiplied to give the signed distance from a point to the plane
    // If the signed distance is positive the point is on the side of the plane where the normal vector points, if negtaive it would be the other way around 
    // The second one does the substraction between that value and signed distance, this essentially translates the distance to be relative to the point
    // Then the Dot product is done again between the aux2 and the normal, which will give as a result the signed distance from the origin point to the plane
    // If this divided by the denominator is greater or equal to epsilon then the point is within the plane or very close, because we use epsilon
    // Finally the point is equalized to the origin of the ray to an scaled dest
    // As a result this point will be a point that will be located at "t" units from the origin in the direction of the ray's dest
    private bool IsPointInPlane(PlaneCustom plane, Ray pointRay, out Vec3 point)
    {
        point = Vec3.Zero;

        float denominator = Vec3.Dot(plane.normal, pointRay.dest);

        if (Mathf.Abs(denominator) > Vec3.epsilon)
        {
            Vec3 aux = plane.normal * plane.distance;
            Vec3 aux2 = aux - pointRay.origin;

            float t = Vec3.Dot(aux2, plane.normal) / denominator;

            if (t >= Vec3.epsilon)
            {
                point = pointRay.origin + pointRay.dest * t;

                return true;
            }
        }

        return false;
    }

    // Summary:
    // Will check if the plane is valid, it asks for a plane and a point
    // It starts by defining 6 floats, 3 for x and 3 for y
    // Then it calculates the area of a triangle made with the 6 floats with the triangle point collision equation
    // http://www.jeffreythompson.org/collision-detection/tri-point.php#:~:text=To%20test%20if%20a%20point,the%20corners%20of%20the%20triangle
    // Heron's formula = Abs((y1 - x1) * (z2 - x2) - (z1 - x1) * (y2 - x2)) = float
    // This will give the area off the triangle
    // Area from the triangle made with a point = Abs((x1 - point.x) * (y2 - point.x) - (x2 - point.x) * (y1 - point.x)) = float
    // This will give the area of the triangle made from the point, it is an example of one of the cases
    // If the sum of the areas is equal to the original, then we know we are inside of the triangle else it is not
    private bool IsValidPlane(PlaneCustom plane, Vec3 point)
    {
        float x1 = plane.pointA.x;
        float x2 = plane.pointB.x;
        float x3 = plane.pointC.x;

        float y1 = plane.pointA.y;
        float y2 = plane.pointB.y;
        float y3 = plane.pointC.y;

        float completeArea = Mathf.Abs((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1));

        float firstArea = Mathf.Abs((x1 - point.x) * (y2 - point.y) - (x2 - point.x) * (y1 - point.y));
        float secondArea = Mathf.Abs((x2 - point.x) * (y3 - point.y) - (x3 - point.x) * (y2 - point.y));
        float thirdArea = Mathf.Abs((x3 - point.x) * (y1 - point.y) - (x1 - point.x) * (y3 - point.y));

        return Mathf.Abs(firstArea + secondArea + thirdArea - completeArea) < Vec3.epsilon;
    }

    // Summary:
    // This function asks for a Vec3 point
    // It will compare if the given point is on the other mesh as well
    // The function will return true if so
    // It uses the Any() function which checks if any element in the collection satisfies a specified condition, such as pointsInsideMesh
    // The lambda expression will compare each element in pointsInsideMesh with point to see which one is equal
    public bool CheckPointsAgainstAnotherMesh(Vec3 point)
    {
        return pointsInsideMesh.Any(pointsInsideMesh => pointsInsideMesh == point);
    }

    private void OnDrawGizmos()
    {
        if (isMeshActive)
        {
            var color = Color.blue;

            foreach (var plane in planes)
            {
                DrawPlane(plane.normal * plane.distance, plane.normal, color);
            }

            foreach (var point in pointsInsideMesh)
            {
                Gizmos.DrawRay(point, Vec3.Forward * 10f);
            }
        }
    }

    private void DrawPlane(Vec3 pos, Vec3 normal, Color color)
    {
        Vec3 vector;

        if (normal.normalized != Vec3.Forward)
        {
            vector = Vec3.Cross(normal, Vec3.Forward).normalized * normal.magnitude;
        }
        else
        {
            vector = Vec3.Cross(normal, Vec3.Up).normalized * normal.magnitude;
        }

        var corner = pos + vector;
        var corner2 = pos - vector;

        var rot = Quaternion.AngleAxis(90.0f, normal);

        vector = rot * vector;

        var corner1 = pos + vector;
        var corner3 = pos - vector;

        Debug.DrawLine(corner, corner2, color);
        Debug.DrawLine(corner1, corner3, color);
        Debug.DrawLine(corner, corner1, color);
        Debug.DrawLine(corner1, corner2, color);
        Debug.DrawLine(corner2, corner3, color);
        Debug.DrawLine(corner3, corner, color);

        Debug.DrawRay(pos, normal, Color.cyan);
    }
}
