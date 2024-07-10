using Dapper;
using System.Data.SqlClient;
using WsApiexamen.Models.BdiExamenModels;
using WsApiexamen.Models.Config;

namespace WsApiexamen.Services
{
    public class BdiExamenServices
    {
        private IConnection _connection;

        public BdiExamenServices(IConnection connection)
        {
            _connection = connection;   
        }



        public IEnumerable<tblExamen> GetAll()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(_connection.cnSQL))
                {
                    
                    cn.Open();
                    var sql = "EXECUTE spLista";
                    var result = cn.Query<tblExamen>(sql);
                    cn.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }




        public  tblExamen ConsultarExamen(int idExamen)
        {
            string sql = "select * from tblExamen where idExamen=@idExamen";
            var param = new { idExamen = idExamen };

            try
            { 

               using (SqlConnection cn = new SqlConnection(_connection.cnSQL))
               {
                 cn.Open();
                 var result = cn.QuerySingle<tblExamen>(sql,param);
                 return result;
                 
               }

            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }



        public bool AgregarExamen(tblExamen otblExamen)
        {
            bool boolresult = false;    

            try
            {
                using (SqlConnection cn = new SqlConnection(_connection.cnSQL))
                {
                    cn.Open();
                    var sql = "insert into tblExamen(nombre, descripcion)values(@nombre, @descripcion)";
                    var param = new { nombre = otblExamen.nombre, descripcion = otblExamen.descripcion};
                    var result = cn.Execute(sql, param);
                    boolresult = (result > 0);
                    cn.Close();
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return boolresult;
        }


        public bool ActualizarExamen(tblExamen otblExamen)
        {
            bool boolres = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(_connection.cnSQL))
                {
                    cn.Open();
                    var sql = "update tblExamen set nombre=@nombre, descripcion=@descripcion where idExamen=@idExamen";
                    var param = new { 
                        idExamen = otblExamen.idExamen,
                        nombre = otblExamen.nombre, 
                        descripcion = otblExamen.descripcion 
                    };
                    var result = cn.Execute(sql, param);
                    boolres = (result > 0);
                    cn.Close();
                }
            }
            catch(Exception ex)
            {
                throw new Exception (ex.Message);
            }

            return boolres; 
        }


        public bool EliminarExamen(int idExamen)
        {
            bool boolres = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(_connection.cnSQL))
                {
                    cn.Open();
                    var slq = "delete from tblExamen where idExamen=@idExamen";
                    var param = new { idExamen = idExamen};
                    var result = cn.Execute(slq, param);    
                    boolres = (result > 0);
                    cn.Close();
                }
            }
            catch (Exception ex) { 
            
               throw new Exception (ex.Message);
            }

            return boolres; 
        }


    }
}
