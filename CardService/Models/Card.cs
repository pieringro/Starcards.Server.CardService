using System.Collections.Generic;
using SimpleAMQPWrapper;
using SimpleMongoDBWrapper;

namespace CardService.Models {
    public class Card : BaseCollection, IMessageData {
        public TranslationsModel Names { get; set; } = new TranslationsModel();
    }
}