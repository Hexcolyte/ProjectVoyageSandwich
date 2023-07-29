using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoyageSandwich.Shell.Base;
using VoyageSandwitch.Shell.Enum;

namespace VoyageSandwich.World.Player
{
    enum SwipeDirection
    {
        Left,
        Right
    }

    [RequireComponent(typeof(VoidListener))]
    [RequireComponent(typeof(IntListener))]
    public class PlayerController : BaseComponent
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Dash = Animator.StringToHash("Dash");
        private static readonly int DashLeft = Animator.StringToHash("DashLeft");

        #region Serialized Fields
        [SerializeField]
        private float _dashDuration;
        #endregion

        #region Private Variables
        private Animator _anim;
        private VoidListener _tapInputListener;
        private IntListener _swipeInputListener;
        
        private SwipeDirection _swipeDirection;

        private PathPositionEnum _pathPosition = PathPositionEnum.Center; 
        
        private int _currentState = 0;
        private float _lockedTill;
        private bool _isDashing;
        #endregion

        public override void Initialize()
        {
            _anim = GetComponent<Animator>();

            if(_anim)
            {
                _anim.CrossFade(Idle, 0f);
            }

            _tapInputListener = GetComponent<VoidListener>();
            _swipeInputListener = GetComponent<IntListener>();

            //_tapInputListener?.GameEvent.EventListeners = OnTap
        }

        public override void Tick(float deltaTime)
        {
            int state = GetState();

            if (state == _currentState) return;

            _anim.CrossFade(state, 0, 0);
            _currentState = state;

            if(_isDashing) _isDashing = false;
        }

        public void OnTap()
        {
            Debug.Log("Attack");
        }

        public void OnSwipe(int swipeDirection)
        {
            if (swipeDirection > 0)
            {
                if(_pathPosition != PathPositionEnum.Right)
                {
                    _swipeDirection = SwipeDirection.Right;
                    transform.position = new Vector2(transform.position.x + 1, transform.position.y);
                    _isDashing = true;

                    if (transform.position.x == 0) _pathPosition = PathPositionEnum.Center;
                    else _pathPosition = PathPositionEnum.Right;
                }
            }

            else
            {
                if(_pathPosition != PathPositionEnum.Left)
                {
                    _swipeDirection = SwipeDirection.Left;
                    transform.position = new Vector2(transform.position.x - 1, transform.position.y);
                    _isDashing = true;

                    if (transform.position.x == 0) _pathPosition = PathPositionEnum.Center;
                    else _pathPosition = PathPositionEnum.Left;
                }
            }

        }

        private int GetState()
        {
            if (Time.time < _lockedTill) return _currentState;

            if (_isDashing)
            {
                if (_swipeDirection == SwipeDirection.Right) return LockState(Dash, _dashDuration);
                else return LockState(DashLeft, _dashDuration);
            }

            else
                return Idle;

            int LockState(int s, float t)
            {
                _lockedTill = Time.time + t;
                return s;
            }
        }
    }
}
