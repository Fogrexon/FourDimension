using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace FourDimension.Core
{
  [ExecuteAlways]
  public class FourDimensionTransform : MonoBehaviour
  {
    #region parameters
    public Vector4 position = Vector4.zero;
    public Vector3 rotateXYZ = Vector3.zero;
    public Vector3 rotateW = Vector3.zero;
    public Vector4 scale = new Vector4(1f, 1f, 1f, 1f);
    private Quaternion rotation = Quaternion.identity;
    #endregion

    #region store_calclated
    public Matrix4x4 propagatedTransformMatrix = new Matrix4x4();
    private Vector4 propagatedPosition = Vector4.zero;
    #endregion

    float epsilon = 0.000001f;

    Material material;
    private bool hasMaterial = false;

    #region parent
    private FourDimensionTransform parentTransform;
    private bool hasParentTransform = false;
    #endregion

    Matrix4x4 MakeScaleRotationMatrix()
    {
      rotation = Quaternion.Euler(rotateXYZ.x, rotateXYZ.y, rotateXYZ.z);

      // normal 3d rotation matrix
      Matrix4x4 rotXYZ = Matrix4x4.Rotate(rotation);

      // rotation matrix with w axis
      float zw = rotateW.z + epsilon;
      Matrix4x4 rotXY = new Matrix4x4();
      rotXY.SetColumn(0, new Vector4(1.0f, 0.0f, 0.0f, 0.0f));
      rotXY.SetColumn(1, new Vector4(0.0f, 1.0f, 0.0f, 0.0f));
      rotXY.SetColumn(2, new Vector4(0.0f, 0.0f, Mathf.Cos(zw), Mathf.Sin(zw)));
      rotXY.SetColumn(3, new Vector4(0.0f, 0.0f, -Mathf.Sin(zw), Mathf.Cos(zw)));

      float xw = rotateW.x + epsilon;
      Matrix4x4 rotYZ = new Matrix4x4();
      rotYZ.SetColumn(0, new Vector4(Mathf.Cos(xw), 0.0f, 0.0f, Mathf.Sin(xw)));
      rotYZ.SetColumn(1, new Vector4(0.0f, 1.0f, 0.0f, 0.0f));
      rotYZ.SetColumn(2, new Vector4(0.0f, 0.0f, 1.0f, 0.0f));
      rotYZ.SetColumn(3, new Vector4(-Mathf.Sin(xw), 0.0f, 0.0f, Mathf.Cos(xw)));

      float yw = rotateW.y + epsilon;
      Matrix4x4 rotXZ = new Matrix4x4();
      rotXZ.SetColumn(0, new Vector4(1.0f, 0.0f, 0.0f, 0.0f));
      rotXZ.SetColumn(1, new Vector4(0.0f, Mathf.Cos(yw), 0.0f, Mathf.Sin(yw)));
      rotXZ.SetColumn(2, new Vector4(0.0f, 0.0f, 1.0f, 0.0f));
      rotXZ.SetColumn(3, new Vector4(0.0f, -Mathf.Sin(yw), 0.0f, Mathf.Cos(yw)));

      // scale matrix
      Matrix4x4 scaleMat = Matrix4x4.identity;
      scaleMat[0, 0] = scale.x;
      scaleMat[1, 1] = scale.y;
      scaleMat[2, 2] = scale.z;
      scaleMat[3, 3] = scale.w;

      // Y * X * Z * WY * WX * WZ * scale
     return rotXZ * rotYZ * rotXY * rotXYZ * rotXZ * rotYZ * rotXY * scaleMat;
    }

    void SetTransform() {
      // 4d transform to 3d transform
      transform.rotation = rotation;
      transform.localPosition = new Vector3(position.x, position.y, position.z);
      transform.localScale = new Vector3(scale.x, scale.y, scale.z);
    }

    void Start()
    {
      hasMaterial = gameObject.HasComponent<Renderer>();
      if (EditorApplication.isPlaying && hasMaterial) material = GetComponent<Renderer>().material;

      // set parent transform
      hasParentTransform = transform.parent != null && transform.parent.gameObject.HasComponent<FourDimensionTransform>();
      if(hasParentTransform) {
        parentTransform = transform.parent.gameObject.GetComponent<FourDimensionTransform>();
      }
    }

    void Update()
    {
      Matrix4x4 transformMatrix = MakeScaleRotationMatrix();
      SetTransform();

      if (hasParentTransform) {
        propagatedTransformMatrix = transformMatrix * parentTransform.propagatedTransformMatrix;
        propagatedPosition = parentTransform.propagatedTransformMatrix * position + parentTransform.propagatedPosition;
      } else {
        propagatedTransformMatrix = transformMatrix;
        propagatedPosition = position;
      }

      if(EditorApplication.isPlaying && hasMaterial) {
        material.SetMatrix("_FourDMatrix", propagatedTransformMatrix);
        material.SetVector("_Position", propagatedPosition);
      }
    }
  }
}
