using UnityEngine;

namespace Managers.Extension
{
    public static class ComponentExtension
    {
        /// <summary>
        ///     Try to get the component in this gameObject, in its children and parent components
        /// </summary>
        /// <returns>
        ///     Returns false if <paramref name="component"/> is still null after loading
        /// </returns>
        public static bool LoadComponent<T>(this Component self, ref T component, bool isDebug = false) where T : Component
        {
            if (component != null) return true;

            component = self.GetComponent<T>();
            component ??= self.GetComponentInChildren<T>();
            component ??= self.GetComponentInParent<T>();

            if (isDebug)
                if (component == null) Debug.LogWarning($"{self.name}: {typeof(T)} load failed!", self);

            return component != null;
        }


        public static bool CheckComponent<T>(this Component self, T component, bool isDebug = false) where T : Component
        {
            if(isDebug)
                if (component == null) Debug.LogWarning($"{self.name}: {typeof(T)} is null!", self);

            return component != null;
        }
    }
}