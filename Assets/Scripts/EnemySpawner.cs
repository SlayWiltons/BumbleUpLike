using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace SceneEnv
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] float spawnDeltaTime = 3f;
        [Range(0, 11)] [SerializeField] float distanceToPlayer = 1;
        [SerializeField] Enemy enemy;

        Vector3 spawnPosition;
        Vector3 enemyScale;
        Vector3 playerScale;


        void Start()
        {
            spawnPosition = Vector3.up;
            enemy = enemy.GetComponent<Enemy>();

            playerScale = Main.self.Player.transform.localScale / 2;
            enemyScale = enemy.transform.localScale;

            StartCoroutine(SpawnEnemies());
        }

        IEnumerator Spawn()
        {
            Instantiate(enemy, spawnPosition, transform.rotation, transform);
            yield return new WaitForSeconds(spawnDeltaTime);
        }


        IEnumerator SpawnEnemies()
        {
            while (true)
            {
                spawnPosition.x = Mathf.Floor(Main.self.Player.transform.position.x - playerScale.x
                    - distanceToPlayer + enemyScale.x) + 0.5f;

                spawnPosition.y = Mathf.Floor(Main.self.Player.transform.position.y - playerScale.y)
                                  + distanceToPlayer + 1.5f;

                spawnPosition.z = Main.self.Player.transform.position.z;

                yield return StartCoroutine(Spawn());
            }
        }

        void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}