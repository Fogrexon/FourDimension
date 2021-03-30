using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FourDimension.Core;

namespace FourDimension.Primitives
{
  public class Cube : FourDimensionModel
  {
    public override void MakePyramids()
    {
      int baseCubeNum = 20;
      List<Vector3> posTemp = new List<Vector3>();
      posTemp.Add(new Vector3(0f, 0f, 0f));
      posTemp.Add(new Vector3(0f, 0f, 1f));
      posTemp.Add(new Vector3(1f, 0f, 1f));
      posTemp.Add(new Vector3(0f, 1f, 1f));

      posTemp.Add(new Vector3(0f, 0f, 0f));
      posTemp.Add(new Vector3(1f, 0f, 0f));
      posTemp.Add(new Vector3(1f, 0f, 1f));
      posTemp.Add(new Vector3(1f, 1f, 0f));

      posTemp.Add(new Vector3(0f, 1f, 1f));
      posTemp.Add(new Vector3(0f, 1f, 0f));
      posTemp.Add(new Vector3(1f, 1f, 0f));
      posTemp.Add(new Vector3(0f, 0f, 0f));

      posTemp.Add(new Vector3(0f, 1f, 1f));
      posTemp.Add(new Vector3(1f, 1f, 1f));
      posTemp.Add(new Vector3(1f, 1f, 0f));
      posTemp.Add(new Vector3(1f, 0f, 1f));

      posTemp.Add(new Vector3(0f, 0f, 0f));
      posTemp.Add(new Vector3(1f, 0f, 1f));
      posTemp.Add(new Vector3(0f, 1f, 1f));
      posTemp.Add(new Vector3(1f, 1f, 0f));

      for (int i = 0; i < baseCubeNum; i++)
      {
        posTemp[i] -= new Vector3(0.5f, 0.5f, 0.5f);
      }

      for (int i = 0; i < baseCubeNum; i++)
      {
        Vector3 p = posTemp[i];
        pyramids.Add(new Vector4(-0.5f, p.x, p.y, p.z));
      }
      for (int i = 0; i < baseCubeNum; i++)
      {
        Vector3 p = posTemp[i];
        pyramids.Add(new Vector4(0.5f, p.x, p.y, p.z));
      }
      for (int i = 0; i < baseCubeNum; i++)
      {
        Vector3 p = posTemp[i];
        pyramids.Add(new Vector4(p.x, -0.5f, p.y, p.z));
      }
      for (int i = 0; i < baseCubeNum; i++)
      {
        Vector3 p = posTemp[i];
        pyramids.Add(new Vector4(p.x, 0.5f, p.y, p.z));
      }
      for (int i = 0; i < baseCubeNum; i++)
      {
        Vector3 p = posTemp[i];
        pyramids.Add(new Vector4(p.x, p.y, -0.5f, p.z));
      }
      for (int i = 0; i < baseCubeNum; i++)
      {
        Vector3 p = posTemp[i];
        pyramids.Add(new Vector4(p.x, p.y, 0.5f, p.z));
      }
      for (int i = 0; i < baseCubeNum; i++)
      {
        Vector3 p = posTemp[i];
        pyramids.Add(new Vector4(p.x, p.y, p.z, -0.5f));
      }
      for (int i = 0; i < baseCubeNum; i++)
      {
        Vector3 p = posTemp[i];
        pyramids.Add(new Vector4(p.x, p.y, p.z, 0.5f));
      }

      Color colTemp = new Color();
      for (int i = 0; i < baseCubeNum / 4 * 8; i++)
      {
        if (i % 8 == 0) colTemp = new Color(Random.value, Random.value, Random.value);
        colors.Add(colTemp);
      }
    }
  }

}
