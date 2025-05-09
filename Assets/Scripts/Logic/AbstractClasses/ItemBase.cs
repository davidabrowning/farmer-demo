using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public abstract class ItemBase : MonoBehaviour
    {
        public Vector2Int AnchorPosition;
        public Vector2Int Size;
        public int ClockwiseRotationDegrees;
        public List<Vector2Int> OccupiedTiles;
        public Vector2Int BottomLeft { get { return AnchorPosition; } }
        public Vector2Int TopRight { get { return AnchorPosition + Size - Vector2Int.one; } }
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            if (_animator != null)
                _animator.speed = 0.3f;
        }

        protected virtual void Start()
        {
            if (transform.Find("PowerIndicator") != null)
            {
                PlayerScript.Instance.ElectricityStatusUpdate += HandleElectricityStatusUpdate;
                HandleElectricityStatusUpdate(); // Update to match initial electricity status
            }
        }

        protected virtual void LateUpdate()
        {
            // Multiply by -100 to convert Y to int and get a good range
            _spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
        }

        protected void AdjustAnimationSpeed(float speedMultiplier)
        {
            _animator.speed *= speedMultiplier;
        }

        protected void StartIdleAnimation()
        {
            if (_animator.GetBool("IsWorking") != null)
                _animator.SetBool("IsWorking", false);
            _animator.SetBool("IsTraveling", false);
        }

        protected void StartWorkingAnimation()
        {
            _animator.SetBool("IsWorking", true);
        }

        protected void StartTravelingAnimation()
        {
            _animator.SetBool("IsTraveling", true);
        }
        private void HandleElectricityStatusUpdate()
        {
            transform.Find("PowerIndicator").GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100 + 1);
            if (PlayerScript.Instance.ElectricityIsOn)
                transform.Find("PowerIndicator").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Adornments/PowerIndicatorOn");
            else
                transform.Find("PowerIndicator").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Adornments/PowerIndicatorOff");
        }
    }
}