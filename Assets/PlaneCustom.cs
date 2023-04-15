using UnityEngine;
using System;
using CustomMath;
using MathDebbuger;

namespace CustomMath
{
    public struct PlaneCustom
    {
        //
        // Summary:
        //     Creates a plane.
        //
        // Parameters:
        //   inNormal:
        //
        //   inPoint:

        //public PlaneCustom(Vector3 inNormal, Vector3 inPoint)
        //{
        //
        //}
        ////
        // Summary:
        //     Creates a plane.
        //
        // Parameters:
        //   inNormal:
        //
        //   d:

        //public PlaneCustom(Vector3 inNormal, float d)
        //{
        //
        //}
        ////
        // Summary:
        //     Creates a plane.
        //
        // Parameters:
        //   a:
        //
        //   b:
        //
        //   c:

        //public PlaneCustom(Vector3 a, Vector3 b, Vector3 c)
        //{
        //
        //}
        //
        // Summary:
        //     Normal vector of the plane.
        public Vector3 normal { get; set; }
        //
        // Summary:
        //     The distance measured from the Plane to the origin, along the Plane's normal.
        public float distance { get; set; }
        //
        // Summary:
        //     Returns a copy of the plane that faces in the opposite direction.
        public Plane flipped { get; }

        //
        // Summary:
        //     Returns a copy of the given plane that is moved in space by the given translation.
        //
        // Parameters:
        //   plane:
        //     The plane to move in space.
        //
        //   translation:
        //     The offset in space to move the plane with.
        //
        // Returns:
        //     The translated plane.
        public static Plane Translate(Plane plane, Vector3 translation)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     For a given point returns the closest point on the plane.
        //
        // Parameters:
        //   point:
        //     The point to project onto the plane.
        //
        // Returns:
        //     A point on the plane that is closest to point.
        public Vector3 ClosestPointOnPlane(Vector3 point)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Makes the plane face in the opposite direction.
        public void Flip()
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Returns a signed distance from plane to point.
        //
        // Parameters:
        //   point:
        public float GetDistanceToPoint(Vector3 point)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Is a point on the positive side of the plane?
        //
        // Parameters:
        //   point:
        public bool GetSide(Vector3 point)
        {
            throw new NotImplementedException();
        }
        public bool Raycast(Ray ray, out float enter)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Are two points on the same side of the plane?
        //
        // Parameters:
        //   inPt0:
        //
        //   inPt1:
        public bool SameSide(Vector3 inPt0, Vector3 inPt1)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Sets a plane using three points that lie within it. The points go around clockwise
        //     as you look down on the top surface of the plane.
        //
        // Parameters:
        //   a:
        //     First point in clockwise order.
        //
        //   b:
        //     Second point in clockwise order.
        //
        //   c:
        //     Third point in clockwise order.
        public void Set3Points(Vector3 a, Vector3 b, Vector3 c)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Sets a plane using a point that lies within it along with a normal to orient
        //     it.
        //
        // Parameters:
        //   inNormal:
        //     The plane's normal vector.
        //
        //   inPoint:
        //     A point that lies on the plane.
        public void SetNormalAndPosition(Vector3 inNormal, Vector3 inPoint)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }
        public void Translate(Vector3 translation)
        {
            throw new NotImplementedException();
        }
    }
}