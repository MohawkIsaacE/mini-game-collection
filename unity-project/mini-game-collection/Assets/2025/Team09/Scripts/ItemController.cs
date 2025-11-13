using UnityEngine;

namespace MiniGameCollection.Games2025.Team09
{
    [RequireComponent(typeof(LineRenderer))]
    public class ItemController : MonoBehaviour
    {
        [field: SerializeField] public PlayerID PlayerID { get; private set; }

        public bool HasBomb = false;
        public GameObject bombVisual;           // The Bomb object on the player
        public GameObject bombProjectilePrefab; // The prefab for throwing
        public float throwForce = 8f;

        private PlayerController playerController;
        private LineRenderer lineRenderer;
        [Range(-180f, 180f)] public float throwAngleOffset = 0f; // Adjustable offset in degrees
        void Start()
        {
            playerController = GetComponent<PlayerController>();
            lineRenderer = GetComponent<LineRenderer>();

            if (lineRenderer != null)
            {
                lineRenderer.positionCount = 2; // straight line
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.05f;
                lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // simple visible line
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
                lineRenderer.enabled = false; // only enable when holding bomb
            }

            if (bombVisual != null)
                bombVisual.SetActive(HasBomb);
        }

        void Update()
        {
            if (HasBomb)
            {
                // Enable and draw line
                if (lineRenderer != null)
                {
                    lineRenderer.enabled = true;
                    DrawPredictionLine();
                }

                // Check for throw input
                if (ArcadeInput.Players[(int)playerController.PlayerID].Action1.Pressed)
                    ThrowBomb();
            }
            else
            {
                // Hide line if no bomb
                if (lineRenderer != null)
                    lineRenderer.enabled = false;
            }
        }

        public void PickUpBomb()
        {
            HasBomb = true;
            if (bombVisual != null)
                bombVisual.SetActive(true);
        }

        void ThrowBomb()
        {
            //Debug.Log("Throwing bomb");

            GameObject bomb = Instantiate(bombProjectilePrefab, transform.position, transform.rotation);

            Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Rotate facing direction by the adjustable offset
                float angleRad = (transform.eulerAngles.z + throwAngleOffset) * Mathf.Deg2Rad;
                Vector2 throwDir = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
                if (PlayerID == PlayerID.Player1)
                {
                    rb.velocity = throwDir * throwForce;
                }
                else
                {
                    rb.velocity = -throwDir * throwForce;
                }
            }

            HasBomb = false;
            if (bombVisual != null)
                bombVisual.SetActive(false);
        }

        void DrawPredictionLine()
        {
            if (!HasBomb || lineRenderer == null) return;

            Vector2 start = transform.position;

            // Adjust the direction by the offset
            float angleRad = (transform.eulerAngles.z + throwAngleOffset) * Mathf.Deg2Rad;
            Vector2 velocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * throwForce;

            float predictionTime = 1f;

            Vector2 end;
            if (PlayerID == PlayerID.Player1)
            {
                end = start + velocity * predictionTime;
            }
            else
            {
                end = start + -velocity * predictionTime;
            }

                lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }

    }
}
