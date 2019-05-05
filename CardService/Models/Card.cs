using System.Collections.Generic;
using SimpleAMQPWrapper;
using SimpleMongoDBWrapper;

namespace CardService.Models {
    public class Card : BaseCollection, IMessageData {
        public TranslationsModel Name { get; set; } = new TranslationsModel();
        public TranslationsModel Description { get; set; } = new TranslationsModel();
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}