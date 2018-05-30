using DeltaMovies.DeltaAPI.DbEntities;
using DeltaMovies.DeltaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaMovies.DeltaAPI.Services.Repository
{
    public interface IProducerRepository
    {
        List<ProducerInfo> ProducerBy(int producerId);
        ProducerInfo SaveProducerInfo(ProducerInfoRequestDTO request);
    }
}
