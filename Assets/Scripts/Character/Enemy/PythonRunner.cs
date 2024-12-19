using System.Diagnostics;
using UnityEngine;

public class PythonRunner : MonoBehaviour
{
    public string pythonScriptPath = "Assets/Scripts/Character/Enemy/model.py";

    public void RunPythonScript()
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "python";
        start.Arguments = pythonScriptPath;
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        start.RedirectStandardError = true;
        start.CreateNoWindow = true;

        using (Process process = Process.Start(start))
        {
            using (System.IO.StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                UnityEngine.Debug.Log(result); // UnityEngine.Debugを明示的に指定
            }
            using (System.IO.StreamReader reader = process.StandardError)
            {
                string error = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(error))
                {
                    UnityEngine.Debug.LogError(error); // UnityEngine.Debugを明示的に指定
                }
            }
        }
    }
}

