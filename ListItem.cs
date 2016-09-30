using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ListItem
{
    private string _key = string.Empty;
    private string _value = string.Empty;
    public ListItem(string pKey, string pValue)
    {
        _key = pKey;
        _value = pValue;
    }
    public override string ToString()
    {
        return this._value;
    }
    public string Key
    {
        get
        {
            return this._key;
        }
        set
        {
            this._key = value;
        }
    }
    public string Value
    {
        get
        {
            return this._value;
        }
        set
        {
            this._value = value;
        }
    }
}
