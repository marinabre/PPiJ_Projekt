using System;
using UnityEngine;

namespace Model
{
    public class GPSLocation
    {
        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public static Vector3 GPSToCartesian(GPSLocation gps)
        {
            var latRad = gps.Latitude * Mathf.Deg2Rad;
            var longRad = gps.Longitude * Mathf.Deg2Rad;
            var x = Mathf.Cos(latRad) * Mathf.Sin(-longRad);
            var y = -1 * Mathf.Sin(latRad);
            var z = -1 * Mathf.Cos(latRad) * Mathf.Cos(-longRad);

            return new Vector3(x, y, z);
        }

        public static GPSLocation RotationToGPS(Vector3 rotation, Vector3 earthOffset)
        {
            var lat = rotation.x - earthOffset.x;
            lat = lat > 180.0f ? lat - 360.0f : lat;

            var lon = rotation.y - earthOffset.y;

            return new GPSLocation { Latitude = lat, Longitude = lon };
        }

        public static float CoordinateDistance(GPSLocation cord1, GPSLocation cord2)
        {
            var latDistance = cord1.Latitude - cord2.Latitude;
            var longDistance = cord1.Longitude - cord2.Longitude;

            longDistance = longDistance > 180.0f ?
                           longDistance - 360 : longDistance;
            longDistance = longDistance < -180.0f ?
                           longDistance + 360 : longDistance;

            var result = Mathf.Sqrt
                         (
                          Mathf.Pow(latDistance, 2.0f) + 
                          Mathf.Pow(longDistance, 2.0f)
                         );

            return result;
        }

        public static float CartesianDistance(GPSLocation cord1, GPSLocation cord2)
        {
            var pos1 = GPSToCartesian(cord1);
            var pos2 = GPSToCartesian(cord2);

            var distance = Mathf.Sqrt(
                                Mathf.Pow(pos1.x - pos2.x, 2.0f) +
                                Mathf.Pow(pos1.y - pos2.y, 2.0f) +
                                Mathf.Pow(pos1.z  - pos2.z, 2.0f));
            
            return distance;
        }
    }
}
