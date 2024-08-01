using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Darkalar{
    [RequireComponent(typeof(Renderer))]
    public class GlitchAnimation : MonoBehaviour
    {
        [Header("---- PARAMETERS ----")]
        [SerializeField] private Vector2 _time;
        [SerializeField] private float _timeWait = 0.3f;

        private Material _material;
        private int _useGlitch = Shader.PropertyToID("_Use_Glitch");

        private void Start()
        {
            _material = GetComponent<Renderer>().material;
            StartCoroutine(StartGlitch());
        }

        private IEnumerator StartGlitch()
        {
            while (true)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(_time.x, _time.y));
                _material.SetFloat(_useGlitch,1);
                yield return new WaitForSeconds(_timeWait);
                _material.SetFloat(_useGlitch,0);
            }
        }
    }
}