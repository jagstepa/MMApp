using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMApp.Data;
using MMApp.Domain.Models;
using MMApp.Domain.Repositories;
using System.ComponentModel;

namespace MMApp.Web.Helpers
{
    public class Extensions
    {
        public static IEnumerable<SelectListItem> GetFilterTypes()
        {
            IList<SelectListItem> filterTypes = new List<SelectListItem>
                                                    {
                                                        new SelectListItem { Text = "Select Type", Value = ""},
                                                        new SelectListItem { Text = "Publisher", Value = "Publisher"},
                                                        new SelectListItem { Text = "Year", Value = "Year"}
                                                    };
            return filterTypes;
        }

        public static IEnumerable<SelectListItem> GetFilterItems(string filterType)
        {
            IBookRepository _dashboardSP = new BookSPRepository();
            IList<SelectListItem> filterTypes;

            if (filterType != "")
            {
                var filterItems = new List<FilterItem>();
                if (filterType == "Publisher")
                {
                    filterItems = new List<FilterItem>(_dashboardSP.GetFilterItems<Publisher>(filterType, "").Cast<FilterItem>());
                }
                filterTypes = new List<SelectListItem>
                                                    {
                                                        new SelectListItem { Text = "Select Filter", Value = ""}
                                                    };
                foreach (var filterItem in filterItems)
                {
                    filterTypes.Add(new SelectListItem
                                        {
                                            Text = filterItem.FilterId,
                                            Value = filterItem.FilterText
                                        });
                }
            }
            else
            {
                filterTypes = new List<SelectListItem>
                                                    {
                                                        new SelectListItem { Text = "Select Filter", Value = ""}
                                                    };
            }
            
            return filterTypes;
        }
    }
}