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
      FourDimansionModel modelBuilder = GetComponent<FourDimansionModel>();
      GetComponent<MeshFilter>().mesh = modelBuilder.BuildMesh();
    }
  }
}
