// Author: simon
// Created: 2024-09-17
// Copyright(c) 2024 SimonG. All Rights Reserved.

namespace Lib.Tools;

public static class Directories
{
    public static void Clean(this DirectoryInfo directory)
    {
        foreach (FileInfo file in directory.EnumerateFiles()) 
            file.Delete();

        foreach (DirectoryInfo subDirectory in directory.EnumerateDirectories()) 
            subDirectory.Delete(true);
    }
}