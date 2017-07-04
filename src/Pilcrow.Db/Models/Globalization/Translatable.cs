using System.Collections.Generic;
using System.Globalization;
using System.Threading;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pilcrow.Db.Models.Globalization
{
    using LocalCulture = ThreadLocal<CultureInfo>;
    
    /// <summary>
    /// Specialized dictionary that handles values for different
    /// cultures.
    /// </summary>
    /// <remarks>
    /// Is this sane?
    /// </remarks>
    public class Translatable<T> : Dictionary<CultureInfo, T>
    {
        private static LocalCulture _currentCulture = new LocalCulture(
            () => CultureInfo.CurrentCulture
        );
        
        private static LocalCulture _fallbackCulture = new LocalCulture(
            () => CultureInfo.CurrentCulture
        );
        
        public static CultureInfo CurrentCulture
        {
            get { return _currentCulture.Value; }
            set { _currentCulture.Value = value; }
        }
        
        public static CultureInfo FallbackCulture
        {
            get { return _fallbackCulture.Value; }
            set { _fallbackCulture.Value = value; }
        }
        
        public static implicit operator T(Translatable<T> translatable)
        {
            return translatable.CurrentValue;
        }
        
        public T this[string key]
        {
            get { return this[new CultureInfo(key)]; }
            set { this[new CultureInfo(key)] = value; }
        }
        
        public T CurrentValue
        {
            get
            {
                if (ContainsKey(CurrentCulture))
                    return this[CurrentCulture];
                T value = default(T);
                TryGetValue(FallbackCulture, out value);
                return value;
            }
            set { this[CurrentCulture] = value; }
        }
    }
}
