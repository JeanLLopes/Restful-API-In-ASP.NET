using BestCricketers.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace BestCricketers.Core.DAL
{
    public class CricketerDAL
    {

        #region Variable  
        ///<summary>  
        /// Specify the Database variable    
        ///</summary>  
        Database objDB;
        ///<summary>  
        /// Specify the static variable    
        ///</summary>  
        static string ConnectionString;
        #endregion
        #region Constructor  
        ///<summary>  
        /// This constructor is used to get the connectionstring from the config file      
        ///</summary>  
        public CricketerDAL()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["CricketerConnectionString"].ToString();
        }
        #endregion
        #region Database Method  
        public List<T> ConvertTo<T>(DataTable datatable) where new()
        {
            List<T> Temp = new List<T>();
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn DataColumn in datatable.Columns)
                    columnsNames.Add(DataColumn.ColumnName);
                Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));
                return Temp;
            }
            catch
            {
                return Temp;
            }
        }

        public T getObject<T>(DataRow row, List<string> columnsName) where new()
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString();
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }


        #endregion  
        #region CricketerProfile  
        ///<summary>  
        /// This method is used to get the cricketer data      
        ///</summary>  
        ///<returns></returns>  
        public List<CricketerProfile> GetCricketerList()
        {
            List<CricketerProfile> objGetCricketers = null;
            objDB = new SqlDatabase(ConnectionString);
            using (DbCommand objcmd = objDB.GetStoredProcCommand("CC_GetCricketerList"))
            {
                try
                {
                    using (DataTable dataTable = objDB.ExecuteDataSet(objcmd).Tables[0])
                    {
                        objGetCricketers = ConvertTo<CricketerProfile>(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    return null;
                }
            }
            return objGetCricketers;
        }

        ///<summary>  
        /// This method is used to get cricketers details by cricketer id    
        ///</summary>  
        ///<returns></returns>  
        public List<CricketerProfile> GetCricketerDetailsById(int Id)
        {
            List<CricketerProfile> objCricketerDetails = null;
            objDB = new SqlDatabase(ConnectionString);
            using (DbCommand objcmd = objDB.GetStoredProcCommand("CC_GetCricketerDetailsById"))
            {
                try
                {
                    objDB.AddInParameter(objcmd, "@ID", DbType.Int32, Id);
                    using (DataTable dataTable = objDB.ExecuteDataSet(objcmd).Tables[0])
                    {
                        objCricketerDetails = ConvertTo<CricketerProfile>(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    return null;
                }
            }
            return objCricketerDetails;
        }



        ///<summary>  
        /// This method is used to add update cricketer info  
        ///</summary>  
        ///<param name="cricketer"></param>  
        ///<returns></returns>  
        public int AddUpdateCricketerInfo(CricketerProfile cricketer)
        {
            int result = 0;
            objDB = new SqlDatabase(ConnectionString);
            using (DbCommand objCMD = objDB.GetStoredProcCommand("CC_AddUpdateCricketerDetails"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, cricketer.Id);
                if (string.IsNullOrEmpty(cricketer.Name)) objDB.AddInParameter(objCMD, "@Name", DbType.String, DBNull.Value);
                else objDB.AddInParameter(objCMD, "@Name", DbType.String, cricketer.Name);
                if (cricketer.ODI == 0) objDB.AddInParameter(objCMD, "@ODI", DbType.Int32, DBNull.Value);
                else objDB.AddInParameter(objCMD, "@ODI", DbType.Int32, cricketer.ODI);
                if (cricketer.Tests == 0) objDB.AddInParameter(objCMD, "@Tests", DbType.Int32, DBNull.Value);
                else objDB.AddInParameter(objCMD, "@Tests", DbType.Int32, cricketer.Tests);
                if (cricketer.OdiRuns == 0) objDB.AddInParameter(objCMD, "@OdiRuns", DbType.Int32, DBNull.Value);
                else objDB.AddInParameter(objCMD, "@OdiRuns", DbType.Int32, cricketer.OdiRuns);
                if (cricketer.TestRuns == 0) objDB.AddInParameter(objCMD, "@TestRuns", DbType.Int32, DBNull.Value);
                else objDB.AddInParameter(objCMD, "@TestRuns", DbType.Int32, cricketer.TestRuns);
                objDB.AddInParameter(objCMD, "@Type", DbType.Int32, cricketer.Type);
                objDB.AddOutParameter(objCMD, "@Status", DbType.Int16, 0);
                try
                {
                    objDB.ExecuteNonQuery(objCMD);
                    result = Convert.ToInt32(objDB.GetParameterValue(objCMD, "@Status"));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }


        ///<summary>  
        /// This method is used to delete cricketer info  
        ///</summary>  
        ///<param name="cricketer"></param>  
        ///<returns></returns>  
        public int DeleteCricketerInfo(CricketerProfile cricketer)
        {
            int result = 0;
            objDB = new SqlDatabase(ConnectionString);
            using (DbCommand objCMD = objDB.GetStoredProcCommand("CC_DeleteCricketerProfile"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, cricketer.Id);
                objDB.AddOutParameter(objCMD, "@Status", DbType.Int16, 0);
                try
                {
                    objDB.ExecuteNonQuery(objCMD);
                    result = Convert.ToInt32(objDB.GetParameterValue(objCMD, "@Status"));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }
        #endregion

    }

}