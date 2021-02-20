﻿using AutoMapper;
using Contoso.Bsl.Flow.Cache;
using Contoso.Repositories;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Contoso.Bsl.Flow
{
    public class FlowManager : IFlowManager
    {
        public FlowManager(IMapper mapper,
            ICustomActions customActions,
            DirectorFactory directorFactory,
            FlowActivityFactory flowActivityFactory,
            ISchoolRepository SchoolRepository,
            ILogger<FlowManager> logger)
        {
            this.CustomActions = customActions;
            this.logger = logger;
            this.SchoolRepository = SchoolRepository;
            this.Mapper = mapper;
            this.Director = directorFactory.Create(this);
            this.FlowActivity = flowActivityFactory.Create(this);
        }

        public IFlowActivity FlowActivity { get; }
        public FlowDataCache FlowDataCache { get; } = new FlowDataCache();
        public Progress Progress { get; } = new Progress();
        public ICustomActions CustomActions { get; }

        private ILogger<FlowManager> logger;

        public ISchoolRepository SchoolRepository { get; }
        public IMapper Mapper { get; }
        public DirectorBase Director { get; }

        public void FlowComplete()
        {
            if (FlowDataCache.Response == null)
            {
                logger.LogError("Response cannot be null.");
                throw new InvalidOperationException("Response cannot be null.");
            }
        }

        public void SetCurrentBusinessBackupData() {}

        public void Terminate() => throw new NotImplementedException();

        public void Wait() => throw new NotImplementedException();

        public void Start(string module)
        {
            try
            {
                this.Director.StartInitialFlow(module);
            }
            catch (Exception ex)
            {
                logger.LogWarning(0, string.Format("Progress Start {0}", Newtonsoft.Json.JsonConvert.SerializeObject(this.Progress)));
                this.logger.LogError(ex, ex.Message);
            }
        }
    }
}