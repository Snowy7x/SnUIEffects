using UnityEngine;

namespace Snowy.Utils
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected static T instance;
        public virtual bool DestroyOnLoad => true;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<T>();
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T) this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            
            if (!DestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
    }
}