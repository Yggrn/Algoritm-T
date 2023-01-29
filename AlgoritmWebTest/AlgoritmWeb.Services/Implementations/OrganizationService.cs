using AlgoritmWeb.DBlayer.Enum;
using AlgoritmWeb.DBlayer.Response;
using AlgoritmWeb.Services.Interfaces;
using DBlayer.Entities;
using DBlayer.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;

namespace AlgoritmWeb.Services.Implementations
{
    public class OrganizationService : IOrganizationsSevice
    {
        private readonly IOrganizationsRepository _organizations;
        private readonly HttpClient httpClient;
        public OrganizationService(IOrganizationsRepository organizations)
        {
            httpClient = new HttpClient();
            _organizations = organizations;
        }

        public async Task<IBaseResponse<IEnumerable<Organizations.OrganizationsArray>>> GetOrganization()
        {
            var baseResponse = new BaseResponse<IEnumerable<Organizations.OrganizationsArray>>();
            try
            {
                var organizations = await _organizations.SelectAsync();
                if (organizations.Count == 0)
                {
                    baseResponse.Description = "No data";
                    baseResponse.StatusCode = StatusCode.NoDataInDB;
                    return baseResponse;
                }
                baseResponse.Data = organizations;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Organizations.OrganizationsArray>>()
                { 
                    Description = $"[GetOrganization] : {ex.Message}" 
                };
            
            }
        }

        public async Task<IBaseResponse<Organizations.OrganizationsArray>> InsertOrganization(string apitoken)
        {
            var data = @"{""organizationIds"": null,
                              ""returnAdditionalInfo"": true,
                              ""includeDisabled"": false}"
            ;
            var url = "https://api-ru.iiko.services/api/1/organizations";
            var baseResponse = new BaseResponse<Organizations.OrganizationsArray>();
            try
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + BearerToken.bearerToken);
                var response = await httpClient.PostAsync(url, new StringContent(data, encoding: System.Text.Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                Organizations.Organizations_ org = JsonConvert.DeserializeObject<Organizations.Organizations_>(result);
                
                if  (_organizations.GetByApiToken(apitoken).Result == null)
                {
                    for (int i = 0; i < org!.organizations!.Count; i++)
                    {
                        org.organizations[i].apiToken = apitoken;
                        await _organizations.CreateAsync(org.organizations[i]);
                    }
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Description = "Data already exist";
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Organizations.OrganizationsArray>()
                {
                    Description = $"[InsertOrganization] : {ex.Message}"
                };
            }
        }
    }
}
