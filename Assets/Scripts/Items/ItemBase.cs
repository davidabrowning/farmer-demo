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

        protected virtual void LateUpdate()
        {
            // Multiply by -100 to convert Y to int and get a good range
            _spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
        }

        protected void StartIdleAnimation()
        {
            _animator.SetBool("IsWorking", false);
        }

        protected void StartWorkingAnimation()
        {
            _animator.SetBool("IsWorking", true);
        }
    }
}