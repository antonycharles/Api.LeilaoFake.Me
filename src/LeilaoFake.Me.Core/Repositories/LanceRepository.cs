﻿using Dapper;
using LeilaoFake.Me.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoFake.Me.Core.Repositories
{
    public class LanceRepository : ILanceRepository
    {
        private readonly IDbConnection _dbConnection;

        public LanceRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Lance> GetLanceById(string lanceId)
        {
            string sql = "SELECT * FROM lances WHERE Id = @Id";

            var resultado = await _dbConnection.QueryFirstOrDefaultAsync<Lance>(sql, new { Id = lanceId });

            return resultado;
        }

        public async Task<IList<Lance>> GetLancesByLeilaoId(string leilaoId)
        {
            string sql = "SELECT * FROM lances WHERE LeilaoId = @LeilaoId";

            var resultado = await _dbConnection.QueryAsync<Lance>(sql, new { LeilaoId = leilaoId });

            return resultado.ToList();
        }

        public async Task<Lance> InsertLanceAsync(Lance lance)
        {
            string sql = "INSERT INTO lances (Id, LeilaoId, InteressadoId, Data, Valor ) " +
                            "VALUES (@Id, @LeilaoId, @InteressadoId, @Data, @Valor)";

            var resultado = await _dbConnection.ExecuteAsync(sql, lance);

            if (resultado == 0)
                throw new ArgumentException("Lance não foi cadastrado!");

            return lance;
        }
    }
}
