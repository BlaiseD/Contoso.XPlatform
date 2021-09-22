using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Domain.Entities;
using Contoso.Web.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.Bsl.Web.Tests
{
    public class SaveStudentTest
    {
        public SaveStudentTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private IHttpClientFactory clientFactory;
        #endregion Fields

        #region Tests
        [Fact]
        public async void SaveStudent()
        {
            List<Task<SaveStudentResponse>> tasks = new List<Task<SaveStudentResponse>>();
            for (int i = 0; i < 30; i++)
            {
                tasks.Add
                (
                    this.clientFactory.PostAsync<SaveStudentResponse>
                    (
                        "api/Student/Save",
                        JsonSerializer.Serialize
                        (
                            new SaveEntityRequest<StudentModel>
                            {
                                Entity = new StudentModel
                                {
                                    ID = 1,
                                    FirstName = "Carson",
                                    LastName = "Alexander",
                                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                                    Enrollments = new HashSet<EnrollmentModel>
                                    {
                                        new EnrollmentModel
                                        {
                                            EnrollmentID = 1,
                                            CourseID = 1050,
                                            Grade = Contoso.Domain.Entities.Grade.A,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        },
                                        new EnrollmentModel
                                        {
                                            EnrollmentID = 2,
                                            CourseID = 4022,
                                            Grade = Contoso.Domain.Entities.Grade.C,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        },
                                        new EnrollmentModel
                                        {
                                            EnrollmentID = 3,
                                            CourseID = 4041,
                                            Grade = Contoso.Domain.Entities.Grade.B,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        }
                                    }
                                }
                            }
                        ),
                        "http://localhost:55688/"
                    )
                );

                await Task.WhenAll(tasks);

                tasks.ForEach(task => Assert.True(task.Result.Success));
            }
        }

        [Fact]
        public async void SaveStudentWithoutRules()
        {
            List<Task<SaveStudentResponse>> tasks = new List<Task<SaveStudentResponse>>();
            for (int i = 0; i < 30; i++)
            {
                tasks.Add
                (
                    this.clientFactory.PostAsync<SaveStudentResponse>
                    (
                        "api/Student/SaveWithoutRules",
                        JsonSerializer.Serialize
                        (
                            new SaveStudentRequest
                            {
                                Student = new StudentModel
                                {
                                    ID = 1,
                                    FirstName = "Carson",
                                    LastName = "Alexander",
                                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                                    Enrollments = new HashSet<EnrollmentModel>
                                    {
                                        new EnrollmentModel
                                        {
                                            EnrollmentID = 1,
                                            CourseID = 1050,
                                            Grade = Contoso.Domain.Entities.Grade.A,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        },
                                        new EnrollmentModel
                                        {
                                            EnrollmentID = 2,
                                            CourseID = 4022,
                                            Grade = Contoso.Domain.Entities.Grade.C,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        },
                                        new EnrollmentModel
                                        {
                                            EnrollmentID = 3,
                                            CourseID = 4041,
                                            Grade = Contoso.Domain.Entities.Grade.B,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        }
                                    }
                                }
                            }
                        ),
                        "http://localhost:55688/"
                    )
                );

                await Task.WhenAll(tasks);
                tasks.ForEach(task => Assert.True(task.Result.Success));
            }
        }
        #endregion Tests

        private void Initialize()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient();
            serviceProvider = services.BuildServiceProvider();

            this.clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        }
    }
}
