using UnityEngine;

/// <summary>
///   <para>Handles converting between text file and game configuration</para>
/// </summary>
public interface IGameConfigHandler
{
    /// <summary>
    /// <para>Convert your text file to a game configuration</para>
    /// </summary>
    /// <param name="textAsset">Text file to convert</param>
    /// <returns>The converted game config</returns>
    public cGameConfiguration FileToConfig(TextAsset textAsset);
    /// <summary>
    /// <para>Convert your game configuration to a text file</para>
    /// </summary>
    /// <param name="path">Path to save file</param>
    /// <param name="gameConfiguration">Save file config values</param>
    public void ConfigToFile(string path, cGameConfiguration gameConfiguration);
}