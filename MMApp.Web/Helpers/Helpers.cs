using System.Web.Mvc;

namespace MMApp.Web.Helpers
{
    public static class Helpers
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
    }
}