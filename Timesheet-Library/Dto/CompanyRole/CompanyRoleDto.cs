﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet_Library.Dto
{
    class CompanyRoleDto
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public bool ManageCompany { get; set; }
        public bool ManageUsers { get; set; }
        public bool ManageProjects { get; set; }
        public bool ManageProjectRoles { get; set; }
        public bool ManageProjectUsers { get; set; }
    }
}
