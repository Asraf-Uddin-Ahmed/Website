using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.WebApi.Codes.Core.Constant
{
    public static class UriName
    {
        public static class Resource
        {
            public static class Students
            {
                public const string GET_STUDENT = "GetStudentByID";
            }
            public static class Instructors
            {
                public const string GET_INSTRUCTOR = "GetInstructorByID";
            }
            public static class Courses
            {
                public const string GET_COURSE = "GetCourseByID";
            }
            public static class ClassLectures
            {
                public const string GET_CLASS_LECTURE = "GetClassLectureByID";
            }
            public static class Questions
            {
                public const string GET_QUESTION = "GetQuestionByID";
            }
            public static class QuestionGroups
            {
                public const string GET_QUESTION_GROUP = "GetQuestionGroupByID";
            }
            public static class Exams
            {
                public const string GET_EXAM = "GetExamByID";
            }
        }
        
        public static class Identity
        {
            public static class Accounts
            {
                public const string GET_USER = "GetUserById";
                public const string CONFIRM_EMAIL = "ConfirmEmailRoute";
            }
            public static class Roles
            {
                public const string GET_ROLE_BY_USER_ID = "GetRoleByUserID";
                public const string GET_ROLE = "GetRoleById";
            }
            public static class Claims
            {
                public const string GET_CLAIM_BY_USER_ID = "GetClaimByUserID";
            }
            
        }
    }
}