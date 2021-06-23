using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class UIInGameManager : Singleton<UIInGameManager>
    {
        public delegate void TimerEvent();
        
        public event TimerEvent TimeIsUp;

        public Text Time;

        public IEnumerator GameTime(int seconds)
        {
            while (seconds > 0)
            {
                yield return new WaitForSeconds(1f);
                seconds--;
                if (seconds % 60 < 10)
                    Time.text = seconds / 60 + ":0" + seconds % 60;
                else
                    Time.text = seconds / 60 + ":" + seconds % 60;
            }

            TimeIsUp?.Invoke();
            yield return null;
        }
    }
}