using System;
using Lykke.Service.Dash.Sign.Core.Services;
using Lykke.Service.Dash.Sign.Models;
using Lykke.Service.Dash.Sign.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lykke.Service.Dash.Sign.Controllers
{
    [Route("api/sign")]
    public class SignController : Controller
    {
        private readonly IDashService _dashService;

        public SignController(IDashService dashService)
        {
            _dashService = dashService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SignResponse), (int)HttpStatusCode.OK)]
        public IActionResult SignTransaction([FromBody]SignTransactionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToErrorResponse());
            }

            string hex;

            try
            {
                hex = _dashService.SignTransaction(request.Tx, request.Coins, request.Keys);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(request.Tx), "Error during transaction signing.");

                return BadRequest(ModelState.ToErrorResponse());
            }

            return Ok(new SignResponse()
            {
                SignedTransaction = hex
            });
        }        
    }
}
