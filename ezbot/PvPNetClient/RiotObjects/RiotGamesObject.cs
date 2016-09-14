// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.RiotGamesObject
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using PvPNetClient.RiotObjects.Platform.Game;
using PvPNetClient.RiotObjects.Platform.Reroll.Pojo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PvPNetClient.RiotObjects
{
  public abstract class RiotGamesObject
  {
    public virtual string TypeName { get; private set; }

    [InternalName("futureData")]
    public int FutureData { get; set; }

    [InternalName("dataVersion")]
    public int DataVersion { get; set; }

    public TypedObject GetBaseTypedObject()
    {
      TypedObject typedObject = new TypedObject(this.TypeName);
      Type type = this.GetType();
      foreach (PropertyInfo property in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
      {
        InternalNameAttribute internalNameAttribute = ((IEnumerable<object>) property.GetCustomAttributes(typeof (InternalNameAttribute), false)).FirstOrDefault<object>() as InternalNameAttribute;
        if (internalNameAttribute != null)
        {
          object obj1 = (object) null;
          Type propertyType = property.PropertyType;
          string name = propertyType.Name;
          if (propertyType == typeof (int[]))
          {
            int[] source = property.GetValue((object) this) as int[];
            if (source != null)
              obj1 = (object) source.Cast<object>().ToArray<object>();
          }
          else if (propertyType == typeof (double[]))
          {
            double[] source = property.GetValue((object) this) as double[];
            if (source != null)
              obj1 = (object) source.Cast<object>().ToArray<object>();
          }
          else if (propertyType == typeof (string[]))
          {
            string[] source = property.GetValue((object) this) as string[];
            if (source != null)
              obj1 = (object) source.Cast<object>().ToArray<object>();
          }
          else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof (List<>))
          {
            IList list = property.GetValue((object) this) as IList;
            if (list != null)
            {
              object[] objArray = new object[list.Count];
              list.CopyTo((Array) objArray, 0);
              List<object> objectList = new List<object>();
              foreach (object obj2 in objArray)
              {
                object obj3 = !typeof (RiotGamesObject).IsAssignableFrom(obj2.GetType()) ? obj2 : (object) (obj2 as RiotGamesObject).GetBaseTypedObject();
                objectList.Add(obj3);
              }
              obj1 = (object) TypedObject.MakeArrayCollection(objectList.ToArray());
            }
          }
          else if (typeof (RiotGamesObject).IsAssignableFrom(propertyType))
          {
            RiotGamesObject riotGamesObject = property.GetValue((object) this) as RiotGamesObject;
            if (riotGamesObject != null)
              obj1 = (object) riotGamesObject.GetBaseTypedObject();
          }
          else
            obj1 = property.GetValue((object) this);
          typedObject.Add(internalNameAttribute.Name, obj1);
        }
      }
      Type baseType = type.BaseType;
      if (baseType != (Type) null)
      {
        foreach (PropertyInfo property in baseType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
          InternalNameAttribute internalNameAttribute = ((IEnumerable<object>) property.GetCustomAttributes(typeof (InternalNameAttribute), false)).FirstOrDefault<object>() as InternalNameAttribute;
          if (internalNameAttribute != null && !typedObject.ContainsKey(internalNameAttribute.Name))
            typedObject.Add(internalNameAttribute.Name, property.GetValue((object) this));
        }
      }
      return typedObject;
    }

    public virtual void DoCallback(TypedObject result)
    {
    }

    public void SetFields<T>(T obj, TypedObject result)
    {
      if (result == null)
        return;
      this.TypeName = result.type;
      foreach (PropertyInfo property in typeof (T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
      {
        InternalNameAttribute internalNameAttribute = ((IEnumerable<object>) property.GetCustomAttributes(typeof (InternalNameAttribute), false)).FirstOrDefault<object>() as InternalNameAttribute;
        if (internalNameAttribute != null)
        {
          Type propertyType = property.PropertyType;
          object obj1;
          if (result.TryGetValue(internalNameAttribute.Name, out obj1))
          {
            try
            {
              if (result[internalNameAttribute.Name] == null)
                obj1 = (object) null;
              else if (propertyType == typeof (string))
                obj1 = (object) Convert.ToString(result[internalNameAttribute.Name]);
              else if (propertyType == typeof (int))
                obj1 = (object) Convert.ToInt32(result[internalNameAttribute.Name]);
              else if (propertyType == typeof (long))
                obj1 = (object) Convert.ToInt64(result[internalNameAttribute.Name]);
              else if (propertyType == typeof (double))
                obj1 = (object) Convert.ToInt64(result[internalNameAttribute.Name]);
              else if (propertyType == typeof (bool))
                obj1 = (object) Convert.ToBoolean(result[internalNameAttribute.Name]);
              else if (propertyType == typeof (DateTime))
                obj1 = result[internalNameAttribute.Name];
              else if (propertyType == typeof (TypedObject))
                obj1 = (object) (TypedObject) result[internalNameAttribute.Name];
              else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof (List<>))
              {
                object[] array = result.GetArray(internalNameAttribute.Name);
                Type genericArgument = propertyType.GetGenericArguments()[0];
                IList instance = (IList) Activator.CreateInstance(typeof (List<>).MakeGenericType(genericArgument));
                foreach (object obj2 in array)
                {
                  if (obj2 == null)
                    instance.Add((object) null);
                  if (genericArgument == typeof (string))
                    instance.Add((object) (string) obj2);
                  else if (genericArgument == typeof (Participant))
                  {
                    TypedObject result1 = (TypedObject) obj2;
                    if (result1.type == "com.riotgames.platform.game.BotParticipant")
                      instance.Add((object) new BotParticipant(result1));
                    else if (result1.type == "com.riotgames.platform.game.ObfruscatedParticipant")
                      instance.Add((object) new ObfruscatedParticipant(result1));
                    else if (result1.type == "com.riotgames.platform.game.PlayerParticipant")
                      instance.Add((object) new PlayerParticipant(result1));
                    else if (result1.type == "com.riotgames.platform.reroll.pojo.AramPlayerParticipant")
                      instance.Add((object) new AramPlayerParticipant(result1));
                  }
                  else if (genericArgument == typeof (int))
                    instance.Add((object) (int) obj2);
                  else
                    instance.Add(Activator.CreateInstance(genericArgument, new object[1]{ obj2 }));
                }
                obj1 = (object) instance;
              }
              else if (propertyType == typeof (Dictionary<string, object>))
                obj1 = (object) (Dictionary<string, object>) result[internalNameAttribute.Name];
              else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof (Dictionary<,>))
                obj1 = (object) (Dictionary<string, object>) result[internalNameAttribute.Name];
              else if (propertyType == typeof (int[]))
                obj1 = (object) result.GetArray(internalNameAttribute.Name).Cast<int>().ToArray<int>();
              else if (propertyType == typeof (string[]))
                obj1 = (object) result.GetArray(internalNameAttribute.Name).Cast<string>().ToArray<string>();
              else if (propertyType == typeof (object[]))
                obj1 = (object) result.GetArray(internalNameAttribute.Name);
              else if (propertyType == typeof (object))
              {
                obj1 = result[internalNameAttribute.Name];
              }
              else
              {
                try
                {
                  obj1 = Activator.CreateInstance(propertyType, new object[1]
                  {
                    result[internalNameAttribute.Name]
                  });
                }
                catch (Exception ex)
                {
                  throw new NotSupportedException(string.Format("Type {0} not supported by flash serializer", (object) propertyType.FullName), ex);
                }
              }
              property.SetValue((object) obj, obj1, (object[]) null);
            }
            catch
            {
            }
          }
        }
      }
    }
  }
}
