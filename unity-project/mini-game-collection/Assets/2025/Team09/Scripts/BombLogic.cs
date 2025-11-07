using System.Collections;
using UnityEngine;
using static MiniGameCollection.ArcadeInput;

namespace MiniGameCollection.Games2025.Team09
{
    public class BombLogic : MonoBehaviour
    {
        public float delay = 2f;             // Time before explosion
        public float radius = 3f;            // Radius of effect
        public GameObject explosionVisual;   // Explosion prefab
        public float explosionDuration = 2f; // Duration to grow visual
        public float maxScale = 3f;          // Final world scale

        private Rigidbody2D rb;
        private Collider2D col;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            StartCoroutine(ExplodeAfterDelay());
        }

        private IEnumerator ExplodeAfterDelay()
        {
            yield return new WaitForSeconds(delay);

            // Stop projectile movement
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
            }

            // Disable collider so it doesn't hit anything else
            if (col != null)
                col.enabled = false;

            // Spawn explosion visual
            if (explosionVisual != null)
            {
                GameObject visual = Instantiate(explosionVisual, transform.position, Quaternion.identity);
                visual.transform.localScale = Vector3.zero; // start from 0

                

                // Start growth coroutine
                yield return StartCoroutine(GrowVisualOverTime(visual.transform));
            }

            // Disable enemies in radius
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D hit in hits)
            {
                var player = hit.GetComponent<PlayerController>();
                if (player != null)
                {
                    //Debug.Log("Hit a player");
                    //player.gameObject.SetActive(false);
                    //player.gameObject.GetComponent<PlayerController>().ShipSpeed = 0f;
                    StartCoroutine(StunPlayerController(player, 2f)); // <-- Use StartCoroutine
                }
            }
        }
        public IEnumerator StunPlayerController(PlayerController pc, float duration)
        {
            // Get SpriteRenderer from the same GameObject as PlayerController
            SpriteRenderer sr = pc.gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.color = Color.red; // show stunned

            pc.enabled = false; // stop movement/input
            Rigidbody2D rb = pc.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = Vector2.zero; // freeze movement

            yield return new WaitForSeconds(duration);

            // Restore
            pc.enabled = true;
            if (sr != null)
                sr.color = Color.white; // back to normal
        }

        private IEnumerator GrowVisualOverTime(Transform visual)
        {
            // Disable enemies in radius
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D hit in hits)
            {
                var player = hit.GetComponent<PlayerController>();
                if (player != null)
                {
                    Debug.Log("Hit a player");
                    Debug.Log(player.gameObject.GetComponent<PlayerController>().isActiveAndEnabled);
                    //player.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    StartCoroutine(StunPlayerController(player, 2f)); // <-- Use StartCoroutine
                    //player.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    //player.gameObject.SetActive(false);
                }
            }
            // Immediately disable the bomb projectile
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color c = sr.color;
                c.a = 0f; // make invisible
                sr.color = c;
            }
            float timer = 0f;
            Vector3 startScale = Vector3.zero;
            Vector3 finalScale = Vector3.one * maxScale;

            while (timer < explosionDuration)
            {
                timer += Time.deltaTime;
                visual.localScale = Vector3.Lerp(startScale, finalScale, timer / explosionDuration);
                yield return null;
            }

            visual.localScale = finalScale;

            // Destroy visual after a short delay
            Destroy(visual.gameObject, 2f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
