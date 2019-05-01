using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CardService.Models;
using CardService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardService.Controllers {
    [Route("/api/[controller]")]
    public class CardsController : Controller {

        private readonly CardCRUDService cardService;

        public CardsController(CardCRUDService cardService) {
            this.cardService = cardService;
        }

        [HttpGet("")]
        public ActionResult<IList<Card>> Get(string page = null) {
            IList<Card> cardResult = null;
            if (page != null) {
                int pageNumber = 1;
                try {
                    pageNumber = int.Parse(page);
                } catch (Exception e) {
                    Console.WriteLine(e);
                }
                cardResult = cardService.GetPage(pageNumber);
            } else {
                cardResult = cardService.GetAll();
            }
            return Ok(cardResult.ToList());
        }

        [HttpGet("id/{id}")]
        public ActionResult<Card> GetOne(string id) {
            try {
                var card = cardService.Get(id);
                if (card == null) {
                    return NotFound();
                }
                return card;
            } catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        #region Create
        [HttpPost("create")]
        public ActionResult<Card> Create([FromBody] Card card) {
            var cardResult = cardService.Create(card);
            return Ok(cardResult);
        }
        #endregion

        #region Update
        [HttpPut("id/{id}")]
        public IActionResult Edit(string id, [FromBody] Card card) {
            try {
                cardService.Update(id, card);
            } catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("id/{id}")]
        public IActionResult Delete(string id) {
            try {
                cardService.Remove(id);
            } catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
            return Ok();
        }
        #endregion
    }
}