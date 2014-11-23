using System.ComponentModel;

namespace OrdersDb.Domain.Utils
{
    public class DisplayNameExtended : DisplayNameAttribute
    {
        public string PluralDisplayName { get; set; }

        public DisplayNameExtended(string displayName, string pluralDisplayName)
            : base(displayName)
        {
            PluralDisplayName = pluralDisplayName;
        }
    }
}
