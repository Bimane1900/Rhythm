using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MidiPlayerTK;


public class Spawner : MonoBehaviour
{
    //public MovingNote script;
    public GameObject Triangle;
     public GameObject Square;
     public GameObject Circle;
     public GameObject Cross;
     public MidiFilePlayer midiFilePlayer;
     public MidiFileLoader midiFileLoader;
     public float DistanceToHit;
     public float NoteSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Triangle.transform.position += new Vector3(0, DistanceToHit, 0);
        Cross.transform.position += new Vector3(0, -DistanceToHit, 0);
        Circle.transform.position += new Vector3(DistanceToHit, 0, 0);
        Square.transform.position += new Vector3(-DistanceToHit, 0, 0);
        //StartCoroutine("SpawnTimer");
        midiFilePlayer.OnEventNotesMidi.AddListener(SpawnNotes);
        midiFileLoader.MPTK_Load();
    }
    bool once = true;
    int[] channelArray = new int[16];
    int CrossUpper;
    int CrossLower;
    int TriangleUpper;
    int TriangleLower;
    int CircleUpper;
    int CircleLower;
    int SquareUpper;
    int SquareLower;
    int PrimaryChannel;
    // Update is called once per frame
    void Update()
    {
        if(once && midiFilePlayer.MPTK_MidiEvents != null && midiFileLoader.MPTK_ReadMidiEvents() != null){
            once=false;
        Debug.LogError(midiFilePlayer.MPTK_MidiEvents.Count);
            // foreach(TrackMidiEvent note in midiFilePlayer.MPTK_MidiEvents){
            //     channelArray[note.Event.Channel]++;

            // }
            List<MPTKEvent> events = midiFileLoader.MPTK_ReadMidiEvents();
            int[] vals = new int[140];
            Debug.LogError(events);
            int[] channelCount = new int[16];
            foreach (MPTKEvent evt in events)
            {
                if(evt.Command == MPTKCommand.NoteOn){
                channelArray[evt.Channel] += evt.Value;
                channelCount[evt.Channel]++;
                }
            }

            for (int i = 0; i < channelArray.Length; i++)
            {
                if(channelCount[i] != 0)
                    channelArray[i] /= channelCount[i];
            }
            
            //System.Array.Sort(channelArray);
            //System.Array.Reverse(channelArray);
            string channels = "";
            for (int i = 0; i < channelArray.Length; i++)
            {
                channels += $"channel {i}: {channelArray[i]}\n";
            }
            Debug.LogError(channels);
            // for (int i = 0; i < vals.Length; i++)
            // {
            //     if(vals[i] > 0)
            //         Debug.LogError(vals[i]);
            // }
            int m = channelArray.Max();
            PrimaryChannel = System.Array.IndexOf(channelArray, m);
            foreach (MPTKEvent evt in events)
            {
                if(evt.Command == MPTKCommand.NoteOn && evt.Channel == PrimaryChannel){
                vals[evt.Value]++;
                }
            }
            string values = "";
            for (int i = 0; i < vals.Length; i++)
            {
                values += $"value {i}: {vals[i]}\n";
            }
            Debug.LogError(values);
            int minVal = 0;
            int maxVal = 140;
            for (int i = 0; i < vals.Length; i++)
            {
                if(vals[i] != 0){
                    minVal = i;
                    break;
                }
            }
            for (int i = vals.Length-1; i >= 0; i--)
            {
                if(vals[i] != 0){
                    maxVal = i;
                    break;
                }
            }
            int range = maxVal-minVal;
            int count = 0;
            for (int i = minVal; i <= maxVal; i++)
            {
                count += vals[i];
            }
            int rangePerButton = range/4;
            int countPerButton = count/4;
            Debug.LogError($"Count per Button {countPerButton}");
            int CrossCount = 0;
            int TriangleCount = 0;
            int CircleCount = 0;
            int SquareCount = 0;
            CrossLower = minVal;
            for (int i = CrossLower; i < maxVal; i++)
            {
                CrossCount += vals[i];
                if(CrossCount >= countPerButton){
                    CrossUpper = i;
                    break;
                }
            }
            TriangleLower = CrossUpper+1;
            for (int i = TriangleLower; i < maxVal; i++)
            {
                TriangleCount += vals[i];
                if(TriangleCount >= countPerButton){
                    TriangleUpper = i;
                    break;
                }
            }
            CircleLower = TriangleUpper+1;
            for (int i = CircleLower; i < maxVal; i++)
            {
                CircleCount += vals[i];
                if(CircleCount >= countPerButton){
                    CircleUpper = i;
                    break;
                }
            }
            SquareLower = CircleUpper+1;
            SquareUpper = 140;
            for (int i = SquareLower; i < maxVal; i++)
            {
                SquareCount += vals[i];
                if(SquareCount >= countPerButton){
                    SquareUpper = i;
                    break;
                }
            }
            // CrossUpper = minVal + rangePerButton;
            // TriangleLower = minVal + rangePerButton;
            // TriangleUpper = TriangleLower + rangePerButton;
            // CircleLower = TriangleUpper;
            // CircleUpper = CircleLower + rangePerButton;
            // SquareLower = CircleUpper;
            // SquareUpper = SquareLower + rangePerButton;
            // m = vals.Max();
            // CrossValue = System.Array.IndexOf(vals, m);
            // vals[CrossValue] = 0;
            // m = vals.Max();
            // CircleValue = System.Array.IndexOf(vals, m);
            // vals[CircleValue] = 0;
            // m = vals.Max();
            // TriangleValue = System.Array.IndexOf(vals, m);
            // vals[TriangleValue] = 0;
            // m = vals.Max();
            // SquareValue = System.Array.IndexOf(vals, m);
            // vals[SquareValue] = 0;
            Debug.LogError($"Primary channel: {PrimaryChannel}");
            Debug.LogError($"CrossUpper : {CrossUpper}, CrossLower : {CrossLower}");
            Debug.LogError($"TriangleUpper : {TriangleUpper}, TriangleLower : {TriangleLower}");
            Debug.LogError($"SquareUpper : {SquareUpper}, SquareLower : {SquareLower}");
            Debug.LogError($"CircleUpper : {CircleUpper}, CircleLower : {CircleLower}");

        }
    }

    // IEnumerator SpawnTimer(){
    //     while(true){
    //         //SpawnNote();
    //         yield return new WaitForSeconds(.5f);
    //     }
    // }

    void SpawnNotes(List<MPTKEvent> notes){
        foreach(var note in notes){
                SpawnNote(note);
        }
    }

    void SpawnNote(MPTKEvent note){
        GameObject clone;
        //Debug.LogError(note.Value);
        //Debug.LogError(note.Channel);
        float offset = 0.5f;
        if(note.Channel == PrimaryChannel && note.Value <= TriangleUpper && note.Value >= TriangleLower){
            clone = Object.Instantiate(Triangle, Triangle.transform.position, Triangle.transform.rotation);
            MovingNote cloneNote = clone.GetComponent<MovingNote>();
            cloneNote.enabled = true;   
            cloneNote.note = note;
            clone.transform.position += new Vector3(0,-offset,0);
            cloneNote.SetSpeed(new Vector3(0, -NoteSpeed, 0));
        }
        else if(note.Channel == PrimaryChannel && note.Value <= SquareUpper && note.Value >= SquareLower){
            clone = Object.Instantiate(Square, Square.transform.position, Square.transform.rotation);
            MovingNote cloneNote = clone.GetComponent<MovingNote>();
            cloneNote.enabled = true;   
            cloneNote.note = note;
            clone.transform.position += new Vector3(offset,0,0);
            cloneNote.SetSpeed(new Vector3(NoteSpeed, 0, 0));
        }
        else if(note.Channel == PrimaryChannel && note.Value <= CrossUpper && note.Value >= CrossLower){
            clone = Object.Instantiate(Cross, Cross.transform.position, Cross.transform.rotation);
            MovingNote cloneNote = clone.GetComponent<MovingNote>();
            cloneNote.enabled = true;   
            cloneNote.note = note;
            clone.transform.position += new Vector3(0,offset,0);
            cloneNote.SetSpeed(new Vector3(0, NoteSpeed, 0));
        }
        else if(note.Channel == PrimaryChannel && note.Value <= CircleUpper && note.Value >= CircleLower){
            clone = Object.Instantiate(Circle, Circle.transform.position, Circle.transform.rotation);
            MovingNote cloneNote = clone.GetComponent<MovingNote>();
            cloneNote.enabled = true;   
            cloneNote.note = note;
            clone.transform.position += new Vector3(-offset,0,0);
            cloneNote.SetSpeed(new Vector3(-NoteSpeed, 0, 0));
        }
        else{
            clone = Object.Instantiate(Triangle, Triangle.transform.position, Triangle.transform.rotation);
            MovingNote n = clone.GetComponent<MovingNote>();
            n.enabled = true;
            n.note = note;
            n.autoPlay = true;
            n.TrailParticleSystem.gameObject.SetActive(false);
            n.CollisionParticleSystem.gameObject.SetActive(false);
            clone.transform.position += new Vector3(0,-0.5f,0);
            n.SetSpeed(new Vector3(0, -NoteSpeed, 0));
            Destroy(n.Sphere.gameObject);
        }
        //clone.AddComponent<MovingNote>();
        //clone.GetComponent<SphereCollider>().isTrigger = false;
        //Destroy(clone.GetComponent<CollisionDetection>());
    }
}
