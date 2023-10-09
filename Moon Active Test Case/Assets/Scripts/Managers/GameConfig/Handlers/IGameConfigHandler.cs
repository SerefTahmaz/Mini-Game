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
    public cGameConfiguration Convert(TextAsset textAsset);
    /// <summary>
    /// <para>Create config file in a given path</para>
    /// </summary>
    /// <param name="path">Path to save file</param>
    /// /// <param name="gameConfiguration">Save file config values</param>
    /// <returns>The converted game config</returns>
    public void CreateConfig(string path, cGameConfiguration gameConfiguration);
}