using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] float _Ypadding = 1f;
        [SerializeField] float _Xpadding = 1f;
        [SerializeField, Range(1f, 5f)] float speedOfPlayer = 2f;

        // Local Variables
        private float _xMin;
        private float _xMax;
        private float _yMin;
        private float _yMax;

        private Camera _gameCamera;

        // Start is called before the first frame update
        private void Start()
        {
            _gameCamera = Camera.main;
            SetupMoveBoundaries();
        }

        // Update is called once per frame
        void Update()
        {
            // Keyboard fallback so the game can be played in the Editor
            if (TryGetActiveTouch(out var touch)) TouchMovement(touch);
            else MovePlayer();
        }

        // Returns the most recent touch currently in progress
        private static bool TryGetActiveTouch(out TouchControl activeTouch)
        {
            activeTouch = null;
            var touchscreen = Touchscreen.current;
            if (touchscreen == null) return false;

            for (var i = touchscreen.touches.Count - 1; i >= 0; i--)
            {
                if (!touchscreen.touches[i].isInProgress) continue;
                activeTouch = touchscreen.touches[i];
                return true;
            }

            return false;
        }

        void TouchMovement(TouchControl touch)
        {
            // Follow the most recent touch
            Vector3 touchPos = _gameCamera.ScreenToWorldPoint(touch.position.ReadValue());
            SetClampedPosition(touchPos.x, touchPos.y);
        }

        private void MovePlayer(){
            var move = ReadMoveInput();
            var deltaX = move.x * Time.deltaTime * speedOfPlayer;
            var deltaY = move.y * Time.deltaTime * speedOfPlayer;
            SetClampedPosition(transform.position.x + deltaX, transform.position.y + deltaY);
        }

        // WASD / arrow keys, or the left stick of a gamepad
        private static Vector2 ReadMoveInput()
        {
            var move = Vector2.zero;

            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed) move.x -= 1f;
                if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed) move.x += 1f;
                if (keyboard.downArrowKey.isPressed || keyboard.sKey.isPressed) move.y -= 1f;
                if (keyboard.upArrowKey.isPressed || keyboard.wKey.isPressed) move.y += 1f;
            }

            var gamepad = Gamepad.current;
            if (gamepad != null) move += gamepad.leftStick.ReadValue();

            return Vector2.ClampMagnitude(move, 1f);
        }

        private void SetClampedPosition(float x, float y)
        {
            transform.position = new Vector2(Mathf.Clamp(x, _xMin, _xMax), Mathf.Clamp(y, _yMin, _yMax));
        }

        private void SetupMoveBoundaries()
        {
            _xMin = _gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + _Xpadding;
            _xMax = _gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _Xpadding;
            _yMin = _gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + _Ypadding;
            _yMax = _gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _Ypadding;
        }
    }
}
