using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace Pilcrow.Db.Models.Globalization
{
    using LocalCulture = ThreadLocal<CultureInfo>;
    using LanguageDictionary = Dictionary<string, CultureInfo>;

    /// <summary>
    /// Helper and Configuration class for Translatable{T}.
    /// </summary>
    public class Translatable
    {
        public static LanguageDictionary LanguageDefaults = new LanguageDictionary()
        {
            { "aa", new CultureInfo("aa-ET") },
            { "ab", new CultureInfo("ab-GE") },
            { "af", new CultureInfo("af-ZA") },
            { "ak", new CultureInfo("ak-GH") },
            { "am", new CultureInfo("am-ET") },
            { "ar", new CultureInfo("ar-EG") },
            { "as", new CultureInfo("as-IN") },
            { "av", new CultureInfo("av-RU") },
            { "ay", new CultureInfo("ay-BO") },
            { "az", new CultureInfo("az-AZ") },
            { "ba", new CultureInfo("ba-RU") },
            { "be", new CultureInfo("be-BY") },
            { "bg", new CultureInfo("bg-BG") },
            { "bi", new CultureInfo("bi-VU") },
            { "bn", new CultureInfo("bn-BD") },
            { "bo", new CultureInfo("bo-CN") },
            { "bs", new CultureInfo("bs-BA") },
            { "ca", new CultureInfo("ca-ES") },
            { "ce", new CultureInfo("ce-RU") },
            { "ch", new CultureInfo("ch-GU") },
            { "cs", new CultureInfo("cs-CZ") },
            { "cy", new CultureInfo("cy-GB") },
            { "da", new CultureInfo("da-DK") },
            { "de", new CultureInfo("de-DE") },
            { "dv", new CultureInfo("dv-MV") },
            { "dz", new CultureInfo("dz-BT") },
            { "ee", new CultureInfo("ee-GH") },
            { "el", new CultureInfo("el-GR") },
            { "en", new CultureInfo("en-US") },
            { "es", new CultureInfo("es-ES") },
            { "et", new CultureInfo("et-EE") },
            { "eu", new CultureInfo("eu-ES") },
            { "eo", new CultureInfo("eo-XX") },
            { "fa", new CultureInfo("fa-IR") },
            { "fi", new CultureInfo("fi-FI") },
            { "fj", new CultureInfo("fj-FJ") },
            { "fo", new CultureInfo("fo-FO") },
            { "fr", new CultureInfo("fr-FR") },
            { "fy", new CultureInfo("fy-NL") },
            { "ga", new CultureInfo("ga-IE") },
            { "gd", new CultureInfo("gd-GB") },
            { "gl", new CultureInfo("gl-ES") },
            { "gn", new CultureInfo("gn-PY") },
            { "gu", new CultureInfo("gu-IN") },
            { "ha", new CultureInfo("ha-NG") },
            { "he", new CultureInfo("he-IL") },
            { "hi", new CultureInfo("hi-IN") },
            { "ho", new CultureInfo("ho-PG") },
            { "hr", new CultureInfo("hr-HR") },
            { "ht", new CultureInfo("ht-HT") },
            { "hu", new CultureInfo("hu-HU") },
            { "hy", new CultureInfo("hy-AM") },
            { "id", new CultureInfo("id-ID") },
            { "ig", new CultureInfo("ig-NG") },
            { "ii", new CultureInfo("ii-CN") },
            { "is", new CultureInfo("is-IS") },
            { "it", new CultureInfo("it-IT") },
            { "iu", new CultureInfo("iu-CA") },
            { "ja", new CultureInfo("ja-JP") },
            { "jv", new CultureInfo("jv-ID") },
            { "ka", new CultureInfo("ka-GE") },
            { "kk", new CultureInfo("kk-KZ") },
            { "kl", new CultureInfo("kl-GL") },
            { "km", new CultureInfo("km-KH") },
            { "kn", new CultureInfo("kn-IN") },
            { "ko", new CultureInfo("ko-KR") },
            { "ks", new CultureInfo("ks-IN") },
            { "ku", new CultureInfo("ku-IQ") },
            { "ky", new CultureInfo("ky-KG") },
            { "la", new CultureInfo("la-VA") },
            { "lb", new CultureInfo("lb-LU") },
            { "ln", new CultureInfo("ln-CD") },
            { "lo", new CultureInfo("lo-LA") },
            { "lt", new CultureInfo("lt-LT") },
            { "lv", new CultureInfo("lv-LV") },
            { "mg", new CultureInfo("mg-MG") },
            { "mh", new CultureInfo("mh-MH") },
            { "mi", new CultureInfo("mi-NZ") },
            { "mk", new CultureInfo("mk-MK") },
            { "ml", new CultureInfo("ml-IN") },
            { "mn", new CultureInfo("mn-MN") },
            { "mr", new CultureInfo("mr-IN") },
            { "ms", new CultureInfo("ms-MY") },
            { "mt", new CultureInfo("mt-MT") },
            { "my", new CultureInfo("my-MM") },
            { "na", new CultureInfo("na-NR") },
            { "nb", new CultureInfo("nb-NO") },
            { "ne", new CultureInfo("ne-NP") },
            { "nl", new CultureInfo("nl-NL") },
            { "nn", new CultureInfo("nn-NO") },
            { "nr", new CultureInfo("nr-ZA") },
            { "ny", new CultureInfo("ny-MW") },
            { "om", new CultureInfo("om-ET") },
            { "or", new CultureInfo("or-IN") },
            { "os", new CultureInfo("os-GE") },
            { "pa", new CultureInfo("pa-IN") },
            { "pl", new CultureInfo("pl-PL") },
            { "ps", new CultureInfo("ps-AF") },
            { "pt", new CultureInfo("pt-BR") },
            { "qu", new CultureInfo("qu-PE") },
            { "rm", new CultureInfo("rm-CH") },
            { "rn", new CultureInfo("rn-BI") },
            { "ro", new CultureInfo("ro-RO") },
            { "ru", new CultureInfo("ru-RU") },
            { "rw", new CultureInfo("rw-RW") },
            { "sa", new CultureInfo("sa-IN") },
            { "sd", new CultureInfo("sd-IN") },
            { "se", new CultureInfo("se-NO") },
            { "sg", new CultureInfo("sg-CF") },
            { "si", new CultureInfo("si-LK") },
            { "sk", new CultureInfo("sk-SK") },
            { "sl", new CultureInfo("sl-SI") },
            { "sm", new CultureInfo("sm-WS") },
            { "sn", new CultureInfo("sn-ZW") },
            { "so", new CultureInfo("so-SO") },
            { "sq", new CultureInfo("sq-AL") },
            { "sr", new CultureInfo("sr-RS") },
            { "ss", new CultureInfo("ss-ZA") },
            { "st", new CultureInfo("st-ZA") },
            { "su", new CultureInfo("su-ID") },
            { "sv", new CultureInfo("sv-SE") },
            { "sw", new CultureInfo("sw-TZ") },
            { "ta", new CultureInfo("ta-IN") },
            { "te", new CultureInfo("te-IN") },
            { "tg", new CultureInfo("tg-TJ") },
            { "th", new CultureInfo("th-TH") },
            { "ti", new CultureInfo("ti-ET") },
            { "tk", new CultureInfo("tk-TM") },
            { "tl", new CultureInfo("tl-PH") },
            { "tn", new CultureInfo("tn-ZA") },
            { "to", new CultureInfo("to-TO") },
            { "tr", new CultureInfo("tr-TR") },
            { "ts", new CultureInfo("ts-ZA") },
            { "tt", new CultureInfo("tt-RU") },
            { "tw", new CultureInfo("tw-GH") },
            { "ty", new CultureInfo("ty-PF") },
            { "ug", new CultureInfo("ug-CN") },
            { "uk", new CultureInfo("uk-UA") },
            { "ur", new CultureInfo("ur-PK") },
            { "uz", new CultureInfo("uz-UZ") },
            { "ve", new CultureInfo("ve-ZA") },
            { "vi", new CultureInfo("vi-VN") },
            { "wo", new CultureInfo("wo-SN") },
            { "xh", new CultureInfo("xh-ZA") },
            { "yo", new CultureInfo("yo-NG") },
            { "za", new CultureInfo("za-CN") },
            { "zh", new CultureInfo("zh-CN") },
            { "zu", new CultureInfo("zu-ZA") }
        };
        
        private static LocalCulture _fallbackCulture = new LocalCulture(
            () => CultureInfo.CurrentCulture
        );
        
        public static CultureInfo CurrentCulture
        {
            get { return CultureInfo.CurrentCulture; }
            set { CultureInfo.CurrentCulture = value; }
        }
        
        public static CultureInfo FallbackCulture
        {
            get { return _fallbackCulture.Value; }
            set { _fallbackCulture.Value = value; }
        }
    }
    
    /// <summary>
    /// Specialized dictionary that handles values for different
    /// cultures.
    /// </summary>
    public class Translatable<T> : Dictionary<CultureInfo, T>
    {
        public string CultureRegex => @"^[a-z]{2}-[A-Z]{2}$";
        
        public static implicit operator T(Translatable<T> translatable)
        {
            return translatable.Value;
        }
        
        public T this[string key]
        {
            get
            {
                if (Regex.IsMatch(key, CultureRegex))
                {
                    T value;
                    if (TryGetValue(new CultureInfo(key), out value))
                        return value;
                    key = key.Split('-')[0];
                }
                CultureInfo keyCulture = null;
                return Translatable.LanguageDefaults.TryGetValue(key, out keyCulture)
                    ? this[keyCulture]
                    : this[new CultureInfo(key)];
            }
            set
            {
                if (!Regex.IsMatch(key, CultureRegex))
                    throw new InvalidOperationException($"unrecognized Culture \"{key}\"");
                this[new CultureInfo(key)] = value;
            }
        }
        
        public T Value
        {
            get
            {
                if (ContainsKey(Translatable.CurrentCulture))
                    return this[Translatable.CurrentCulture];
                T value = default(T);
                TryGetValue(Translatable.FallbackCulture, out value);
                return value;
            }
            set { this[Translatable.CurrentCulture] = value; }
        }
    }
}
