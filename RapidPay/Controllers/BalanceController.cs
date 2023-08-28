using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Business.Helpers;
using RapidPay.Business.Services;
using RapidPay.View.Entities;

namespace RapidPay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BalanceController : ControllerBase
    {
        private readonly DataServiceBase<BalanceView, Guid> balanceService;
        private readonly ILogger<BalanceController> _logger;
        private readonly IMapper _mapper;

        public BalanceController(ILogger<BalanceController> logger, IMapper mapper, DataServiceBase<BalanceView, Guid> cardService)
        {
            balanceService = cardService;
            _logger = logger;
            _mapper = mapper;
        }
        [Route("Add")]
        [HttpPost]
        public IActionResult Post(BalanceDetailView balanceDetail)
        {
            if (balanceDetail is null)
            {
                throw new ArgumentNullException(nameof(balanceDetail));
            }

            try
            {

                if (!balanceService.Validate(balanceDetail, DataAction.Create))
                {
                    return BadRequest(
                                new Response
                                {
                                    Code = ErrorCodes.InvalidObject,
                                    Message = Literals.Invalid
                                });

                }
                var result = balanceService.Set(balanceDetail);

                if (!result)
                {
                    var retrievedCard = balanceService.GetById(balanceDetail.Number);
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
                var card = balanceService.GetById(id);

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
            var list = balanceService.GetAll();
            return Ok(list);
        }

        [Route("Update")]
        [HttpPost]
        public IActionResult Update(BalanceView card)
        {
            if (card is null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            try
            {

                if (!balanceService.Validate(card, DataAction.Create))
                {
                    return BadRequest(
                                new Response
                                {
                                    Code = ErrorCodes.InvalidObject,
                                    Message = Literals.Invalid
                                });

                }
                balanceService.Update(card.Number, card);

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
                balanceService.Delete(id);

                return Ok(
                             new Response
                             {
                                 Code = ErrorCodes.Ok,
                                 Message = Literals.Ok
                             });
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