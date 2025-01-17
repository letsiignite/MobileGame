using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    public GameObject player; // Assign the player GameObject in the Inspector
    public float respawnDelay = 2.0f; // Time delay before respawning

    private bool isRespawning = false;

    void Update()
    {
        if (player != null && !player.activeSelf && !isRespawning)
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    private IEnumerator RespawnPlayer()
    {
        isRespawning = true;
        // Wait for the respawn delay
        yield return new WaitForSeconds(respawnDelay);

        //Respawn on last saved point
        CheckPoints.Instance.RespawnPlayer();
        player.SetActive(true);
        //Debug.Log($"Player respawned at {savedPosition}");
        isRespawning = false;
    }
}
