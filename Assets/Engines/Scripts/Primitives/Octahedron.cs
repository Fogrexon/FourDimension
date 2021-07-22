using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FourDimension.Core;

namespace FourDimension.Primitives
{
  public class Octahedron : FourDimensionModel
  {

    private float getSign(float t)
    {
      return Mathf.Floor(t % 2f) * 2f - 1f;
    }
    public override void MakePyramids()
    {
      for (int i = 0; i < Mathf.Pow(2f, 4f); i++)
      {
        pyramids.Add(new Vector4(getSign(i), 0f, 0f, 0f));
        pyramids.Add(new Vector4(0f, getSign(i / 2), 0f, 0f));
        pyramids.Add(new Vector4(0f, 0f, getSign(i / 4), 0f));
        pyramids.Add(new Vector4(0f, 0f, 0f, getSign(i / 8)));
        colors.Add(new Color(Random.value, Random.value, Random.value));
      }
    }
  }
}