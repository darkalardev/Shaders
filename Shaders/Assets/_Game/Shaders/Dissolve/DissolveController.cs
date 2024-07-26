using System;
using System.Collections;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    [Header("---- DISSOLVE ----")]
    [SerializeField] private float _dissolveSpeed = 1;
    [SerializeField] private float _dissolveWait = 1;
    [Space]
    [SerializeField] private SkinnedMeshRenderer _dissolveMesh;
    [SerializeField] private int _dissolveMeshIndex;

    [Header("---- REFERENCES ----")]
    [SerializeField] private Material _dissolveMaterial;
    [SerializeField] private Material _dissolveMaterial_2;

    private bool _isDissolving;
    private float _dissolveValue;
    private float _valueStart = 1;
    private float _valueEnd = 0;

    // for optimization
    private int _hash_Dissolve = Shader.PropertyToID("_Dissolve");

    private void Start()
    {
        _dissolveMaterial = _dissolveMesh.materials[0];
        _dissolveMaterial_2 = _dissolveMesh.materials[1];
        
        _dissolveMaterial.SetFloat(_hash_Dissolve, _valueStart);
        _dissolveMaterial_2.SetFloat(_hash_Dissolve, _valueStart);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isDissolving)
            StartCoroutine(Dissolve());
    }

    private IEnumerator Dissolve()
    {
        _isDissolving = true;
        _dissolveValue = _valueStart;

        while (_dissolveValue > _valueEnd)
        {
            _dissolveValue -= Time.deltaTime * _dissolveSpeed;
            _dissolveMaterial.SetFloat(_hash_Dissolve, _dissolveValue);
            _dissolveMaterial_2.SetFloat(_hash_Dissolve, _dissolveValue);
            yield return null;
        }

        _dissolveValue = _valueEnd;
        yield return new WaitForSeconds(_dissolveWait);

        while (_dissolveValue < _valueStart)
        {
            _dissolveValue += Time.deltaTime * _dissolveSpeed;
            _dissolveMaterial.SetFloat(_hash_Dissolve, _dissolveValue);
            _dissolveMaterial_2.SetFloat(_hash_Dissolve, _dissolveValue);
            yield return null;
        }
        _isDissolving = false;
    }
}
