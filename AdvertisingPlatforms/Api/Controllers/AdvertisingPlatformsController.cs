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
    
    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded");
        }

        string InfoPost = string.Empty;
        string ErrorPostStrings = string.Empty;

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            string content = await reader.ReadToEndAsync();
            var result = _locationStorage.Add(content);
            var correctDataCount = result.Where(str => str.Item1 == true).ToArray().Count();
            InfoPost = $"All: {result.Count}\n" +
                       $"Correct: {correctDataCount}\n" +
                       $"Incorrect: {result.Count - correctDataCount}";
            
            ErrorPostStrings = string.Join('\n', result
                .Where(str => str.Item1 == false)
                .Select(c =>c.Item2)
                .ToArray()
            );
        }
        
        
        return Ok($"{InfoPost}\n{ErrorPostStrings}");
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

    [HttpGet("locationStorage")]
    public IActionResult Get()
    {

        return Ok(_locationStorage.Get());
    }

    // private struct PostData
    // {
    //     public int AllPostData = 0;
    //     public int CorrectPostData = 0;
    //     public int ErrorPostData = 0;
    //     public List<string> ErrorPostDataString = new List<string>();
    //
    //     public PostData()
    //     {
    //         
    //     }
    //     
    // }
    
    
   
}