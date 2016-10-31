using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespaces
using System.ComponentModel; //ODS
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Track> ListTracks()
        {
            using (var context = new ChinookContext())
            {
                //return all records all attributes

                return context.Tracks.ToList();
            }

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track GetTrack(int trackid)
        {
            using (var context = new ChinookContext())
            {
                //return a records all attributes

                return context.Tracks.Find(trackid);
            }

        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddTrack(Track trackinfo)
        {
            using (var context = new ChinookContext())
            {
                //any business rules

                //any data refinements

                //review of using iif
                //composer can be a null string
                //we do not wish to store an empty string

                trackinfo.Composer = string.IsNullOrEmpty(trackinfo.Composer) ? null : trackinfo.Composer;

                //add the instance of trackinfo to the database

                context.Tracks.Add(trackinfo);

                //comit of the add

                context.SaveChanges();
                
            }

        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void UpdateTrack(Track trackinfo)
        {
            using (var context = new ChinookContext())
            {
                //any business rules

                //any data refinements

                //review of using iif
                //composer can be a null string
                //we do not wish to store an empty string

                trackinfo.Composer = string.IsNullOrEmpty(trackinfo.Composer) ? null : trackinfo.Composer;

                //update the existing instance of trackinfo on the database

                context.Entry(trackinfo).State = System.Data.Entity.EntityState.Modified;

                //comit of the update

                context.SaveChanges();

            }

        }

        //the delete is an overload method technique
        [DataObjectMethod(DataObjectMethodType.Delete,true)]
        public void DeleteTrack(Track trackinfo)
        {
            DeleteTrack(trackinfo.TrackId);
        }

        public void DeleteTrack(int trackid)
        {
            using (var context = new ChinookContext())
            {
                //any business rules

                //do the delete
                //find the existing record on the database

                var existing = context.Tracks.Find(trackid);
                //var existing = GetTrack(trackid);
                //delete the record from the database
                context.Tracks.Remove(existing);
                //commit of the transaction
                context.SaveChanges();
            }
        }
    }
}
