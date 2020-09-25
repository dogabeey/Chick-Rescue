using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class FieldOfView : MonoBehaviour {

    private Mesh mesh;
    private Vector3 origin;
    private bool targetSeen;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float fov;
    [SerializeField] private float viewDistance;
    [SerializeField] private GameObject owner;
    [SerializeField] private bool startAsFront;
    [SerializeField] private float startingAngle;

    private void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;

        startingAngle = (startAsFront ? (fov / 2 + 90) : startingAngle);
    }

    private void LateUpdate()
    {
        int rayCount = 20;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        RaycastHit hit;

        targetSeen = false;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++) {
            Vector3 vertex;
            Vector3 direction = UtilsClass.GetVectorFromAngle((int)(angle - transform.parent.rotation.y),UtilsClass.Direction.y);
            Ray ray = new Ray(transform.position, UtilsClass.GetVectorFromAngle((int)(angle - transform.eulerAngles.y), UtilsClass.Direction.y));
            Physics.Raycast(ray, out hit, viewDistance, layerMask);
            Debug.DrawRay(ray.origin,ray.direction);
            if (hit.collider == null) {
                // No hit
                vertex = origin + direction * viewDistance;
            } else {
                vertex = transform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);
                if (!targetSeen && transform.parent.GetComponent<Hunter>().isHuntable(hit))
                {
                    targetSeen = true;
                    transform.parent.GetComponent<Hunter>().HandleHunting(hit);
                }
            }

            vertices[vertexIndex] = vertex;


            if (i > 0) {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);

        Vector2[] vertices2d = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices2d[i] = new Vector2(vertices[i].x, vertices[i].y);
        }

    }

}
