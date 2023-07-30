using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoyageSandwich.Shell.Base;
using VoyageSandwitch.Shell.Enum;

namespace VoyageSandwich.Core.Player
{
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
        [SerializeField]
        private Animator _anim;
        #endregion

        #region Private Variables
        private VoidListener _tapInputListener;
        private IntListener _swipeInputListener;
        
        private SwipeDirectionEnum _swipeDirection;

        private PathPositionEnum _pathPosition = PathPositionEnum.Center; 
        
        private int _currentState = 0;
        private float _lockedTill;
        private bool _isDashing;
        private Transform PlayerTransform => _anim.transform;
        #endregion

        public override void Initialize()
        {
            if(_anim)
            {
                _anim.CrossFade(Idle, 0f);
            }
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
                    _swipeDirection = SwipeDirectionEnum.Right;
                    PlayerTransform.position = new Vector2(PlayerTransform.position.x + 1, PlayerTransform.position.y);
                    _isDashing = true;

                    if (PlayerTransform.position.x == 0) _pathPosition = PathPositionEnum.Center;
                    else _pathPosition = PathPositionEnum.Right;
                }
            }

            else
            {
                if(_pathPosition != PathPositionEnum.Left)
                {
                    _swipeDirection = SwipeDirectionEnum.Left;
                    PlayerTransform.position = new Vector2(PlayerTransform.position.x - 1, PlayerTransform.position.y);
                    _isDashing = true;

                    if (PlayerTransform.position.x == 0) _pathPosition = PathPositionEnum.Center;
                    else _pathPosition = PathPositionEnum.Left;
                }
            }

        }

        private int GetState()
        {
            if (Time.time < _lockedTill) return _currentState;

            if (_isDashing)
            {
                if (_swipeDirection == SwipeDirectionEnum.Right) return LockState(Dash, _dashDuration);
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
