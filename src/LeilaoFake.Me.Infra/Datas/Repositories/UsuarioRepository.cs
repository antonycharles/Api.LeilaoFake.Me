﻿using Dapper;
using LeilaoFake.Me.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoFake.Me.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _dbConnection;
        private string tableUsuarios = "\"Usuarios\"";

        public UsuarioRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task DeleteAsync(string usuarioId)
        {
            string sql = $"DELETE FROM {tableUsuarios} WHERE Id = @UsuarioId";

            var resultado = await _dbConnection.ExecuteAsync(sql, new{ UsuarioId = usuarioId });

            if (resultado == 0)
                throw new ArgumentException("Usuário não foi deletado");

        }

        public async Task<IList<Usuario>> GetAllAsync(UsuarioPaginacao data)
        {
            string sql = $"SELECT * FROM {tableUsuarios} LIMIT @ItensPorPagina OFFSET(@Pagina - 1) * @ItensPorPagina";

            var resultado = await _dbConnection.QueryAsync<Usuario>(sql, new { ItensPorPagina = data.PorPagina, Pagina = data.Pagina });

            return resultado.ToList();
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            string sql = $"SELECT * FROM {tableUsuarios} WHERE Email = @Email";

            var resultado = await _dbConnection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Email = email });

            return resultado;
        }

        public async Task<Usuario> GetByIdAsync(string usuarioId)
        {
            string sql = $"SELECT * FROM {tableUsuarios} WHERE Id = @Id";

            var resultado = await _dbConnection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = usuarioId });

            return resultado;
        }

        public async Task<Usuario> InsertAsync(Usuario usuario)
        {
            string sql = $"INSERT INTO {tableUsuarios} (Id, Nome ) VALUES (@Id, @Nome)";

            var resultado = await _dbConnection.ExecuteAsync(sql, usuario);

            if (resultado == 0)
                throw new ArgumentException("Usuário não foi criado");

            return usuario;
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            string sql = $"UPDATE {tableUsuarios} SET Nome = @Nome WHERE Id = @Id";

            var resultado = await _dbConnection.ExecuteAsync(sql, usuario);

            if (resultado == 0)
                throw new ArgumentException("Usuário não foi alterado");
        }
    }
}
