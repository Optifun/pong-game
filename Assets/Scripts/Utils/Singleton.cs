using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T SingletonObj
        {
            get
            {
                if (_instance == null)
                    _instance = GameObject.FindObjectOfType<T>();
                return _instance;
            }
        }
    }
}