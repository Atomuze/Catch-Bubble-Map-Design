using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ScenceSystem : MonoBehaviour
{
    public GameObject note;
    public Reference refe;
    public AudioSource AudioSource;
    public Button buttonPause;
    public Text debugText;
    public GameObject sliderTime;

    public static int speed = 50;
    public static bool isPause = true;
    public float frameRate = 60;
    private int tapCounts = 0;
    private float displayTime = 0f, bpm, startTime;

    private List<float> tapSpawnTime = new List<float>();
    private List<int> tapSpawnDegree = new List<int>();
    private List<string> notes = new List<string>();

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        Debug.Log("initializing...");

        displayTime = 0;
        Application.targetFrameRate = (int)frameRate;

        debugText.color = new Color(0, 255, 0, 100);

        initialize();
        setTheStartTime();
    }

    void Update()
    {
        if (!isPause)
        {
            displayTime = Time.time - startTime;
            //Force Correction
            if (System.Math.Abs(AudioSource.GetComponent<AudioSource>().time - displayTime - (refe.offset/1000f)) > 0.2f)
            {
                setTheStartTime();
            }

            if (displayTime > tapSpawnTime[tapCounts] && displayTime < tapSpawnTime[tapCounts + 1])
            {
                SpawnTap(tapSpawnDegree[tapCounts]);
                tapCounts++;
            }
            debugText.text = "Display Time\t" + displayTime.ToString() + "\n Taps Count\t" + tapCounts + "\n musictimediff\t" + (AudioSource.GetComponent<AudioSource>().time - displayTime);
        }
    }

    public void initialize()
    {
        StreamReader sReader = new StreamReader(Application.dataPath + "/data/test.txt");
        string data = sReader.ReadToEnd();
        sReader.Close();
        refe = JsonUtility.FromJson<Reference>(data);
        tapSpawnTime.Clear();
        tapSpawnDegree.Clear();
        notes.Clear();

        //read
        bpm = refe.bpm;

        for (int i = 0; i < refe.note.Count; i++)
        {
            foreach (string j in refe.note[i].Split(','))
            {
                notes.Add(j);
            }
        }

        Debug.Log("format tap...");

        float beatCount = 0;
        float beat = 4;
        for (int i = 0; i < notes.Count; i++)
        {
            float time;
            int degree = -1;

            if (notes[i].Contains("{"))
            {
                string[] temp = notes[i].Split('{', '}');
                beat = System.Convert.ToSingle(int.Parse(temp[1]));       //beats   4/4, 3/4...
                beatCount += (4f / beat);
                time = ((frameRate / bpm) * beatCount);                         //Respawn time base on sec
                if (!temp[2].Equals(""))
                {
                    degree = int.Parse(temp[2]);
                }
            }
            else
            {
                beatCount += (4 / beat);
                time = ((frameRate / bpm) * beatCount);

                if (!notes[i].Equals(""))
                {
                    degree = int.Parse(notes[i]);
                }
            }

            if (time != -1)
            {
                tapSpawnTime.Add(time);
                tapSpawnDegree.Add(degree);
            }

        }
        Debug.Log("initialize complete");
    }

    public void setTheStartTime()
    {
        Debug.Log("setTheStartTime");
        AudioSource.GetComponent<AudioSource>().time = sliderTime.GetComponent<Slider>().value;
        startTime = Time.time - (AudioSource.GetComponent<AudioSource>().time - refe.offset / 1000f);
        displayTime = Time.time - startTime;

        for (int i = 0; i < tapSpawnTime.Count - 1; i++)
        {
            if (tapSpawnTime[i] < displayTime && displayTime < tapSpawnTime[i + 1])
            {
                tapCounts = i;
                break;
            }
            else
            {
                tapCounts = 0;
            }
        }
    }

    public void OnBtn()
    {
        if (isPause)
        {
            initialize();
            setTheStartTime();
            AudioSource.GetComponent<AudioSource>().Play();
            sliderTime.GetComponent<Slider>().maxValue = AudioSource.GetComponent<AudioSource>().clip.length;
            isPause = false;
        }
        else
        {
            isPause = true;
            AudioSource.GetComponent<AudioSource>().Pause();
        }
    }

    private void SpawnTap(int spawnDegree)
    {
        if (spawnDegree != -1)
        {
            try
            {
                //Debug.Log(spawnDegree);
                if (spawnDegree >= 0)
                {
                    spawnDegree = spawnDegree >= 360 ? spawnDegree % 360 : spawnDegree;
                    float fl = spawnDegree * Mathf.PI / 180.0f;
                    GameObject commandTap = Instantiate(note) as GameObject;
                    commandTap.transform.position = new Vector2(Mathf.Sin(fl), Mathf.Cos(fl)) * 5;
                    commandTap.name = "tap" + displayTime.ToString();
                }
            }
            catch (System.FormatException e)
            {
                Debug.LogError(e);
            }
        }
    }

    
}