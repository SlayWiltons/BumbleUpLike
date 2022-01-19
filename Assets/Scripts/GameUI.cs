using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using SceneEnv;

namespace InGameUI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI points;
        [SerializeField] TextMeshProUGUI pointsEndGame;
        [SerializeField] GameObject gameOverPanel;

        void Awake()
        {
            Time.timeScale = 1;
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        void Update()
        {
            if (Main.self.StateGame == GameStates.Play)
                points.text = string.Format("{0}", Main.self.Player.NumOvercomedStairs);

            if (Main.self.StateGame == GameStates.GameOver)
                StartCoroutine(ShowDefeat());
        }

        IEnumerator ShowDefeat()
        {
            yield return new WaitForSeconds(.5f);
            pointsEndGame.text = string.Format("{0}", Main.self.Player.NumOvercomedStairs);
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }

        void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}