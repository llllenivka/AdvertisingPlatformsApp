using System.Net;
using Api.Core.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdvertisingPlatformsController : ControllerBase
{
    private readonly LocationStorage _locationStorage;
    
    public AdvertisingPlatformsController(LocationStorage locationStorage)
    {
        _locationStorage = locationStorage;
    }

    [HttpGet("locationStorage")]
    public IActionResult Get()
    {

        return Ok(_locationStorage.Get());
    }
    
    [HttpGet("locationStorage/{*location}")]
    public IActionResult Get(string location)
    { 
        var decodedLocation = WebUtility.UrlDecode(location);
        var result = _locationStorage.Get(decodedLocation).Item2;
       
        if (result == null) 
           return BadRequest("Location not found");
       
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            
            return BadRequest("No file uploaded");
        }
        
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            string content = await reader.ReadToEndAsync();
            _locationStorage.Add(content);
           
        }
        return Ok();
    }
}