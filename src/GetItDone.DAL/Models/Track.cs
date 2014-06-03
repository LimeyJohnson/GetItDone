using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace GetItDone.DAL.Models
{
    public class Track
    {
        public int TrackId
        {
            get;
            set;
        }
        public List<PlayListTracks> PlaylistTracks
        {
            get;
            set;
        }
        public string SongName
        {
            get;
            set;
        }
        public string Artist
        {
            get;
            set;
        }
        public string Album
        {
            get;
            set;
        }

        public int UserID
        {
            get;
            set;
        }

        [ForeignKey("UserID")]
        public User User
        {
            get;
            set;
        }

        public string BlobURL
        {
            get;
            set;
        }
    }
}