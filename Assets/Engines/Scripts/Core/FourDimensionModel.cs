using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourDimension.Core
{
  public abstract class FourDimensionModel : MonoBehaviour
  {
    protected List<Vector4> pyramids = new List<Vector4>();
    protected List<Color> colors = new List<Color>();
    public Mesh BuildMesh()
    {
      MakePyramids();
      if (colors.Count * 4 != pyramids.Count) throw new System.Exception("Counts are mismatch: colors, pyramids");
      List<List<Vector4>> pos = new List<List<Vector4>>();
      pos.Add(new List<Vector4>());
      pos.Add(new List<Vector4>());
      pos.Add(new List<Vector4>());
      pos.Add(new List<Vector4>());

      List<Vector3> vertices = new List<Vector3>();
      List<int> indices = new List<int>();
      for (int i = 0; i < colors.Count; i++)
      {
        vertices.Add(Vector3.zero);
        indices.Add(i);
        pos[0].Add(pyramids[i * 4 + 0]);
        pos[1].Add(pyramids[i * 4 + 1]);
        pos[2].Add(pyramids[i * 4 + 2]);
        pos[3].Add(pyramids[i * 4 + 3]);
      }

      Mesh mesh = new Mesh();
      mesh.SetVertices(vertices);
      mesh.SetIndices(indices.ToArray(), MeshTopology.Points, 0);
      mesh.SetColors(colors);
      mesh.SetUVs(0, pos[0]);
      mesh.SetUVs(1, pos[1]);
      mesh.SetUVs(2, pos[2]);
      mesh.SetUVs(3, pos[3]);

      return mesh;
    }

    public abstract void MakePyramids();
  }
}
