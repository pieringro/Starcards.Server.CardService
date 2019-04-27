using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace CardService.Models {
    public class CardCollection : BaseCollection {
        public TranslationsModel Names { get; set; } = new TranslationsModel();
    }
}