﻿namespace SharpVE.Blocks
{
    public class BlockRegistry
    {
        public readonly Dictionary<string, Block> Blocks;
        public readonly Block DefaultBlock;
        public readonly Block UnknownBlock;

        public BlockRegistry(Block? defaultBlock = null, Block? unknownBlock = null)
        {
            Blocks = new Dictionary<string, Block>();
            DefaultBlock = defaultBlock ?? new Block("air") { IsOpaque = false };
            UnknownBlock = unknownBlock ?? new Block("unknown");
        }

        public Block GetBlock(string? identifier, bool returnDefault = false)
        {
            if (identifier == null) return returnDefault ? DefaultBlock : UnknownBlock;

            Blocks.TryGetValue(identifier, out var block);
            if (block == null) return returnDefault? DefaultBlock : UnknownBlock;
            return block;
        }
    }
}
