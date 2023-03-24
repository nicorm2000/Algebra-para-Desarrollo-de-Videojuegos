using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace CustomMath
{
    public struct Vec3 : IEquatable<Vec3>
    {
        #region Variables
        public float x;
        public float y;
        public float z;

        public float sqrMagnitude { get { return (Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2)); } }
        public Vector3 normalized { get { return new Vec3(x / magnitude, y / magnitude, z / magnitude); } }
        public float magnitude { get { return Magnitude(new Vec3(x, y, z)); } }
        #endregion

        #region constants
        public const float epsilon = 1e-05f;
        #endregion

        #region Default Values
        public static Vec3 Zero { get { return new Vec3(0.0f, 0.0f, 0.0f); } }
        public static Vec3 One { get { return new Vec3(1.0f, 1.0f, 1.0f); } }
        public static Vec3 Forward { get { return new Vec3(0.0f, 0.0f, 1.0f); } }
        public static Vec3 Back { get { return new Vec3(0.0f, 0.0f, -1.0f); } }
        public static Vec3 Right { get { return new Vec3(1.0f, 0.0f, 0.0f); } }
        public static Vec3 Left { get { return new Vec3(-1.0f, 0.0f, 0.0f); } }
        public static Vec3 Up { get { return new Vec3(0.0f, 1.0f, 0.0f); } }
        public static Vec3 Down { get { return new Vec3(0.0f, -1.0f, 0.0f); } }
        public static Vec3 PositiveInfinity { get { return new Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); } }
        public static Vec3 NegativeInfinity { get { return new Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); } }
        #endregion                                                                                                                                                                               

        #region Constructors
        public Vec3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3(Vec3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector2 v2)
        {
            this.x = v2.x;
            this.y = v2.y;
            this.z = 0.0f;
        }
        #endregion

        #region Operators
        public static bool operator ==(Vec3 left, Vec3 right)
        {
            float diff_x = left.x - right.x;
            float diff_y = left.y - right.y;
            float diff_z = left.z - right.z;
            float sqrmag = diff_x * diff_x + diff_y * diff_y + diff_z * diff_z;
            return sqrmag < epsilon * epsilon;
        }
        public static bool operator !=(Vec3 left, Vec3 right)
        {
            return !(left == right);
        }

        public static Vec3 operator +(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x + rightV3.x, leftV3.y + rightV3.y, leftV3.z + rightV3.z);
        }

        public static Vec3 operator -(Vec3 leftV3, Vec3 rightV3)
        {
            throw new NotImplementedException();
        }

        public static Vec3 operator -(Vec3 v3)
        {
            return new Vec3(-1 * v3.x, -1 * v3.y, -1 * v3.z);
        }

        public static Vec3 operator *(Vec3 v3, float scalar)
        {
            throw new NotImplementedException();
        }
        public static Vec3 operator *(float scalar, Vec3 v3)
        {
            throw new NotImplementedException();
        }
        public static Vec3 operator /(Vec3 v3, float scalar)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Vector3(Vec3 v3)
        {
            return new Vector3(v3.x, v3.y, v3.z);
        }

        public static implicit operator Vector2(Vec3 v2)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Functions
        public override string ToString()
        {
            return "X = " + x.ToString() + "   Y = " + y.ToString() + "   Z = " + z.ToString();
        }
        public static float Angle(Vec3 from, Vec3 to)
        {
            //Angle between two vectors using Dot Product
            //θ = cos-1 [ (a · b) / (|a| |b|) ]

            float num1 = Dot(from, to);
            float num2 = Magnitude(from);
            float num3 = Magnitude(to);
            float division = ((num1) / (num2 * num3));

            return MathF.Acos(division);
        }
        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength)
        {
            if (Magnitude(vector) <= maxLength && Magnitude(vector) >= 0)
            {
                return vector;
            }
            else
            {
                return (vector / Magnitude(vector)) * maxLength;
            }
        }
        public static float Magnitude(Vec3 vector)
        {
            float vectorMagnitudeSum = (vector.x * vector.x) + (vector.y * vector.y) + (vector.z * vector.z);

            return MathF.Sqrt(vectorMagnitudeSum);
        }
        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            float xCrossProduct = (a.y * b.z) - (a.z * b.y);
            float yCrossProduct = (a.y * b.z) - (a.z * b.y);
            float zCrossProduct = (a.y * b.z) - (a.z * b.y);

            Vec3 crossProduct = new Vec3(xCrossProduct, yCrossProduct, zCrossProduct);

            return crossProduct;
        }
        public static float Distance(Vec3 a, Vec3 b)
        {
            Vector3 vector = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);

            return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }
        public static float Dot(Vec3 a, Vec3 b)
        {
            float dotProduct = (a.x * b.x) + (a.y * b.y) + (a.z * b.z);

            return dotProduct;
        }
        public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
        {
            //Linearly interpolates between two points.
            //Interpolates between the points a and b by the interpolant t.The parameter t is clamped to the range[0, 1]. This is most commonly used to find a point some fraction of the way along a line between two endpoints(e.g.to move an object gradually between those points).
            //a + (b - a) * t

            float x = a.x;
            float y = a.y;
            float z = a.z;

            if (t < 1.0f)
            {
                x = a.x + (b.x - a.x) * t;
                y = a.y + (b.y - a.y) * t;
                z = a.z + (b.z - a.z) * t;
            }

            return new Vec3(x, y, z);
        }
        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t)
        {
            //Linearly interpolates between two vectors.
            //Interpolates between the vectors a and b by the interpolant t.This is most commonly used to find a point some fraction of the way along a line between two endpoints(e.g.to move an object gradually between those points).
            //When t = 0 returns a. When t = 1 returns b. When t = 0.5 returns the point midway between a and b.

            float x = a.x;
            float y = a.y;
            float z = a.z;

            x = a.x + (b.x - a.x) * t;
            y = a.y + (b.y - a.y) * t;
            z = a.z + (b.z - a.z) * t;

            return new Vec3(x, y, z);
        }
        public static Vec3 Max(Vec3 a, Vec3 b)
        {
            Vec3 vecMax;

            if (a.x > b.x)
            {
                vecMax.x = a.x;
            }
            else
            {
                vecMax.x = b.x;
            }

            if (a.y > b.y)
            {
                vecMax.y = a.y;
            }
            else
            {
                vecMax.y = b.y;
            }

            if (a.z > b.z)
            {
                vecMax.z = a.z;
            }
            else
            {
                vecMax.z = b.z;
            }

            return vecMax;
        }
        public static Vec3 Min(Vec3 a, Vec3 b)
        {
            Vec3 vecMin;

            if (a.x < b.x)
            {
                vecMin.x = a.x;
            }
            else
            {
                vecMin.x = b.x;
            }

            if (a.y < b.y)
            {
                vecMin.y = a.y;
            }
            else
            {
                vecMin.y = b.y;
            }

            if (a.z < b.z)
            {
                vecMin.z = a.z;
            }
            else
            {
                vecMin.z = b.z;
            }

            return vecMin;
        }
        public static float SqrMagnitude(Vec3 vector)
        {
            return (Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));
        }
        public static Vec3 Project(Vec3 vector, Vec3 onNormal) 
        {
            /* ( a . b )
                -------- * a
               ( a . a )
            */
            float division = Dot(vector, onNormal) / Dot(onNormal, onNormal);
            Vec3 vecProjection = division * onNormal;
            
            return vecProjection;
        }
        public static Vec3 Reflect(Vec3 inDirection, Vec3 inNormal) 
        {
            /*
               ( U . V )
               (-------) * V
               ( |V|˄2 )
             */
            float num1 = Dot(inDirection, inNormal);
            float num2 = Mathf.Pow(Magnitude(inNormal), 2);
            float num3 = num1 / num2;

            inNormal *= num3;
            
            return new Vec3(inNormal);
        }
        public void Set(float newX, float newY, float newZ)
        {
            this.x = newX;
            this.y = newY;
            this.z = newZ;
        }
        public void Scale(Vec3 scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
        }
        public void Normalize()
        {
            Set(normalized.x, normalized.y, normalized.z);
        }
        //Added new Normalize static function
        public static Vec3 Normalize(Vec3 vector)
        {
            return new Vec3(vector.x / Magnitude(vector), vector.y / Magnitude(vector), vector.z / Magnitude(vector));
        }
        #endregion

        #region Internals
        public override bool Equals(object other)
        {
            if (!(other is Vec3)) return false;
            return Equals((Vec3)other);
        }

        public bool Equals(Vec3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }
        #endregion
    }
}