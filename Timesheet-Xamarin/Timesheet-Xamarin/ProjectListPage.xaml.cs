﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet_Library.Dto.Project;
using Timesheet_Library.Dto.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timesheet_Xamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectListPage : ContentPage
	{
        private ProjectServices projectServices = new ProjectServices();
        private UserServices userServices = new UserServices();
        private CompanyServices companyServices = new CompanyServices();

        private string idUser = Application.Current.Properties["IdUser"].ToString();
        private string idCompany = Application.Current.Properties["IdCompany"].ToString();

        public ObservableCollection<string> ProjectCollection = null;

        //Projecten met Key
        Dictionary<int, string> projectsWithKey = new Dictionary<int, string>();

        public ProjectListPage ()
		{
            InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            //Haalt alle projecten op
            List<ProjectDto> projects = await companyServices.GetAllCompanyProjectsAsync(int.Parse(idCompany));

            //Steekt alle projecten in ProjectList (Picker)
            AddProjectsToProjectList(ProjectCollection, projects);
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            string projectToEdit = ProjectList.SelectedItem.ToString();
            int projid = 0;

            //Zoekt id van het project
            foreach (var p in projectsWithKey)
            {
                if (p.Value == projectToEdit)
                {
                    projid = p.Key;
                }
            }
            Application.Current.Properties["projectid"] = projid;
            ProjectList.SelectedItem = null;
            await Navigation.PushModalAsync(new EditProjectPage());
        }

        //Projecten toevoegen aan ProjectList (ListView)
        private void AddProjectsToProjectList(ObservableCollection<string> ProjectCollection, List<ProjectDto> projectDto = null)
        {
            ProjectCollection = new ObservableCollection<string>();
            //Projecten(naam en id) in Dictionary steken
            foreach (var project in projectDto)
            {
                projectsWithKey.Add(project.ID, project.Name);
            }

            //Dictionary in ProjectList(ListView) steken
            foreach (var project in projectsWithKey)
            {
                ProjectCollection.Add(project.Value.ToString());
            }

            //Dictionary in ProjectList(ListView) steken
            ProjectList.ItemsSource = ProjectCollection;
        }

        private void AddProjectButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AddProjectPage();
        }

        private void BackToOptionpageButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CompanyOptionPage();
        }
    }
}