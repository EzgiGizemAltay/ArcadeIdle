using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed = 500;

    private Touch _touch;

    private Vector3 _touchPosSec, _touchPosFirst;

    private bool _dragStarted;
    private bool _isMoving;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                _dragStarted = true;
                _isMoving = true;
                _touchPosFirst = _touch.position;
                _touchPosSec = _touch.position;
            }
        }

        if (_dragStarted)
        {
            if (_touch.phase == TouchPhase.Moved)
            {
                _touchPosSec = _touch.position;
            }

            if (_touch.phase == TouchPhase.Ended)
            {
                _touchPosSec = _touch.position;
                _isMoving = false;
                _dragStarted = false;
            }

            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(),
                rotationSpeed * Time.deltaTime);
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }

        Quaternion CalculateRotation()
        {
            Quaternion temp = Quaternion.LookRotation(CalculateDirection(), Vector3.up);
            return temp;
        }

        Vector3 CalculateDirection()
        {
            Vector3 temp = (_touchPosSec - _touchPosFirst).normalized;
            temp.z = temp.y;
            temp.y = 0;
            return temp;
        }
    }
}
