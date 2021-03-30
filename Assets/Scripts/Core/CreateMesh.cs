using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourDimension.Core
{
  [ExecuteInEditMode]
  public class CreateMesh : MonoBehaviour
  {
    void Start()
    {
      FourDimensionModel modelBuilder = GetComponent<FourDimensionModel>();
      GetComponent<MeshFilter>().mesh = modelBuilder.BuildMesh();
    }
  }
}
