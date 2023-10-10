using UnityEngine;

namespace SimonSays.Managers
{
    public interface IObjectPooler
    {
        /// <summary>
        /// Spawns the pooled object and parents the object to given Transform
        /// </summary>
        /// <param name="poolTag">Tag of the object to be spawned</param>
        /// <param name="parent">Parent that will be assigned to the object</param>
        /// <returns>The GameObject found matching the tag specified</returns>
        public GameObject Spawn(string poolTag, Transform parent);
        /// <summary>
        /// Spawns the pooled object and parents the object to given Transform
        /// </summary>
        /// <param name="poolTag">Tag of the object to be spawned</param>
        /// <param name="parent">Parent that will be assigned to the object</param>
        /// <returns>The object found matching the tag specified as given type</returns>
        public T Spawn<T>(string poolTag, Transform parent) where T : Component;
        /// <summary>
        /// Deactives and set parent to the pool,
        /// </summary>
        /// <param name="ins">Object to despawn</param>
        public void DeSpawn(GameObject ins);
    }
}