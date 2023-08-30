using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Business.Helpers;
using RapidPay.Business.Services;
using RapidPay.View.Entities;

namespace RapidPay.Controllers
{
    public class CardHolderController : Controller
    {
        private DataServiceBase<CardHolderView, string> _cardHolderService;
        private ILogger<CreditCardController> _logger;
        private IMapper _mapper;

        public CardHolderController(ILogger<CreditCardController> logger, IMapper mapper, DataServiceBase<CardHolderView, string> cardHolderService)
        {
            _cardHolderService = _cardHolderService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost(Name = "Create")]
        public IActionResult Post(CardHolderView holder)
        {
            if (holder is null)
            {
                return BadRequest(
                        new Response
                        {
                            Code = ErrorCodes.InvalidObject,
                            Message = Literals.Invalid
                        });
            }

            try
            {

                if (!_cardHolderService.Validate(holder, DataAction.Create))
                {
                    return BadRequest(
                                new Response
                                {
                                    Code = ErrorCodes.InvalidObject,
                                    Message = Literals.Invalid
                                });

                }
                var result = _cardHolderService.Create(holder);

                if (!result)
                {
                    var retrievedCard = _cardHolderService.GetById(holder.IdNumber);
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
                //Log Exception properly

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
