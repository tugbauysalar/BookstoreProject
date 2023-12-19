﻿using BookstoreProject.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreProject.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    [NonAction]
    public IActionResult CreateIActionResult<T>(CustomResponseDto<T> response)
    {
        if (response.StatusCode == 204)
        {
            return new ObjectResult(null)
            {
                StatusCode = response.StatusCode
            };
        }

        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}