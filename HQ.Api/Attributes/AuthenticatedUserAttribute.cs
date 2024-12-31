using HQ.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HQ.Api.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}