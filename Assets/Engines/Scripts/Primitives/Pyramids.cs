using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FourDimension.Core;

namespace FourDimension.Primitives
{
  public class Pyramids : FourDimensionModel
  {

    private float getSign(float t)
    {
      return Mathf.Floor(t % 2f) * 2f - 1f;
    }
    public override void MakePyramids()
    {
      pyramids.Add(new Vector4(Mathf.Sin(0) * Mathf.Cos(0), Mathf.Cos(0), Mathf.Sin(0) * Mathf.Sin(0), 0f));
      pyramids.Add(new Vector4(Mathf.Sin(120f * Mathf.Deg2Rad) * Mathf.Cos(0f * Mathf.Deg2Rad), Mathf.Cos(120f * Mathf.Deg2Rad), Mathf.Sin(120f * Mathf.Deg2Rad) * Mathf.Sin(0f * Mathf.Deg2Rad), 0f));
      pyramids.Add(new Vector4(Mathf.Sin(120f * Mathf.Deg2Rad) * Mathf.Cos(120f * Mathf.Deg2Rad), Mathf.Cos(120f * Mathf.Deg2Rad), Mathf.Sin(120f * Mathf.Deg2Rad) * Mathf.Sin(120f * Mathf.Deg2Rad), 0f));
      pyramids.Add(new Vector4(Mathf.Sin(120f * Mathf.Deg2Rad) * Mathf.Cos(-120f * Mathf.Deg2Rad), Mathf.Cos(120f * Mathf.Deg2Rad), Mathf.Sin(120f * Mathf.Deg2Rad) * Mathf.Sin(-120f * Mathf.Deg2Rad), 0f));
      colors.Add(new Color(Random.value, Random.value, Random.value));
    }
  }
}