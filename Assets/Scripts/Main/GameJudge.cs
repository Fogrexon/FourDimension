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

        private Material materialTarget;
        private Material materialPlayer;

        private Material gameEnd;

        public static bool isPlaying = false;
        public static bool isEndgame = false;
        public static float waitTime;

        private float startTime = 0f;

        void Start()
        {
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

            materialTarget = fdTransformTarget.gameObject.GetComponent<Renderer>().material;
            materialPlayer = fdTransformPlayer.gameObject.GetComponent<Renderer>().material;
            materialTarget.SetFloat("_Pulse", 4f);
            materialPlayer.SetFloat("_Pulse", 4f);

            gameEnd = GameObject.Find("Clear").GetComponent<RawImage>().material;
            gameEnd.SetFloat("_Alpha", 0f);
        }

        void Update()
        {
            if (GameJudge.isPlaying) {
                Vector4 axisX = new Vector4(1f, 0f, 0f, 0f);
                Vector4 axisY = new Vector4(0f, 1f, 0f, 0f);
                Vector4 axisZ = new Vector4(0f, 0f, 1f, 0f);
                Vector4 axisW = new Vector4(0f, 0f, 0f, 1f);

                Matrix4x4 targetRotation = fdTransformTarget.GetWorldRotation();
                Matrix4x4 playerRotation = fdTransformPlayer.GetWorldRotation();

                Vector4 targetX = targetRotation * axisX;
                Vector4 targetY = targetRotation * axisY;
                Vector4 targetZ = targetRotation * axisZ;
                Vector4 targetW = targetRotation * axisW;

                Vector4 playerX = playerRotation * axisX;
                Vector4 playerY = playerRotation * axisY;
                Vector4 playerZ = playerRotation * axisZ;
                Vector4 playerW = playerRotation * axisW;

                // axis diff
                float diff = 
                    MinDot(playerX, targetX, targetY, targetZ, targetW) +
                    MinDot(playerY, targetX, targetY, targetZ, targetW) +
                    MinDot(playerZ, targetX, targetY, targetZ, targetW) +
                    MinDot(playerW, targetX, targetY, targetZ, targetW);
                
                materialTarget.SetFloat("_Pulse", diff);
                materialPlayer.SetFloat("_Pulse", diff);
                
                if (diff < 0.07f) {
                    GameJudge.isPlaying = false;
                    GameJudge.isEndgame = true;
                    waitTime = 1f;
                    GameEnd();
                }
            } else if (GameJudge.isEndgame) {
                waitTime -= Time.deltaTime;
                if (waitTime < 0 && Input.anyKey) {
                    SceneManager.LoadScene("Title");
                }
            }
        }

        float MinDot(Vector4 playerAxis, Vector4 targetX, Vector4 targetY, Vector4 targetZ, Vector4 targetW) {
            return 1f - Mathf.Max(
                Mathf.Max(Mathf.Abs(Vector4.Dot(playerAxis, targetX)), Mathf.Abs(Vector4.Dot(playerAxis, targetY))),
                Mathf.Max(Mathf.Abs(Vector4.Dot(playerAxis, targetZ)), Mathf.Abs(Vector4.Dot(playerAxis, targetW)))
            );
        }

        float CalcDiff(float a) {
            float v = a % (Mathf.PI * 0.5f);
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
