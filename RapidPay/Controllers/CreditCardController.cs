using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Business.Entities;
using RapidPay.Business.Helpers;
using RapidPay.Business.Services;
using RapidPay.View.Entities;

namespace RapidPay.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CreditCardController : ControllerBase
    {
        private readonly DataServiceBase<Card, string> _cardService;
        private readonly ILogger<CreditCardController> _logger;
        private readonly IMapper MapperService;

        public CreditCardController(ILogger<CreditCardController> logger, IMapper mapper, DataServiceBase<Card, string> cardService)
        {
            _cardService = cardService;
            _logger = logger;
            MapperService = mapper;
        }
        [Route("Create")]
        [HttpPost]
        public IActionResult Post(CardView cardView)
        {
            if (cardView is null)
            {
                throw new ArgumentNullException(nameof(cardView));
            }

            try
            {
                var card = MapperService.Map<Card>(cardView);

                if (!_cardService.Validate(card, DataAction.Create))
                {
                    return BadRequest(
                                new Response
                                {
                                    Code = ErrorCodes.InvalidObject,
                                    Message = Literals.Invalid
                                });

                }
                var result = _cardService.Create(card);

                if (!result)
                {
                    var retrievedCard = _cardService.GetById(card.Number);
                    if (retrievedCard != null)
                        return BadRequest(
                            new Response
                            {
                                Code = ErrorCodes.NotFound,
                                Message = Literals.NotFound
                            });
                }

                return Ok(
                            new Response
                            {
                                Code = ErrorCodes.Ok,
                                Message = Literals.Ok
                            });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                            new Response
                            {
                                Code = ErrorCodes.SystemError,
                                Message = Literals.SystemError
                            });
            }


        }

        [Route("Get")]
        [HttpGet]
        public IActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }
            try
            {
                var card = _cardService.GetById(id);

                return Ok(card);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(
                          new Response
                          {
                              Code = ErrorCodes.InvalidObject,
                              Message = Literals.InvalidKey
                          });
            }
            catch (KeyNotFoundException notFoundEx)
            {
                return NotFound(
                    new Response
                    {
                        Code = ErrorCodes.NotFound,
                        Message = Literals.NotFound
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                            new Response
                            {
                                Code = ErrorCodes.SystemError,
                                Message = Literals.SystemError
                            });
            }

        }

        [Route("List")]
        [HttpGet]
        public IActionResult GetList()
        {
            var list = _cardService.GetAll();
            return Ok(list);
        }

        [Route("Update")]
        [HttpPost]
        public IActionResult Update(CardView cardView)
        {
            if (cardView is null)
            {
                throw new ArgumentNullException(nameof(cardView));
            }

            try
            {
                var card = MapperService.Map<Card>(cardView);

                if (!_cardService.Validate(card, DataAction.Create))
                {
                    return BadRequest(
                                new Response
                                {
                                    Code = ErrorCodes.InvalidObject,
                                    Message = Literals.Invalid
                                });

                }
                _cardService.Update(card.Number, card);

                return Ok(
                             new Response
                             {
                                 Code = ErrorCodes.Ok,
                                 Message = Literals.Ok
                             });
            }
            catch (KeyNotFoundException notFoundEx)
            {
                return NotFound(
                    new Response
                    {
                        Code = ErrorCodes.NotFound,
                        Message = Literals.NotFound
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                            new Response
                            {
                                Code = ErrorCodes.SystemError,
                                Message = Literals.SystemError
                            });
            }


        }

        [Route("Delete")]
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }
            try
            {
                _cardService.Delete(id);

                return Ok(
                             new Response
                             {
                                 Code = ErrorCodes.Ok,
                                 Message = Literals.Ok
                             });
            }
             catch (KeyNotFoundException notFoundEx)
            {
                return NotFound(
                    new Response
                    {
                        Code = ErrorCodes.NotFound,
                        Message = Literals.NotFound
                    }
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(
                          new Response
                          {
                              Code = ErrorCodes.InvalidObject,
                              Message = Literals.InvalidKey
                          });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                            new Response
                            {
                                Code = ErrorCodes.SystemError,
                                Message = Literals.SystemError
                            });
            }

        }
    }
}