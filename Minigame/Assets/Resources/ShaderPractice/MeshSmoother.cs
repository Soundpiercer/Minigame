using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSmoother : MonoBehaviour
{
    public Material outlineMaterial;

    private void Start()
    {
        GameObject outlineObject = new GameObject("Outline");
        outlineObject.transform.parent = gameObject.transform;
        outlineObject.transform.position = gameObject.transform.position;
        outlineObject.transform.rotation = gameObject.transform.rotation;
        outlineObject.transform.localScale = gameObject.transform.localScale;

        outlineObject.AddComponent<MeshRenderer>();
        outlineObject.AddComponent<MeshFilter>();

        Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        Mesh smoothedMesh = SmoothMesh(mesh);

        outlineObject.GetComponent<MeshFilter>().sharedMesh = smoothedMesh;
        outlineObject.GetComponent<MeshRenderer>().sharedMaterial = outlineMaterial;
    }

    /// <summary>
    /// 같은 Vertex ID를 사용하는 vertex들의 법선벡터의 평균을 내서 새 Mesh를 만든다.
    /// </summary>
    private Mesh SmoothMesh(Mesh mesh)
    {
        // Vertex 추출.
        Dictionary<Vector3, List<int>> map = new Dictionary<Vector3, List<int>>();

        for (int v = 0; v < mesh.vertexCount; v++)
        {
            if (!map.ContainsKey(mesh.vertices[v]))
            {
                map.Add(mesh.vertices[v], new List<int>());
            }

            map[mesh.vertices[v]].Add(v);
        }

        // 법선벡터 평균내기.
        Vector3[] normals = mesh.normals;
        Vector3 normal;

        foreach (var p in map)
        {
            normal = Vector3.zero;

            foreach (var n in p.Value)
            {
                normal += mesh.normals[n];
            }

            normal /= p.Value.Count;

            foreach (var n in p.Value)
            {
                normals[n] = normal;
            }
        }

        mesh.normals = normals;

        return mesh;
    }
}
