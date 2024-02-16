// SPDX-License-Identifier: MIT

using System.Collections.Concurrent;

namespace fennecs;

public static class MaskPool
{
    internal static readonly ConcurrentBag<Mask> Pool = [];
    
    public static void Rent(out Mask destination)
    {
        destination = Rent();
    }

    public static Mask Rent()
    {
        return Pool.TryTake(out var mask) ? mask : new Mask();
    }

    public static void Return(Mask mask)
    {
        mask.Clear();
        Pool.Add(mask);
    }
    
    static MaskPool()
    {
        for (var i = 0; i < 32; i++) Pool.Add(new Mask());
    }
}