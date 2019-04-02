using System;
using OpenTK;

namespace WorldOfCode
{
    /// <summary>
    /// Contains the projection matrix, and the view matrix we can use to draw elements
    /// </summary>
    public class Camera : Component
    {
        /// <summary>
        /// The main camera used to draw the application
        /// </summary>
        public static Camera Main { get; set; }

        /// <summary>
        /// Basic constructor, sets the singleton if none has been set yet
        /// </summary>
        public Camera()
        {
            if (Main == null)
            {
                Main = this;
            }

            Yaw = -90;
            CalculateDirectionVectors();
            CalculateProjectionMatrix();
        }
        
        #region Projection
        /// <summary>
        /// The projection matrix, this is the main part of the camera, along with the view matrix
        /// </summary>
        private Matrix4 _projection;
        /// <summary>
        /// Get the projection so it can be passed to the shader
        /// </summary>
        public ref Matrix4 Projection => ref _projection;

        /// <summary>
        /// The field of view used for the projection matrix
        /// </summary>
        private float _fov = 45;
        /// <summary>
        /// The field of view used for the projection matrix
        /// </summary>
        public float Fov
        {
            get => _fov;
            set
            {
                if (value >= 100.0f)
                {
                    _fov = 100.0f;
                }
                else if (value <= 30.0f)
                {
                    _fov = 30.0f;
                }
                else
                {
                    _fov = value;
                }
                CalculateProjectionMatrix();
            }
        }

        /// <summary>
        /// The aspect ratio of the window
        /// </summary>
        private float _aspectRatio;
        /// <summary>
        /// The aspect ratio of the window
        /// </summary>
        public float AspectRatio
        {
            get => _aspectRatio;
            set
            {
                _aspectRatio = value; 
                CalculateProjectionMatrix();
            }
        }

        /// <summary>
        /// Recalculates the projection matrix
        /// </summary>
        private void CalculateProjectionMatrix()
        {
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(Fov), 500 / 500f, 0.001f, 1000f);
        }
        #endregion Projection

        #region View
        /// <summary>
        /// The view matrix is one of the core parts of the camera component
        /// </summary>
        private Matrix4 _view;
        /// <summary>
        /// Get the view matrix, so it can be passed to the shader
        /// </summary>
        public ref Matrix4 View => ref _view;

        /// <summary>
        /// The position of the camera in 3D space
        /// </summary>
        private Vector3 _position;
        /// <summary>
        /// The position of the camera in 3D space
        /// </summary>
        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                CalculateViewMatrix();
            }
            
        }

        /// <summary>
        /// The pitch of the camera is the rotation around the Z axis
        /// </summary>
        private float _pitch;
        /// <summary>
        /// The pitch of the camera is the rotation around the Z axis
        /// </summary>
        public float Pitch
        {
            get => _pitch;
            set
            {
                _pitch = MathHelper.Clamp(value, -89f, 89f);
                CalculateDirectionVectors();
            }
        }
        /// <summary>
        /// The yaw of the camera is the rotation around the Y axis
        /// </summary>
        private float _yaw;
        /// <summary>
        /// The yaw of the camera is the rotation around the Y axis
        /// </summary>
        public float Yaw
        {
            get => _yaw;
            set
            {
                _yaw = value;
                CalculateDirectionVectors();
            }
        }
        
        /// <summary>
        /// The forwards direction of the camera
        /// </summary>
        public Vector3 Front { get; private set; }
        /// <summary>
        /// The right direction of the camera
        /// </summary>
        public Vector3 Right { get; private set; }
        /// <summary>
        /// The up direction of the camera
        /// </summary>
        public Vector3 Up { get; private set; }

        /// <summary>
        /// Calculates the direction vectors based on the Yaw and Pitch
        /// </summary>
        private void CalculateDirectionVectors()
        {
            //Start getting the forwards facing vectors
            Front = new Vector3((float)Math.Cos(MathHelper.DegreesToRadians(Pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(Yaw)),
                (float)Math.Sin(MathHelper.DegreesToRadians(Pitch)), 
                (float)Math.Cos(MathHelper.DegreesToRadians(Pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(Yaw)));
            Front = Vector3.Normalize(Front);
            
            //Then get the right and up facing vectors
            Right = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, Front));
            Up = Vector3.Normalize(Vector3.Cross(Front, Right));
            
            CalculateViewMatrix();
        }

        /// <summary>
        /// Calculate the view matrix
        /// </summary>
        private void CalculateViewMatrix() => _view = Matrix4.LookAt(Position, Position + Front, Up);
        #endregion View
    }
}