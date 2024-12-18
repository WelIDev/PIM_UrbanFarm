﻿using Dominio.Enums;

namespace Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Funcao Funcao { get; set; }
        public DateTime DataCriacao { get; set; }

        public IList<AbastecimentoEstoque> AbastecimentosEstoque { get; set; } =
            new List<AbastecimentoEstoque>();
    }
}