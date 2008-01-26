using System;
using System.Collections.Generic;
using System.Text;

using libeveapi;

namespace eveposmon
{
    public class Settings
    {
        public static List<Account> AccountList = new List<Account>();

        /// <summary>
        /// Information about an account that can be used to monitor
        /// starbases.
        /// </summary>
        public class Account
        {
            private int userId;
            private string apiKey;
            private CharacterList.CharacterListItem characterListItem;

            public int UserId
            {
                get { return this.userId; }
                set { this.userId = value; }
            }

            public string ApiKey
            {
                get { return this.apiKey; }
                set { this.apiKey = value; }
            }

            public CharacterList.CharacterListItem CharacterListItem
            {
                get { return this.characterListItem; }
                set { this.characterListItem = value; }
            }
        }
    }
}