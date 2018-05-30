using DeltaMovies.DeltaAPI.DbEntities;
using DeltaMovies.DeltaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaMovies.DeltaAPI.Services.Repository
{
    public class ProducerRepository : IProducerRepository
    {
        private DeltaMoviesEntities dbContext;
        public ProducerRepository()
        {
            dbContext = new DeltaMoviesEntities();
        }

        public List<ProducerInfo> ProducerBy(int producerId)
        {
            return dbContext.ProducerInfoes.Where(m => m.ProducerId == (producerId == 0 ? m.ProducerId : producerId)).ToList();
        }


        public ProducerInfo SaveProducerInfo(ProducerInfoRequestDTO request)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    DateTime dateOfBirth = new DateTime(request.DOB.Year, request.DOB.Month, request.DOB.Day, 0, 0, 0);
                    if (dbContext.ProducerInfoes.Where(a => (a.Name == request.ProducerName && a.DOB == dateOfBirth && a.Sex == request.Sex)).FirstOrDefault() != null)
                    {
                        if (!request.NameConfirm)
                        {
                            return null;
                        }
                    }

                    ProducerInfo producerInfo = new ProducerInfo()
                    {
                        Bio = request.Bio,
                        CreatedDate = DateTime.Now,
                        DOB = request.DOB,
                        Name = request.ProducerName,
                        Sex = request.Sex,
                        Status = true
                    };

                    dbContext.ProducerInfoes.Add(producerInfo);
                    dbContext.SaveChanges();
                    transaction.Commit();
                    return producerInfo;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}