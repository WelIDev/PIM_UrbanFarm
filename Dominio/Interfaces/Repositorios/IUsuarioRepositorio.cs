﻿using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorios;

public interface IUsuarioRepositorio
{
    bool InserirUsuario(Usuario usuario);
    Usuario? ObterPorEmail(string email);
    Usuario? ObterPorId(int id);
    List<Usuario> ObterUsuarios();
    bool ExcluirUsuario(int id);
}