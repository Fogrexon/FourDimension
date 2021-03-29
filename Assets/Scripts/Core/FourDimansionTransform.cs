using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourDimension.Core
{
  public class FourDimansionTransform : MonoBehaviour
  {
    public float positionW = 0f;
    public Vector3 rotateXYZ = Vector3.zero;
    public Vector3 rotateW = Vector3.zero;
    public float scaleW = 1f;

    Material material;

    Matrix4x4 MakeScaleRotationMatrix()
    {
      Matrix4x4 rotXY = new Matrix4x4();
      rotXY.SetColumn(0, new Vector4(1.0f, 0.0f, 0.0f, 0.0f));
      rotXY.SetColumn(1, new Vector4(0.0f, 1.0f, 0.0f, 0.0f));
      rotXY.SetColumn(2, new Vector4(0.0f, 0.0f, Mathf.Cos(rotateW.x), Mathf.Sin(rotateW.z)));
      rotXY.SetColumn(3, new Vector4(0.0f, 0.0f, -Mathf.Sin(rotateW.z), Mathf.Cos(rotateW.z)));
      
      Matrix4x4 rotYZ = new Matrix4x4();
      rotYZ.SetColumn(0, new Vector4(Mathf.Cos(rotateW.x), 0.0f, 0.0f, Mathf.Sin(rotateW.x)));
      rotYZ.SetColumn(1, new Vector4(0.0f, 1.0f, 0.0f, 0.0f));
      rotYZ.SetColumn(2, new Vector4(0.0f, 0.0f, 1.0f, 0.0f));
      rotYZ.SetColumn(3, new Vector4(-Mathf.Sin(rotateW.x), 0.0f, 0.0f, Mathf.Cos(rotateW.x)));
      
      Matrix4x4 rotXZ = new Matrix4x4();
      rotXZ.SetColumn(0, new Vector4(1.0f, 0.0f, 0.0f, 0.0f));
      rotXZ.SetColumn(1, new Vector4(0.0f, Mathf.Cos(rotateW.y), 0.0f, Mathf.Sin(rotateW.y)));
      rotXZ.SetColumn(2, new Vector4(0.0f, 0.0f, 1.0f, 0.0f));
      rotXZ.SetColumn(3, new Vector4(0.0f, -Mathf.Sin(rotateW.y), 0.0f, Mathf.Cos(rotateW.y)));

      Matrix4x4 m = rotXZ * rotYZ * rotXY;
      m[3, 3] = scaleW;
      m[2, 3] = positionW;
      return m;
    }

    void Start()
    {
      material = GetComponent<MeshRenderer>().material; 
    }

    void Update()
    {
      material.SetMatrix("_FourDMatrix", MakeScaleRotationMatrix());
    }
  }
}
