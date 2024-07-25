using System.Collections;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    [SerializeField] private float _dissolveSpeed = 1;
    [SerializeField] private float _dissolveWait = 1;

    [SerializeField] private Material _dissolveMaterial;

    private bool _isDissolving;
    private float _dissolveValue;
    private float _valueStart = 1;
    private float _valueEnd = 0;

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
            _dissolveMaterial.SetFloat("_Dissolve", _dissolveValue);
            yield return null;
        }

        _dissolveValue = _valueEnd;
        yield return new WaitForSeconds(_dissolveWait);

        while (_dissolveValue < _valueEnd)
        {
            _dissolveValue += Time.deltaTime * _dissolveSpeed;
            _dissolveMaterial.SetFloat("_Dissolve", _dissolveValue);
            yield return null;
        }
        _isDissolving = false;
    }
}
