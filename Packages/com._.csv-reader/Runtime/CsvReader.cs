using System;
using System.Linq;

public class CsvReader
{
    private const string DefaultRowSeparator = "\r\n";
    private const char DefaultFieldSeparator = ',';

    private readonly char _fieldSeparator;
    private readonly string[] _rows;
    private string[] _cashedRow;
    
    private int _rowIterator;
    private int _fieldIterator;

    public CsvReader(string data, string rowSeparator = DefaultRowSeparator, char fieldSeparator = DefaultFieldSeparator)
    {
        _fieldSeparator = fieldSeparator;
        _rows = data.Split(rowSeparator).Where(s => s.Length != 0).ToArray();
    }

    public bool TryRead()
    {
        var isWasLastRow = _rowIterator == _rows.Length;
        if (isWasLastRow) return false;

        _cashedRow = _rows[_rowIterator].Split(_fieldSeparator);
        _rowIterator++;
        return true;
    }

    public bool TryGetField(int i, out string value)
    {
        value = null;
        if (i < 0) throw new Exception();
        if (i >= _cashedRow.Length) return false;
        
        value = _cashedRow[i];
        return true;
    }

    public bool TryGetNextField(out string value)
    {
        value = null;
        var isWasLastField = _fieldIterator == _cashedRow.Length;
        if (isWasLastField) return false;
        
        value = _cashedRow[_fieldIterator++];
        return true;
    }
}

internal static class StringArrayExtension
{
    public static string[] WithoutLast(this string[] target)
        => target.Where((_, index) => index != target.Length - 1).ToArray();
}