using EPiServer.Framework.Localization;
using EPiServer.Globalization;
using EPiServer.Shell.ObjectEditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPiServer.Commerce.Models.Catalog
{
    public class ColorSelectionFactory : ISelectionFactory 
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata) {
            var allColors = Enum.GetValues(typeof(ProductColor));
            ISelectItem[] colorItems = new ISelectItem[allColors.Length - 1];
            for (var i = 1; i < allColors.Length; i++) {
                var value = LocalizationService.Current.GetStringByCulture("/common/product/colors/" + ((ProductColor)i).ToString(), ContentLanguage.PreferredCulture);
                var item = new SelectItem() { Text = value, Value = value };
                colorItems[i - 1] = item; 
            }
            return colorItems; 
        }

        public enum ProductColor
        {
            None,
            White,
            Beige,
            Yellow,
            Red,
            Blue,
            Green,
            Brown,
            Grey,
            Black
        }
    }
}
