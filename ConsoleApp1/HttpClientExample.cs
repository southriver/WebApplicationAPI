// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WebApplication1.Model.Dto;

Console.WriteLine("HTTP Client to consume a rest service");
String baseAddress = "http://localhost:24300";
using (var client = new HttpClient())
{
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    var query = new CourseDto
    {
        CourseCode = "1010",
        CourseDesc = "Test",
        DeptCode = "CE"
    };
    JsonContent content = JsonContent.Create(query);
    try
    {
        var response = client.PostAsync(baseAddress + "/Course", content).Result;
        //var token = tokenResponse.Content.ReadAsStringAsync().Result;
        var result = response.Content.ReadAsStringAsync().Result;
        //Logger(result);
    }
    catch (Exception ex)
    {
        ex.ToString();
    }

}