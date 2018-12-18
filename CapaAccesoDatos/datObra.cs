﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace CapaAccesoDatos
{
    public class datObra
    {
        #region singleton
        private static readonly datObra _instancia = new datObra();
        public static datObra Instancia
        {
            get { return datObra._instancia; }
        }
        #endregion

        #region Metodos
        public List<entObra> ListarObras()
        {
            SqlCommand cmd = null;
            List<entObra> lista = new List<entObra>();
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("SP_LISTAROBRAS", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entObra obra = new entObra();
                    obra.ObraID = Convert.ToInt16(dr["ObraID"]);
                    obra.NombreObra = dr["Nombreobra"].ToString();
                    obra.ResponsableObra = dr["Responsableobra"].ToString();
                    obra.TipoObra = dr["Tipoobra"].ToString();
                    obra.UbicacionObra = dr["Ubicacionobra"].ToString();
                    lista.Add(obra);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return lista;
        }

        public Boolean InsertarObra(entObra obra)
        {
            SqlCommand cmd = null;
            Boolean Inserta = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("SP_INSERTAROBRA", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmstrNombreObra", obra.NombreObra);
                cmd.Parameters.AddWithValue("@prmstrResponsableObra", obra.ResponsableObra);
                cmd.Parameters.AddWithValue("@prmstrTipoObra", obra.TipoObra);
                cmd.Parameters.AddWithValue("@prmstrUbicacionObra", obra.UbicacionObra);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    Inserta = true;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return Inserta;
        }

        public Boolean EditarObra(entObra obra)
        {
            SqlCommand cmd = null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("SP_EDITAROBRA", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmintObraID", obra.ObraID);
                cmd.Parameters.AddWithValue("@prmstrNombreObra", obra.NombreObra);
                cmd.Parameters.AddWithValue("@prmstrResponsableObra", obra.ResponsableObra);
                cmd.Parameters.AddWithValue("@prmstrTipoObra", obra.TipoObra);
                cmd.Parameters.AddWithValue("@prmstrUbicacionObra", obra.UbicacionObra);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i >= 0)
                {
                    edita = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return edita;
        }

        public Boolean EliminarObra(int ObraID)
        {
            SqlCommand cmd = null;
            Boolean elimina = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("SP_ELIMINAROBRA", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmintObraID", ObraID);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i >= 0)
                {
                    elimina = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return elimina;
        }

        public entObra ObtenerObra(int ObraID)
        {
            SqlCommand cmd = null;
            entObra obra = new entObra();
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("SP_OBTENEROBRA", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmintObraID", ObraID);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    obra.ObraID = Convert.ToInt16(dr["ObraID"]);
                    obra.NombreObra = dr["Nombreobra"].ToString();
                    obra.ResponsableObra = dr["Responsableobra"].ToString();
                    obra.TipoObra = dr["Tipoobra"].ToString();
                    obra.UbicacionObra = dr["Ubicacionobra"].ToString();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return obra;
        }

        #endregion
    }
}
