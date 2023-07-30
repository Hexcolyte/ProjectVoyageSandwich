using UnityEngine;
using VoyageSandwich.Shell.Base;
using MoreMountains.Feedbacks;
namespace VoyageSandwich.World.Base
{
    [RequireComponent(typeof(MMF_Player))]
    public abstract class MovableObjectBase<T1> : BaseComponent
        where T1: MovableObjectRuntimeData
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;

        public float CurrentYPos => transform.localPosition.y;
        public bool IsCheckSongPosition => IsInitialized && _runtimeData.ToReachSongPosition > 0f;
        public virtual bool IsOverReach(float currentAudioTime) => currentAudioTime >= _runtimeData.ToReachSongPosition;
        
        private T1 _runtimeData;
        public T1 RuntimeData => _runtimeData;

        [SerializeField]
        protected MMF_Player _feedBackPlayer;

        private MMF_Position _positionFeedBack = new MMF_Position();

        public void Initialize(T1 runtimeData)
        {
            base.Initialize();

            _runtimeData = runtimeData;
            InitializeFeedbackPlayer();
        }

        private void InitializeFeedbackPlayer()
        {
            _feedBackPlayer.StopFeedbacks();
            _positionFeedBack.AnimatePositionTarget = gameObject;
            _positionFeedBack.RelativePosition = false;
            _positionFeedBack.DeterminePositionsOnPlay = true;
            _positionFeedBack.FeedbackDuration = 0.1f;
            _positionFeedBack.Space = MMF_Position.Spaces.Local;
            _positionFeedBack.AllowAdditivePlays = true;

            _feedBackPlayer.AddFeedback(_positionFeedBack);
        }

        public void SetSprite(Sprite newSprite)
        {
            _spriteRenderer.sprite = newSprite;
        }

        public virtual void MoveY(float newYPos)
        {
            Vector3 currentPos = transform.localPosition;
            //transform.localPosition = new Vector3(currentPos.x, newYPos, currentPos.z);
            _positionFeedBack.InitialPosition = currentPos;
            _positionFeedBack.DestinationPosition = new Vector3(currentPos.x, newYPos, currentPos.z);
            _feedBackPlayer.PlayFeedbacks();
        }

        public virtual void Move(Vector3 newPosition)
        {
            _positionFeedBack.InitialPosition = newPosition;
            transform.localPosition = newPosition;
        }

        public virtual void Rotate(Quaternion newRotation)
        {
            _spriteRenderer.transform.rotation = newRotation;
        }

        public virtual void Show()
        {
            _spriteRenderer.enabled = true;
        }

        public virtual void Hide()
        {
            _feedBackPlayer.FeedbacksList.Clear();
            _feedBackPlayer.StopFeedbacks();
            _spriteRenderer.enabled = false;
        }

        public virtual void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}