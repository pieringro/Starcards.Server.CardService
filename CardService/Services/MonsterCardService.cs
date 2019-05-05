using System.Collections.Generic;
using System.Linq;
using CardService.Models;
using Microsoft.Extensions.Configuration;
using SimpleAMQPWrapper;
using SimpleMongoDBWrapper;

namespace CardService.Services {
    public class MonsterCardService : BaseService {

        public MonsterCardService(IConfiguration config) : base(config) {

        }

        #region Data Read
        public IList<MonsterCard> GetAll() {
            var cardRepository = new Repository<MonsterCard>();
            var getAllTask = cardRepository.GetAll();
            var cards = getAllTask.Result;
            return cards;
        }

        public IList<MonsterCard> GetPage(int page) {
            var cardRepository = new Repository<MonsterCard>();
            var getAllTask = cardRepository.GetPage(page);
            var cards = getAllTask.Result;
            return cards;
        }

        public MonsterCard Get(string id) {
            var cardRepository = new Repository<MonsterCard>();
            var getAllTask = cardRepository.GetById(id);
            var cards = getAllTask.Result;
            return cards;
        }
        #endregion

        #region Data Modification
        public MonsterCard Create(MonsterCard card) {
            var cardRepository = new Repository<MonsterCard>();
            var insertTask = cardRepository.InsertOne(card);
            var cardResult = insertTask.Result;

            Factory.Sender.publishStructureMessage("create", card);
            return cardResult;
        }

        public void Update(string id, MonsterCard cardIn) {
            var cardRepository = new Repository<MonsterCard>();
            cardRepository.UpdateOne(id, cardIn).Wait();

            Factory.Sender.publishStructureMessage("update", cardIn);
        }

        public void Delete(MonsterCard card) {
            var cardRepository = new Repository<MonsterCard>();
            cardRepository.DeleteOne(card).Wait();

            Factory.Sender.publishStructureMessage("delete", card);
        }

        public void Delete(string id) {
            var cardRepository = new Repository<MonsterCard>();
            cardRepository.DeleteOne(id).Wait();

            Factory.Sender.publishStructureMessage("delete", new MonsterCard() {
                Id = id
            });
        }

        #endregion

    }
}