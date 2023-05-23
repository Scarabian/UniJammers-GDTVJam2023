using System;
using uj.input.actions;
using UnityEngine;

namespace uj.input
{
    public class InputReader : MonoBehaviour
    {
        InputActions inputActions;

        private void Awake()
        {
            inputActions = new InputActions();
            inputActions.Enable();
        }

        private void OnDestroy()
        {
            inputActions.Disable();
        }

        public Vector2 GetMoveInput()
        {
            return inputActions.Player.Move.ReadValue<Vector2>();
        }

        public Vector2 GetLookInput()
        {
            return inputActions.Player.Look.ReadValue<Vector2>();
        }

        public bool GetCameraSwitchPressedThisFrame()
        {
            return inputActions.Player.SwitchCamera.WasPressedThisFrame();
        }

        public bool GetPauseButtonPressed()
        {
            return inputActions.Player.PauseGame.WasPerformedThisFrame();
        }
    }
}