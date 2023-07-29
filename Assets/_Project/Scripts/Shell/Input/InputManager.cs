using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VoyageSandwich.Shell.Input
{
    public class InputManager : MonoBehaviour
    {
        private IA_PlayerControls playerControls;

        private void Awake()
        {
            playerControls = new IA_PlayerControls();
        }

        private void OnEnable()
        {
            playerControls?.Enable();
        }

        private void OnDisable()
        {
            playerControls?.Disable();
        }

        private void Start()
        {
            playerControls.Player.Attack.performed += ctx => AttackAction(ctx);
        }

        private void AttackAction(InputAction.CallbackContext context)
        {
            Debug.Log($"Touch started {context.ReadValue<float>()}");
        }
    }
}