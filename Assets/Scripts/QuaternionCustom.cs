using System;
using System.Numerics;
using UnityEngine;

namespace CustomMath
{
    public struct QuaternionCustom
    {
        #region Variables

        public float _x, _y, _z, _w;
        public const float kEpsilon = 1E-06F;

        #endregion

        #region Constructor
        public QuaternionCustom(float x, float y, float z, float w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        #endregion

        #region Methods

        public float this[int index] //Entre 0 y 3. 
        {
            get //Devuelve esa componente.
            {
                switch (index)
                {
                    case 0:
                        return _x;
                    case 1:
                        return _y;
                    case 2:
                        return _z;
                    case 3:
                        return _w;
                    default:
                        throw new IndexOutOfRangeException("Index out of Range!");
                }
            }
            set
            {
                switch (index) //Setea esa componente.
                {
                    case 0:
                        _x = value;
                        break;
                    case 1:
                        _y = value;
                        break;
                    case 2:
                        _z = value;
                        break;
                    case 3:
                        _w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Index out of Range!");
                }
            }
        }

        public static QuaternionCustom identity { get; } = new QuaternionCustom(0, 0, 0, 1); //Devuelve la identidad del quaternion (0,0,0,1).



        #endregion
    }

    //variables
    //constructor
    //this[int index]
    //identity
    //eulerAngles
    //normalized
    //Angle(Quaternion a, Quaternion b)
    //AngleAxis(float angle, Vector3 axis)
    //AxisAngle(Vector3 axis, float angle)
    //Dot(Quaternion a, Quaternion b)
    //Euler(Vector3 euler)
    //Euler(float x, float y, float z)
    //FromToRotation(Vector3 fromDirection, Vector3 toDirection)
    //Inverse(Quaternion rotation)
    //Lerp(Quaternion a, Quaternion b, float t)
    //LerpUnclamped(Quaternion a, Quaternion b, float t)
    //LookRotation(Vector3 forward)
    //LookRotation(Vector3 forward, Vector3 upwards)[DefaultValue("Vector3.up")]
    //Normalize(Quaternion q)
    //RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
    //Slerp(Quaternion a, Quaternion b, float t)
    //SlerpUnclamped(Quaternion a, Quaternion b, float t)
    //Set(float newX, float newY, float newZ, float newW)
    //SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
    //SetLookRotation(Vector3 view)
    //SetLookRotation(Vector3 view, Vector3 up)[DefaultValue("Vector3.up")]
    //ToAngleAxis(out float angle, out Vector3 axis)
    //ToString()
    //Vector3 operator *(Quaternion rotation, Vector3 point)
    //Quaternion operator *(Quaternion lhs, Quaternion rhs)
    //bool operator ==(Quaternion lhs, Quaternion rhs)
    //bool operator !=(Quaternion lhs, Quaternion rhs)    
}