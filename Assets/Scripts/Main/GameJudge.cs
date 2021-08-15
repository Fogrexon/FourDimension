using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FourDimension.Core;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Game.Main {
    public class GameJudge : MonoBehaviour
    {
        private FourDimensionTransform fdTransformTarget;
        private FourDimensionTransform fdTransformPlayer;

        private Material gameEnd;

        public static bool isPlaying = true;
        public static float waitTime;

        void Start()
        {
            GameJudge.isPlaying = true;
            fdTransformTarget = GameObject.Find("Example4DCube").GetComponent<FourDimensionTransform>();
            fdTransformTarget.rotateXYZ = new Vector3(
                Random.Range(0f, Mathf.PI * 2f),
                Random.Range(0f, Mathf.PI * 2f),
                Random.Range(0f, Mathf.PI * 2f)
            );
            fdTransformTarget.rotateW = new Vector3(
                Random.Range(0f, Mathf.PI * 2f),
                Random.Range(0f, Mathf.PI * 2f),
                Random.Range(0f, Mathf.PI * 2f)
            );
            fdTransformPlayer = GameObject.Find("4DCube").GetComponent<FourDimensionTransform>();
            gameEnd = GameObject.Find("Clear").GetComponent<RawImage>().material;
            gameEnd.SetFloat("_Alpha", 0f);
        }

        void Update()
        {
            if (GameJudge.isPlaying) {
                Vector3 baseVal = fdTransformPlayer.rotateXYZ - fdTransformTarget.rotateXYZ + new Vector3(Mathf.PI * 2f, Mathf.PI * 2f, Mathf.PI * 2f);
                Vector3 baseValW = fdTransformPlayer.rotateW - fdTransformTarget.rotateW + new Vector3(Mathf.PI * 2f, Mathf.PI * 2f, Mathf.PI * 2f);
                float diff =
                    Vector3.Dot(new Vector3(CalcDiff(baseVal.x), CalcDiff(baseVal.y), CalcDiff(baseVal.z)), new Vector3(1f, 1f, 1f)) +
                    Vector3.Dot(new Vector3(CalcDiff(baseValW.x), CalcDiff(baseValW.y), CalcDiff(baseValW.z)), new Vector3(1f, 1f, 1f));

                if (diff < Mathf.PI * 0.02f) {
                    GameJudge.isPlaying = false;
                    waitTime = 1f;
                    GameEnd();
                }
            } else {
                waitTime -= Time.deltaTime;
                if (waitTime < 0 && Input.anyKey) {
                    SceneManager.LoadScene("Title");
                }
            }
        }

        float CalcDiff(float a) {
            float v = a % (Mathf.PI * 0.5f);
            Debug.Log(Mathf.Min(Mathf.PI * 0.5f - v, v));
            return Mathf.Min(Mathf.PI * 0.5f - v, v);
        }

        void GameEnd() {
            DOTween.To(
                () => 0f,
                v => gameEnd.SetFloat("_Alpha", v),
                1f,
                1f
            );
            // DOTween.To(
            //     () => fdTransformPlayer.rotateXYZ,
            //     v => fdTransformPlayer.rotateXYZ = v,
            //     fdTransformTarget.rotateXYZ,
            //     1f
            // );
            // DOTween.To(
            //     () => fdTransformPlayer.rotateW,
            //     v => fdTransformPlayer.rotateW = v,
            //     fdTransformTarget.rotateW,
            //     1f
            // );
        }
    }
}
