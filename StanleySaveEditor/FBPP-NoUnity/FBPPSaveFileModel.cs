﻿using System;
using System.Linq;

[Serializable]
public class FBPPFileModel
{
    public StringItem[] StringData = new StringItem[0];
    public IntItem[] IntData = new IntItem[0];
    public FloatItem[] FloatData = new FloatItem[0];
    public BoolItem[] BoolData = new BoolItem[0];

    [Serializable]
    public class StringItem
    {
        string key;
        string val;

        public string Key { get { return key; } set { key = value; } }
        public string Value { get { return val; } set { val = value; } }

        public StringItem(string K, string V)
        {
            Key = K;
            Value = V;
        }
    }

    [Serializable]
    public class IntItem
    {
        string key;
        int val;

        public string Key { get { return key; } set { key = value; } }
        public int Value { get { return val; } set { val = value; } }

        public IntItem(string K, int V)
        {
            Key = K;
            Value = V;
        }
    }

    [Serializable]
    public class FloatItem
    {
        string key;
        float val;

        public string Key { get { return key; } set { key = value; } }
        public float Value { get { return val; } set { val = value; } }

        public FloatItem(string K, float V)
        {
            Key = K;
            Value = V;
        }
    }

    [Serializable]
    public class BoolItem
    {
        string key;
        bool val;

        public string Key { get { return key; } set { key = value; } }
        public bool Value { get { return val; } set { val = value; } }

        public BoolItem(string K, bool V)
        {
            Key = K;
            Value = V;
        }
    }

    public object GetValueForKey(string key, object defaultValue)
    {
        if (defaultValue is string)
        {
            for (int i = 0; i < StringData.Length; i++)
            {
                if (StringData[i].Key.Equals(key))
                {
                    return StringData[i].Value;
                }
            }
        }
        if (defaultValue is int)
        {
            for (int i = 0; i < IntData.Length; i++)
            {
                if (IntData[i].Key.Equals(key))
                {
                    return IntData[i].Value;
                }
            }
        }
        if (defaultValue is float)
        {
            for (int i = 0; i < FloatData.Length; i++)
            {
                if (FloatData[i].Key.Equals(key))
                {
                    return FloatData[i].Value;
                }
            }
        }
        if (defaultValue is bool)
        {
            for (int i = 0; i < BoolData.Length; i++)
            {
                if (BoolData[i].Key.Equals(key))
                {
                    return BoolData[i].Value;
                }
            }
        }
        return defaultValue;
    }

    public void UpdateOrAddData(string key, object value)
    {
        if (HasKeyFromObject(key, value))
        {
            SetValueForExistingKey(key, value);
        }
        else
        {
            SetValueForNewKey(key, value);
        }
    }

    private void SetValueForNewKey(string key, object value)
    {
        if (value is string)
        {
            var dataAsList = StringData.ToList();
            dataAsList.Add(new StringItem(key, (string)value));
            StringData = dataAsList.ToArray();
        }
        if (value is int)
        {
            var dataAsList = IntData.ToList();
            dataAsList.Add(new IntItem(key, (int)value));
            IntData = dataAsList.ToArray();
        }
        if (value is float)
        {
            var dataAsList = FloatData.ToList();
            dataAsList.Add(new FloatItem(key, (float)value));
            FloatData = dataAsList.ToArray();
        }
        if (value is bool)
        {
            var dataAsList = BoolData.ToList();
            dataAsList.Add(new BoolItem(key, (bool)value));
            BoolData = dataAsList.ToArray();
        }
    }

    private void SetValueForExistingKey(string key, object value)
    {
        if (value is string)
        {
            for (int i = 0; i < StringData.Length; i++)
            {
                if (StringData[i].Key.Equals(key))
                {
                    StringData[i].Value = (string)value;
                }
            }
        }
        if (value is int)
        {
            for (int i = 0; i < IntData.Length; i++)
            {
                if (IntData[i].Key.Equals(key))
                {
                    IntData[i].Value = (int)value;
                }
            }
        }
        if (value is float)
        {
            for (int i = 0; i < FloatData.Length; i++)
            {
                if (FloatData[i].Key.Equals(key))
                {
                    FloatData[i].Value = (float)value;
                }
            }
        }
        if (value is bool)
        {
            for (int i = 0; i < BoolData.Length; i++)
            {
                if (BoolData[i].Key.Equals(key))
                {
                    BoolData[i].Value = (bool)value;
                }
            }
        }
    }

    public bool HasKeyFromObject(string key, object value)
    {
        if (value is string)
        {
            for (int i = 0; i < StringData.Length; i++)
            {
                if (StringData[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }

        if (value is int)
        {
            for (int i = 0; i < IntData.Length; i++)
            {
                if (IntData[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }

        if (value is float)
        {
            for (int i = 0; i < FloatData.Length; i++)
            {
                if (FloatData[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }

        if (value is bool)
        {
            for (int i = 0; i < BoolData.Length; i++)
            {
                if (BoolData[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void DeleteKey(string key)
    {
        for (int i = 0; i < StringData.Length; i++)
        {
            if (StringData[i].Key.Equals(key))
            {
                var dataAsList = StringData.ToList();
                dataAsList.RemoveAt(i);
                StringData = dataAsList.ToArray();
            }
        }
        for (int i = 0; i < IntData.Length; i++)
        {
            if (IntData[i].Key.Equals(key))
            {
                var dataAsList = IntData.ToList();
                dataAsList.RemoveAt(i);
                IntData = dataAsList.ToArray();
            }
        }
        for (int i = 0; i < FloatData.Length; i++)
        {
            if (FloatData[i].Key.Equals(key))
            {
                var dataAsList = FloatData.ToList();
                dataAsList.RemoveAt(i);
                FloatData = dataAsList.ToArray();
            }
        }
        for (int i = 0; i < BoolData.Length; i++)
        {
            if (BoolData[i].Key.Equals(key))
            {
                var dataAsList = BoolData.ToList();
                dataAsList.RemoveAt(i);
                BoolData = dataAsList.ToArray();
            }
        }
    }

    public void DeleteString(string key)
    {
        for (int i = 0; i < StringData.Length; i++)
        {
            if (StringData[i].Key.Equals(key))
            {
                var dataAsList = StringData.ToList();
                dataAsList.RemoveAt(i);
                StringData = dataAsList.ToArray();
            }
        }
    }

    public void DeleteInt(string key)
    {
        for (int i = 0; i < IntData.Length; i++)
        {
            if (IntData[i].Key.Equals(key))
            {
                var dataAsList = IntData.ToList();
                dataAsList.RemoveAt(i);
                IntData = dataAsList.ToArray();
            }
        }
    }

    public void DeleteFloat(string key)
    {
        for (int i = 0; i < FloatData.Length; i++)
        {
            if (FloatData[i].Key.Equals(key))
            {
                var dataAsList = FloatData.ToList();
                dataAsList.RemoveAt(i);
                FloatData = dataAsList.ToArray();
            }
        }
    }

    public void DeleteBool(string key)
    {
        for (int i = 0; i < BoolData.Length; i++)
        {
            if (BoolData[i].Key.Equals(key))
            {
                var dataAsList = BoolData.ToList();
                dataAsList.RemoveAt(i);
                BoolData = dataAsList.ToArray();
            }
        }
    }

    public bool HasKey(string key)
    {
        for (int i = 0; i < StringData.Length; i++)
        {
            if (StringData[i].Key.Equals(key))
            {
                return true;
            }
        }
        for (int i = 0; i < IntData.Length; i++)
        {
            if (IntData[i].Key.Equals(key))
            {
                return true;
            }
        }
        for (int i = 0; i < FloatData.Length; i++)
        {
            if (FloatData[i].Key.Equals(key))
            {
                return true;
            }
        }
        for (int i = 0; i < BoolData.Length; i++)
        {
            if (BoolData[i].Key.Equals(key))
            {
                return true;
            }
        }
        return false;
    }
}
