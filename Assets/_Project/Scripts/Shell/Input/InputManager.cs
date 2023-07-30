using Lean.Touch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VoyageSandwich.Shell.Input
{
    public class InputManager : MonoBehaviour
    {
        private IA_PlayerControls _playerControls;

        [SerializeField]
        private VoidEvent _touchEvent;

        [SerializeField]
        private IntEvent _swipeEvent;

        private void OnEnable()
        {
            //_playerControls?.Enable();
            Lean.Touch.LeanTouch.OnFingerTap += HandleOnTap;
            LeanTouch.OnFingerSwipe += HandleOnSwipe;
        }


        private void OnDisable()
        {
            _playerControls?.Disable();
        }

        private void HandleOnTap(LeanFinger finger)
        {
            Debug.Log($"{nameof(InputManager)} Tap Action");

            _touchEvent?.Raise();
        }
        private void HandleOnSwipe(LeanFinger finger)
        {
            Debug.Log($"{nameof(InputManager)} Swipe Action");
            if (finger.SwipeScaledDelta.x > 0)
            {
                _swipeEvent?.Raise(1);
                Debug.Log($"{nameof(InputManager)}: RIGHT");
            }

            else if (finger.SwipeScaledDelta.x < 0)
            {
                _swipeEvent?.Raise(-1);
                Debug.Log($"{nameof(InputManager)}: LEFT");
            }

            else if (finger.SwipeScaledDelta.y > 0)
            {
                Debug.Log($"{nameof(InputManager)}: JUMP");
            }
        }

        private void SwipeActionEnded(InputAction.CallbackContext ctx)
        {
            Debug.Log($"{nameof(InputManager)} Swipe Action Ended");

        }

        private void SwipeAction(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                Debug.Log($"{nameof(InputManager)} Srated");
            }
            Debug.Log($"{nameof(InputManager)} Swipe Action Started");
        }
    }
}