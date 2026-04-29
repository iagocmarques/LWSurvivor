using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Core.Bootstrap
{
    public sealed class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private string gameSceneName = "Game";
        [SerializeField] private bool createFpsHud = true;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            Time.fixedDeltaTime = 1f / 60f;

            if (createFpsHud)
                Project.UI.Debug.FpsHud.EnsureExists();
        }

        private void Start()
        {
            StartCoroutine(LoadGameRoutine());
        }

        private IEnumerator LoadGameRoutine()
        {
            if (string.IsNullOrWhiteSpace(gameSceneName))
                yield break;

            if (SceneManager.GetActiveScene().name == gameSceneName)
                yield break;

            var op = SceneManager.LoadSceneAsync(gameSceneName, LoadSceneMode.Single);
            if (op == null)
                yield break;

            op.allowSceneActivation = true;

            while (!op.isDone)
                yield return null;
        }
    }
}