using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

//一份vertices数据，索引数据应该也是1份，需要具体看下源码。
//含有多个submesh的Mesh有几份索引数据，submesh的信息是怎么存储的？
public class MultiSubMeshMesh : MonoBehaviour
{
    void Start()
    {
        // 1. 定义顶点和索引数据
        Vector3[] vertices = {
            new Vector3(-1, -1, 0),
            new Vector3(1, -1, 0),
            new Vector3(1, 1, 0),
            new Vector3(-1, 1, 0),

            new Vector3(-0.5f, -0.5f, 1),
            new Vector3(0.5f, -0.5f, 1),
            new Vector3(0.5f, 0.5f, 1),
            new Vector3(-0.5f, 0.5f, 1)
        };

        int[] submesh1Indices = {
            0, 1, 2,
            2, 3, 0
        };

        int[] submesh2Indices = {
            4, 5, 6,
            6, 7, 4
        };

        // 2. 创建Mesh对象
        Mesh mesh = new Mesh();
        mesh.subMeshCount = 2;//必须要提前调用这句话，否则后面SetTriangles会失败
        // 3. 设置顶点和索引数据
        mesh.vertices = vertices;
        mesh.SetTriangles(submesh1Indices, 0);
        mesh.SetTriangles(submesh2Indices, 1);

        Debug.Log("subMeshCount " + mesh.subMeshCount);
        
        // 4. 计算法线和切线
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        // 5. 更新Mesh
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        // 6. 将Mesh赋给MeshFilter组件
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        Debug.Log("index start:"+mesh.GetIndexStart(0)+" "+mesh.GetIndexStart(1));
        Debug.Log("index count:"+mesh.GetIndexCount(0)+" "+mesh.GetIndexCount(1));
        DebugArray(mesh.GetIndices(0));
        DebugArray(mesh.GetIndices(1));
    }
    void DebugArray(int[] array)
    {
        StringBuilder builder=new StringBuilder();

        foreach(var item in array)
        {
            builder.Append(item.ToString());
            builder.Append(",");
        }
        Debug.Log(builder.ToString());
    }
}
