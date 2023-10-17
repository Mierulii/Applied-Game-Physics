using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigglePhysics : MonoBehaviour
{
    public float intensity, mass, stiffness, dampness;

    private Mesh OriginalMesh, MeshClone;
    private MeshRenderer RendererGO;
    private GameObjVertex[] GV;
    private Vector3[] VertexArray;

    void Start()
    {
        OriginalMesh = GetComponent<MeshFilter>().sharedMesh;
        MeshClone = Instantiate(OriginalMesh);
        GetComponent<MeshFilter>().sharedMesh = MeshClone;
        RendererGO = GetComponent<MeshRenderer>();

        GV = new GameObjVertex[MeshClone.vertices.Length];
        for (int i = 0; i < MeshClone.vertices.Length; i++)
        {
            GV[i] = new GameObjVertex(i, transform.TransformPoint(MeshClone.vertices[i]));
        }
    }

    void FixedUpdate()
    {
        VertexArray = OriginalMesh.vertices;
        for (int i = 0; i < GV.Length; i++)
        {
            Vector3 target = transform.TransformPoint(VertexArray[GV[i].ID]);
            float _intensity = (1 - (RendererGO.bounds.max.y - target.y) / RendererGO.bounds.size.y) * intensity;
            GV[i].Shake(target, mass, stiffness, dampness);
            target = transform.InverseTransformPoint(GV[i].position);
            VertexArray[GV[i].ID] = Vector3.Lerp(VertexArray[GV[i].ID], target, _intensity);
        }

        MeshClone.vertices = VertexArray;
    }

    public class GameObjVertex
    {
        public int ID;
        public Vector3 position;
        public Vector3 velocity, force;

        public GameObjVertex(int _ID, Vector3 _pos)
        {
            ID = _ID;
            position = _pos;
        }

        public void Shake(Vector3 target, float m, float s, float d)
        {
            force = (target - position) * s;
            velocity = (velocity + force / m) * d;
            position += velocity;

            if ((velocity + force + force / m).magnitude < 0.0001f)
            {
                position = target;
            }
        }
    }
}
