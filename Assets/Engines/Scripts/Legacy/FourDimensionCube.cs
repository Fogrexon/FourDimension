using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FourDimensionCube : MonoBehaviour
{
  List<Vector3> vertices = new List<Vector3>();
  List<int> indices = new List<int>();
  List<Color> color = new List<Color>();
  List<Vector3> posTemp = new List<Vector3>();
  List<List<Vector4>> pos = new List<List<Vector4>>();
  void Start()
  {
    int baseCubeNum = 20;
    posTemp.Add(new Vector4(0f, 0f, 0f));
    posTemp.Add(new Vector4(0f, 0f, 1f));
    posTemp.Add(new Vector4(1f, 0f, 1f));
    posTemp.Add(new Vector4(0f, 1f, 1f));

    posTemp.Add(new Vector4(0f, 0f, 0f));
    posTemp.Add(new Vector4(1f, 0f, 0f));
    posTemp.Add(new Vector4(1f, 0f, 1f));
    posTemp.Add(new Vector4(1f, 1f, 0f));

    posTemp.Add(new Vector4(0f, 1f, 1f));
    posTemp.Add(new Vector4(0f, 1f, 0f));
    posTemp.Add(new Vector4(1f, 1f, 0f));
    posTemp.Add(new Vector4(0f, 0f, 0f));

    posTemp.Add(new Vector4(0f, 1f, 1f));
    posTemp.Add(new Vector4(1f, 1f, 1f));
    posTemp.Add(new Vector4(1f, 1f, 0f));
    posTemp.Add(new Vector4(1f, 0f, 1f));

    posTemp.Add(new Vector4(0f, 0f, 0f));
    posTemp.Add(new Vector4(1f, 0f, 1f));
    posTemp.Add(new Vector4(0f, 1f, 1f));
    posTemp.Add(new Vector4(1f, 1f, 0f));

    for(int i=0;i<baseCubeNum;i++)
    {
      posTemp[i] -= new Vector3(0.5f, 0.5f, 0.5f);
    }

    pos.Add(new List<Vector4>());
    pos.Add(new List<Vector4>());
    pos.Add(new List<Vector4>());
    pos.Add(new List<Vector4>());
    for (int i = 0; i < baseCubeNum; i++)
    {
      Vector3 p = posTemp[i];
      pos[i % 4].Add(new Vector4(-0.5f, p.x, p.y, p.z));
    }
    for (int i = 0; i < baseCubeNum; i++)
    {
      Vector3 p = posTemp[i];
      pos[i % 4].Add(new Vector4(0.5f, p.x, p.y, p.z));
    }
    for (int i = 0; i < baseCubeNum; i++)
    {
      Vector3 p = posTemp[i];
      pos[i % 4].Add(new Vector4(p.x, -0.5f, p.y, p.z));
    }
    for (int i = 0; i < baseCubeNum; i++)
    {
      Vector3 p = posTemp[i];
      pos[i % 4].Add(new Vector4(p.x, 0.5f, p.y, p.z));
    }
    for (int i = 0; i < baseCubeNum; i++)
    {
      Vector3 p = posTemp[i];
      pos[i % 4].Add(new Vector4(p.x, p.y, -0.5f, p.z));
    }
    for (int i = 0; i < baseCubeNum; i++)
    {
      Vector3 p = posTemp[i];
      pos[i % 4].Add(new Vector4(p.x, p.y, 0.5f, p.z));
    }
    for (int i = 0; i < baseCubeNum; i++)
    {
      Vector3 p = posTemp[i];
      pos[i % 4].Add(new Vector4(p.x, p.y, p.z, -0.5f));
    }
    for (int i = 0; i < baseCubeNum; i++)
    {
      Vector3 p = posTemp[i];
      pos[i % 4].Add(new Vector4(p.x, p.y, p.z, 0.5f));
    }

    Color colTemp = new Color();
    for (int i = 0; i < baseCubeNum / 4 * 8; i++)
    {
      if(i % 8 == 0) colTemp = new Color(Random.value, Random.value, Random.value);
      vertices.Add(Vector3.zero);
      indices.Add(i);
      color.Add(colTemp);
    }

    Mesh mesh = new Mesh();
    mesh.SetVertices(vertices);
    mesh.SetIndices(indices.ToArray(), MeshTopology.Points, 0);
    mesh.SetColors(color);
    mesh.SetUVs(0, pos[0]);
    mesh.SetUVs(1, pos[1]);
    mesh.SetUVs(2, pos[2]);
    mesh.SetUVs(3, pos[3]);

    GetComponent<MeshFilter>().mesh = mesh;
    GetComponent<MeshFilter>().mesh = mesh;
  }
}
