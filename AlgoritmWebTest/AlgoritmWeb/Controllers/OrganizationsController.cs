using AlgoritmWeb.Services;
using AlgoritmWeb.Services.Interfaces;
using DBlayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AlgoritmWeb.Controllers
{
    public class OrganizationsController : Controller
    {
        HttpClient httpClient;
        private readonly IOrganizationsSevice _organizations;

        public OrganizationsController(IOrganizationsSevice organizations)
        {
            _organizations = organizations;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganizations()
        {
           await BearerToken.GetToken();
            _organizations.InsertOrganization("1f9487e9-f30");
          //  var response = await _organizations.GetOrganization();
            return View();
        }
        async Task GetOrganizatons(string bearerKey)
        {
            var data = @"{""organizationIds"": null,
                              ""returnAdditionalInfo"": true,
                              ""includeDisabled"": false}"
            ;
            var url = "https://api-ru.iiko.services/api/1/organizations";
            try
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenController.BearerToken);
                var response = await httpClient.PostAsync(url, new StringContent(data, encoding: System.Text.Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                Organizations.Organizations_ org = JsonConvert.DeserializeObject<Organizations.Organizations_>(result);
                //if (_organizations.GetByApiToken("1f9487e9-f30").Result == null)
                //{
                //    for (int i = 0; i < org!.organizations!.Count; i++)
                //    {
                //        org.organizations[i].apiToken = "1f9487e9-f30";
                //        _organizations.CreateAsync(org.organizations[i]);
                //    }
                //}
            }
            catch (Exception ex)
            {

            }
        }
    }
}
