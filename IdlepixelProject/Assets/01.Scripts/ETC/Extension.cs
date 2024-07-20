using System;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public static class Extension 
{
    // 1.Id 가져옴
    // 2.GachaItem의 Level 상승폭 만큼 증가
    public static float LevelFormet(this float _value ,int _level)
    {
        return _value * _level;
    }
    public static void FormetRarityTypeColor(this Text _origin,string _text ,Rarity _rarity)
    {
        switch (_rarity)
        {
            case Rarity.Comon:
                _origin.color = Color.white;
                break;
            case Rarity.Uncomon:
                _origin.color = Color.green;
                break;
            case Rarity.Rare:
                _origin.color = Color.blue;
                break;
            case Rarity.Epic:
                _origin.color = Color.magenta;
                break;
            case Rarity.Legendary:
                _origin.color = Color.yellow;
                break;
            case Rarity.Mythical:
                _origin.color = Color.cyan;
                break;
            case Rarity.Transcendental:
                _origin.color = Color.red;
                break;
            default:
                break;
        }
        _origin.text = _text;
    }
        public static string FormatNumber(this int number)
    {
        string numbers  = number.ToString();

        int numCount = numbers.Length;
        if (numCount < 4)
            return number.ToString();

        string str = "";
        for (int i = 0; numCount > i; i++)
        {
            if (i == 0) str += "1";
            else str += "0";
        }
        float f = (float)number / float.Parse(str);

        char letter = (char)('A' + numCount - 4);
        str = $"{f.ToString("0.00")}{letter}";
      
        return str;
    }
    public static string DateTimeToString(this DateTime _Oregin)
    {
        return _Oregin.ToString("yyyy-MM-dd HH:mm:ss");
    }
    public static DateTime StringToDateTime(this string _dateString)
    {
        DateTime dateTime;
        if (DateTime.TryParseExact(_dateString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
        {
            return dateTime;
        }
        else
        {
            // 예외 처리: 올바른 형식의 날짜가 아닌 경우 처리
            throw new ArgumentException("올바른 날짜 형식이 아닙니다.");
        }
    }
    public static (int A,int B) GetHalf(this int _num)
    {
        float a = 0;
        float b = 0;

        a = _num / 2;
        b = _num / 2;

        if (_num % 2 == 1)
            a += 1;
        return ((int)a, (int)b);
    }
    public static bool Between(this float _float, float _min, float _max)
    {
        if(_float >= _min && _float <= _max)
            return true;
        return false;
    }
    public static bool Between(this int _int, int _min, int _max)
    {
        if (_int >= _min && _int <= _max)
            return true;
        return false;
    }
    /// <summary>
    /// Return the values of two integers from 0 to 1
    /// </summary>
    /// <param name="_min"></param>
    /// <param name="_max"></param>
    /// <returns></returns>
    public static float ReverseMapRange(int _min, int _max,int _targetVal)
    {
        float ratio = (float)(_targetVal - _min) / (_max - _min);
        return ratio;
    }
    
}