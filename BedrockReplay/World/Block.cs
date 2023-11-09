﻿using BedrockReplay.Textures;
using OpenTK.Mathematics;

namespace BedrockReplay.World
{
    public class Block
    {
        public Vector3 Position;
        public BlockType Type;

        public Dictionary<Faces, FaceData> Faces;
        public Dictionary<Faces, List<Vector2>> TexUV = new Dictionary<Faces, List<Vector2>>()
        {
            { BedrockReplay.World.Faces.FRONT, new List<Vector2>() },
            { BedrockReplay.World.Faces.BACK, new List<Vector2>() },
            { BedrockReplay.World.Faces.LEFT, new List<Vector2>() },
            { BedrockReplay.World.Faces.RIGHT, new List<Vector2>() },
            { BedrockReplay.World.Faces.TOP, new List<Vector2>() },
            { BedrockReplay.World.Faces.BOTTOM, new List<Vector2>() }
        };

        public Dictionary<Faces, List<Vector2>> GetUVsFromCoordinates(Dictionary<Faces, Vector2> coords, float textureSize = 16)
        {
            var faceData = new Dictionary<Faces, List<Vector2>>();

            foreach (var faceCoord in coords)
            {
                faceData[faceCoord.Key] = new List<Vector2>()
                {
                    new Vector2((faceCoord.Value.X+1f)/textureSize, (faceCoord.Value.Y+1f)/textureSize),
                    new Vector2(faceCoord.Value.X/textureSize, (faceCoord.Value.Y+1f)/textureSize),
                    new Vector2(faceCoord.Value.X/textureSize, faceCoord.Value.Y/textureSize),
                    new Vector2((faceCoord.Value.X+1)/textureSize, faceCoord.Value.Y/textureSize)
                };
            }

            return faceData;
        }

        public Block(Vector3 position, BlockType type = BlockType.Air)
        {
            Type = type;
            Position = position;

            if (Type != BlockType.Air)
            {
                TexUV = GetUVsFromCoordinates(TextureData.BlockUVs[Type]);
            }

            Faces = new Dictionary<Faces, FaceData>()
            {
                { BedrockReplay.World.Faces.FRONT, new FaceData {
                    Vertices = AddTransformedVertices(RawFaceData.rawVertexData[BedrockReplay.World.Faces.FRONT]),
                    UV = TexUV[BedrockReplay.World.Faces.FRONT]
                } },
                { BedrockReplay.World.Faces.BACK, new FaceData {
                    Vertices = AddTransformedVertices(RawFaceData.rawVertexData[BedrockReplay.World.Faces.BACK]),
                    UV = TexUV[BedrockReplay.World.Faces.BACK]
                } },
                { BedrockReplay.World.Faces.LEFT, new FaceData {
                    Vertices = AddTransformedVertices(RawFaceData.rawVertexData[BedrockReplay.World.Faces.LEFT]),
                    UV = TexUV[BedrockReplay.World.Faces.LEFT]
                } },
                { BedrockReplay.World.Faces.RIGHT, new FaceData {
                    Vertices = AddTransformedVertices(RawFaceData.rawVertexData[BedrockReplay.World.Faces.RIGHT]),
                    UV = TexUV[BedrockReplay.World.Faces.RIGHT]
                } },
                { BedrockReplay.World.Faces.TOP, new FaceData {
                    Vertices = AddTransformedVertices(RawFaceData.rawVertexData[BedrockReplay.World.Faces.TOP]),
                    UV = TexUV[BedrockReplay.World.Faces.TOP]
                } },
                { BedrockReplay.World.Faces.BOTTOM, new FaceData {
                    Vertices = AddTransformedVertices(RawFaceData.rawVertexData[BedrockReplay.World.Faces.BOTTOM]),
                    UV = TexUV[BedrockReplay.World.Faces.BOTTOM]
                } }
            };
        }

        public List<Vector3> AddTransformedVertices(List<Vector3> vertices)
        {
            var transformedVertices = new List<Vector3>();
            foreach (var vert in vertices)
            {
                transformedVertices.Add(vert + Position);
            }
            return transformedVertices;
        }
        public FaceData AddFace(Faces face)
        {
            return Faces[face];
        }
    }
}
