using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using TemplateManager.Services;
using Attribute=TemplateManager.Services.Attribute;

namespace TemplateManager
{
    internal class DataFetcher
    {
        private readonly bool useCache;
        private IEnumerable<Attribute> attributes;
        private IEnumerable<Campaign> campaigns;
        private IDictionary<string, BitmapImage> professionImages;
        private IEnumerable<Profession> professions;
        private TemplateManagerSkillDataService service;
        private IDictionary<string, BitmapImage> skillImages;
        private IEnumerable<Skill> skills;
        private IEnumerable<SkillType> skillTypes;

        public DataFetcher(bool useCache)
        {
            this.useCache = useCache;
        }

        public IDictionary<string, BitmapImage> ProfessionImages
        {
            get
            {
                if(professionImages == null)
                    professionImages = GetProfessionImages();
                return professionImages;
            }
        }

        public IDictionary<string, BitmapImage> SkillImages
        {
            get
            {
                if(skillImages == null)
                    skillImages = GetSkillImages();
                return skillImages;
            }
        }

        public IEnumerable<Campaign> Campaigns
        {
            get
            {
                if(campaigns == null)
                    campaigns = GetCampaigns();
                return campaigns;
            }
        }

        public IEnumerable<SkillType> SkillTypes
        {
            get
            {
                if(skillTypes == null)
                    skillTypes = GetSkillTypes();
                return skillTypes;
            }
        }

        public IEnumerable<Profession> Professions
        {
            get
            {
                if(professions == null)
                    professions = GetProfessions();
                return professions;
            }
        }

        private TemplateManagerSkillDataService Service
        {
            get
            {
                if(service == null)
                    service = new TemplateManagerSkillDataService();
                return service;
            }
        }

        public IEnumerable<Skill> Skills
        {
            get
            {
                if(skills == null)
                    skills = GetSkills();
                return skills;
            }
        }

        public IEnumerable<Attribute> Attributes
        {
            get
            {
                if(attributes == null)
                    attributes = GetAttributes();
                return attributes;
            }
        }

        private IDictionary<string, BitmapImage> GetProfessionImages()
        {
            Console.WriteLine("Downloading profession images");
            return Professions.DownloadImages();
        }

        private IDictionary<string, BitmapImage> GetSkillImages()
        {
            Console.WriteLine("Downloading skill images");
            return Skills.DownloadImages();
        }

        private Campaign[] GetCampaigns()
        {
            Console.WriteLine("Retrieving campaign data");
            return Service.GetCampaigns();
        }

        private SkillType[] GetSkillTypes()
        {
            Console.WriteLine("Retrieving skill type data");
            return Service.GetSkillTypes();
        }

        private Profession[] GetProfessions()
        {
            Console.WriteLine("Retrieving profession data");
            return Service.GetProfessions();
        }

        private Skill[] GetSkills()
        {
            Console.WriteLine("Retrieving skill data");
            return Service.GetSkills();
        }

        private Attribute[] GetAttributes()
        {
            Console.WriteLine("Retrieving attribute data");
            return Service.GetAttributes();
        }
    }
}