using BestCricketers.Core.DAL;
using BestCricketers.Models;
using System;
using System.Collections.Generic;

namespace BestCricketers.Core.BL
{
    public class CricketerBL
    {
        ///<summary>  
        /// This method is used to get the cricketer list  
        ///</summary>  
        ///<returns></returns>  
        public List<CricketerProfile> GetCricketerList()
        {
            List<CricketerProfile> ObjCricketers = null;
            try
            {
                ObjCricketers = new CricketerDAL().GetCricketerList();
            }
            catch (Exception)
            {
                throw;
            }
            return ObjCricketers;
        }

        ///<summary>  
        /// This method is used to get cricketers details by cricketer id    
        ///</summary>  
        ///<returns></returns>  
        public List<CricketerProfile> GetCricketerDetailsById(int Id)
        {
            List<CricketerProfile> ObjCricketerDetails = null;
            try
            {
                ObjCricketerDetails = new CricketerDAL().GetCricketerDetailsById(Id);
            }
            catch (Exception)
            {
                throw;
            }
            return ObjCricketerDetails;
        }
        ///<summary>  
        /// This method is used to add update cricketer info  
        ///</summary>  
        ///<param name="cricketer"></param>  
        ///<returns></returns>  
        public int AddUpdateCricketerInfo(CricketerProfile cricketer)
        {
            int result = 0;
            try
            {
                result = new CricketerDAL().AddUpdateCricketerInfo(cricketer);
            }
            catch (Exception)
            {
                return 0;
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
            try
            {
                result = new CricketerDAL().DeleteCricketerInfo(cricketer);
            }
            catch (Exception)
            {
                return 0;
            }
            return result;
        }
    }
}
