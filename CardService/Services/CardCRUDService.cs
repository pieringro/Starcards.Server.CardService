using System.Collections.Generic;
using System.Linq;
using CardService.Models;
using Microsoft.Extensions.Configuration;
using SimpleAMQPWrapper;
using SimpleMongoDBWrapper;

namespace CardService.Services {
    public class CardCRUDService : BaseService {

        public CardCRUDService(IConfiguration config) : base(config) {

        }

        public Card Create(Card card) {
            var cardRepository = new Repository<Card>();
            var insertTask = cardRepository.InsertOne(card);
            var cardResult = insertTask.Result;

            return cardResult;
        }

        public IList<Card> GetAll() {
            var cardRepository = new Repository<Card>();
            var getAllTask = cardRepository.GetAll();
            var cards = getAllTask.Result;
            return cards;
        }

        public IList<Card> GetPage(int page) {
            var cardRepository = new Repository<Card>();
            var getAllTask = cardRepository.GetPage(page);
            var cards = getAllTask.Result;
            return cards;
        }

        public Card Get(string id) {
            var cardRepository = new Repository<Card>();
            var getAllTask = cardRepository.GetById(id);
            var cards = getAllTask.Result;
            return cards;
        }

        public void Update(string id, Card cardIn) {
            var cardRepository = new Repository<Card>();
            cardRepository.UpdateOne(id, cardIn).Wait();

            Factory.Sender.publishStructureMessage("update", cardIn);
        }

        public void Delete(Card card) {
            var cardRepository = new Repository<Card>();
            cardRepository.DeleteOne(card);

            Factory.Sender.publishStructureMessage("delete", card);
        }

        public void Delete(string id) {
            var cardRepository = new Repository<Card>();
            cardRepository.DeleteOne(id);

            Factory.Sender.publishStructureMessage("delete", new Card() {
                Id = id
            });
        }

    }
}