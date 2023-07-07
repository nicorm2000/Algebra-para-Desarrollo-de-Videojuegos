using System;
using UnityEngine;

namespace CustomMath
{
    public struct QuaternionCustom
    {
        #region Variables

        public float x;
        public float y;
        public float z;
        public float w;

        public QuaternionCustom normalized => Normalize(this);

        //The eulerAngles property in the quaternion class converts the quaternion rotation into Euler angles in degrees.
        public Vec3 eulerAngles
        {
            get => ToEulerRad(this) * Mathf.Rad2Deg;//It retrieves the value of the eulerAngles property.
                                                    //It calls the ToEulerRad method on the current quaternion object to obtain the Euler angles in radians.
                                                    //It then multiplies the resulting Euler angles by Mathf.Rad2Deg to convert them to degrees.

            set => this = Euler(value);//It allows you to assign new Euler angles to the quaternion object.
                                       //It takes a value of type Vec3, representing the new Euler angles in degrees.
                                       //It calls the Euler method, passing the value to create a new quaternion with the specified Euler angles.
        }

        //With the indexer method, you can access or modify the components of a quaternion
        //The indexer method provides a convenient way to access and modify the individual components of a quaternion using index notation, similar to accessing elements of an array.
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:

                        return x;

                    case 1:

                        return y;

                    case 2:

                        return z;

                    case 3:

                        return w;

                    default:

                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:

                        x = value;

                        break;

                    case 1:

                        y = value;

                        break;

                    case 2:

                        z = value;

                        break;

                    case 3:

                        w = value;

                        break;

                    default:

                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
                }
            }
        }

        #endregion

        #region Constants

        //The epsiolon value allows for more flexible and robust comparisons, preventing unexpected issues that may arise from relying on strict equality checks.
        public const float kEpsilon = 1e-06f;

        #endregion

        #region Constructors

        //The first constructor takes in four float values (x, y, z, w) and assigns them directly to the corresponding components of the object.
        public QuaternionCustom(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        //The second constructor, which is a copy constructor, takes in another QuaternionCustom object (q) as a parameter.
        //It then copies the values of x, y, z, and w from the q object and assigns them to the respective components of the new QuaternionCustom object being constructed.
        public QuaternionCustom(QuaternionCustom q)
        {
            x = q.x;
            y = q.y;
            z = q.z;
            w = q.w;
        }

        #endregion

        #region Default Values

        //The quaternion identity is a special quaternion that represents the absence of rotation.
        //It has the components (0, 0, 0, 1), where the scalar part (w) is 1 and the vector part (x, y, z) is 0.
        //Multiplying a quaternion by the identity quaternion does not change the rotation. It serves as a neutral element for quaternion multiplication.
        public static QuaternionCustom Identity { get; } = new QuaternionCustom(0f, 0f, 0f, 1f);

        #endregion

        #region Operators

        //The multiplication of two quaternions is non-commutative, meaning the order in which you multiply them matters.
        public static QuaternionCustom operator *(QuaternionCustom lhs, QuaternionCustom rhs)
        {
            float x = lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y;
            float y = lhs.w * rhs.y - lhs.x * rhs.z + lhs.y * rhs.w + lhs.z * rhs.x;
            float z = lhs.w * rhs.z + lhs.x * rhs.y - lhs.y * rhs.x + lhs.z * rhs.w;
            float w = lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z;
            return new QuaternionCustom(x, y, z, w);
        }

        //When you multiply a quaternion with a Vec3, it applies the rotation represented by the quaternion to the vector.
        //By multiplying a quaternion with a vector, the vector is transformed according to the rotation encoded in the quaternion.
        //The rotation is applied using quaternion multiplication, which combines the rotation and translation properties of the quaternion to rotate the vector in 3D space.
        public static Vec3 operator *(QuaternionCustom rotation, Vec3 point)
        {
            float x = rotation.x * 2f;
            float y = rotation.y * 2f;
            float z = rotation.z * 2f;
            float xx = rotation.x * x;
            float yy = rotation.y * y;
            float zz = rotation.z * z;
            float xy = rotation.x * y;
            float xz = rotation.x * z;
            float yz = rotation.y * z;
            float wx = rotation.w * x;
            float wy = rotation.w * y;
            float wz = rotation.w * z;

            Vec3 res;
            res.x = (1f - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z;
            res.y = (xy + wz) * point.x + (1f - (xx + zz)) * point.y + (yz - wx) * point.z;
            res.z = (xz - wy) * point.x + (yz + wx) * point.y + (1f - (xx + yy)) * point.z;

            return res;
        }

        //To account for floating-point imprecision in quaternion values, calculating the dot product between the two quaternions provides a more reliable way to determine their equality.
        //The dot product gives a single scalar value which can be compared against a threshold to determine if the quaternions are considered equal.
        public static bool operator ==(QuaternionCustom lhs, QuaternionCustom rhs)
        {
            return IsEqualUsingDot(Dot(lhs, rhs));
        }

        public static bool operator !=(QuaternionCustom lhs, QuaternionCustom rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator QuaternionCustom(Quaternion q) => new QuaternionCustom(q.x, q.y, q.z, q.w);

        public static implicit operator Quaternion(QuaternionCustom q) => new Quaternion(q.x, q.y, q.z, q.w);

        #endregion

        #region Functions

        public void Set(float newX, float newY, float newZ, float newW)
        {
            x = newX;
            y = newY;
            z = newZ;
            w = newW;
        }

        public void Set(QuaternionCustom q)
        {
            x = q.x;
            y = q.y;
            z = q.z;
            w = q.w;
        }

        //By calculating the dot product of two quaternions, you can determine how close they are to being parallel or pointing in the same direction.
        //In the context of equality comparison, comparing the dot value to a threshold allows you to check if the quaternions are approximately equal, taking into account floating-point imprecision.
        private static bool IsEqualUsingDot(float dot)
        {
            return dot > 1.0f - kEpsilon;
        }

        //The dot value gives you a scalar value that represents the similarity or alignment between two quaternions.
        public static float Dot(QuaternionCustom a, QuaternionCustom b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static QuaternionCustom Inverse(QuaternionCustom rotation)
        {
            return new QuaternionCustom(-rotation.x, -rotation.y, -rotation.z, rotation.w);
        }

        //The formula for the angle is 2 * acos(abs(dot(q1, q2)))
        //The dot function calculates the dot product of the two quaternions, and abs ensures a positive value.
        //The acos function calculates the arccosine (inverse cosine) of the dot product.
        //By multiplying the result by 2, you obtain the angle between the quaternions.
        public static float Angle(QuaternionCustom a, QuaternionCustom b)
        {
            float dot = Dot(a, b);

            return IsEqualUsingDot(dot) ? 0.0f : Mathf.Acos(Mathf.Min(Mathf.Abs(dot), 1.0F)) * 2.0F * Mathf.Rad2Deg;
        }

        //Represents a rotation in 3D space by specifying an angle and an axis of rotation.
        //The axis defines the direction of rotation, while the angle determines the amount of rotation around that axis.
        public static QuaternionCustom AngleAxis(float angle, Vec3 axis)
        {
            axis.Normalize();//The axis vector is normalized to ensure it has a length of 1.
            axis *= Mathf.Sin(angle * Mathf.Deg2Rad * 0.5f);//This represents the rotated axis based on the angle provided.
                                                            //The sine function helps determine the magnitude of the rotation around the axis.
                                                            //By multiplying the axis vector by the sine value, we ensure that the resulting quaternion captures the correct amount of rotation.

            return new QuaternionCustom(axis.x, axis.y, axis.z, Mathf.Cos(angle * Mathf.Deg2Rad * 0.5f));//This cosine value represents the rotation component of the quaternion and is used to combine the rotational effect with the axis vector.
                                                                                                         //Together, the real part and the rotated axis vector form a quaternion that represents the desired rotation.
        }

        public static QuaternionCustom AxisAngle(Vec3 axis, float angle)
        {
            return QuaternionCustom.AngleAxis(angle, axis);
        }

        //Converts a quaternion into an angle-axis representation.
        //It extracts the rotation angle and axis from the quaternion.
        //The angle is calculated using the arc cosine of the w component of the normalized quaternion, while the axis is determined by dividing the x, y, and z components of the normalized quaternion by the sine of the angle.
        //This allows for a more intuitive representation of the rotation.
        public void ToAngleAxis(out float angle, out Vec3 axis)
        {
            // Normalize the quaternion to ensure it has a length of 1
            QuaternionCustom normalizedQuaternion = this.normalized;

            // Calculate the angle of rotation by taking the arc cosine of the w component of the normalized quaternion
            angle = 2 * Mathf.Acos(normalizedQuaternion.w) * Mathf.Rad2Deg;

            // Calculate the sine of the angle using the w component of the normalized quaternion
            float sinAngle = Mathf.Sqrt(1 - normalizedQuaternion.w * normalizedQuaternion.w);

            // Check if the sinAngle is approximately 0 (small or no rotation)
            if (Mathf.Approximately(sinAngle, 0))
            {
                // Set the axis as a default value (e.g., when there is no rotation)
                axis = Vec3.Up;
            }
            else
            {
                // Calculate the axis of rotation by dividing the x, y, and z components of the normalized quaternion by the sinAngle
                axis = new Vec3(normalizedQuaternion.x, normalizedQuaternion.y, normalizedQuaternion.z) / sinAngle;
            }
        }
        
        //Smoothly interpolate between two quaternions based on a specified fraction or interpolation factor.
        public static QuaternionCustom Lerp(QuaternionCustom a, QuaternionCustom b, float t)
        {
            t = Mathf.Clamp(t, 0, 1);

            return LerpUnclamped(a, b, t);
        }

        //The LerpUnclamped function interpolates between rotations "a" and "b" based on the unclamped variable "t," and normalizes the result.
        public static QuaternionCustom LerpUnclamped(QuaternionCustom a, QuaternionCustom b, float t)
        {
            QuaternionCustom q = Identity;

            float timeLeft = 1f - t;//The remaining time is calculated (to reach the rotation from "a" to "b").

            if (Dot(a, b) >= 0)//It checks if the dot product is greater than 0 to determine the shortest path for interpolation, and based on that,
                              //either addition or subtraction is performed for the linear interpolation formula from "a" to "b."
            {
                q.x = (timeLeft * a.x) + (t * b.x);
                q.y = (timeLeft * a.y) + (t * b.y);
                q.z = (timeLeft * a.z) + (t * b.z);
                q.w = (timeLeft * a.w) + (t * b.w);
            }
            else
            {
                q.x = (timeLeft * a.x) - (t * b.x);
                q.y = (timeLeft * a.y) - (t * b.y);
                q.z = (timeLeft * a.z) - (t * b.z);
                q.w = (timeLeft * a.w) - (t * b.w);
            }

            q.Normalize();//Ensures that the resulting quaternion has a length of 1, or in other words, it is a unit quaternion.
                          //A unit quaternion represents a rotation without any scale or skew.

            return q;
        }

        //The slerp function in the quaternion class stands for "spherical linear interpolation."
        //It is used to smoothly interpolate between two quaternions based on a specified fraction or interpolation factor, taking into account the shortest path along the surface of a sphere.
        //Slerp: smooth spherical interpolation, considers shortest path on sphere
        //Lerp: linear interpolation, doesn't consider shortest path and can result in longer rotations.
        public static QuaternionCustom Slerp(QuaternionCustom a, QuaternionCustom b, float t)
        {
            t = Mathf.Clamp(t, 0, 1);

            return SlerpUnclamped(a, b, t);
        }

        //This function performs unclamped spherical linear interpolation (Slerp) between two quaternions.
        //Calculate the dot product of a and b, and store it in cosAngle.
        //If cosAngle is less than 0, negate b to ensure the interpolation takes the shortest path.
        //If cosAngle is less than 0.95f:
        //Calculate the angle between a and b using Mathf.Acos(cosAngle).
        //Compute the sine of the angle using Mathf.Sin(angle).
        //Calculate the reciprocal of the sine value(1 / sinAngle) and store it in invSinAngle.
        //Calculate intermediate factors t1 = Mathf.Sin((1 - t) * angle) * invSinAngle and t2 = Mathf.Sin(t * angle) * invSinAngle.
        //Interpolate the components of the quaternions using the formula:
        //new QuaternionCustom(
        //    a.x* t1 + b.x* t2,
        //    a.y* t1 + b.y* t2,
        //    a.z* t1 + b.z* t2,
        //    a.w* t1 + b.w* t2
        //    )
        //Return the interpolated quaternion.
        //If cosAngle is greater than or equal to 0.95f, use the Lerp function to interpolate between a and b.
        //Normalize the resulting quaternion to ensure it represents a valid rotation.
        public static QuaternionCustom SlerpUnclamped(QuaternionCustom a, QuaternionCustom b, float t)
        {
            float cosAngle = Dot(a, b);//Calculate the dot product of a and b.

            if (cosAngle < 0)//Check if the dot product is negative, which indicates a larger rotation.
                             //Flipping the quaternion when the dot product is negative ensures that the interpolation takes the shortest path on the unit sphere.
                             //This is important because there are two possible paths to interpolate between two quaternions, but we want to choose the one that requires the smallest rotation.
                             //By flipping one of the quaternions, we ensure that the interpolation follows the shortest path, resulting in smoother and more intuitive animations or rotations.
            {
                cosAngle = -cosAngle;//Negate cosAngle to ensure the shortest path.
                b = new QuaternionCustom(-b.x, -b.y, -b.z, -b.w);//Negate b to correct the rotation direction.
            }

            float t1, t2;//Intermediate variables for interpolation factors.

            if (cosAngle < 0.95f) //Check if the angle between a and b is smaller than 0.95 (cos 18 degrees).
            {
                float angle = Mathf.Acos(cosAngle);//Calculate the angle between a and b using acos.
                float sinAngle = Mathf.Sin(angle);//Calculate the sine of the angle.
                float invSinAngle = 1 / sinAngle;//Calculate the reciprocal of the sine.
                                                 //It is used in this context to normalize the interpolation factors t1 and t2 so that the resulting quaternion remains a unit quaternion.
                                                 //This ensures that the interpolated quaternion represents a valid rotation.

                // Calculate interpolation factors for a and b.
                t1 = Mathf.Sin((1 - t) * angle) * invSinAngle;
                t2 = Mathf.Sin(t * angle) * invSinAngle;

                //Interpolate the quaternion components using the interpolation factors.
                return new QuaternionCustom(
                    a.x * t1 + b.x * t2,
                    a.y * t1 + b.y * t2,
                    a.z * t1 + b.z * t2,
                    a.w * t1 + b.w * t2
                );
            }
            else
            {
                return Lerp(a, b, t);//If the angle is larger, perform linear interpolation (Lerp) instead.
                                     //Because when the angle between the two quaternions is larger than 0.95, the difference between them becomes close to a straight line in the quaternion space.
            }
        }

        //This function returns a rotation that goes from fromDirection to toDirection.
        public static QuaternionCustom FromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            Vec3 axis = Vec3.Cross(fromDirection, toDirection);//Calculate the rotation axis using the cross product of the two directions.
                                                               //Cross is used to find a perpendicular vector to both directions.
            float angle = Vec3.Angle(fromDirection, toDirection);//Calculate the angle of rotation using the angle between the directions.
                                                                 //Determines how much the rotation needs to occur to align the two directions.

            return AngleAxis(angle, axis.normalized);//Creates a quaternion using the angle-axis representation to represent the rotation.
                                                     //The angle-axis representation describes a rotation by specifying an angle of rotation around an axis in three-dimensional space.
                                                     //By using this representation, the quaternion can effectively store and represent the rotation information in a compact and efficient manner.
        }

        //Sets from to rotation to a value
        public void SetFromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            this = FromToRotation(fromDirection, toDirection);
        }

        //Calculates the interpolated quaternion between two input quaternions (from and to) based on a specified maximum rotation angle (maxDegreesDelta).
        //It first calculates the normalized delta angle (t) by dividing maxDegreesDelta by the angle between the from and to quaternions.
        //Then, it performs spherical interpolation (Slerp) between the from and to quaternions using t as the interpolation factor to smoothly rotate from from to to with the specified maximum rotation angle.
        public static QuaternionCustom RotateTowards(QuaternionCustom from, QuaternionCustom to, float maxDegreesDelta)
        {
            float t = Mathf.Min(1f, maxDegreesDelta / Angle(from, to));//Calculate the normalized delta angle (t) by dividing maxDegreesDelta by the angle between from and to quaternions.
                                                                       //Using Mathf.Min(1f, ...) ensures that t is capped at a value of 1 to prevent over-rotation.
                                                                       //By dividing maxDegreesDelta by the angle between the quaternions, you can obtain a normalized value that represents the desired amount of rotation.
                                                                       //This allows you to smoothly rotate from from to to with the specified maximum rotation angle (maxDegreesDelta).

            return SlerpUnclamped(from, to, t);//Perform spherical interpolation (Slerp) between from and to quaternions using the calculated t value as the interpolation factor.
        }

        //The SetLookRotation function sets the rotation of an object to look in a specific direction view.
        //By default, it aligns the object's up vector with the world up vector, which is commonly represented as the vector pointing upwards along the y-axis.
        public void SetLookRotation(Vec3 view)
        {
            Vec3 up = Vec3.Up;

            SetLookRotation(view, up);
        }
        //This override lets you specify a custom up vector (up) when setting the look rotation.
        public void SetLookRotation(Vec3 view, Vec3 up)
        {
            this = LookRotation(view, up);
        }

        //This method makes the look rotation look up
        public static QuaternionCustom LookRotation(Vec3 forward)
        {
            Vec3 up = Vec3.Up;

            return LookRotation(forward, up);
        }

        //The LookRotation function transforms a direction vector into a rotation where its z-axis is aligned with "forward".
        //https://d3cw3dd2w32x2b.cloudfront.net/wp-content/uploads/2015/01/matrix-to-quat.pdf
        public static QuaternionCustom LookRotation(Vec3 forward, Vec3 upwards)
        {
            //It is a really big function so it is gonna be divided in two, the rotation matrix creation and the second part aplication.
            //This is the first part, it is responsible for setting up the axis that will compose the quaternion rotation.
            forward = Vec3.Normalize(forward);
            Vec3 right = Vec3.Normalize(Vec3.Cross(upwards, forward));//Creating a third axis from the two parameters using the cross product of both.
            upwards = Vec3.Normalize(Vec3.Cross(forward, right));//We create the "up" vector again just to normalize it and ensure that there are no issues with the axis being misaligned or any strange anomalies.

            float m00 = right.x;
            float m10 = upwards.x;
            float m20 = forward.x;
            float m01 = right.y;
            float m11 = upwards.y;
            float m21 = forward.y;
            float m02 = right.z;
            float m12 = upwards.z;
            float m22 = forward.z;
            //With all of this values up to here we create the components of a rotation matrix.

            //This is the second part.
            //file:///C:/Users/Nico/Desktop/formula2.png
            float diagonals = m00 + m11 + m22;
            var quaternion = new QuaternionCustom();

            if (diagonals > 0f)//If the sum of the diagonal elements of the rotation matrix (diagonals) is greater than 0, it means the rotation matrix is well-conditioned.
                               //Well-conditioned refers to a rotation matrix that is appropriate and suitable for calculating a rotation quaternion.(determinante)
            {
                float num = Mathf.Sqrt(diagonals + 1f);//Calculate the square root of (diagonals + 1).
                quaternion.w = num * 0.5f;//Set the w component of the quaternion.
                num = 0.5f / num;//Calculate a factor for the other components.

                //Set the x, y, and z components of the quaternion using the matrix components.
                quaternion.x = (m12 - m21) * num;
                quaternion.y = (m20 - m02) * num;
                quaternion.z = (m01 - m10) * num;

                return quaternion;
            }
            if ((m00 >= m11) && (m00 >= m22))//It ensures that the determinant is positive, which is necessary for a well-conditioned matrix.
                                             //A positive determinant indicates that the matrix represents a proper rotation without any reflections.
            {
                float num = Mathf.Sqrt(((1f + m00) - m11) - m22);//Calculate the square root of ((1 + m00) - m11 - m22).
                float num4 = 0.5f / num;//Calculate a factor for the other components.
                quaternion.x = 0.5f * num;

                //Set the x, y, and z components of the quaternion using the matrix components.
                quaternion.y = (m01 + m10) * num4;
                quaternion.z = (m02 + m20) * num4;
                quaternion.w = (m12 - m21) * num4;

                return quaternion;
            }
            if (m11 > m22)//The determinant of the rotation matrix is negative, indicating a reflection or an improper rotation.
            {
                float num = Mathf.Sqrt(((1f + m11) - m00) - m22);//Calculate the square root of ((1 + m11) - m00 - m22)
                float num3 = 0.5f / num;//Calculate a factor for the other components
                quaternion.y = 0.5f * num;

                //Set the x, y, and z components of the quaternion using the matrix components.
                quaternion.x = (m10 + m01) * num3;
                quaternion.z = (m21 + m12) * num3;
                quaternion.w = (m20 - m02) * num3;

                return quaternion;
            }

            float num5 = Mathf.Sqrt(((1f + m22) - m00) - m11);//Calculate the square root of ((1 + m22) - m00 - m11)
            float num2 = 0.5f / num5;//Calculate a factor for the other components
            quaternion.z = 0.5f * num5;

            //Set the x, y, and z components of the quaternion using the matrix components
            quaternion.x = (m20 + m02) * num2;
            quaternion.y = (m21 + m12) * num2;
            quaternion.w = (m01 - m10) * num2;

            return quaternion;
        }

        //This method takes Euler angles (in degrees) as input and returns a QuaternionCustom representing the rotation.
        //Euler works by calculating the sine and cosine values for each angle (x, y, z).
        //It then creates individual quaternions for each axis using these sine and cosine values.
        //Finally, it returns the combined rotation by multiplying the quaternions in the order: y * x * z.
        //Euler formula is represented by e^(ix) = cos(x) + i sin(x), where e is the base of natural logarithm, i is the imaginary unit, and x is the angle.
        //By using the Euler formula, we can calculate the sine and cosine values of the angles and set them as components of the corresponding quaternions, resulting in the desired rotation.
        public static QuaternionCustom Euler(float x, float y, float z)
        {
            //Create QuaternionCustom variables for each axis (x, y, z) and initialize them to identity
            QuaternionCustom qx = Identity;
            QuaternionCustom qy = Identity;
            QuaternionCustom qz = Identity;

            //Variables to store sine and cosine values of the angles
            float sinAngle = 0.0f;
            float cosAngle = 0.0f;

            //Calculate sine and cosine of the y angle
            sinAngle = Mathf.Sin(Mathf.Deg2Rad * y * 0.5f);//In the context of complex numbers and trigonometry, the sine function relates the input angle (in radians) to the imaginary component of the corresponding complex number.
            cosAngle = Mathf.Cos(Mathf.Deg2Rad * y * 0.5f);//In the context of complex numbers and trigonometry, the cosine function relates the input angle (in radians) to the real component of the corresponding complex number.
            qy.Set(0, sinAngle, 0, cosAngle);//Euler formula: e^(iy) = cos(y) + i sin(y)

            //Calculate sine and cosine of the x angle
            sinAngle = Mathf.Sin(Mathf.Deg2Rad * x * 0.5f);
            cosAngle = Mathf.Cos(Mathf.Deg2Rad * x * 0.5f);
            qx.Set(sinAngle, 0, 0, cosAngle);//Euler formula: e^(iy) = cos(x) + i sin(x)

            //Calculate sine and cosine of the z angle
            sinAngle = Mathf.Sin(Mathf.Deg2Rad * z * 0.5f);
            cosAngle = Mathf.Cos(Mathf.Deg2Rad * z * 0.5f);
            qz.Set(0, 0, sinAngle, cosAngle);//Euler formula: e^(iy) = cos(z) + i sin(z)

            //Return the combined rotation by multiplying the quaternions in the order: y * x * z
            return qy * qx * qz;
        }

        public static QuaternionCustom Euler(Vec3 euler)
        {
            return Euler(euler.x, euler.y, euler.z);
        }

        //To convert from Euler angles in radians to degrees, you can use the formula: degrees = radians* (180 / π)
        public static Vec3 ToEulerRad(QuaternionCustom rotation)
        {
            float sqw = rotation.w * rotation.w;//Square of the quaternion component w
            float sqx = rotation.x * rotation.x;//Square of the quaternion component x
            float sqy = rotation.y * rotation.y;//Square of the quaternion component y
            float sqz = rotation.z * rotation.z;//Square of the quaternion component z
            float unit = sqx + sqy + sqz + sqw;//Sum of the squared quaternion components

            float test = rotation.x * rotation.w - rotation.y * rotation.z;//Calculation to check for certain conditions

            Vec3 v;//Variable to store the Euler angles

            //Check if the test condition for positive y rotation is met
            if (test > 0.4995f * unit)
            {
                v.y = 2f * Mathf.Atan2(rotation.y, rotation.x);//Calculate the y angle using the arctan2 function
                v.x = Mathf.PI / 2;//Set the x angle to pi/2
                v.z = 0;//Set the z angle to 0

                //Normalize and convert the angles to degrees
                return NormalizeAngles(v * Mathf.Rad2Deg);
            }
            //Check if the test condition for negative y rotation is met
            if (test < -0.4995f * unit)
            {
                v.y = -2f * Mathf.Atan2(rotation.y, rotation.x);//Calculate the y angle using the arctan2 function with a negative sign
                v.x = -Mathf.PI / 2;//Set the x angle to -pi/2
                v.z = 0;//Set the z angle to 0

                //Normalize and convert the angles to degrees
                return NormalizeAngles(v * Mathf.Rad2Deg);
            }

            //If none of the above conditions are met, create a new quaternion 'q' by reordering the components
            QuaternionCustom q = new QuaternionCustom(rotation.w, rotation.z, rotation.x, rotation.y);

            //Calculate the Euler angles using the components of 'q'
            //file:///C:/Users/Nico/Desktop/formula.png
            v.y = Mathf.Atan2(2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));//Calculate y angle
            v.x = Mathf.Asin(2f * (q.x * q.z - q.w * q.y));//Calculate x angle
            v.z = Mathf.Atan2(2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));//Calculate z angle

            //Normalize and convert the angles to degrees
            return NormalizeAngles(v * Mathf.Rad2Deg);
        }

        private static Vec3 NormalizeAngles(Vec3 angles)
        {
            angles.x = NormalizeAngle(angles.x);
            angles.y = NormalizeAngle(angles.y);
            angles.z = NormalizeAngle(angles.z);

            return angles;
        }

        private static float NormalizeAngle(float angle)
        {
            while (angle > 360)
            {
                angle -= 360;
            }

            while (angle < 0)
            {
                angle += 360;
            }

            return angle;
        }

        public static QuaternionCustom Normalize(QuaternionCustom q)
        {
            float mag = Mathf.Sqrt(Dot(q, q));//This line calculates the magnitude or length of the Quaternion q.
                                              //It uses the Dot function, which calculates the dot product of q with itself. 

            if (mag < kEpsilon)
            {
                return Identity;
            }

            return new QuaternionCustom(q.x / mag, q.y / mag, q.z / mag, q.w / mag);//If the magnitude is not close to zero, the code proceeds to divide each component of the Quaternion q by the magnitude (mag) to normalize it.
        }

        public void Normalize()
        {
            this = Normalize(this);
        }

        #endregion

        #region Internals

        public bool Equals(QuaternionCustom other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z) && w.Equals(other.w);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((QuaternionCustom)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();//The number 397 is often used in hash code calculations because it is a prime number, and multiplying by a prime number improves the distribution of hash codes across a larger range of values.
                                                              //This helps reduce collisions and improves the efficiency of hash-based data structures.
                hashCode = (hashCode * 397) ^ z.GetHashCode();//The number 397 is often used in hash code calculations because it is a prime number, and multiplying by a prime number improves the distribution of hash codes across a larger range of values.
                                                              //This helps reduce collisions and improves the efficiency of hash-based data structures.
                hashCode = (hashCode * 397) ^ w.GetHashCode();//The number 397 is often used in hash code calculations because it is a prime number, and multiplying by a prime number improves the distribution of hash codes across a larger range of values.
                                                              //This helps reduce collisions and improves the efficiency of hash-based data structures.

                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"({x:F1}, {y:F1}, {z:F1}, {w:F1})";
        }

        public string ToString(string format)
        {
            return string.Format("({0}, {1}, {2}, {3})", x.ToString(format), y.ToString(format), z.ToString(format), w.ToString(format));
        }

        #endregion
    }
}