﻿namespace WebApp.Models;

public class UsuarioModel
{
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Funcao { get; set; }
        public DateTime DataCriacao { get; set; }
}