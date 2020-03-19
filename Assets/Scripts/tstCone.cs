using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tstCone : MonoBehaviour {

    public float angle;
    public float minDist;
    public float maxDist;

    private void Update()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = GenerateConeMesh(gameObject, angle, minDist, maxDist);
        GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;
    }

    public static Mesh GenerateConeMesh(GameObject gameObj, float angle_fov, float dist_min, float dist_max)
    {
        int quality = 30;

        //------------- PART 1----------------//
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[4 * quality];   // Could be of size [2 * quality + 2] if circle segment is continuous
        mesh.triangles = new int[3 * 2 * quality];

        Vector3[] normals = new Vector3[4 * quality];
        Vector2[] uv = new Vector2[4 * quality];

        for (int i = 0; i < uv.Length; i++)
            uv[i] = new Vector2(0, 0);
        for (int i = 0; i < normals.Length; i++)
            normals[i] = new Vector3(0, 1, 0);

        mesh.uv = uv;
        mesh.normals = normals;

        //------------- PART 2----------------//
        float angle_lookat = GetLookAtAngle(gameObj);
        float angle_start = angle_lookat - angle_fov;
        float angle_end = angle_lookat + angle_fov;
        float angle_delta = (angle_end - angle_start) / quality;

        float angle_curr = angle_start;
        float angle_next = angle_start + angle_delta;

        Vector3 pos_curr_min = Vector3.zero;
        Vector3 pos_curr_max = Vector3.zero;

        Vector3 pos_next_min = Vector3.zero;
        Vector3 pos_next_max = Vector3.zero;

        Vector3[] vertices = new Vector3[4 * quality];   // Could be of size [2 * quality + 2] if circle segment is continuous
        int[] triangles = new int[3 * 2 * quality];

        for (int i = 0; i < quality; i++)
        {
            Vector3 sphere_curr = new Vector3(
            Mathf.Sin(Mathf.Deg2Rad * (angle_curr)), 0,   // Left handed CW
            Mathf.Cos(Mathf.Deg2Rad * (angle_curr)));

            Vector3 sphere_next = new Vector3(
            Mathf.Sin(Mathf.Deg2Rad * (angle_next)), 0,
            Mathf.Cos(Mathf.Deg2Rad * (angle_next)));

            pos_curr_min = gameObj.transform.position + sphere_curr * dist_min;
            pos_curr_max = gameObj.transform.position + sphere_curr * dist_max;

            pos_next_min = gameObj.transform.position + sphere_next * dist_min;
            pos_next_max = gameObj.transform.position + sphere_next * dist_max;

            int a = 4 * i;
            int b = 4 * i + 1;
            int c = 4 * i + 2;
            int d = 4 * i + 3;

            vertices[a] = pos_curr_min;
            vertices[b] = pos_curr_max;
            vertices[c] = pos_next_max;
            vertices[d] = pos_next_min;

            triangles[6 * i] = a;       // Triangle1: abc
            triangles[6 * i + 1] = b;
            triangles[6 * i + 2] = c;
            triangles[6 * i + 3] = c;   // Triangle2: cda
            triangles[6 * i + 4] = d;
            triangles[6 * i + 5] = a;

            angle_curr += angle_delta;
            angle_next += angle_delta;

        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        return mesh;
    }

    static float GetLookAtAngle(GameObject gameObj)
    {
        return 90 - Mathf.Rad2Deg * Mathf.Atan2(gameObj.transform.forward.z, gameObj.transform.forward.x); // Left handed CW. z = angle 0, x = angle 90
    }
}
