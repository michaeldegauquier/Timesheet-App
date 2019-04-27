﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet_Library.Dto.Services
{
    public class CompanyRoleServices
    {
        private static HttpClient client = null;

        public static void GetClient()
        {
            if (client == null)
            {
                client = new HttpClient();

                //Set up client
                client.BaseAddress = new Uri("https://timesheetappapi20190303094246.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMiIsImVtYWlsIjoibWljaGFlbEBnbWFpbC5jb20iLCJuYmYiOjE1NTQyMjU1ODAsImV4cCI6MTU1NDMxMTk4MCwiaWF0IjoxNTU0MjI1NTgwfQ.Qe8R3CvFyQkZvu-bLdOT4yrrYjSEGK5aeVvmqMIPkpH-qlYGx2cBrcsoIJ4TL-KjrEuudWCIgOWDrD2364cq_w");
            }
        }

        public static async Task<List<CompanyRoleDto>> GetAllCompanyRolesAsync(int id)
        {
            GetClient();
            string getAllCompanyRoles = null;
            List<CompanyRoleDto> CompanyRoleList = null;

            HttpResponseMessage response = await client.GetAsync($"api/Companies/{id}/Roles");
            if (response.IsSuccessStatusCode)
            {
                getAllCompanyRoles = await response.Content.ReadAsStringAsync();
                CompanyRoleList = JsonConvert.DeserializeObject<List<CompanyRoleDto>>(getAllCompanyRoles);
            }
            return CompanyRoleList;
        }

        public static async Task<CompanyRoleDto> GetCompanyRoleByIdAsync(int id, int roleId)
        {
            GetClient();
            CompanyRoleDto getCompanyRole = null;
            HttpResponseMessage response = await client.GetAsync($"api/Companies/{id}/Roles/{roleId}");
            if (response.IsSuccessStatusCode)
            {
                getCompanyRole = await response.Content.ReadAsAsync<CompanyRoleDto>();
            }
            return getCompanyRole;
        }

        public static async Task<CompanyRoleDto> CreateCompanyRoleAsync(CompanyRoleToCreateDto companyRole, int id)
        {
            GetClient();
            CompanyRoleDto createdCompanyRole = null;
            HttpResponseMessage response = await client.PostAsJsonAsync($"api/Companies/{id}/Roles", companyRole);
            if (response.IsSuccessStatusCode)
            {
                createdCompanyRole = await response.Content.ReadAsAsync<CompanyRoleDto>();
            }
            return createdCompanyRole;
        }

        public static async Task<CompanyRoleDto> UpdateCompanyRoleByIdAsync(CompanyRoleToUpdateDto companyRole, int id, int roleId)
        {
            GetClient();
            CompanyRoleDto updatedCompanyRole = null;
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Companies/{id}/Roles/{roleId}", companyRole);
            if (response.IsSuccessStatusCode)
            {
                updatedCompanyRole = await response.Content.ReadAsAsync<CompanyRoleDto>();
            }
            return updatedCompanyRole;
        }

        public static async Task<CompanyRoleDto> DeleteCompanyRoleByIdAsync(int id, int roleId)
        {
            GetClient();
            CompanyRoleDto deletedCompanyRole = null;
            HttpResponseMessage response = await client.DeleteAsync($"api/Companies/{id}/Roles/{roleId}");
            if (response.IsSuccessStatusCode)
            {
                deletedCompanyRole = await response.Content.ReadAsAsync<CompanyRoleDto>();
            }
            return deletedCompanyRole;
        }
    }
}