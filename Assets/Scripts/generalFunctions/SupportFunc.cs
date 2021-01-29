using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SupportFunc
{
    public class AngleFunc
    {
        public static Vector3 VectorFromAngle(float angle)
        {
            float X = Mathf.Sin(angle * Mathf.Deg2Rad) / Mathf.Sin(90 * Mathf.Deg2Rad);
            float Z = Mathf.Sin((180 - angle - 90) * Mathf.Deg2Rad) / Mathf.Sin(90 * Mathf.Deg2Rad);
            Vector3 normal = new Vector3(X, 0, Z).normalized;
            return normal;
        }
        public static Vector3 NormalToObject(Vector3 from, Vector3 to)
        {
            Vector3 normal = (to - from).normalized;
            normal.Set(normal.x, 0, normal.z);                        
            return normal;
        }
    }
}
