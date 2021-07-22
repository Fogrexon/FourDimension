using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FourDimension.Core
{
  public class FourDimensionTransform : MonoBehaviour
  {
    public float positionW = 0f;
    public Vector3 rotateW = Vector3.zero;
    public float scaleW = 1f;

    float epsilon = 0.000001f;

    Material material;

    Matrix4x4 MakeScaleRotationMatrix()
    {
      float z = rotateW.z + epsilon;
      Matrix4x4 rotXY = new Matrix4x4();
      rotXY.SetColumn(0, new Vector4(1.0f, 0.0f, 0.0f, 0.0f));
      rotXY.SetColumn(1, new Vector4(0.0f, 1.0f, 0.0f, 0.0f));
      rotXY.SetColumn(2, new Vector4(0.0f, 0.0f, Mathf.Cos(z), Mathf.Sin(z)));
      rotXY.SetColumn(3, new Vector4(0.0f, 0.0f, -Mathf.Sin(z), Mathf.Cos(z)));

      float x = rotateW.x + epsilon;
      Matrix4x4 rotYZ = new Matrix4x4();
      rotYZ.SetColumn(0, new Vector4(Mathf.Cos(x), 0.0f, 0.0f, Mathf.Sin(x)));
      rotYZ.SetColumn(1, new Vector4(0.0f, 1.0f, 0.0f, 0.0f));
      rotYZ.SetColumn(2, new Vector4(0.0f, 0.0f, 1.0f, 0.0f));
      rotYZ.SetColumn(3, new Vector4(-Mathf.Sin(x), 0.0f, 0.0f, Mathf.Cos(x)));

      float y = rotateW.y + epsilon;
      Matrix4x4 rotXZ = new Matrix4x4();
      rotXZ.SetColumn(0, new Vector4(1.0f, 0.0f, 0.0f, 0.0f));
      rotXZ.SetColumn(1, new Vector4(0.0f, Mathf.Cos(y), 0.0f, Mathf.Sin(y)));
      rotXZ.SetColumn(2, new Vector4(0.0f, 0.0f, 1.0f, 0.0f));
      rotXZ.SetColumn(3, new Vector4(0.0f, -Mathf.Sin(y), 0.0f, Mathf.Cos(y)));

      Matrix4x4 scale = Matrix4x4.identity;
      scale[3, 3] = scaleW;
      Matrix4x4 m = rotXZ * rotYZ * rotXY * scale;
      return m;
    }

    void Start()
    {
      material = GetComponent<Renderer>().material;
    }

    void Update()
    {
      material.SetMatrix("_FourDMatrix", MakeScaleRotationMatrix());
      material.SetFloat("_PositionW", positionW);
    }
  }
}
