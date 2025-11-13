using System.Collections;
using UnityEngine;

namespace MiniGameCollection.Games2025.Team09
{
    public class PlayerController : MiniGameBehaviour
    {
        [field: SerializeField] public PlayerID PlayerID { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public ScoreKeeper ScoreKeeper { get; private set; }
        [field: SerializeField] public float BulletSpeed { get; private set; } = 8f;
        [field: SerializeField] public float ShipSpeed { get; private set; } = 40f;
        [field: SerializeField] public float MinMaxY { get; private set; } = 4.5f;
        [field: SerializeField] public float MinMaxX { get; private set; } = 9f;
        [field: SerializeField] public bool CanShoot { get; private set; } = false;

        private float speedMultiplier = 1f; // For speed potions or debuffs
        private SpriteRenderer spriteRenderer;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            Vector2 playerMovement = ArcadeInput.Players[(int)PlayerID].Joystick8Way;

            // Invert for Player1 if needed
            if (PlayerID == PlayerID.Player1)
                playerMovement = -playerMovement;

            if (playerMovement.sqrMagnitude < 0.01f)
                return;

            // Apply speed multiplier
            Vector3 movement = new Vector3(-playerMovement.y, playerMovement.x, 0f) * ShipSpeed * speedMultiplier * Time.deltaTime;
            Vector3 newPosition = transform.position + movement;

            // Clamp to map boundaries
            //newPosition.x = Mathf.Clamp(newPosition.x, -MinMaxX, MinMaxX);
            //newPosition.y = Mathf.Clamp(newPosition.y, -MinMaxY, MinMaxY);

            Rigidbody2D.MovePosition(newPosition);

            // Rotate player to face movement direction
            float angle = Mathf.Atan2(-playerMovement.x, playerMovement.y) * Mathf.Rad2Deg;
            float angleOffset = 0f; // adjust depending on sprite
            if (PlayerID == PlayerID.Player1) angleOffset = 180f;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + angleOffset);
        }

        //void ShootBullet()
        //{
        //    Vector3 position = transform.position + transform.up;
        //    GameObject prefab = Instantiate(BulletPrefab, position, transform.rotation);
        //    Bullet bullet = prefab.GetComponent<Bullet>();
        //    bullet.Shoot(Owner, transform.up, BulletSpeed, ScoreKeeper, MiniGameManager);
        //}

        private void OnValidate()
        {
            if (Rigidbody2D == null)
                Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        protected override void OnGameStart()
        {
            CanShoot = true;
        }

        protected override void OnGameEnd()
        {
            CanShoot = false;
        }

        // =========================
        // Speed potion system
        // =========================
        public void ApplySpeedPotion(float multiplier, float duration)
        {
            StopCoroutine("SpeedPotionRoutine");
            StartCoroutine(SpeedPotionRoutine(multiplier, duration));
        }

        private IEnumerator SpeedPotionRoutine(float multiplier, float duration)
        {
            speedMultiplier = multiplier;

            // Optional: show visual feedback (change color)
            if (spriteRenderer != null)
                spriteRenderer.color = Color.cyan;

            yield return new WaitForSeconds(duration);

            speedMultiplier = 1f;

            if (spriteRenderer != null)
                spriteRenderer.color = Color.white;
        }
    }
}
