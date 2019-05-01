using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using SimpleAMQPWrapper;

namespace CardService.Models {
    public class CardCollection : BaseCollection, IMessageData {
        public TranslationsModel Names { get; set; } = new TranslationsModel();
    }
}