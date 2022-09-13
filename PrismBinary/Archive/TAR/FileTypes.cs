﻿namespace PrismBinary.Archive.TAR
{
    public enum FileTypes
    {
        NormalFile = 0,
        HardLink = 1,
        SymbolicLink = 2,
        DeviceOrSpecialFile = 3,
        BlockDevice = 4,
        Directory = 5,
        NamedPipe = 6,
    }
}