using UnityEngine;

public interface IGameConfig
{
    public cGameConfiguration GameConfiguration { get; set; }
    public void Save();
    public void Load();
    public cGameConfiguration Load(TextAsset textAsset);
    public void DeleteSaveFile();
}