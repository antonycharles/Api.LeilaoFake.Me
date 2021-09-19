﻿using LeilaoFake.Me.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoFake.Me.Infra.Data.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IList<Usuario>> GetAllAsync(UsuarioPaginacao data);
        Task<Usuario> GetByIdAsync(string usuarioId);
        Task<Usuario> GetByEmailAsync(string email);
        Task<Usuario> InsertAsync(Usuario usuario);
        Task DeleteAsync(string usuarioId);
        Task UpdateAsync(Usuario usuario);
    }
}
