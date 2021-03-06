using System;
using Math = System.Math;

namespace OSTTools {

    [Serializable]
    public struct Vector3 {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3(double x, double y, double z) {
            X = x; Y = y; Z = z;
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2) {
            return new Vector3(
                v1.X + v2.X,
                v1.Y + v2.Y,
                v1.Z + v2.Z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2) {
            return new Vector3(
                v1.X - v2.X,
                v1.Y - v2.Y,
                v1.Z - v2.Z);
        }

        public static Vector3 operator *(Vector3 v1, double v2) {
            return new Vector3(
                v1.X * v2,
                v1.Y * v2,
                v1.Z * v2);
        }

        public Vector3 Normalize() {
            return new Vector3(0.0, 0.0, 0.0);
        }

        public double Distance(Vector3 other) {
            return Math.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y) + (Z - other.Z) * (Z - other.Z));
        }
    }
}