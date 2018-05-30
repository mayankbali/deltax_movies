using DeltaMovies.DeltaAPI.DbEntities;
using DeltaMovies.DeltaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaMovies.DeltaAPI.Services.Repository
{
    public class ActorRepository : IActorRepository
    {
        private DeltaMoviesEntities dbContext;
        public ActorRepository()
        {
            dbContext = new DeltaMoviesEntities();
        }

        public List<ActorInfo> ActorsBy(int actorId)
        {
            return dbContext.ActorInfoes.Where(m => m.ActorId == (actorId == 0 ? m.ActorId : actorId)).ToList();
        }

        public ActorInfo SaveActorInfo(ActorInfoRequestDTO request)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    DateTime dateOfBirth = new DateTime(request.DOB.Year, request.DOB.Month, request.DOB.Day, 0, 0, 0);
                    if (dbContext.ActorInfoes.Where(a => (a.Name == request.ActorName && a.DOB == dateOfBirth && a.Sex == request.Sex)).FirstOrDefault() != null)
                    {
                        if (!request.NameConfirm)
                        {
                            return null;
                        }
                    }

                    ActorInfo actorInfo = new ActorInfo()
                    {
                        Bio = request.Bio,
                        CreatedDate = DateTime.Now,
                        DOB = request.DOB,
                        Name = request.ActorName,
                        Sex = request.Sex,
                        Status = true
                    };

                    dbContext.ActorInfoes.Add(actorInfo);
                    dbContext.SaveChanges();
                    transaction.Commit();
                    return actorInfo;

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