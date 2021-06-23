using UnityEngine;

namespace UI
{
    public class ChangePlace : MonoBehaviour
    {
        public Canvas GeneralCanvas;
        public Canvas PlayerCanvas;
    
        public void SetPlace()
        {
            GeneralCanvas.enabled = false;
            PlayerCanvas.enabled = true;
        }

        public void GoBack()
        {
            GeneralCanvas.enabled = true;
            PlayerCanvas.enabled = false;
        }

        public void Exit()
        {
            Application.Quit();
        }

        private void Start()
        {
            GeneralCanvas.enabled = true;
            PlayerCanvas.enabled = false;
        }
    }
}