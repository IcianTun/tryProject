using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tstCone : MonoBehaviour {

    public float angle;
    public float minDist;
    public float maxDist;

    public float thickness;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh = GenerateConeMesh(gameObject, angle, minDist, maxDist);
            MeshCollider meshCollider = GetComponent<MeshCollider>();
            meshCollider.sharedMesh = meshFilter.mesh;
            meshCollider.bounds.Expand(new Vector3(0, 1, 0));
            //UpdateoldVerticesgonCollider2D(meshFilter);
        }
    }

    public Mesh GenerateConeMesh(GameObject gameObj, float angle_fov, float dist_min, float dist_max)
    {
        int quality = 30;

        //------------- PART 1----------------//
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[4 * quality];   // Could be of size [2 * quality + 2] if circle segment is continuous
        mesh.triangles = new int[3 * 2 * quality];

        //Vector3[] normals = new Vector3[4 * quality];
        Vector2[] uv = new Vector2[4 * quality];

        for (int i = 0; i < uv.Length; i++)
            uv[i] = new Vector2(0, 0);
        //for (int i = 0; i < normals.Length; i++)
        //    normals[i] = new Vector3(0, 1, 0);

        mesh.uv = uv;
        //mesh.normals = normals;

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

        //mesh.vertices = vertices;
        //mesh.triangles = triangles;

        return Extrude(vertices,triangles);
    }

    static float GetLookAtAngle(GameObject gameObj)
    {
        return 90 - Mathf.Rad2Deg * Mathf.Atan2(gameObj.transform.forward.z, gameObj.transform.forward.x); // Left handed CW. z = angle 0, x = angle 90
    }

    public Mesh Extrude(Vector3[] oldVertices, int[] tris)
    {
        Vector3[] vertices = new Vector3[oldVertices.Length * 2];
        
        for(int i=0;i<oldVertices.Length;i++)
        {
            vertices[i].x = oldVertices[i].x;
            vertices[i].y = 0;
            vertices[i].z = oldVertices[i].z; // front vertex
            vertices[i+oldVertices.Length].x = oldVertices[i].x;
            vertices[i+oldVertices.Length].y = -thickness;
            vertices[i+oldVertices.Length].z = oldVertices[i].z;  // back vertex    
        }
        int[] triangles = new int[tris.Length*2+oldVertices.Length*6];
        int count_tris = 0;
        for(int i=0;i<tris.Length;i+=3)
        {
            triangles[i] = tris[i];
            triangles[i+1] = tris[i+1];
            triangles[i+2] = tris[i+2];
        } // front vertices
        count_tris+=tris.Length;
        for(int i=0;i<tris.Length;i+=3)
        {
            triangles[count_tris+i] = tris[i+2]+oldVertices.Length;
            triangles[count_tris+i+1] = tris[i+1]+oldVertices.Length;
            triangles[count_tris+i+2] = tris[i]+oldVertices.Length;
        } // back vertices
        count_tris+=tris.Length;
        //Debug.Log(count_tris);
        for (int i = 0; i < oldVertices.Length-1; i++)
        {
            // triangles around the perimeter of the object
            int n = (i + 1) % oldVertices.Length;
            //if (i ==  0)
            //{
                Debug.Log(vertices[n]);
                Debug.Log(vertices[i + oldVertices.Length]);
                Debug.Log(vertices[i]);
                Debug.Log(vertices[n]);
                Debug.Log(vertices[n + oldVertices.Length]);
                Debug.Log(vertices[i + oldVertices.Length]);
            Debug.Log("------------------------");
            //}
            triangles[count_tris] = n;
            triangles[count_tris + 1] = i + oldVertices.Length;
            triangles[count_tris + 2] = i;
            triangles[count_tris + 3] = n;
            triangles[count_tris + 4] = n + oldVertices.Length;
            triangles[count_tris + 5] = i + oldVertices.Length;
            count_tris += 6;
        }
        Mesh m = new Mesh();
        m.vertices = vertices;
        m.triangles = triangles;
        m.RecalculateNormals();
        m.RecalculateBounds();
        UnityEditor.MeshUtility.Optimize(m);
        return m;
    }
}