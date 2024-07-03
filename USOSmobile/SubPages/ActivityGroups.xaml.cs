using USOSmobile.Models;
using System.Collections.ObjectModel;

namespace USOSmobile.SubPages
{
    public partial class ActivityGroupsPage : ContentPage
    {
        public ObservableCollection<CourseViewModel> Courses { get; set; }

        public ActivityGroupsPage()
        {
            InitializeComponent();
            Courses = new ObservableCollection<CourseViewModel>();
            cv.ItemsSource = Courses;
            Task.Run(() =>
            {
                loadData();
            });
        }


        private async void DataButtonClicked(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            Courses.Clear();
            foreach (var item in ModelObjects.userCourses.Values)
            {
                foreach (var course in item.termCourses)
                {
                    var courseViewModel = new CourseViewModel
                    {
                        CourseId = course.course_id,
                        CourseNamePL = course.course_name.pl,
                        TermId = course.term_id,
                        UserGroups = new ObservableCollection<UserGroupViewModel>()
                    };

                    foreach (var group in course.user_groups)
                    {
                        var groupViewModel = new UserGroupViewModel
                        {
                            GroupNumber = group.group_number.ToString(),
                            ClassTypePL = group.class_type.pl,
                            ClassTypeId = group.class_type_id,
                            GroupUrl = group.group_url,
                            CourseNamePL = group.course_name.pl,
                            CourseHomepageUrl = group.course_homepage_url,
                            CourseProfileUrl = group.course_profile_url,
                            CourseIsCurrentlyConducted = group.course_is_currently_conducted.ToString(),
                            CourseFacId = group.course_fac_id,
                            CourseLangId = group.course_lang_id,
                            TermId = group.term_id,
                            Lecturers = new ObservableCollection<UserViewModel>(),
                            Participants = new ObservableCollection<UserViewModel>()
                        };

                        foreach (var lecturer in group.lecturers)
                        {
                            groupViewModel.Lecturers.Add(new UserViewModel
                            {
                                FullName = $"{lecturer.first_name} {lecturer.last_name}"
                            });
                        }

                        foreach (var participant in group.participants)
                        {
                            groupViewModel.Participants.Add(new UserViewModel
                            {
                                FullName = $"{participant.first_name} {participant.last_name}"
                            });
                        }

                        courseViewModel.UserGroups.Add(groupViewModel);
                    }

                    Courses.Add(courseViewModel);
                }
            }
        }
    }

    public class CourseViewModel
    {
        public string CourseId { get; set; }
        public string CourseNamePL { get; set; }
        public string TermId { get; set; }
        public ObservableCollection<UserGroupViewModel> UserGroups { get; set; }
    }

    public class UserGroupViewModel
    {
        public string GroupNumber { get; set; }
        public string ClassTypePL { get; set; }
        public string ClassTypeId { get; set; }
        public string GroupUrl { get; set; }
        public string CourseNamePL { get; set; }
        public string CourseHomepageUrl { get; set; }
        public string CourseProfileUrl { get; set; }
        public string CourseIsCurrentlyConducted { get; set; }
        public string CourseFacId { get; set; }
        public string CourseLangId { get; set; }
        public string TermId { get; set; }
        public ObservableCollection<UserViewModel> Lecturers { get; set; }
        public ObservableCollection<UserViewModel> Participants { get; set; }
    }

    public class UserViewModel
    {
        public string FullName { get; set; }
    }
}