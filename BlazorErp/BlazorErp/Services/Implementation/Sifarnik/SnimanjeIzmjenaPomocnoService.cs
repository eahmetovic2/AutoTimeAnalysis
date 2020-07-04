using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using BlazorErp.Core.Database;
using BlazorErp.Entities;
using BlazorErp.Shared.Models.Request.Sifarnik;
using BlazorErp.Services.Definition.Sifarnik;

namespace BlazorErp.Services.Implementation.Sifarnik
{
    /// <summary>
    /// Genericke metode za snimanje i izmjenu sifarnika
    /// </summary>
    public class SnimanjeIzmjenaPomocnoService : Service, ISnimanjeIzmjenaPomocnoService
    {
        private static string ToUpperFirstLetter(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }
        public SnimanjeIzmjenaPomocnoService(ILifetimeScope scope) : base(scope)
        {
        }

        public bool SaveEntity<TEntity>(Context context, KreirajSifarnikRequestModel model, TEntity entity, DbSet<TEntity> entities, ILifetimeScope scope) where TEntity : class
        {
            var securityLevel = new SecurityLevel { Create = true };

            try
            {
                foreach (KeyValuePair<string, object> polje in model.Sifarnik)
                {
                    try
                    {
                        var info = entity.GetType().GetProperty(ToUpperFirstLetter(polje.Key));
                        if (info != null)
                        {
                            Type t = Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType;
                            if (t.GetTypeInfo().IsEnum)
                            {
                                info.SetValue(entity, Convert.ChangeType(Enum.Parse(t, polje.Value.ToString()), t));
                            }
                            else if (polje.Value.GetType().IsGenericType && polje.Value.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)))
                            {
                                System.Collections.IEnumerable l = (System.Collections.IEnumerable)polje.Value;
                                info.SetValue(entity, l);
                            }
                            else
                            {
                                JsonElement json = (JsonElement)polje.Value;
                                var valueString = json.ToString();
                                var value = Convert.ChangeType(valueString, t);
                                info.SetValue(entity, value);
                            }
                        }
                    }
                    catch (Exception e) { }

                }
                entities.Add(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateEntity<TEntity>(Context context, UpdateSifarnikRequestModel model, TEntity entity, DbSet<TEntity> entities, ILifetimeScope scope) where TEntity : class
        {
            var securityLevel = new SecurityLevel { Update = true };

            try
            {
                foreach (KeyValuePair<string, object> polje in model.Sifarnik)
                {
                    try
                    {
                        if (polje.Key.ToLower() == "isdeleted")
                        {
                            entities.Remove(entity);
                        }
                        else
                        {
                            var info = entity.GetType().GetProperty(ToUpperFirstLetter(polje.Key));
                            if (info != null)
                            {
                                Type t = Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType;

                                if (t.GetTypeInfo().IsEnum)
                                {
                                    info.SetValue(entity, Convert.ChangeType(Enum.Parse(t, polje.Value.ToString()), t));
                                }
                                else
                                {
                                    JsonElement json = (JsonElement)polje.Value;
                                    var valueString = json.ToString();
                                    var value = Convert.ChangeType(valueString, t);
                                    info.SetValue(entity, value);
                                    //info.SetValue(entity, Convert.ChangeType(polje.Value, t));
                                }
                            }
                        }
                    }
                    catch (Exception) { }

                }
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
