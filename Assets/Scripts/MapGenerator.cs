using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap[] tilemaps;
    public int SectionCount = 100;

    //public PipeSection[] SectionPrefabs;

    private List<PipeSection> pipeSections;
    private Dictionary<PipeSection.JointType, List<PipeSection>> sectionDictionary;
    private List<PipeSection> sectionInstances;

    void Start()
    {
        DoGenerateMap();
    }
    
    [InspectorButton("DoGenerateMap")]
    public bool _doGenerateMap;

    public string PipeSectionsFolder = "PipeSections";

    public PipeSection StartSection, EndSection;
    private PipeSection smallToMediumSection;

    public void DoGenerateMap()
    {
        GenerateMap(new System.Random().Next());
    }
    

    public void GenerateMap(int seed)
    {
        PreprocessSections();

        for (int t = 0; t < tilemaps.Length; t++)
        {
            tilemaps[t].ClearAllTiles();

            for (int c = tilemaps[t].transform.childCount - 1; c >= 0; c--)
            {
                DestroyImmediate(tilemaps[t].transform.GetChild(c).gameObject);
            }
        }

        try
        {
            GenerateSections(seed);
            GenerateTiles();
        }
        catch (Exception e)
        {
            Debug.LogError("There was an error generating the map");
            Debug.LogError(e.Message);
            Debug.LogError(e.StackTrace);
        }
        PostProcess();
    }

    private void PostProcess()
    {
        for (int s = 0; s < sectionInstances.Count; s++)
        {
            DestroyImmediate(sectionInstances[s].gameObject);
        }
    }

    private void GenerateSections(int seed)
    {
        var rng = new System.Random(seed);

        pipeSections = new List<PipeSection>();
        pipeSections.Add(StartSection);

        PipeSection prevSection = StartSection, nextSection;
        for (int s = 0; s < SectionCount; s++)
        {
            List<PipeSection> possibleSections = prevSection == null ? new List<PipeSection>(sectionInstances) : sectionDictionary[prevSection.EndJoint];
            nextSection = possibleSections[rng.Next(possibleSections.Count)];
            pipeSections.Add(nextSection);
            prevSection = nextSection;
        }
        if (prevSection.EndJoint == PipeSection.JointType.Small)
        {
            pipeSections.Add(smallToMediumSection);
        }
        pipeSections.Add(EndSection);
    }

    private void GenerateTiles()
    {
        Vector3Int cursor = new Vector3Int(0, 0, 0);
        
        for (int s = 0; s < pipeSections.Count; s++)
        {
            PipeSection pipeSection = pipeSections[s];
            for (int t = 0; t < pipeSection.Tilemaps.Length; t++)
            {
                TileBase[] tiles = pipeSection.Tilemaps[t].GetTilesBlock(pipeSection.SectionSize);

                Vector3Int sectionOffset = new Vector3Int(0, -Mathf.FloorToInt(pipeSection.SectionSize.size.y * 0.5f), 0);
                BoundsInt writePos = new BoundsInt(cursor + sectionOffset, pipeSection.SectionSize.size);
                tilemaps[t].SetTilesBlock(writePos, tiles);

                foreach (Transform child in pipeSection.Tilemaps[t].transform)
                {
                    Transform thing = Instantiate(child);
                    thing.parent = tilemaps[t].transform;
                    thing.position = thing.localPosition + cursor + Vector3Int.right * 6;  // i don't know why
                }
            }
            
            cursor += Vector3Int.right * pipeSection.SectionSize.size.x + Vector3Int.up * pipeSection.VerticalOffset;
        }
    }

    private void PreprocessSections()
    {
        var SectionPrefabs = Resources.LoadAll<PipeSection>(PipeSectionsFolder);

        sectionDictionary = new Dictionary<PipeSection.JointType, List<PipeSection>>();
        sectionInstances = new List<PipeSection>();
        for (int p = 0; p < SectionPrefabs.Length; p++)
        {
            PipeSection section = Instantiate(SectionPrefabs[p]);
            for (int t = 0; t < section.Tilemaps.Length; t++)
            {
                section.Tilemaps[t].CompressBounds();
                section.Tilemaps[t].ResizeBounds();
            }
            sectionInstances.Add(section);
            if (!sectionDictionary.ContainsKey(section.StartJoint))
            {
                sectionDictionary[section.StartJoint] = new List<PipeSection>();
            }
            sectionDictionary[section.StartJoint].Add(section);

            if (smallToMediumSection == null && section.StartJoint == PipeSection.JointType.Small && section.EndJoint == PipeSection.JointType.Medium)
            {
                smallToMediumSection = section;
            }
        }
    }
    
}
