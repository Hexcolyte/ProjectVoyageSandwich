using UnityEngine;
using MoreMountains.Feedbacks;
using VoyageSandwich.Shell.Base;
using VoyageSandwich.Shell.Enum;

namespace VoyageSandwich.Shell.Player
{
    [RequireComponent(typeof(VoidListener))]
    [RequireComponent(typeof(IntListener))]
    public class PlayerController : BaseComponent
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Dash = Animator.StringToHash("Dash");
        private static readonly int DashLeft = Animator.StringToHash("DashLeft");
        private static readonly int Attack = Animator.StringToHash("Attack");

        #region Serialized Fields
        [SerializeField]
        private float _dashDuration;
        [SerializeField]
        private float _attackDuration;
        [SerializeField]
        private Animator _anim;
        [SerializeField]
        MMF_Player DashFeedbacks;
        [SerializeField]
        MMF_Player AttackFeedbacks;
        #endregion

        #region Private Variables
        private VoidListener _tapInputListener;
        private IntListener _swipeInputListener;
        
        private SwipeDirectionEnum _swipeDirection;

        private PathPositionEnum _pathPosition = PathPositionEnum.Center; 
        
        private int _currentState = 0;
        private float _lockedTill;
        private bool _isDashing;
        private bool _isAttacking;

        private MMF_Position DashPositionFeedback = new MMF_Position();

        private Transform PlayerTransform => _anim.transform;

        public PathPositionEnum PathPosition => _pathPosition;
        #endregion

        public override void Initialize()
        {
            if(_anim)
            {
                _anim.CrossFade(Idle, 0f);
            }

            DashFeedbacks.AddFeedback(DashPositionFeedback);
            DashPositionFeedback.AnimatePositionTarget = PlayerTransform.gameObject;
            DashPositionFeedback.RelativePosition = false;
            DashPositionFeedback.DeterminePositionsOnPlay = true;
            DashPositionFeedback.FeedbackDuration = _dashDuration;

        }

        public override void Tick(float deltaTime)
        {
            int state = GetState();

            if (state == _currentState) return;

            _anim.CrossFade(state, 0, 0);
            _currentState = state;

            if(_isDashing) _isDashing = false;
            if (_isAttacking) _isAttacking = false;
        }

        public void OnTap()
        {
            if(!_isAttacking) _isAttacking = true;
            AttackFeedbacks.PlayFeedbacks();
        }

        public void OnSwipe(int swipeDirection)
        {
            if (swipeDirection > 0)
            {
                if (_pathPosition != PathPositionEnum.Right)
                {
                    _swipeDirection = SwipeDirectionEnum.Right;
                    //PlayerTransform.position = new Vector2(PlayerTransform.position.x + 1, PlayerTransform.position.y);
                    _isDashing = true;

                    Vector3 playerDestination = PlayerTransform.position + Vector3.right;

                    if (playerDestination.x == 0) _pathPosition = PathPositionEnum.Center;
                    else _pathPosition = PathPositionEnum.Right;

                    DashPositionFeedback.InitialPosition = PlayerTransform.position;
                    DashPositionFeedback.DestinationPosition = playerDestination;
                    DashFeedbacks.PlayFeedbacks();
                }
            }
            else
            {
                if (_pathPosition != PathPositionEnum.Left)
                {
                    _swipeDirection = SwipeDirectionEnum.Left;
                    //PlayerTransform.position = new Vector2(PlayerTransform.position.x - 1, PlayerTransform.position.y);
                    _isDashing = true;
                    Vector3 playerDestination = PlayerTransform.position - Vector3.right;

                    if (playerDestination.x == 0) _pathPosition = PathPositionEnum.Center;
                    else _pathPosition = PathPositionEnum.Left;

                    DashPositionFeedback.InitialPosition = PlayerTransform.position;
                    DashPositionFeedback.DestinationPosition = playerDestination;
                    DashFeedbacks.PlayFeedbacks();
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

            if(_isAttacking)
            {
                return LockState(Attack, _attackDuration);
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
