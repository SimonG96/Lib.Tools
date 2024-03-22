// Author: Simon.Gockner
// Created: 2024-03-22
// Copyright(c) 2024 SimonG. All Rights Reserved.

namespace Lib.Tools;

public class AsyncLock
{
    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

    public async Task<Lock> Lock()
    {
        await _semaphoreSlim.WaitAsync();
        return new Lock(_semaphoreSlim);
    }
}

public class Lock(SemaphoreSlim semaphoreSlim) : IDisposable
{
    public void Dispose() => semaphoreSlim.Release();
}