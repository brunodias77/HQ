using HQ.Application.Abstractions;
using HQ.Application.Dtos.Donations.Requests;
using HQ.Application.Dtos.Donations.Responses;
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
    public IActionResult GenerateQrCodePix([FromServices] IUseCase<RequestGeneratePixDonation, ResponseGeneratePixDonation> useCase, [FromBody] RequestGeneratePixDonation request)
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
        var result = useCase.Execute(request);
        return File(result.Result.QrCodeBase64, "image/png");
    }
    
}