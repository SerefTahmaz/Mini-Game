using UnityEngine;

public interface IObjectPooler
{
    public GameObject Spawn(string poolTag, Transform parent);
    public T Spawn<T>(string poolTag, Transform parent) where T : Component;
    public void DeSpawn(GameObject ins);
}