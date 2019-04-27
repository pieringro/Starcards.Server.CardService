using System.Collections.Generic;
using System.Linq;
using CardService.Models;
using Microsoft.Extensions.Configuration;
using SimpleAMQPWrapper;

namespace CardService.Services {
    public class CardCRUDService : BaseService {

        public CardCRUDService(IConfiguration config) : base(config) {

        }

        public CardCollection Create(CardCollection card) {
            var cardRepository = new Repository<CardCollection>();
            var insertTask = cardRepository.InsertOne(card);
            var cardResult = insertTask.Result;

            return cardResult;
        }

        public IList<CardCollection> GetAll() {
            var cardRepository = new Repository<CardCollection>();
            var getAllTask = cardRepository.GetAll();
            var cards = getAllTask.Result;
            return cards;
        }

        public IList<CardCollection> GetPage(int page) {
            var cardRepository = new Repository<CardCollection>();
            var getAllTask = cardRepository.GetPage(page);
            var cards = getAllTask.Result;
            return cards;
        }

        public CardCollection Get(string id) {
            var cardRepository = new Repository<CardCollection>();
            var getAllTask = cardRepository.GetById(id);
            var cards = getAllTask.Result;
            return cards;
        }

        public void Update(string id, CardCollection cardIn) {
            var cardRepository = new Repository<CardCollection>();
            cardRepository.UpdateOne(id, cardIn).Wait();

            Factory.Sender.publishMessage("hellooooo");
            Factory.GetSenderCustom("CustomQueueeeee").publishMessage("hellooooo");
        }

        public void Remove(CardCollection card) {
            var cardRepository = new Repository<CardCollection>();
            cardRepository.DeleteOne(card);
        }

        public void Remove(string id) {
            var cardRepository = new Repository<CardCollection>();
            cardRepository.DeleteOne(id);
        }

    }
}