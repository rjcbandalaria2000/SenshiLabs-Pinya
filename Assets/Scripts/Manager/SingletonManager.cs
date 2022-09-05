using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SingletonManager 
{
    private static List<MonoBehaviour> singletons = new List<MonoBehaviour>();

    static SingletonManager()
    {
        SceneManager.sceneUnloaded += delegate
        {
            // Remove null singletons
            var toDelete = singletons.ToList();
            foreach (var singleton in toDelete)
            {
                if (singleton == null)
                {
                    singletons.Remove(singleton);
                }

            }

        };
    }

    public static T Get<T>() where T : MonoBehaviour
    {
        return singletons
            .Where(s => s is T)
            .FirstOrDefault() as T;
    }

    public static void Register<T>(T obj) where T : MonoBehaviour
    {
        singletons.Add(obj);
    }

    public static void Remove<T>() where T : MonoBehaviour
    {
        T singleton = Get<T>();
        singletons.Remove(singleton);
    }
}
