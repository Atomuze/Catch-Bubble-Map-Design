using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class JsonDemo : MonoBehaviour
{
    public static void createFile()
    {
        List<string> note = new List<string>();

        note.Add("10");
        note.Add("90");
        note.Add("210");
        note.Add("Tap");

        Reference refe = new Reference
        {
            title = "homura",
            artist = "lisa",
            offset = 0,
            level_1 = 1,
            level_2 = 0,
            level_3 = 0,
            note_designer_1 = "atomuze",
            note_designer_2 = "",
            note_designer_3 = "",
            bpm = 152,
            note = note
        };

        string jsonInfo = JsonUtility.ToJson(refe, true);
        File.WriteAllText("data/test.txt", jsonInfo);

        Debug.Log("finish");
    }
}
