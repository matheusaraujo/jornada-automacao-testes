using GestaoContratos.Util;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GestaoContratos.Repositorio.Base
{
    public abstract class RepositorioBase
    {
        private SQLiteCommand CriarComando(string sql, SQLiteConnection conexao, Tuple<string, object>[] parametros)
        {
            var comando = new SQLiteCommand(sql, conexao);
            if (parametros != null && parametros.Length > 0)
            {
                foreach (var parametro in parametros)
                    comando.Parameters.AddWithValue(parametro.Item1, parametro.Item2);
            }
            return comando;
        }

        protected Tuple<string, object> Parametro(string s, object o)
        {
            return new Tuple<string, object>(s, o);
        }

        protected SQLiteConnection CriarConexao()
        {
            return new SQLiteConnection(string.Format("Data Source={0}{1};Version=3;", Configuracao.Diretorio, Configuracao.CaminhoBancoDados));
        }

        protected int ExecutarRetornoInteiro(string sql, params Tuple<string, object>[] parametros)
        {
            using (var conexao = CriarConexao())
            {
                using (var comando = CriarComando(sql, conexao, parametros))
                {   
                    comando.Connection.Open();
                    return int.Parse(comando.ExecuteScalar().ToString());
                }
            }
        }

        protected IList<T> ExecutarConsulta<T>(string sql, Func<SQLiteDataReader, T> conversor, params Tuple<string, object>[] parametros)
        {
            var retorno = new List<T>();
            using (var conexao = CriarConexao())
            {
                using (var comando = CriarComando(sql, conexao, parametros))
                {
                    comando.Connection.Open();
                    using (var consulta = comando.ExecuteReader())
                    {
                        while (consulta.Read())
                            retorno.Add(conversor.Invoke(consulta));
                        return retorno;
                    }
                }
            }
        }

        protected T ExecutarConsultaUnica<T>(string sql, Func<SQLiteDataReader, T> conversor, params Tuple<string, object>[] parametros)
        {
            using (var conexao = CriarConexao())
            {
                using (var comando = CriarComando(sql, conexao, parametros))
                {   
                    comando.Connection.Open();
                    using (var consulta = comando.ExecuteReader())
                    {
                        if (!consulta.Read())
                            return default(T);

                        return conversor.Invoke(consulta);
                    }
                }
            }
        }

        protected void Executar(string sql)
        {
            Executar(sql, null);
        }

        protected void Executar(string sql, params Tuple<string, object>[] parametros)
        {
            using (var conexao = CriarConexao())
            {
                using (var comando = CriarComando(sql, conexao, parametros))
                {   
                    comando.Connection.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
