using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using static MMApp.Web.Helpers.ENums;

namespace MMApp.Web.Helpers
{
    public static class Helper
    {
        public static MvcHtmlString WebsiteLink(this HtmlHelper htmlHelper, string websiteLink, string websiteText)
        {
            var tb = new TagBuilder("a");
            tb.Attributes.Add("href", websiteLink);
            tb.SetInnerText(websiteText);
            return new MvcHtmlString(tb.ToString());
        }

        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src, string altText, string className)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", altText);
            builder.MergeAttribute("class", className);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static void CheckCashedSearchText(string searchText, string cachedSearchText, string cachedKey, out string outSearchText, out bool outRefreshAuthorList)
        {
            InMemoryCache _cache = new InMemoryCache();
            bool refreshAuthorList = false;

            if (searchText != "")
            {
                if (cachedSearchText != null)
                {
                    if (searchText != cachedSearchText)
                    {
                        _cache.RemoveItem(cachedKey);
                        _cache.Set(cachedKey, searchText);
                        refreshAuthorList = true;
                    }
                }
                else
                {
                    _cache.Set(cachedKey, searchText);
                    refreshAuthorList = true;
                }
            }
            else
            {
                if (cachedSearchText != null)
                {
                    searchText = cachedSearchText;
                }
            }

            outSearchText = searchText;
            outRefreshAuthorList = refreshAuthorList;
        }

        public static void CheckCashedSearchByType(string filterType, string filterItem, string cachedFiltertType, string cachedFilterItem,
            string cachedFTKey, string cachedFIKey, out string outFilterType, out string outFilterItem, out bool refreshFromDB)
        {
            InMemoryCache _cache = new InMemoryCache();
            bool refreshModelList = false;

            if (filterType != "" && filterItem != "")
            {
                if (cachedFiltertType != null && cachedFilterItem != null)
                {
                    if (filterType != cachedFiltertType && filterItem != cachedFilterItem)
                    {
                        _cache.RemoveItem(cachedFTKey);
                        _cache.Set(cachedFTKey, filterType);
                        _cache.RemoveItem(cachedFIKey);
                        _cache.Set(cachedFIKey, filterItem);
                        refreshModelList = true;
                    }
                }
                else
                {
                    _cache.Set(cachedFTKey, filterType);
                    _cache.Set(cachedFIKey, filterItem);
                    refreshModelList = true;
                }
            }
            else
            {
                if (cachedFiltertType != "" && cachedFilterItem != "")
                {
                    filterType = cachedFiltertType;
                    filterItem = cachedFilterItem;
                }
            }

            outFilterType = filterType;
            outFilterItem = filterItem;
            refreshFromDB = refreshModelList;
        }

        public static Dictionary<string, string> GetEntityProperties<T>(IModelInterface value, bool includeId) where T : IModelInterface
        {
            Dictionary<string, string> paramDict = new Dictionary<string, string>();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var dbFields = property.GetCustomAttributes(typeof(DBFieldAttribute), false);

                if (dbFields.Length > 0)
                {
                    if (property.Name == "Id")
                    {
                        if (includeId)
                        {
                            paramDict.Add(property.Name, property.GetValue(value, null).ToString());
                        }
                    }
                    else
                    {
                        var pName = property.Name;
                        var pValue = property.GetValue(value, null);

                        if (pValue == null)
                        {
                            paramDict.Add(pName, string.Empty);
                        }
                        else
                        {
                            paramDict.Add(pName, pValue.ToString());
                        }
                    }
                }
            }

            return paramDict;
        }

        public static Dictionary<string, string> GetDuplicateProperties<T>(IModelInterface value) where T : IModelInterface
        {
            Dictionary<string, string> paramDict = new Dictionary<string, string>();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var dbFields = property.GetCustomAttributes(typeof(DBFieldAttribute), false);

                if (dbFields.Length > 0)
                {
                    var pName = property.Name;
                    var pValue = property.GetValue(value, null);

                    if (pValue == null)
                    {
                        paramDict.Add(pName, string.Empty);
                    }
                    else
                    {
                        paramDict.Add(pName, pValue.ToString());
                    }
                }
            }

            return paramDict;
        }

        public static bool CheckForChanges<T>(IModelInterface entity1, IModelInterface entity2) where T : IModelInterface
        {
            bool result = true;

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            object ent1, ent2;

            foreach (var property in entity1.GetType().GetProperties())
            {
                var dbFields = property.GetCustomAttributes(typeof(DBFieldAttribute), false);

                if (dbFields.Length > 0)
                {
                    ent1 = property.GetValue(entity1);
                    ent2 = property.GetValue(entity2);
                    if (!ent1.Equals(ent2)) result = false;
                }
            }

            return result;
        }

        public static ViewDataDictionary ToViewDataDictionary(this object values)
        {
            var dictionary = new ViewDataDictionary();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(values))
            {
                dictionary.Add(property.Name, property.GetValue(values));
            }

            return dictionary;
        }

        public static void DbAction<T>(IMusicRepository db, string spName, IModelInterface entity, out string errorMessage, out int entityId) where T : IModelInterface
        {
            entityId = 0;
            errorMessage = string.Empty;

            try
            {
                switch (spName)
                {
                    case "Add":
                        entityId = db.Add<T>(entity);
                        break;
                }
                
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public static ModelStateDictionary CheckForErrors(ModelStateDictionary modelState, string errorMessage)
        {
            if (errorMessage != string.Empty)
            {
                modelState.AddModelError(string.Empty, errorMessage);
            }

            return modelState;
        }

        public static ModelStateDictionary AddCustomError(ModelStateDictionary modelState, string entityName, ErrorMessageType errorType)
        {
            var errorMessage = ErrorMessages.GetErrorMessage<Country>(entityName, errorType);
            modelState.AddModelError("CustomError", errorMessage);
            return modelState;
        }

        public static string GetErrors(ModelStateDictionary modelState)
        {
            StringBuilder result = new StringBuilder();

            foreach (var item in modelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    foreach (var error in item.Errors)
                    {
                        result.Append(error.ErrorMessage);
                    }
                }
            }

            return result.ToString();
        }
    }
}