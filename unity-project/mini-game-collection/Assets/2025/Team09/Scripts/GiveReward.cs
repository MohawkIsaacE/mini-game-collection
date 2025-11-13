using MiniGameCollection.Games2025.Team09;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameCollection.Games2025.Team09
{

    public class GiveReward : MonoBehaviour
    {
        public TheControlScript control;
        public GameObject Player1;
        public GameObject Player2;
        void Awake()
        {
            if (Player1 == null)
            {
                Player1 = GameObject.Find("2025-team09-player1");
            }
            if (Player2 == null)
            {
                Player2 = GameObject.Find("2025-team09-player2");
            }
            // Automatically find the main game controller
            if (control == null)
                control = FindObjectOfType<TheControlScript>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if what touched us has a PlayerController
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player != null) // It is a player
            {
                //Debug.Log("This game obj name is: " + this.gameObject.name);
                if (this.gameObject.name == "2025-team09-SilverCoin(Clone)")
                {
                    if (player.PlayerID == PlayerID.Player1)
                    {
                        control.GivePlayer1APoint(1);
                    }
                    else if (player.PlayerID == PlayerID.Player2)
                    {
                        control.GivePlayer2APoint(1);
                    }
                }
                else if (this.gameObject.name == "2025-team09-GoldCoin(Clone)")
                {
                    if (player.PlayerID == PlayerID.Player1)
                    {
                        control.GivePlayer1APoint(3);
                    }
                    else if (player.PlayerID == PlayerID.Player2)
                    {
                        control.GivePlayer2APoint(3);
                    }
                }
                else if (this.gameObject.name == "2025-team09-BombPickup(Clone)")
                {
                    //Debug.Log("Picking up");
                    // Only players can pick up
                    ItemController itemController = collision.GetComponent<ItemController>();
                    if (itemController != null && !itemController.HasBomb)
                    {
                        itemController.PickUpBomb(); // Activate the bomb visual
                        Destroy(gameObject); // Remove the pickup from the scene
                    }
                }
                else if (this.gameObject.name == "2025-team09-SpeedPotion(Clone)")
                {
                    player.GetComponent<PlayerController>().ApplySpeedPotion(2f, 4f);
                }
                Destroy(gameObject); //  Remove the coin
            }
        }
    }
}
