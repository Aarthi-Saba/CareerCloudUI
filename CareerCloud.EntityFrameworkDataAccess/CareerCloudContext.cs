using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        public CareerCloudContext(DbContextOptions<CareerCloudContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionStr = SqlConnection();
            optionsBuilder.UseSqlServer(connectionStr);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>(
                entity => {
                    entity.HasOne(primeentity => primeentity.ApplicantProfile)
                        .WithMany(foreignentity => foreignentity.ApplicantEducation)
                        .HasForeignKey(primeentity => primeentity.Applicant);
                });
            modelBuilder.Entity<ApplicantJobApplicationPoco>(
                entity => {
                    entity.HasOne(primeentity => primeentity.ApplicantProfile)
                    .WithMany(foreignentity => foreignentity.ApplicantJob)
                    .HasForeignKey(primeentity => primeentity.Applicant);
               
                    entity.HasOne(primeentity => primeentity.CompanyJob)
                    .WithMany(foreignentity => foreignentity.ApplicantJob)
                    .HasForeignKey(primeentity => primeentity.Job);
                });
            modelBuilder.Entity<ApplicantProfilePoco>(
                entity => {
                    entity.HasOne(primeentity => primeentity.SecurityLogin)
                    .WithOne(foreignentity => foreignentity.ApplicantProfile)
                    .HasForeignKey<ApplicantProfilePoco>(primeentity => primeentity.Login);

                    entity.HasOne(primeentity => primeentity.SystemCountry)
                    .WithMany(foreignentity => foreignentity.ApplicantProfile)
                    .HasForeignKey(primeentity => primeentity.Country);
                });
            modelBuilder.Entity<ApplicantResumePoco>(
                entity => {
                    entity.HasOne(primeentity => primeentity.ApplicantProfile)
                    .WithMany(foriegnentity => foriegnentity.ApplicantResume)
                    .HasForeignKey(primeentity => primeentity.Applicant);
                });
            modelBuilder.Entity<ApplicantSkillPoco>(
                entity =>
                {
                    entity.HasOne(primeentity => primeentity.ApplicantProfile)
                    .WithMany(foreignentity => foreignentity.ApplicantSkill)
                    .HasForeignKey(primeentity => primeentity.Applicant);
                });
            modelBuilder.Entity<ApplicantWorkHistoryPoco>(
                entity =>
                {
                    entity.HasOne(primeentity => primeentity.ApplicantProfile)
                    .WithMany(foreignentity => foreignentity.ApplicantWork)
                    .HasForeignKey(primeentity => primeentity.Applicant);

                    entity.HasOne(primeentity => primeentity.SystemCountryCode)
                    .WithMany(foreignentity => foreignentity.ApplicantWork)
                    .HasForeignKey(primeentity => primeentity.CountryCode);
                });
            modelBuilder.Entity<CompanyDescriptionPoco>(
                entity =>
                {
                    entity.HasOne(primeentity => primeentity.CompanyProfile)
                    .WithMany(foreignentity => foreignentity.CompanyDescription)
                    .HasForeignKey(primeentity => primeentity.Company);

                    entity.HasOne(primeentity => primeentity.SystemLanguageCode)
                    .WithMany(foreignentity => foreignentity.CompanyDescription)
                    .HasForeignKey(primeentity => primeentity.LanguageId);
                });
            modelBuilder.Entity<CompanyJobEducationPoco>(
                entity =>
                {
                    entity.HasOne(primeentity => primeentity.CompanyJob)
                    .WithOne(foreignentity => foreignentity.CompanyJobEducation)
                    .HasForeignKey<CompanyJobEducationPoco>(primeentity => primeentity.Job);
                    //entity.HasKey(k => new { k.Id, k.Job });
                });
            modelBuilder.Entity<CompanyJobSkillPoco>(
                entity =>
                {
                    entity.HasOne(primeentity => primeentity.CompanyJob)
                    .WithMany(foreignentity => foreignentity.CompanyJobSkill)
                    .HasForeignKey(primeentity => primeentity.Job);
                });
            modelBuilder.Entity<CompanyJobPoco>(
                entity => {
                    entity.HasOne(primeentity => primeentity.CompanyProfile)
                    .WithMany(foreignentity => foreignentity.CompanyJob)
                    .HasForeignKey(primeentity => primeentity.Company);
                });
            modelBuilder.Entity<CompanyJobDescriptionPoco>(
                entity =>
                {
                    entity.HasOne(primeentity => primeentity.CompanyJob)
                    .WithOne(foreignentity => foreignentity.CompanyJobDescription)
                    .HasForeignKey<CompanyJobDescriptionPoco>(primeentity => primeentity.Job);
                });
            modelBuilder.Entity<CompanyLocationPoco>(
                entity =>
                {
                    entity.HasOne(primeentity => primeentity.CompanyProfile)
                    .WithMany(foreignentity => foreignentity.CompanyLocation)
                    .HasForeignKey(primeentity => primeentity.Company);
                });
            modelBuilder.Entity<CompanyProfilePoco>(
                entity =>
                {
                    entity.HasMany(primeentity => primeentity.CompanyJob)
                    .WithOne(foreignentity => foreignentity.CompanyProfile);
                });
            //modelBuilder.Entity<SecurityLoginPoco>(
            //    entity =>
            //    {
            //        entity.HasMany(primeentity => primeentity.ApplicantProfile)
            //        .WithOne(foreignentity => foreignentity.SecurityLogin)
            //        .HasForeignKey(foreignentity => foreignentity.Login);
            //    });
            modelBuilder.Entity<SecurityLoginsLogPoco>(
                entity =>
                {
                    entity.HasOne(primeentity => primeentity.SecurityLogin)
                    .WithMany(foreignentity => foreignentity.SecurityLoginsLog)
                    .HasForeignKey(primeentity => primeentity.Login);
                });
            modelBuilder.Entity<SecurityLoginsRolePoco>(
                entity =>
                {
                    entity.HasOne(primeentity => primeentity.SecurityLogin)
                    .WithOne(foreignentity => foreignentity.SecurityLoginsRole)
                    .HasForeignKey<SecurityLoginsRolePoco>(primeentity => primeentity.Login);

                    entity.HasOne(primeentity => primeentity.SecurityRole)
                    .WithMany(foreignentity => foreignentity.SecurityLoginsRole)
                    .HasForeignKey(primeentity => primeentity.Role);
                });
            base.OnModelCreating(modelBuilder);
        }
        private string SqlConnection()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            return root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
    }
}
