using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Reference
{
    public string title, artist, note_designer_1, note_designer_2, note_designer_3;
    public int offset, level_1, level_2, level_3, bpm;
    public static float noteTime, musicTime;
    public List<string> note = new List<string>();
}
