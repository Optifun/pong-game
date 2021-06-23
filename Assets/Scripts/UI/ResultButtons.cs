using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class ResultButtons : MonoBehaviour
    {
        public void GoToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}