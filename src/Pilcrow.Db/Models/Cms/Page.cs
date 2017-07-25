using System;
using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Pilcrow.Db.Models.Globalization;

namespace Pilcrow.Db.Models.Cms
{
    public class Page : Entity, IAutoMappable, IPublishable, IRevisionable
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        
        public Guid ParentGuid { get; set; }
        
        public bool Live { get; set; }
        
        public int Version { get; set; }
        
        public Translatable<string> Title { get; set; }
        
        public Translatable<string> MetaTitle { get; set; }
        
        public Translatable<string> MetaDescription { get; set; }
        
        public Translatable<string> Slug { get; set; }
        
        public Translatable<List<Element>> Elements { get; set; }
    }
}
