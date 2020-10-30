﻿using Autodesk.DesignScript.Runtime;
using Newtonsoft.Json;
using ProtoCore.Lang;
using Speckle.Core.Kits;
using Speckle.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Speckle.ConnectorDynamo.Functions
{
  [IsVisibleInDynamoLibrary(false)]
  public class BatchConverter
  {
    private ISpeckleConverter _converter { get; set; }

    public BatchConverter()
    {
      var kit = KitManager.GetDefaultKit();
      _converter = kit.LoadConverter(Applications.Dynamo);
    }

    /// <summary>
    /// Helper method to convert a tree-like structure (nested lists) to Speckle
    /// </summary>
    /// <param name="object"></param>
    /// <returns></returns>
    public Base ConvertRecursivelyToSpeckle(object @object)
    {
      if (@object is ProtoCore.DSASM.StackValue)
        throw new Exception("Invalid input");

      var converted = RecurseTreeToSpeckle(@object);
      var @base = new Base();

      //case 1: lists and basic types => add them to a wrapper Base object in a `data` prop
      //case 2: Base => just use it as it is
      if (IsList(converted) || converted.GetType().IsSimpleType())
      {
        @base["@data"] = converted;
      }
      else if (converted is Base convertedBase)
      {
        @base = convertedBase;
      }


      return @base;
    }

    private object RecurseTreeToSpeckle(object @object)
    {
      if (IsList(@object))
      {
        var list = ((IEnumerable) @object).Cast<object>().ToList();
        return list.Select(x => RecurseTreeToSpeckle(x)).ToList();
      }

      if (@object is DesignScript.Builtin.Dictionary dsDic)
      {
        return DictionaryToBase(dsDic);
      }

      if (@object is Dictionary<string, object> dic)
      {
        return DictionaryToBase(dic);
      }

      //is item
      return TryConvertItemToSpeckle(@object);
    }

    private Base DictionaryToBase(DesignScript.Builtin.Dictionary dsDic)
    {
      var @base = new Base();
      foreach (var key in dsDic.Keys)
      {
        @base[key] = RecurseTreeToSpeckle(dsDic.ValueAtKey(key));
      }

      return @base;
    }

    private Base DictionaryToBase(Dictionary<string, object> dic)
    {
      var @base = new Base();
      foreach (var key in dic.Keys)
      {
        @base[key] = RecurseTreeToSpeckle(dic[key]);
      }

      return @base;
    }

    private object TryConvertItemToSpeckle(object value)
    {
      object result = null;

      if (value is Base || value.GetType().IsSimpleType())
      {
        return value;
      }

      try
      {
        return _converter.ConvertToSpeckle(value);
      }
      catch (Exception ex)
      {
        Core.Logging.Log.CaptureAndThrow(ex);
      }

      return result;
    }


    /// <summary>
    /// Helper method to convert a tree-like structure (nested lists) to Native
    /// </summary>
    /// <param name="base"></param>
    /// <returns></returns>
    public object ConvertRecursivelyToNative(Base @base)
    {
      // case 1: it's an item that has a direct conversion method, eg a point
      if (_converter.CanConvertToNative(@base))
        return RecusrseTreeToNative(@base);

      // case 2: it's a wrapper Base
      //       2a: if there's only one member unpack it
      //       2b: otherwise return dictionary of unpacked members
      var members = @base.GetDynamicMembers();

      if (members.Count() == 1)
      {
        return RecusrseTreeToNative(@base[members.ElementAt(0)]);
      }

      return members.ToDictionary(x => x, x => RecusrseTreeToNative(@base[x]));
    }


    private object RecusrseTreeToNative(object @object)
    {
      if (IsList(@object))
      {
        var list = @object as List<object>;
        return list.Select(x => RecusrseTreeToNative(x));
      }

      return TryConvertItemToNative(@object);
    }

    private object TryConvertItemToNative(object value)
    {
      //it's a simple type or not a Base
      if (value.GetType().IsSimpleType() || !(value is Base))
      {
        return value;
      }

      var @base = (Base) value;
      
      //it's an unsupported Base, return a dictionary
      if (!_converter.CanConvertToNative(@base))
      {
        var members = @base.GetDynamicMembers();
        return members.ToDictionary(x => x, x => RecusrseTreeToNative(@base[x]));
      }

      try
      {
        return _converter.ConvertToNative(@base);
      }
      catch (Exception ex)
      {
        Core.Logging.Log.CaptureAndThrow(ex);
      }

      return null;
    }


    public static bool IsList(object @object)
    {
      var type = @object.GetType();
      return (typeof(IEnumerable).IsAssignableFrom(type) && !typeof(IDictionary).IsAssignableFrom(type) &&
              type != typeof(string));
    }

    public static bool IsDictionary(object @object)
    {
      Type type = @object.GetType();
      return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
    }
  }
}
