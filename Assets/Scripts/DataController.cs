using UnityEngine;
using System.IO;


/// <summary>
/// The DataControlelr class is a Unity MonoBehaviour that saves user-related data from PlayerPrefs to a .txt file stored in Application.persistentDataPath — typically used for persistent data storage, 
/// even in Android/Oculus builds. It gathers information like username, skipped video flags, time spent in activities, and user mistakes during an extinguisher task, then formats and writes this into a structured log file.
/// </summary>
/// <Author: Play2Make></Author>
public class DataControlelr : MonoBehaviour
{
    // Keys used to store and retrieve player data from PlayerPrefs
    private const string USER_NAME_KEY = "UserName";
    private const string VIDEO_SKIPPED_KEY = "VideoSkipped";
    private const string EXPLANATION_SKIPPED_KEY = "ExplanationSkipped";
    private const string VIDEO_TIME_SPENT_KEY = "VideoTimeSpent";
    private const string EXPLANATION_TIME_SPENT_KEY = "ExplanationTimeSpent";
    private const string WRONG_EXTINGUISHER_COUNT_KEY = "WrongExtinguisherCount";
    private const string FINAL_WRONG_EXTINGUISHER_COUNT_KEY = "FinalWrongExtinguisherCount";

    // Main function to save user data as a text file
    public void SaveDataToTXT()
    {
        // Retrieve stored values from PlayerPrefs (with default fallbacks)
        string userName = PlayerPrefs.GetString(USER_NAME_KEY, "Desconhecido");
        int videoSkipped = PlayerPrefs.GetInt(VIDEO_SKIPPED_KEY, 0);
        int explanationSkipped = PlayerPrefs.GetInt(EXPLANATION_SKIPPED_KEY, 0);
        float videoTimeSpent = PlayerPrefs.GetFloat(VIDEO_TIME_SPENT_KEY, 0f);
        float explanationTimeSpent = PlayerPrefs.GetFloat(EXPLANATION_TIME_SPENT_KEY, 0f);
        int wrongCount = PlayerPrefs.GetInt(WRONG_EXTINGUISHER_COUNT_KEY, 0);
        int finalWrongCount = PlayerPrefs.GetInt(FINAL_WRONG_EXTINGUISHER_COUNT_KEY, 0);

        // Set the output file name and its full path
        string fileName = $"UserData_{userName}.txt";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Format the log content
        string logData = $"Nome do Usuário: {userName}\n" +
                         $"Pulou ou voltou o vídeo: {(videoSkipped == 1 ? "Sim" : "Não")}\n" +
                         $"Pulou ou voltou a explicação: {(explanationSkipped == 1 ? "Sim" : "Não")}\n" +
                         $"Tempo assistindo ao vídeo: {videoTimeSpent:F2} segundos\n" +
                         $"Tempo na explicação: {explanationTimeSpent:F2} segundos\n" +
                         $"Erros com extintor (parcial): {wrongCount}\n" +
                         $"Erros com extintor (final): {finalWrongCount}\n" +
                         $"---------------------------\n";

        // Attempt to write the file
        try
        {
            File.WriteAllText(filePath, logData);
            Debug.Log($"Dados salvos em: {filePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao salvar o arquivo: " + e.Message);
        }
    }
}
