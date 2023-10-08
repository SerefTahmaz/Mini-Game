using UnityEngine;

public interface IGameConfigHandler
{
    public cGameConfiguration Load(TextAsset textAsset);
    public void CreateConfig(string path, cGameConfiguration gameConfiguration);
}