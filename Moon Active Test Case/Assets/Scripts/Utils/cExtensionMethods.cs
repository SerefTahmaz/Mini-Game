using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Seref.Utils
{
    public static class cExtensionMethods
    {
        public static void SetParentResetTransform(this Transform transform, Transform parent, bool scale = true)
        {
            transform.SetParent(parent);
            transform.ResetTransform(scale);
        }
    
        public static void ResetTransform(this Transform transform, bool scale = true)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            if(scale) transform.localScale = Vector3.one;
        }

        public static void PlayWithClear(this ParticleSystem particleSystem)
        {
            particleSystem.StopWithClear();
            particleSystem.Play(true);
        }
    
        public static void StopWithClear(this ParticleSystem particleSystem)
        {
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    
        public static void SetLayerAllChildren(this GameObject go, int layer)
        {
            foreach (Transform VARIABLE in go.transform)
            {
                VARIABLE.gameObject.layer = layer;
                VARIABLE.gameObject.SetLayerAllChildren(layer);
            }
        }
    
        public static Vector3 RandomPointInBounds(this Bounds bounds) {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    
        public static float Remap (this float value, float from1, float to1, float from2, float to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach(T item in enumeration)
            {
                action(item);
            }
        }
    
        public static float Noise3D(float x, float y, float z, float frequency, float amplitude, float persistence, int octave, int seed)
        {
            float noise = 0.0f;

            for (int i = 0; i < octave; ++i)
            {
                // Get all permutations of noise for each individual axis
                float noiseXY = Mathf.PerlinNoise(x * frequency + seed, y * frequency + seed) * amplitude;
                float noiseXZ = Mathf.PerlinNoise(x * frequency + seed, z * frequency + seed) * amplitude;
                float noiseYZ = Mathf.PerlinNoise(y * frequency + seed, z * frequency + seed) * amplitude;

                // Reverse of the permutations of noise for each individual axis
                float noiseYX = Mathf.PerlinNoise(y * frequency + seed, x * frequency + seed) * amplitude;
                float noiseZX = Mathf.PerlinNoise(z * frequency + seed, x * frequency + seed) * amplitude;
                float noiseZY = Mathf.PerlinNoise(z * frequency + seed, y * frequency + seed) * amplitude;

                // Use the average of the noise functions
                noise += (noiseXY + noiseXZ + noiseYZ + noiseYX + noiseZX + noiseZY) / 6.0f;

                amplitude *= persistence;
                frequency *= 2.0f;
            }

            // Use the average of all octaves
            return noise / octave;
        }
    
        public static List<Transform> GetChilds(this GameObject target)
        {
            List<Transform> childs = new List<Transform>();
            for (int i = 0; i < target.transform.childCount; i++)
            {
                childs.Add(target.transform.GetChild(i));
            }
            return childs;
        }
    
        public static void Shuffle<T>(this List<T> list) {
            int swapIndex;
            for (int i = 0; i < list.Count; ++i)
            {
                swapIndex = Random.Range(0, list.Count);
                T temp = list[i];
                list[i] = list[swapIndex];
                list[swapIndex] = temp;
            }
        }

        public static void SuccessShakeUI(this Transform transform)
        {
            transform.DOComplete();

            transform.DOShakeScale(.3f, .2f);

            foreach (var VARIABLE in transform.GetComponentsInChildren<Image>())
            {
                VARIABLE.DOComplete();
                Color colorToLerp = Color.green;
                colorToLerp.a = VARIABLE.color.a;
                VARIABLE.DOColor(colorToLerp, .15f).SetLoops(2, LoopType.Yoyo);
            }
        }
    
        public static void FailShakeUI(this Transform transform)
        {
            transform.DOComplete();

            transform.DOShakeRotation(.3f, 10, 25);

            foreach (var VARIABLE in transform.GetComponentsInChildren<Image>())
            {
                VARIABLE.DOComplete();
                Color colorToLerp = Color.red;
                colorToLerp.a = VARIABLE.color.a;
                VARIABLE.DOColor(colorToLerp, .15f).SetLoops(2, LoopType.Yoyo);
            }
        }
    }
}

public static class QuaternionExtension
{
    public static Quaternion Lerp(Quaternion p, Quaternion q, float t, bool shortWay)
    {
        if (shortWay)
        {
            float dot = Quaternion.Dot(p, q);
            if (dot < 0.0f)
                return Lerp(ScalarMultiply(p, -1.0f), q, t, true);
        }
 
        Quaternion r = Quaternion.identity;
        r.x = p.x * (1f - t) + q.x * (t);
        r.y = p.y * (1f - t) + q.y * (t);
        r.z = p.z * (1f - t) + q.z * (t);
        r.w = p.w * (1f - t) + q.w * (t);
        return r;
    }
 
    public static Quaternion Slerp(Quaternion p, Quaternion q, float t, bool shortWay)
    {
        float dot = Quaternion.Dot(p, q);
        if (shortWay)
        {
            if (dot < 0.0f)
                return Slerp(ScalarMultiply(p, -1.0f), q, t, true);
        }
 
        float angle = Mathf.Acos(dot);
        Quaternion first = ScalarMultiply(p, Mathf.Sin((1f - t) * angle));
        Quaternion second = ScalarMultiply(q, Mathf.Sin((t) * angle));
        float division = 1f / Mathf.Sin(angle);
        return ScalarMultiply(Add(first, second), division);
    }
 
 
    public static Quaternion ScalarMultiply(Quaternion input, float scalar)
    {
        return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
    }
 
    public static Quaternion Add(Quaternion p, Quaternion q)
    {
        return new Quaternion(p.x + q.x, p.y + q.y, p.z + q.z, p.w + q.w);
    }
}