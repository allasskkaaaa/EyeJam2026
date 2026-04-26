using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_Bobber : MonoBehaviour
{
    bool doMove;
    public bool doBobbing;
    private Vector3 target, origin;
    private float moveSpeed;
    private float maxHeight;
    float trueZero;
    private AnimationCurve trajectoryCurve;
    Vector3 trajectoryRange;
    Vector3 flattenedPosition;
    private void OnEnable()
    {
        transform.position = origin;
        flattenedPosition = origin;
    }

    private void Update()
    {
        if (doMove)
        {
            UpdateBobberPosition();
            return;
        }

        if (doBobbing) BobDoSomething();
        else transform.position = new Vector3(transform.position.x, trueZero, transform.position.z);
    }

    void BobDoSomething()
    {
        float noise = Mathf.PerlinNoise(Time.time * 0.5f, 0f) * 0.05f;
        float newY = trueZero - (Mathf.Sin(Time.time * 20f) * 0.02f + noise) - 0.05f;
        Vector3 bobPos = new Vector3(transform.position.x, newY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, bobPos, 0.5f);
    }

    void UpdateBobberPosition()
    {
        float dist = Vector3.Distance(target, flattenedPosition);
        float t = Vector3.Distance(origin, flattenedPosition) / Vector3.Distance(origin, target);
        float newY = trajectoryCurve.Evaluate(t);
        if (dist > 0.05f)
        {
            Vector3 moveDir = (target - flattenedPosition).normalized;
            Vector3 nextPos = moveDir * moveSpeed * Time.deltaTime;
            flattenedPosition += nextPos;
            transform.position = flattenedPosition + (transform.up * newY * maxHeight);
        }
        else
        {
            doMove = false;
            transform.position = target;
        }
    }

    public void InitializeBobber(Vector3 _origin, Vector3 _target, float _trueZero, float _moveSpeed, float _maxHeight)
    {
        doMove = true;
        origin = _origin;
        target = _target;
        trueZero = _trueZero;
        moveSpeed = _moveSpeed;
        maxHeight = _maxHeight;
        trajectoryRange = _target - _origin;
    }

    public void InitializeAnimation(AnimationCurve _curve)
    {
        trajectoryCurve = _curve;
    }
}
