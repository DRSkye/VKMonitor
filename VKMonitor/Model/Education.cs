namespace VKMonitor.Model
{
    public class Education
    {
        public long? UniversityId { get; set; }

        public string UniversityName { get; set; }

        public long? FacultyId { get; set; }

        public string FacultyName { get; set; }

        public int? Graduation { get; set; }

        public string EducationForm { get; set; }

        public string EducationStatus { get; set; }

        public Education(VkNet.Model.Education education)
        {
            UniversityId = education.UniversityId;
            UniversityName = education.UniversityName;

            FacultyId = education.FacultyId;
            FacultyName = education.FacultyName;

            EducationForm = education.EducationForm;
            EducationStatus = education.EducationStatus;

            Graduation = education.Graduation;

        }
    }
}
