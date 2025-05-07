using System.Collections;
using UnityEngine;

namespace FarmerDemo
{
    public abstract class AnimalBase : ItemBase
    {
        public float MovementSpeed = 1f;
        public float PauseTimeBetweenMoves = 5f;
        public Vector2Int _targetPosition;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(MoveAround());
            SetRandomTargetPosition();
        }
        protected IEnumerator MoveAround()
        {
            while (true)
            {
                StartIdleAnimation();
                yield return new WaitForSeconds(Random.Range(1, PauseTimeBetweenMoves));

                SetRandomTargetPosition();
                StartTravelingAnimation();
                yield return new WaitForSeconds(0.2f);

                while ((Vector2)transform.position != _targetPosition)
                {
                    yield return new WaitForSeconds(0.01f);
                    float movementProgress = Time.deltaTime * MovementSpeed;
                    transform.position = Vector2.MoveTowards(transform.position, _targetPosition, movementProgress);
                    AnchorPosition.x = (int)Mathf.Round(transform.position.x);
                    AnchorPosition.y = (int)Mathf.Round(transform.position.y);
                }
            }
        }

        public void SetRandomTargetPosition()
        {
            int attempts = 0;
            while (attempts < 10)
            {
                attempts++;
                int randomX = AnchorPosition.x + Random.Range(-1, 2);
                int randomY = AnchorPosition.y + Random.Range(-1, 2);
                Vector2Int proposedLocation = new Vector2Int(randomX, randomY);
                if (!GridManagerScript.Instance.IsOccupied(proposedLocation))
                {
                    _targetPosition = new Vector2Int(randomX, randomY);
                    break;
                }
            }
        }
    }
}