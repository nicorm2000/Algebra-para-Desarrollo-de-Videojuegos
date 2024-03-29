using UnityEngine;

namespace CustomMath
{
    public struct PlaneCustom
    {
        // Plane function -> AX + BY + CZ + D = 0
        // D = distance
        public Vec3 _normal;// The normal to the plane
        public Vec3 pointA;
        public Vec3 pointB;
        public Vec3 pointC;
        private float _distance;// The distance to the plane

        // Normal vector of the plane.
        public Vec3 normal 
        { 
            get { return _normal; } 
            set { _normal = value; } 
        }

        // The distance measured from the Plane to the origin, along the Plane's normal.
        public float distance 
        {
            get { return _distance; }
            set { _distance = value; } 
        }
        
        // Returns a copy of the plane that faces in the opposite direction.
        public PlaneCustom flipped 
        {
            get { return new PlaneCustom(-_normal, -_distance); }
        }

        public PlaneCustom(Vec3 normal, Vec3 point)
        {
            _normal = Vec3.Normalize(normal);
            _distance = Vec3.Dot(normal, point);

            pointA = point;
            pointB = point;
            pointC = point;
        }
        
        public PlaneCustom(Vec3 normal, float distance)
        {
            _normal = Vec3.Normalize(normal);
            _distance = distance;

            pointA = normal;
            pointB = normal;
            pointC = normal;
        }

        public PlaneCustom(Vec3 a, Vec3 b, Vec3 c)
        {
            _normal = Vec3.Normalize(Vec3.Cross(b - a, c - a));
            _distance = -Vec3.Dot(_normal, a);

            pointA = a;
            pointB = b;
            pointC = c;
        }

        // Summary:
        //     Returns a copy of the given plane that is moved in space by the given translation.
        // Parameters:
        //   plane:
        //     The plane to move in space.
        //   translation:
        //     The offset in space to move the plane with.
        // Returns:
        //     The translated plane.
        public static PlaneCustom Translate(PlaneCustom plane, Vec3 translation)
        {
            PlaneCustom planeCopy = new PlaneCustom(plane._normal, plane._distance);

            planeCopy.Translate(translation);

            return planeCopy;
        }

        // Summary:
        //     Moves the plane in space by the translation vector.
        // Parameters:
        //   translation:
        //     The offset in space to move the plane with.
        public void Translate(Vec3 translation)
        {
            _distance += Vec3.Dot(_normal, translation);
        }

        // Summary:
        //     For a given point returns the closest point on the plane.
        // Parameters:
        //   point:
        //     The point to project onto the plane.
        // Returns:
        //     A point on the plane that is closest to point.
        public Vector3 ClosestPointOnPlane(Vec3 point)
        {
            var dist = Vec3.Dot(_normal, point) + _distance;

            return (point - _normal * dist);
        }

        // Summary:
        //     Makes the plane face in the opposite direction.
        public void Flip()
        {
            _normal = -_normal;
            _distance = -_distance;
        }

        // Summary:
        //     Returns a signed distance from plane to point.
        // Parameters:
        //   point:
        public float GetDistanceToPoint(Vec3 point)
        {
            return Vec3.Dot(_normal, point) + _distance;
        }

        // Summary:
        //     Is a point on the positive side of the plane?
        public bool GetSide(Vec3 point)
        {
            return Vec3.Dot(_normal, point) + _distance > 0.0f;
        }

        // Summary:
        //     Are two points on the same side of the plane?
        public bool SameSide(Vec3 inPt0, Vec3 inPt1)
        {
            float d0 = GetDistanceToPoint(inPt0);
            float d1 = GetDistanceToPoint(inPt1);

            return (d0 > 0.0f && d1 > 0.0f) || (d0 <= 0.0f && d1 <= 0.0f);
        }

        // Summary:
        //     Sets a plane using three points that lie within it. The points go around clockwise as you look down on the top surface of the plane.
        // Parameters:
        //   a:
        //     First point in clockwise order.
        //   b:
        //     Second point in clockwise order.
        //   c:
        //     Third point in clockwise order.
        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            _normal = Vec3.Normalize(Vec3.Cross(b - a, c - a));
            _distance = Vec3.Dot(_normal, a);
        }

        // Summary:
        //     Sets a plane using a point that lies within it along with a normal to orient it.
        // Parameters:
        //   inNormal:
        //     The plane's normal vector.
        //   inPoint:
        //     A point that lies on the plane.
        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            _normal = Vec3.Normalize(inNormal);
            _distance = -Vec3.Dot(_normal, inPoint);
        }
    }
}