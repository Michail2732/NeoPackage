using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Utilities
{
    /// <summary>
    /// Ключи должны быть униакльны для каждого из механизмов 
    /// программы (извлечение, проверка, условие)
    /// </summary>
    public class JsonIdentifierKeys
    {
        #region checks
        public static readonly string LengthCheckId = "length_check";
        public static readonly string MDictionaryCheckId = "m_dictionary_check";
        public static readonly string SDictionaryCheckId = "s_dictionary_check";
        public static readonly string ValueCheckId = "value_check";
        public static readonly string LoadlistCheckId = "loadlist_check";
        public static readonly string LoadlistLinkCheckId = "loadlist_link_check";
        public static readonly string LoadlistSDictCheckId = "loadlist_s_dict_check";
        public static readonly string LoadlistStructCheckId = "loadlist_struct_check";
        #endregion
        #region extracts
        public static readonly string RegexExtractId = "regex_extract";
        public static readonly string StaticExtractId = "static_resource_extract";
        public static readonly string StaticValueExtractId = "static_value_extract";
        public static readonly string SubstringExtractId = "substring_extract";
        public static readonly string LoadlistExtractId = "loadlist_extract";
        #endregion
        #region conditions
        public static readonly string ContainsConditionId = "contains_condition";
        public static readonly string EqualConditionId = "equal_condition";
        public static readonly string RegexConditionId = "regex_condition";
        public static readonly string LoadlistColumnConditionId = "loadlist_column_codition";
        #endregion
    }
}
