using System;
using System.Collections.Generic;
using System.Linq;
using TemplateManager.Data.GuildWars;
using TemplateManager.Infrastructure.Model;
using TemplateManager.Infrastructure.Services;

namespace TemplateManager.Modules.Services
{
    internal class DataService : IDataService
    {
        private readonly Model data;
        private ISkill invalidSkill;

        public DataService()
        {
            data = Model.Load();
        }

        #region IDataService Members

        public IEnumerable<IProfession> Professions 
        {
            get
            {
                return from item in data.Professions
                       select item as IProfession;
            }
        }

        public ISkill InvalidSkill
        {
            get
            {
                if (invalidSkill == null)
                    invalidSkill = Skills.First(i => i.TemplateId == -1);

                return invalidSkill;
            }
        }

        public IEnumerable<IProfession> PrimaryProfessions
        {
            get
            {
                return from item in Professions
                       where item.IsValidPrimary
                       select item;
            }
        }

        public IEnumerable<IProfession> SecondaryProfessions
        {
            get
            {
                return from item in Professions
                       select item;
                       
            }
        }

        public IProfession EmptyProfession
        {
            get { return data.Professions.First(i => i.TemplateId == 0); }
        }

        public IEnumerable<ISkill> Skills
        {
            get
            {
                return from item in data.Skills
                       select item as ISkill;
            }
        }

        public IEnumerable<IAttribute> Attributes 
        { 
            get
            {
                return from item in data.Attributes
                       select item as IAttribute;
            }
        }

        #endregion
    }
}