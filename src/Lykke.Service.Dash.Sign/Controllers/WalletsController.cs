using Lykke.Service.Dash.Sign.Core.Services;
using Lykke.Service.Dash.Sign.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.Dash.Sign.Controllers
{
    [Route("api/wallets")]
    public class WalletsController : Controller
    {
        private readonly IDashService _dashService;

        public WalletsController(IDashService dashService)
        {
            _dashService = dashService;
        }

        [HttpPost]
        public WalletResponse Post()
        {
            var privateKey = _dashService.GetPrivateKey();
            var publicAddress = _dashService.GetPublicAddress(privateKey);

            return new WalletResponse()
            {
                PrivateKey = privateKey,
                PublicAddress = publicAddress
            };
        }
    }
}
