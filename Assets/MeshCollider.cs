using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class MeshCollider : MonoBehaviour
{
    private List<PlaneCustom> planes;
    public bool isMeshActive;
    public List<Vec3> pointsInsideMesh;
    public List<Vec3> previousVertex;
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
        
    }
}
