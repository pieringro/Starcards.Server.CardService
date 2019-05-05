using System.Collections.Generic;
using SimpleAMQPWrapper;
using SimpleMongoDBWrapper;

namespace CardService.Models {
    public class MonsterCard : Card {
        public int Attack { get; set; }
        public int Defence { get; set; }
    }
}