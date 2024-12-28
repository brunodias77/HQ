using HQ.Application.Dtos.Donations.Requests;
using HQ.Domain.Entities;
using HQ.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace HQ.Api.Controllers;

public class DonationController : BaseController
{
    public DonationController(IGenerateQrCodeService generateQrCodeService)
    {
        _generateQrCodeService = generateQrCodeService;
    }

    private readonly IGenerateQrCodeService _generateQrCodeService;

    [HttpPost("pix")]
    public async Task<IActionResult> GenerateQrCodePix(RequestPaymentPix request)
    {
        // Validação básica
        if (string.IsNullOrWhiteSpace(request.Key))
        {
            return BadRequest("A chave PIX é obrigatória.");
        }
        if (string.IsNullOrWhiteSpace(request.MerchantName))
        {
            return BadRequest("O nome do destinatário é obrigatório.");
        }
        if (string.IsNullOrWhiteSpace(request.MerchantCity))
        {
            return BadRequest("A cidade do destinatário é obrigatória.");
        }
        
        var pix = new Pix
        {
            Key = request.Key,
            Value = request.Value,
            MerchantName = request.MerchantName,
            MerchantCity = request.MerchantCity,
            TxId = request.TxId
        };
        string pixCode = pix.GeneratePixCode();
        return File(_generateQrCodeService.GenerateQRCode(pixCode), "image/png");
    }
    
}