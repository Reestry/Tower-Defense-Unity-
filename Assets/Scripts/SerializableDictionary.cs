// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;

[System.Serializable]
public class SerializableDictionary<TKey, TValue>
{
    public List<TKey> Keys = new();
    public List<TValue> Values = new();
}