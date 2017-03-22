using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using My.AppStore.Models;

namespace My.AppStore
{
    public class StateData
    {
        public static CountryModel[] Countries
        {
            get
            {
                return _countries;
            }
        }

        private static CountryModel[] _countries = new CountryModel[]
        {
            new CountryModel
            {
                ID = 1,
                Value = "USA",
                Text = "United States of America",
                States = new StateModel[]
                {
                    new StateModel { ID = 1, Text = "Alabama", Value = "AL" },
                    new StateModel { ID = 2, Value = "AL", Text = "Alabama" },
                    new StateModel { ID = 3, Value = "AK", Text =  "Alaska" },
                    new StateModel { ID = 4, Value = "AZ", Text =  "Arizona" },
    new StateModel { ID = 5, Value = "AR", Text =  "Arkansas" },
    new StateModel { ID = 6, Value = "CA", Text =  "California" },
    new StateModel { ID = 7, Value = "CO", Text =  "Colorado" },
    new StateModel { ID = 8, Value = "CT", Text =  "Connecticut" },
    new StateModel { ID = 9, Value = "DE", Text =  "Delaware" },
    new StateModel { ID = 10, Value = "DC", Text =  "District Of Columbia" },
    new StateModel { ID = 11, Value = "FL", Text =  "Florida" },
    new StateModel { ID = 12, Value = "GA", Text =  "Georgia" },
    new StateModel { ID = 13, Value = "HI", Text =  "Hawaii" },
    new StateModel { ID = 14, Value = "ID", Text =  "Idaho" },
    new StateModel { ID = 15, Value = "IL", Text =  "Illinois" },
    new StateModel { ID = 16, Value = "IN", Text =  "Indiana" },
    new StateModel { ID = 17, Value = "IA", Text =  "Iowa" },
    new StateModel { ID = 18, Value = "KS", Text =  "Kansas" },
    new StateModel { ID = 19, Value = "KY", Text =  "Kentucky" },
    new StateModel { ID = 20, Value = "LA", Text =  "Louisiana" },
    new StateModel { ID = 21, Value = "ME", Text =  "Maine" },
    new StateModel { ID = 22, Value = "MD", Text =  "Maryland" },
    new StateModel { ID = 23, Value = "MA", Text =  "Massachusetts" },
    new StateModel { ID = 24, Value = "MI", Text =  "Michigan" },
    new StateModel { ID = 25, Value = "MN", Text =  "Minnesota" },
    new StateModel { ID = 26, Value = "MS", Text =  "Mississippi" },
    new StateModel { ID = 27, Value = "MO", Text =  "Missouri" },
    new StateModel { ID = 28, Value = "MT", Text =  "Montana" },
    new StateModel { ID = 29, Value = "NE", Text =  "Nebraska" },
    new StateModel { ID = 30, Value = "NV", Text =  "Nevada" },
    new StateModel { ID = 31, Value = "NH", Text =  "New Hampshire" },
    new StateModel { ID = 32, Value = "NJ", Text =  "New Jersey" },
    new StateModel { ID = 33, Value = "NM", Text =  "New Mexico" },
    new StateModel { ID = 34, Value = "NY", Text =  "New York" },
    new StateModel { ID = 35, Value = "NC", Text =  "North Carolina" },
    new StateModel { ID = 36, Value = "ND", Text =  "North Dakota" },
    new StateModel { ID = 37, Value = "OH", Text =  "Ohio" },
    new StateModel { ID = 38, Value = "OK", Text =  "Oklahoma" },
    new StateModel { ID = 39, Value = "OR", Text =  "Oregon" },
    new StateModel { ID = 40, Value = "PA", Text =  "Pennsylvania" },
    new StateModel { ID = 41, Value = "RI", Text =  "Rhode Island" },
    new StateModel { ID = 42, Value = "SC", Text =  "South Carolina" },
    new StateModel { ID = 43, Value = "SD", Text =  "South Dakota" },
    new StateModel { ID = 44, Value = "TN", Text =  "Tennessee" },
    new StateModel { ID = 45, Value = "TX", Text =  "Texas" },
    new StateModel { ID = 46, Value = "UT", Text =  "Utah" },
    new StateModel { ID = 47, Value = "VT", Text =  "Vermont" },
    new StateModel { ID = 48, Value = "VA", Text =  "Virginia" },
    new StateModel { ID = 49, Value = "WA", Text =  "Washington" },
    new StateModel { ID = 50, Value = "WV", Text =  "West Virginia" },
    new StateModel { ID = 51, Value = "WI", Text =  "Wisconsin" },
    new StateModel { ID = 52, Value = "WY", Text =  "Wyoming" },
                }

            }, new CountryModel
            {
                ID = 2,
                Text = "Canada",
                Value = "Canada",
                States = new StateModel[]
                {
                    new StateModel { ID = 1, Value = "AB", Text ="Alberta" },
                    new StateModel { ID = 2, Value = "BC", Text ="British Columbia" },
                    new StateModel { ID = 3, Value = "MB", Text ="Manitoba" },
                    new StateModel { ID = 4, Value = "NB", Text ="New Brunswick" },
                    new StateModel { ID = 1, Value = "NL", Text ="Newfoundland and Labrador" },
                    new StateModel { ID = 1, Value = "NS", Text ="Nova Scotia" },
                    new StateModel { ID = 1, Value = "NT", Text ="Northwest Territories" },
                    new StateModel { ID = 1, Value = "NU", Text ="Nunavut" },
                    new StateModel { ID = 1, Value = "ON", Text ="Ontario" },
                    new StateModel { ID = 1, Value = "PE", Text ="Prince Edward Island" },
                    new StateModel { ID = 1, Value = "QC", Text ="Quebec" },
                    new StateModel { ID = 1, Value = "SK", Text ="Saskatchewan" },
                    new StateModel { ID = 1, Value = "YT", Text ="Yukon" }
                }
            }
        };
    }
}