using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public int SectionCount = 100;

    public PipeSection[] SectionPrefabs;

    private List<PipeSection> pipeSections;
    private Dictionary<PipeSection.JointType, List<PipeSection>> sectionDictionary;

    void Start()
    {
        DoGenerateMap();
    }
    
    [InspectorButton("DoGenerateMap")]
    public bool _doGenerateMap;

    public void DoGenerateMap()
    {
        GenerateMap(UnityEngine.Random.Range(1, 100000));
    }
    

    public void GenerateMap(int seed)
    {
        FillRuleDictionary();

        tilemap.ClearAllTiles();
        GenerateSections(seed);
        GenerateTiles();
    }
    
    private void GenerateSections(int seed)
    {
        var rng = new System.Random(seed);

        pipeSections = new List<PipeSection>();

        PipeSection prevSection = null, nextSection;
        for (int s = 0; s < SectionCount; s++)
        {
            List<PipeSection> possibleSections = prevSection == null ? new List<PipeSection>(SectionPrefabs) : sectionDictionary[prevSection.EndJoint];
            nextSection = possibleSections[rng.Next(possibleSections.Count)];
            pipeSections.Add(nextSection);
            prevSection = nextSection;
        }
    }

    private void GenerateTiles()
    {
        Vector3Int cursor = new Vector3Int(0, 0, 0);
        
        for (int s = 0; s < pipeSections.Count - 1; s++)
        {
            PipeSection pipeSection = pipeSections[s];
            TileBase[] tiles = pipeSection.Tilemap.GetTilesBlock(pipeSection.SectionSize);

            BoundsInt writePos = new BoundsInt(cursor, pipeSection.SectionSize.size);
            tilemap.SetTilesBlock(writePos, tiles);
            cursor += Vector3Int.right*pipeSection.SectionSize.size.x + Vector3Int.up * pipeSection.VerticalOffset;
        }
    }

    private void FillRuleDictionary()
    {
        sectionDictionary = new Dictionary<PipeSection.JointType, List<PipeSection>>();
        for (int p = 0; p < SectionPrefabs.Length; p++)
        {
            PipeSection section = SectionPrefabs[p];
            if (!sectionDictionary.ContainsKey(section.StartJoint))
            {
                sectionDictionary[section.StartJoint] = new List<PipeSection>();
            }
            sectionDictionary[section.StartJoint].Add(section);
        }
    }
    
}
