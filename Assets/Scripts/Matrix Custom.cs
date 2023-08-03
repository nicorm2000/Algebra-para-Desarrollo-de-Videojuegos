using System;
using UnityEngine;

namespace CustomMath
{
    public struct MatrixCustom : IEquatable<MatrixCustom>
    {
        #region Variables

        public float m00;
        public float m10;
        public float m20;
        public float m30;
        public float m01;
        public float m11;
        public float m21;
        public float m31;
        public float m02;
        public float m12;
        public float m22;
        public float m32;
        public float m03;
        public float m13;
        public float m23;
        public float m33;

        #endregion

        #region constructor

        //Assigns each value to each position in the 4x4 matrix
        public MatrixCustom(Vector4 col0, Vector4 col1, Vector4 col2, Vector4 col3)
        {
            m00 = col0.x;
            m01 = col1.x;
            m02 = col2.x;
            m03 = col3.x;
            m10 = col0.y;
            m11 = col1.y;
            m12 = col2.y;
            m13 = col3.y;
            m20 = col0.z;
            m21 = col1.z;
            m22 = col2.z;
            m23 = col3.z;
            m30 = col0.w;
            m31 = col1.w;
            m32 = col2.w;
            m33 = col3.w;
        }

        #endregion

        #region Methods

        //Indexer that allows you to access specific elements of the matrix using row and column indices. 
        public float this[int row, int col]
        {
            get
            {
                return this[row + col * 4];
            }
            set
            {
                this[row + col * 4] = value;
            }
        }

        //This indexer allows you to access and modify individual elements of a Matrix4x4 object using either a single index.
        //The get accessor returns the value of the matrix element corresponding to the given index or row and column indices.
        //The set accessor sets the value of the matrix element at the specified index or row and column indices.
        //This provides a convenient way to access and modify specific elements of the matrix.
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:

                        return m00;

                    case 1:

                        return m10;

                    case 2:

                        return m20;

                    case 3:

                        return m30;

                    case 4:

                        return m01;

                    case 5:

                        return m11;

                    case 6:

                        return m21;

                    case 7:

                        return m31;

                    case 8:

                        return m02;

                    case 9:

                        return m12;

                    case 10:

                        return m22;

                    case 11:

                        return m32;

                    case 12:

                        return m03;

                    case 13:

                        return m13;

                    case 14:

                        return m23;

                    case 15:

                        return m33;

                    default:

                        throw new IndexOutOfRangeException("Index out of Range!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:

                        m00 = value;

                        break;

                    case 1:

                        m10 = value;

                        break;

                    case 2:
                        m20 = value;
                        break;
                    case 3:

                        m30 = value;

                        break;

                    case 4:

                        m01 = value;

                        break;

                    case 5:

                        m11 = value;

                        break;

                    case 6:

                        m21 = value;

                        break;

                    case 7:

                        m31 = value;

                        break;

                    case 8:

                        m02 = value;

                        break;

                    case 9:

                        m12 = value;

                        break;

                    case 10:

                        m22 = value;

                        break;

                    case 11:

                        m32 = value;

                        break;

                    case 12:

                        m03 = value;

                        break;

                    case 13:

                        m13 = value;

                        break;

                    case 14:

                        m23 = value;

                        break;

                    case 15:

                        m33 = value;

                        break;

                    default:

                        throw new IndexOutOfRangeException("Index out of Range!");
                }
            }
        }

        //The lossyScale method in the Matrix4x4 class attempts to get a scale value from the matrix.
        //It is a convenience property that tries to match the scale from the matrix as much as possible.
        //However, the accuracy of the value depends on whether the given matrix is orthogonal or not.
        //So, it provides an estimate of the scale, but it may not always be completely accurate.
        public Vec3 lossyScale
        {
            get
            {
                //Get the magnitude of each column vector using the GetColumn method and create a new Vec3 object with the magnitudes.
                //In a 4x4 matrix, the first column represents the scaling applied along the x-axis, the second column represents the scaling along the y-axis, and the third column represents the scaling along the z-axis. 
                //By calculating the magnitude of each column vector, you can determine the scale factors along each axis. 
                return new Vec3(GetColumn(0).magnitude, GetColumn(1).magnitude, GetColumn(2).magnitude);
            }
        }

        //Sets values to zero
        public static MatrixCustom zero
        {
            get
            {
                return new MatrixCustom()
                {
                    m00 = 0.0f,
                    m01 = 0.0f,
                    m02 = 0.0f,
                    m03 = 0.0f,
                    m10 = 0.0f,
                    m11 = 0.0f,
                    m12 = 0.0f,
                    m13 = 0.0f,
                    m20 = 0.0f,
                    m21 = 0.0f,
                    m22 = 0.0f,
                    m23 = 0.0f,
                    m30 = 0.0f,
                    m31 = 0.0f,
                    m32 = 0.0f,
                    m33 = 0.0f
                };
            }
        }

        //The Matrix4x4 identity represents the default state of a matrix where all elements are set to their default values.
        //It is an identity matrix, meaning it has a diagonal of ones and all other elements are zeros.
        //Properties:
        //1)It's always a square matrix
        //2)By multiplying any matrix by the unit matrix, the proper matrix is ​​obtained.
        //3)We always get a matrix identity after multiplying two inverse matrices.
        //A = [2, 0; 0, 2]  (inverse matrix of B), B = [0.5, 0; 0, 0.5] (inverse matrix of A)
        //A * B = [20.5 + 00, 20 + 00.5; 00.5 + 20, 00 + 20.5] = [1, 0; 0, 1]
        //https://matematicasguia.com/que-es-una-matriz-identidad/
        public static MatrixCustom identity
        {
            get
            {
                MatrixCustom m = zero;
                m.m00 = 1.0f;
                m.m11 = 1.0f;
                m.m22 = 1.0f;
                m.m33 = 1.0f;

                return m;
            }
        }

        //Formula to calculate the inverse of a matrix A^-1= adj(A)/det(A) (one of various methods)
        //If matrix A is non-singular (meaning it has an inverse), then there exists a matrix A-1 (called the inverse matrix of A) that satisfies the property:
        //AA-1 = A-1A = I, where I is the identity matrix.
        //In other words, when you multiply matrix A by its inverse A-1 or vice versa, the result is the identity matrix.
        //https://byjus.com/maths/inverse-matrix/#:~:text=A-1%3D%20adj(A)%2Fdet(A)%2C&text=take%20the%20transpose%20of%20a%20cofactor%20matrix.&text=Here%2C%20Mij%20refers%20to,adjoint%20of%20a%20matrix%20here.
        public static MatrixCustom Inverse(MatrixCustom m) //Devuelve la inversa de la matriz ingresada
        {
            float detA = Determinant(m);//To have an inverse it nor only has to have a determinant but also the detrminant should not be equal to zero.
                                        //When the determinant is zero, it means the matrix is singular, and there is no unique inverse.
                                        //Only square matrices have determinants!!!

            if (detA == 0)
            {
                return zero;
            }

            MatrixCustom aux = new MatrixCustom()
            {
                //Cofactor expansion->https://math.libretexts.org/Bookshelves/Linear_Algebra/Interactive_Linear_Algebra_(Margalit_and_Rabinoff)/04%3A_Determinants/4.02%3A_Cofactor_Expansions
                //Cofactor expansion is a method used to calculate the determinant of a matrix by expanding along a row or column.
                //The cofactor of an element in a matrix is the signed determinant of the submatrix obtained by excluding the row and column containing that element.
                //In the context of finding the adjugate matrix, we use cofactor expansion to calculate the cofactors for each element of the original matrix.
                //The cofactors are then assigned to the elements of the adjugate matrix.
                //The adjugate matrix, also known as the adjoint matrix, is obtained by flipping the rows and columns of the cofactor matrix.
                //It's like flipping a matrix horizontally.
                //https://www.problemasyecuaciones.com/matrices/matriz-adjunta-ejemplos-calcular-matrices-cofactores.html
                //The Steps:
                //1)We start by defining an auxiliary matrix where each element represents the cofactor of the corresponding element in the original matrix.
                //2)For each element of the auxiliary matrix, we calculate its cofactor using cofactor expansion.
                //This involves calculating the determinant of the submatrix obtained by excluding the row and column containing that element, and multiplying it by the appropriate sign.
                //3)We assign the calculated cofactors to the elements of the adjugate matrix.
                //4)We repeat steps 2 and 3 for the remaining elements of the first row.
                //5)We continue this process for the remaining rows, calculating cofactors and assigning them to the adjugate matrix.
                //------0,n---------
                m00 = m.m11 * m.m22 * m.m33 + m.m12 * m.m23 * m.m31 + m.m13 * m.m21 * m.m32 - m.m11 * m.m23 * m.m32 - m.m12 * m.m21 * m.m33 - m.m13 * m.m22 * m.m31,//Calculate the cofactor for the element at position (0,0)
                m01 = m.m01 * m.m23 * m.m32 + m.m02 * m.m21 * m.m33 + m.m03 * m.m22 * m.m31 - m.m01 * m.m22 * m.m33 - m.m02 * m.m23 * m.m31 - m.m03 * m.m21 * m.m32,//Calculate the cofactor for the element at position (0,1)
                m02 = m.m01 * m.m12 * m.m33 + m.m02 * m.m13 * m.m32 + m.m03 * m.m11 * m.m32 - m.m01 * m.m13 * m.m32 - m.m02 * m.m11 * m.m33 - m.m03 * m.m12 * m.m31,//Calculate the cofactor for the element at position (0,2)
                m03 = m.m01 * m.m13 * m.m22 + m.m02 * m.m11 * m.m23 + m.m03 * m.m12 * m.m21 - m.m01 * m.m12 * m.m23 - m.m02 * m.m13 * m.m21 - m.m03 * m.m11 * m.m22,//Calculate the cofactor for the element at position (0,3)
                //-------1,n--------					     								    
                m10 = m.m10 * m.m23 * m.m32 + m.m12 * m.m20 * m.m33 + m.m13 * m.m22 * m.m30 - m.m10 * m.m22 * m.m33 - m.m12 * m.m23 * m.m30 - m.m13 * m.m20 * m.m32,//Calculate the cofactor for the element at position (1,0)
                m11 = m.m00 * m.m22 * m.m33 + m.m02 * m.m23 * m.m30 + m.m03 * m.m20 * m.m32 - m.m00 * m.m23 * m.m32 - m.m02 * m.m20 * m.m33 - m.m03 * m.m22 * m.m30,//Calculate the cofactor for the element at position (1,1)
                m12 = m.m00 * m.m13 * m.m32 + m.m02 * m.m10 * m.m33 + m.m03 * m.m12 * m.m30 - m.m00 * m.m12 * m.m33 - m.m02 * m.m13 * m.m30 - m.m03 * m.m10 * m.m32,//Calculate the cofactor for the element at position (1,2)
                m13 = m.m00 * m.m12 * m.m23 + m.m02 * m.m13 * m.m20 + m.m03 * m.m10 * m.m22 - m.m00 * m.m13 * m.m22 - m.m02 * m.m10 * m.m23 - m.m03 * m.m12 * m.m20,//Calculate the cofactor for the element at position (1,3)
                //-------2,n--------					     								    
                m20 = m.m10 * m.m21 * m.m33 + m.m11 * m.m23 * m.m30 + m.m13 * m.m20 * m.m31 - m.m10 * m.m23 * m.m31 - m.m11 * m.m20 * m.m33 - m.m13 * m.m31 * m.m30,//Calculate the cofactor for the element at position (2,0)
                m21 = m.m00 * m.m23 * m.m31 + m.m01 * m.m20 * m.m33 + m.m03 * m.m21 * m.m30 - m.m00 * m.m21 * m.m33 - m.m01 * m.m23 * m.m30 - m.m03 * m.m20 * m.m31,//Calculate the cofactor for the element at position (2,1)
                m22 = m.m00 * m.m11 * m.m33 + m.m01 * m.m13 * m.m31 + m.m03 * m.m10 * m.m31 - m.m00 * m.m13 * m.m31 - m.m01 * m.m10 * m.m33 - m.m03 * m.m11 * m.m30,//Calculate the cofactor for the element at position (2,2)
                m23 = m.m00 * m.m13 * m.m21 + m.m01 * m.m10 * m.m23 + m.m03 * m.m11 * m.m31 - m.m00 * m.m11 * m.m23 - m.m01 * m.m13 * m.m20 - m.m03 * m.m10 * m.m21,//Calculate the cofactor for the element at position (2,3)
                //------3,n---------					     								    
                m30 = m.m10 * m.m22 * m.m31 + m.m11 * m.m20 * m.m32 + m.m12 * m.m21 * m.m30 - m.m00 * m.m21 * m.m32 - m.m11 * m.m22 * m.m30 - m.m12 * m.m20 * m.m31,//Calculate the cofactor for the element at position (3,0)
                m31 = m.m00 * m.m21 * m.m32 + m.m01 * m.m22 * m.m30 + m.m02 * m.m20 * m.m31 - m.m00 * m.m22 * m.m31 - m.m01 * m.m20 * m.m32 - m.m02 * m.m21 * m.m30,//Calculate the cofactor for the element at position (3,1)
                m32 = m.m00 * m.m12 * m.m31 + m.m01 * m.m10 * m.m32 + m.m02 * m.m11 * m.m30 - m.m00 * m.m11 * m.m32 - m.m01 * m.m12 * m.m30 - m.m02 * m.m10 * m.m31,//Calculate the cofactor for the element at position (3,2)
                m33 = m.m00 * m.m11 * m.m22 + m.m01 * m.m12 * m.m20 + m.m02 * m.m10 * m.m21 - m.m00 * m.m12 * m.m21 - m.m01 * m.m10 * m.m22 - m.m02 * m.m11 * m.m20 //Calculate the cofactor for the element at position (3,3)
            };

            //Represents the new inverse matrix. The reason for dividing by the determinant is based on the mathematical definition of the inverse of a matrix.
            //For a square matrix A, its inverse A^-1 is defined as a matrix that satisfies the equation A * A^-1 = I, where I is the identity matrix.
            //When calculating the inverse of a matrix using the adjugate matrix method, we have the formula: A ^ -1 = adj(A) / det(A), where adj(A) represents the adjugate matrix of A and det(A) is the determinant of A.
            //This is a property of the inverse.
            //Dividing each element of the adjugate matrix by the determinant ensures that the resulting matrix, when multiplied with the original matrix, yields the identity matrix.
            MatrixCustom ret = new MatrixCustom()
            {
                //Calculate the inverse of the determinant and divide each element in the cofactor matrix by it
                m00 = aux.m00 / detA,
                m01 = aux.m01 / detA,
                m02 = aux.m02 / detA,
                m03 = aux.m03 / detA,
                m10 = aux.m10 / detA,
                m11 = aux.m11 / detA,
                m12 = aux.m12 / detA,
                m13 = aux.m13 / detA,
                m20 = aux.m20 / detA,
                m21 = aux.m21 / detA,
                m22 = aux.m22 / detA,
                m23 = aux.m23 / detA,
                m30 = aux.m30 / detA,
                m31 = aux.m31 / detA,
                m32 = aux.m32 / detA,
                m33 = aux.m33 / detA
            };

            return ret;
        }

        //The transpose method flips the rows and columns of a matrix, effectively swapping its elements across the main diagonal.
        //The transpose function can be useful in matrix operations for tasks like calculating dot products, solving systems of equations, and finding orthogonal matrices.
        //It helps rearrange the elements in a way that simplifies calculations or aligns matrices for specific operations.
        public static MatrixCustom Transpose(MatrixCustom m)
        {
            return new MatrixCustom()
            {
                m01 = m.m10,
                m02 = m.m20,
                m03 = m.m30,
                m10 = m.m01,
                m12 = m.m21,
                m13 = m.m31,
                m20 = m.m02,
                m21 = m.m12,
                m23 = m.m32,
                m30 = m.m03,
                m31 = m.m13,
                m32 = m.m23,
            };
        }

        //Set the Transform, Rotation and Scale factors
        public void SetTRS(Vector3 pos, Quaternion q, Vector3 s)
        {
            this = TRS(pos, q, s);
        }

        //The translate function in the matrix class is used to create a translation matrix.
        //It allows you to move an object or transform its position in a specified direction by applying the translation values provided.
        public static MatrixCustom Translate(Vector3 v)
        {
            //Setting the values to 0 or 1 in the translation matrix ensures that the elements responsible for scaling and rotation remain unchanged, as they are not affected by a translation operation.
            MatrixCustom m;
            m.m00 = 1f;
            m.m01 = 0.0f;
            m.m02 = 0.0f;
            m.m03 = v.x;
            m.m10 = 0.0f;
            m.m11 = 1f;
            m.m12 = 0.0f;
            m.m13 = v.y;
            m.m20 = 0.0f;
            m.m21 = 0.0f;
            m.m22 = 1f;
            m.m23 = v.z;
            m.m30 = 0.0f;
            m.m31 = 0.0f;
            m.m32 = 0.0f;
            m.m33 = 1f;

            return m;
        }

        //The rotation property calculates and returns the quaternion representation of the rotation component of the matrix.
        //It extracts the rotation information from the matrix and constructs a Quaternion object accordingly. 
        public Quaternion rotation
        {
            get
            {
                MatrixCustom m = this;
                Quaternion q = new Quaternion();

                //The calculations are used to determine the components of the quaternion rotation.
                //These formulas are derived from the mathematical operations involved in extracting the quaternion representation of a rotation from a matrix.
                //The Mathf.Sqrt and Mathf.Max functions are used to ensure the values are not negative.
                //Dividing by 2 after calculating the square root of the maximum value ensures that the resulting quaternion components have magnitudes in the range of -1 to 1.
                q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;                                                                       
                q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
                q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
                q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;

                //The Mathf.Sign function is used to handle the sign of the calculated values.
                //It is doing the calculations using different elements of the matrix (m) in order to properly determine the signs of the x, y, and z components of the quaternion rotation.
                q.x *= Mathf.Sign(q.x * (m[2, 1] - m[1, 2]));
                q.y *= Mathf.Sign(q.y * (m[0, 2] - m[2, 0]));
                q.z *= Mathf.Sign(q.z * (m[1, 0] - m[0, 1]));

                return q;
            }
        }

        //This function takes a quaternion q as input and returns a rotation matrix m that represents the rotation transformation corresponding to the given quaternion.
        public static MatrixCustom Rotate(Quaternion q)
        {
            //Multiplying the quaternion components by 2 is a common step in calculating a rotation matrix from a quaternion. It helps simplify subsequent calculations.
            double num1 = q.x * 2f;//Multiply x component of the quaternion by 2.
            double num2 = q.y * 2f;//Multiply y component of the quaternion by 2.
            double num3 = q.z * 2f;//Multiply z component of the quaternion by 2.

            //Squaring the components is necessary to obtain the terms required for the diagonal elements of the rotation matrix.
            double num4 = q.x * num1;//Square the x component.
            double num5 = q.y * num2;//Square the y component.
            double num6 = q.z * num3;//Square the z component.

            //These terms are used to determine the off-diagonal elements of the rotation matrix.
            double num7 = q.x * num2;//Multiply x and y components.
            double num8 = q.x * num3;//Multiply x and z components.
            double num9 = q.y * num3;//Multiply y and z components.
            double num10 = q.w * num1;//Multiply scalar component by x component.
            double num11 = q.w * num2;//Multiply scalar component by y component.
            double num12 = q.w * num3;//Multiply scalar component by z component.

            MatrixCustom m;

            //By assigning these calculated values to the corresponding elements of the rotation matrix, the resulting matrix represents the desired rotation transformation.
            m.m00 = (float)(1.0 - num5 + num6);//This element represents the scale factor or cosine of the rotation around the X-axis.
                                               //It is derived from the quaternion components and is calculated as 1 minus the squared Y component plus the squared Z component.
            m.m10 = (float)(num7 + num12);//This element represents the sine of the rotation around the X-axis.
                                          //It is derived from the quaternion components and is calculated by adding the product of the X and Y components with the product of the scalar component and the Z component.
            m.m20 = (float)(num8 - num11);//This element represents the sine of the rotation around the X-axis.
                                          //It is derived from the quaternion components and is calculated by subtracting the product of the X and Z components with the product of the scalar component and the Y component.
            m.m30 = 0.0f;//This element represents the translation in the X direction.
                         //Since this is a rotation matrix, there is no translation component along the X-axis, so it is set to 0.
            m.m01 = (float)(num7 - num12);//This element represents the sine of the rotation around the Y-axis.
                                          //It is derived from the quaternion components and is calculated by subtracting the product of the X and Y components with the product of the scalar component and the Z component.
            m.m11 = (float)(1.0 - num4 + num6);//This element represents the scale factor or cosine of the rotation around the Y-axis.
                                               //It is derived from the quaternion components and is calculated as 1 minus the squared X component plus the squared Z component.
            m.m21 = (float)(num9 + num10);//This element represents the sine of the rotation around the Y-axis.
                                          //It is derived from the quaternion components and is calculated by adding the product of the Y and Z components with the product of the scalar component and the X component.
            m.m31 = 0.0f;//This element represents the translation in the Y direction.
                         //Since this is a rotation matrix, there is no translation component along the Y-axis, so it is set to 0.
            m.m02 = (float)(num8 + num11);//This element represents the sine of the rotation around the Z-axis.
                                          //It is derived from the quaternion components and is calculated by adding the product of the X and Z components with the product of the scalar component and the Y component.
            m.m12 = (float)(num9 - num10);//This element represents the sine of the rotation around the Z-axis.
                                          //It is derived from the quaternion components and is calculated by subtracting the product of the Y and Z components with the product of the scalar component and the X component.
            m.m22 = (float)(1.0 - num4 + num5);//This element represents the scale factor or cosine of the rotation around the Z-axis.
                                               //It is derived from the quaternion components and is calculated as 1 minus the squared X component plus the squared Y component.
            m.m32 = 0.0f;//This element represents the translation in the Z direction.
                         //Since this is a rotation matrix, there is no translation component along the Z-axis, so it is set to 0.
            m.m03 = 0.0f;//This element represents the translation in the X direction.
                         //Since this is a rotation matrix, there is no translation component along the X-axis, so it is set to 0.
            m.m13 = 0.0f;//This element represents the translation in the Y direction.
                         //Since this is a rotation matrix, there is no translation component along the Y-axis, so it is set to 0.
            m.m23 = 0.0f;//This element represents the translation in the Z direction.
                         //Since this is a rotation matrix, there is no translation component along the Z-axis, so it is set to 0.
            m.m33 = 1f;//This element represents the homogeneous coordinate or scale factor.
                       //It is always set to 1 in a rotation matrix, as it ensures that translations are not affected by the rotation transformation.

            return m;
        }

        //The Scale function creates a scaling matrix based on a given Vector3 parameter v, which represents the scale factors along the x, y, and z axes. 
        public static MatrixCustom Scale(Vector3 v)
        {
            MatrixCustom m;

            m.m00 = v.x;
            m.m01 = 0.0f;
            m.m02 = 0.0f;
            m.m03 = 0.0f;
            m.m10 = 0.0f;
            m.m11 = v.y;
            m.m12 = 0.0f;
            m.m13 = 0.0f;
            m.m20 = 0.0f;
            m.m21 = 0.0f;
            m.m22 = v.z;
            m.m23 = 0.0f;
            m.m30 = 0.0f;
            m.m31 = 0.0f;
            m.m32 = 0.0f;
            m.m33 = 1f;

            return m;
        }

        //The TRS function combines translation, rotation, and scaling transformations into a single transformation matrix. 
        public static MatrixCustom TRS(Vector3 pos, Quaternion q, Vector3 s)
        {
            return (Translate(pos) * Rotate(q) * Scale(s));

        }

        //The checks if the matrix represents a valid translation-rotation-scaling (TRS) transformation.
        public bool ValidTRS()
        {
            //Check if the matrix is a valid TRS matrix
            //Translation: The last row of the matrix should be (0, 0, 0, 1).
            //Translation Check: The method first checks if the last row of the matrix is (0, 0, 0, 1).
            //This ensures that the translation part of the matrix is valid.
            if (m30 != 0 || m31 != 0 || m32 != 0 || m33 != 1)
            {
                return false;
            }

            //Rotation: The upper-left 3x3 submatrix should be an orthogonal matrix.
            //An orthogonal matrix is a square matrix where the columns and rows are orthogonal unit vectors.
            //This means that the dot product of any two columns (or rows) is zero, and the length of each column (or row) is 1.
            //In other words, the transpose of the matrix is equal to its inverse.
            //Rotation Check: The method checks if the upper-left 3x3 submatrix (columns 0, 1, and 2) is an orthogonal matrix.
            //It does this by verifying that the dot products of any two columns are approximately zero.
            //It also checks if the magnitudes of the columns are approximately 1.
            //These checks ensure that the rotation part of the matrix is valid.
            Vec3 column0 = new Vec3(m00, m10, m20);
            Vec3 column1 = new Vec3(m01, m11, m21);
            Vec3 column2 = new Vec3(m02, m12, m22);

            if (!Mathf.Approximately(Vec3.Dot(column0, column1), 0) ||
                !Mathf.Approximately(Vec3.Dot(column0, column2), 0) ||
                !Mathf.Approximately(Vec3.Dot(column1, column2), 0))
            {
                return false;
            }

            //Scale: The scale factors should be positive
            //Scale Check: The method verifies that the diagonal elements of the matrix (m00, m11, and m22) representing the scale factors are positive.
            //This ensures that the scale part of the matrix is valid.
            if (m00 < 0 || m11 < 0 || m22 < 0)
            {
                return false;
            }

            return true;
        }

        //The MultiplyVector method multiplies a 4D vector by the matrix.
        //It applies the transformation represented by the matrix to the vector and returns the resulting transformed vector.
        //Multiplies the x, y, and z components of the vector, but does not multiply the w component.
        //It treats the vector as a direction and magnitude, considering only the rotation part of the matrix.
        public Vec3 MultiplyVector(Vec3 v)
        {
            Vec3 v3;
            v3.x = (float)((double)m00 * (double)v.x + (double)m01 * (double)v.y + (double)m02 * (double)v.z);
            v3.y = (float)((double)m10 * (double)v.x + (double)m11 * (double)v.y + (double)m12 * (double)v.z);
            v3.z = (float)((double)m20 * (double)v.x + (double)m21 * (double)v.y + (double)m22 * (double)v.z);

            return v3;
        }

        //Performs a 3x4 matrix multiplication, where the w component of the input vector is assumed to be 1.
        //This method is typically used for transforming points in 3D space, where the fourth component of the vector is considered as a constant value.
        public Vec3 MultiplyPoint3x4(Vec3 p)
        {
            Vec3 v3;
            v3.x = (float)((double)m00 * (double)p.x + (double)m01 * (double)p.y + (double)m02 * (double)p.z) + m03;
            v3.y = (float)((double)m10 * (double)p.x + (double)m11 * (double)p.y + (double)m12 * (double)p.z) + m13;
            v3.z = (float)((double)m20 * (double)p.x + (double)m21 * (double)p.y + (double)m22 * (double)p.z) + m23;

            return v3;
        }

        //Performs a complete 4x4 matrix multiplication, where the w component of the input vector is also multiplied by the matrix.
        //This method is commonly used for transforming homogeneous vectors or points, where the w component represents a scaling factor or a perspective coordinate.
        public Vec3 MultiplyPoint(Vec3 p)
        {
            Vec3 v3;

            v3.x = (float)((double)m00 * (double)p.x + (double)m01 * (double)p.y + (double)m02 * (double)p.z) + m03;
            v3.y = (float)((double)m10 * (double)p.x + (double)m11 * (double)p.y + (double)m12 * (double)p.z) + m13;
            v3.z = (float)((double)m20 * (double)p.x + (double)m21 * (double)p.y + (double)m22 * (double)p.z) + m23;

            float num = 1f / ((float)((double)m30 * (double)p.x + (double)m31 * (double)p.y + (double)m32 * (double)p.z) + m33);

            v3.x *= num;
            v3.y *= num;
            v3.z *= num;

            return v3;
        }

        //Sets rows
        public void SetRow(int index, Vector4 row)
        {
            this[index, 0] = row.x;
            this[index, 1] = row.y;
            this[index, 2] = row.z;
            this[index, 3] = row.w;
        }

        //Sets columns
        public void SetColumn(int index, Vector4 col)
        {
            this[0, index] = col.x;
            this[1, index] = col.y;
            this[2, index] = col.z;
            this[3, index] = col.w;
        }

        //Gets rows
        public Vector4 GetRow(int index)
        {
            switch (index)
            {
                case 0:

                    return new Vector4(m00, m01, m02, m03);

                case 1:

                    return new Vector4(m10, m11, m12, m13);

                case 2:

                    return new Vector4(m20, m21, m22, m23);

                case 3:

                    return new Vector4(m30, m31, m32, m33);

                default:

                    throw new IndexOutOfRangeException("Index out of Range!");
            }
        }

        //Gets columns
        public Vector4 GetColumn(int i)
        {
            return new Vector4(this[0, i], this[1, i], this[2, i], this[3, i]);
        }

        //The implementation of this overridden Equals method checks if the other object is an instance of MatrixCustom using the is keyword.
        //If it is, it assigns the other object to a new variable other1 of type MatrixCustom.
        //Then, it calls the Equals method with the other1 object as a parameter to compare the two instances.
        public override bool Equals(object other) => other is MatrixCustom other1 && this.Equals(other1);

        //It returns if the matrix is equal or not to another matrix by chhecking every element.
        public bool Equals(MatrixCustom other)
        {
            int num;

            //It checks if each column of the current matrix is equal to the corresponding column of the other matrix by calling the GetColumn method.
            if (GetColumn(0).Equals(other.GetColumn(0)))
            {
                Vector4 col = GetColumn(1);

                if (col.Equals(other.GetColumn(1)))
                {
                    col = GetColumn(2);

                    if (col.Equals(other.GetColumn(2)))
                    {
                        col = GetColumn(3);

                        num = col.Equals(other.GetColumn(3)) ? 1 : 0;

                        return num != 0;
                    }
                }
            }
            num = 0;

            return num != 0;
        }

        //The method iterates over the columns of the matrix, calculates the hash code for each column using the GetHashCode method of the Vector4 class,
        //and combines the hash codes using bitwise operations to produce a final hash code for the matrix.
        public override int GetHashCode()
        {
            //The method begins by assigning the first column of the matrix to the col variable and calculates its hash code using the GetHashCode method.
            //This initial hash code is stored in the hashCode variable.
            //Next, the second column's hash code is calculated, and then bitwise left-shifted by 2(value is shifted to the left by 2 positions, effectively multiplying it by 2 raised to the power of 2).
            //This shifted hash code is stored in the num1 variable.
            //The hashCode and num1 variables are then combined using the bitwise XOR operator (^) and stored in the num2 variable.
            //The process is repeated for the third column, with the calculated hash code right-shifted by 2 and combined with the num2 variable using ^, resulting in the num4 variable.
            //Finally, the hash code of the fourth column is calculated and right-shifted by 1, stored in the num5 variable.
            //The num4 and num5 variables are combined using ^, and the resulting value is returned as the hash code for the MatrixCustom object.
            Vector4 col = GetColumn(0);
            int hashCode = col.GetHashCode();

            col = GetColumn(1);
            int num1 = col.GetHashCode() << 2;
            int num2 = hashCode ^ num1;

            col = GetColumn(2);
            int num3 = col.GetHashCode() >> 2;
            int num4 = num2 ^ num3;

            col = GetColumn(3);
            int num5 = col.GetHashCode() >> 1;

            return num4 ^ num5;
        }

        //Gives me the determinant of this matrix
        public float determinant => Determinant(this);

        //The determinant of a square matrix is a scalar value that represents certain properties of the matrix.
        //In the case of a 4x4 matrix, the determinant is calculated by expanding along any row or column and summing the products of the elements with their corresponding cofactors.
        //https://byjus.com/maths/determinant-of-4x4-matrix/
        public static float Determinant(MatrixCustom m)
        {
            return
                m[0, 3] * m[1, 2] * m[2, 1] * m[3, 0] - m[0, 2] * m[1, 3] * m[2, 1] * m[3, 0] -
                m[0, 3] * m[1, 1] * m[2, 2] * m[3, 0] + m[0, 1] * m[1, 3] * m[2, 2] * m[3, 0] +
                m[0, 2] * m[1, 1] * m[2, 3] * m[3, 0] - m[0, 1] * m[1, 2] * m[2, 3] * m[3, 0] -
                m[0, 3] * m[1, 2] * m[2, 0] * m[3, 1] + m[0, 2] * m[1, 3] * m[2, 0] * m[3, 1] +
                m[0, 3] * m[1, 0] * m[2, 2] * m[3, 1] - m[0, 0] * m[1, 3] * m[2, 2] * m[3, 1] -
                m[0, 2] * m[1, 0] * m[2, 3] * m[3, 1] + m[0, 0] * m[1, 2] * m[2, 3] * m[3, 1] +
                m[0, 3] * m[1, 1] * m[2, 0] * m[3, 2] - m[0, 1] * m[1, 3] * m[2, 0] * m[3, 2] -
                m[0, 3] * m[1, 0] * m[2, 1] * m[3, 2] + m[0, 0] * m[1, 3] * m[2, 1] * m[3, 2] +
                m[0, 1] * m[1, 0] * m[2, 3] * m[3, 2] - m[0, 0] * m[1, 1] * m[2, 3] * m[3, 2] -
                m[0, 2] * m[1, 1] * m[2, 0] * m[3, 3] + m[0, 1] * m[1, 2] * m[2, 0] * m[3, 3] +
                m[0, 2] * m[1, 0] * m[2, 1] * m[3, 3] - m[0, 0] * m[1, 2] * m[2, 1] * m[3, 3] -
                m[0, 1] * m[1, 0] * m[2, 2] * m[3, 3] + m[0, 0] * m[1, 1] * m[2, 2] * m[3, 3];
        }

        #endregion

        #region Operators

        //Row times column, over each component
        public static MatrixCustom operator *(MatrixCustom a, MatrixCustom b)
        {
            MatrixCustom ret = zero;

            for (int i = 0; i < 4; i++)
            {
                ret.SetColumn(i, a * b.GetColumn(i));
            }
            //ret.m00 = (float)((double)a.m00 * (double)b.m00 + (double)a.m01 * (double)b.m10 + (double)a.m02 * (double)b.m20 + (double)a.m03 * (double)b.m30);
            //ret.m01 = (float)((double)a.m00 * (double)b.m01 + (double)a.m01 * (double)b.m11 + (double)a.m02 * (double)b.m21 + (double)a.m03 * (double)b.m31);
            //ret.m02 = (float)((double)a.m00 * (double)b.m02 + (double)a.m01 * (double)b.m12 + (double)a.m02 * (double)b.m22 + (double)a.m03 * (double)b.m32);
            //ret.m03 = (float)((double)a.m00 * (double)b.m03 + (double)a.m01 * (double)b.m13 + (double)a.m02 * (double)b.m23 + (double)a.m03 * (double)b.m33);
            //ret.m10 = (float)((double)a.m10 * (double)b.m00 + (double)a.m11 * (double)b.m10 + (double)a.m12 * (double)b.m20 + (double)a.m13 * (double)b.m30);
            //ret.m11 = (float)((double)a.m10 * (double)b.m01 + (double)a.m11 * (double)b.m11 + (double)a.m12 * (double)b.m21 + (double)a.m13 * (double)b.m31);
            //ret.m12 = (float)((double)a.m10 * (double)b.m02 + (double)a.m11 * (double)b.m12 + (double)a.m12 * (double)b.m22 + (double)a.m13 * (double)b.m32);
            //ret.m13 = (float)((double)a.m10 * (double)b.m03 + (double)a.m11 * (double)b.m13 + (double)a.m12 * (double)b.m23 + (double)a.m13 * (double)b.m33);
            //ret.m20 = (float)((double)a.m20 * (double)b.m00 + (double)a.m21 * (double)b.m10 + (double)a.m22 * (double)b.m20 + (double)a.m23 * (double)b.m30);
            //ret.m21 = (float)((double)a.m20 * (double)b.m01 + (double)a.m21 * (double)b.m11 + (double)a.m22 * (double)b.m21 + (double)a.m23 * (double)b.m31);
            //ret.m22 = (float)((double)a.m20 * (double)b.m02 + (double)a.m21 * (double)b.m12 + (double)a.m22 * (double)b.m22 + (double)a.m23 * (double)b.m32);
            //ret.m23 = (float)((double)a.m20 * (double)b.m03 + (double)a.m21 * (double)b.m13 + (double)a.m22 * (double)b.m23 + (double)a.m23 * (double)b.m33);
            //ret.m30 = (float)((double)a.m30 * (double)b.m00 + (double)a.m31 * (double)b.m10 + (double)a.m32 * (double)b.m20 + (double)a.m33 * (double)b.m30);
            //ret.m31 = (float)((double)a.m30 * (double)b.m01 + (double)a.m31 * (double)b.m11 + (double)a.m32 * (double)b.m21 + (double)a.m33 * (double)b.m31);
            //ret.m32 = (float)((double)a.m30 * (double)b.m02 + (double)a.m31 * (double)b.m12 + (double)a.m32 * (double)b.m22 + (double)a.m33 * (double)b.m32);
            //ret.m33 = (float)((double)a.m30 * (double)b.m03 + (double)a.m31 * (double)b.m13 + (double)a.m32 * (double)b.m23 + (double)a.m33 * (double)b.m33);

            return ret;
        }

        //m4x4 * m1x4, returns a vector4 or m1x4
        public static Vector4 operator *(MatrixCustom a, Vector4 v)
        {
            Vector4 ret;

            ret.x = (float)((double)a.m00 * (double)v.x + (double)a.m01 * (double)v.y + (double)a.m02 * (double)v.z + (double)a.m03 * (double)v.w);
            ret.y = (float)((double)a.m10 * (double)v.x + (double)a.m11 * (double)v.y + (double)a.m12 * (double)v.z + (double)a.m13 * (double)v.w);
            ret.z = (float)((double)a.m20 * (double)v.x + (double)a.m21 * (double)v.y + (double)a.m22 * (double)v.z + (double)a.m23 * (double)v.w);
            ret.w = (float)((double)a.m30 * (double)v.x + (double)a.m31 * (double)v.y + (double)a.m32 * (double)v.z + (double)a.m33 * (double)v.w);

            return ret;
        }

        //The == operator compares each column of matrix a with the corresponding column of matrix b using the GetColumn method and the == operator for the Vector4 objects returned by GetColumn.
        //It returns true if all the columns are equal and false otherwise.
        public static bool operator ==(MatrixCustom a, MatrixCustom b) => a.GetColumn(0) == b.GetColumn(0) && a.GetColumn(1) == b.GetColumn(1) && a.GetColumn(2) == b.GetColumn(2) && a.GetColumn(3) == b.GetColumn(3);

        //The != operator negates the result of the == operator.
        //It returns true if the matrices are not equal and false if they are equal.
        public static bool operator !=(MatrixCustom a, MatrixCustom b) => !(a == b);
        
        #endregion
    }
}