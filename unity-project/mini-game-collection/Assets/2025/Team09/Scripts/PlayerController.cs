using UnityEngine;

namespace MiniGameCollection.Games2025.Team09
{
    public class PlayerController : MiniGameBehaviour
    {
        [field: SerializeField] public PlayerID PlayerID { get; private set; }
        [field: SerializeField] public GameObject BulletPrefab { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public ScoreKeeper ScoreKeeper { get; private set; }
        [field: SerializeField] public float BulletSpeed { get; private set; } = 8f; // units per second
        [field: SerializeField] public float ShipSpeed { get; private set; } = 20f; // units per second
        [field: SerializeField] public float MinMaxY { get; private set; } = 4.5f; // constraints along Y axis movement
        [field: SerializeField] public float MinMaxX { get; private set; } = 7.5f; // constraints along X axis movement
        [field: SerializeField] public bool CanShoot { get; private set; } = false;

        private BulletOwner Owner => PlayerID switch
        {
            PlayerID.Player1 => BulletOwner.Player1,
            PlayerID.Player2 => BulletOwner.Player2,
            _ => throw new System.Exception(),
        };


        void Update()
        {
            // Get the joystick input
            Vector2 playerMovement = ArcadeInput.Players[(int)PlayerID].Joystick8Way;

            // Inverted movement for player 1
            if (PlayerID == PlayerID.Player1)
            {
                playerMovement = -playerMovement;
            }

            // Update the player position
            float movementX = playerMovement.x * Time.deltaTime * ShipSpeed;
            float movementY = -playerMovement.y * Time.deltaTime * ShipSpeed;
            Vector3 newPosition = transform.position + new Vector3(movementY, movementX, 0);

            // Keep the player in the map
            newPosition.y = Mathf.Clamp(newPosition.y, -MinMaxY, MinMaxY);
            newPosition.x = Mathf.Clamp(newPosition.x, -MinMaxX, MinMaxX);
            
            // Move the player
            Rigidbody2D.MovePosition(newPosition);

            // Old
            if (!CanShoot)
                return;
            if (ArcadeInput.Players[(int)PlayerID].Action1.Pressed)
                ShootBullet();
        }

        void ShootBullet()
        {
            Vector3 position = transform.position + transform.up;
            GameObject prefab = Instantiate(BulletPrefab, position, transform.rotation);
            Bullet bullet = prefab.GetComponent<Bullet>();
            bullet.Shoot(Owner, transform.up, BulletSpeed, ScoreKeeper, MiniGameManager);
        }

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
    }
}
